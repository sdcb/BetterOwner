using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BetterOwner.Services.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Attachment>().Property(x => x.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");
        }

        public DbSet<Attachment> Attachment { get; set; }
    }
}
