using Heavy.Web.Models;
using Microsoft.EntityFrameworkCore;
using Heavy.Web.ViewModels;

namespace Heavy.Web.Data
{
    public class HeavyContext : DbContext
    {
        public HeavyContext(DbContextOptions<HeavyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
        }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Heavy.Web.ViewModels.UserCreateViewModel> UserCreateViewModel { get; set; }
    }
}
