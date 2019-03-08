using System;
using System.Data;
using System.Data.Common;
using BetterOwner.Services.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.IO;
using System.Data.SqlTypes;

namespace BetterOwner.Services.FileStore
{
    public class NativeFileStore : EFFileStore
    {
        const string TableName = nameof(TreasurePicture);

        public NativeFileStore(ApplicationDbContext db) : base(db)
        {
        }

        public override FileDownloadItem Download(Guid id)
        {
            DbConnection connection = GetConnection();
            DbTransaction transaction = connection.BeginTransaction();
            string sql = $@"
                    SELECT 
                        FileName                             AS FileName, 
                        ContentType                          AS ContentType, 
                        FileStream.PathName()                AS PathName, 
                        GET_FILESTREAM_TRANSACTION_CONTEXT() AS Context 
                    FROM [{TableName}] WHERE Id = @Id";
            SqlFileContext ctx = connection.QueryFirst<SqlFileContext>(sql, new { Id = id }, transaction);

            var stream = new SqlFileStream(ctx.PathName, ctx.Context, FileAccess.Read);
            return new FileDownloadItem
            {
                FileName = ctx.FileName,
                ContentType = ctx.ContentType,
                FileStream = new AspNetSqlFileStream(stream, connection, transaction),
            };
        }

        public override int Upload(int treasureId, IFormFileCollection files)
        {
            using (DbConnection connection = GetConnection())
            using (DbTransaction transaction = connection.BeginTransaction())
            {
                byte[] transactionContext = GetFilestreamTransactionContext(connection, transaction);
                foreach (IFormFile file in files)
                {
                    string sql = $@"
                        INSERT INTO [{TableName}](TreasureId, FileName, ContentType, FileSize, FileStream)
                        OUTPUT inserted.FileStream.PathName() AS PathName
                        SELECT @TreasureId, @FileName, @ContentType, @FileSize, 0x0";

                    string pathName = connection.QueryFirst<string>(sql, new
                    {
                        TreasureId = treasureId,
                        FileName = file.FileName,
                        ContentType = file.ContentType,
                        FileSize = (int)file.Length
                    }, transaction);

                    using (var stream = new SqlFileStream(pathName, transactionContext, FileAccess.Write))
                    {
                        file.CopyTo(stream);
                    }
                }

                transaction.Commit();
                return files.Count;
            }
        }

        private DbConnection GetConnection()
        {
            DbConnection connection = _db.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open) connection.Open();
            return connection;
        }

        private byte[] GetFilestreamTransactionContext(IDbConnection connection, IDbTransaction transaction)
        {
            return connection.QueryFirst<byte[]>("SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()", null, transaction);
        }
    }
}
