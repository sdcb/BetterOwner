using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BetterOwner.Services.Database
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            RemovePrefixAndSuffix(modelBuilder);

            modelBuilder.Entity<TreasurePicture>().Property(x => x.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");
            modelBuilder.Entity<OAUser>(entity =>
            {
                entity.HasOne(p => p.User)
                    .WithOne(p => p.OAUser)
                    .HasForeignKey<OAUser>(p => p.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private static void RemovePrefixAndSuffix(ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                string tableName = entityType.Relational().TableName;
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.Relational().TableName = tableName[6..^1];
                }
                else
                {
                    // Delete suffix s in ALL tables
                    entityType.Relational().TableName = tableName[..^1];
                }
            }
        }

        public DbSet<OAUser> OAUsers { get; set; }

        public DbSet<Treasure> Treasures { get; set; }

        public DbSet<TreasurePicture> TreasurePictures { get; set; }
    }
}
