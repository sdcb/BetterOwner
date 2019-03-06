using System;
using System.Data;
using System.Data.Common;
using System.Transactions;
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
        public NativeFileStore(ApplicationDbContext db) : base(db)
        {
        }

        public override FileDownloadItem Download(Guid id)
        {
            DbConnection connection = _db.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open) connection.Open();

            DbTransaction transaction = connection.BeginTransaction();
            string sql = @"
                    SELECT 
                        FileName                             AS FileName, 
                        ContentType                          AS ContentType, 
                        FileStream.PathName()                AS PathName, 
                        GET_FILESTREAM_TRANSACTION_CONTEXT() AS Context 
                    FROM Attachment WHERE Id = @Id";
            SqlFileContext ctx = connection.QueryFirst<SqlFileContext>(sql, new { Id = id }, transaction);

            var stream = new SqlFileStream(ctx.PathName, ctx.Context, FileAccess.Read);
            return new FileDownloadItem
            {
                FileName = ctx.FileName,
                ContentType = ctx.ContentType,
                FileStream = new AspNetSqlFileStream(stream, connection, transaction),
            };
        }

        public override int Upload(IFormFileCollection files)
        {
            return base.Upload(files);
        }
    }
}
