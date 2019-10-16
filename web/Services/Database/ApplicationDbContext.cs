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

            modelBuilder.Entity<Treasure>()
                .Property(x => x.Price).HasColumnType("decimal(19,4)");

            modelBuilder.Entity<TreasurePicture>()
                .Property(x => x.Id)
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
                string tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName[6..^1]);
                }
                else
                {
                    // Delete suffix s in ALL tables
                    entityType.SetTableName(tableName[..^1]);
                }
            }
        }

        public DbSet<OAUser> OAUsers { get; set; }

        public DbSet<Treasure> Treasures { get; set; }

        public DbSet<TreasurePicture> TreasurePictures { get; set; }
    }
}
