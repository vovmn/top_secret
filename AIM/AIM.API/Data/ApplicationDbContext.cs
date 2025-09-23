using AIM.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIM.API.Data
{
    /// <summary>
    /// Контекст EF Core
    /// </summary>
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        
        public DbSet<LoginInfo> loginInfos { get; set; } = null!;
        public DbSet<FIO> FIOs { get; set; } = null!;
        public DbSet<Messangers> Messangers { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
