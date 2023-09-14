using Microsoft.EntityFrameworkCore;
using Models;

namespace NBP
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<NBPTable> Tables { get; set; }
        public DbSet<Rate> Rates { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Rate>()
                .HasOne(x => x.Table)
                .WithMany(m => m.Rates)
                .HasForeignKey(x => x.Id_Table);
        }

    }
}
