using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BetterOwner.Services.Database
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Delete prefix AspNet in AspNet*s tables
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                string tableName = entityType.Relational().TableName;
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.Relational().TableName = tableName[6..];
                }

                // Delete suffix s in ALL tables
                entityType.Relational().TableName = tableName[..^1];
            }

            modelBuilder.Entity<TreasurePicture>().Property(x => x.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");
            modelBuilder.Entity<OAUser>(o =>
            {
                o.HasOne(p => p.User).WithOne().HasForeignKey<User>(p => p.Id);
            });
        }

        public DbSet<OAUser> OAUsers { get; set; }

        public DbSet<Treasure> Treasures { get; set; }

        public DbSet<TreasurePicture> TreasurePictures { get; set; }
    }
}
