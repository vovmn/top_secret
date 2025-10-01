using IAM.Domain.Entities;
using IAM.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace IAM.Infrastructure.Data
{
    /// <summary>
    /// Контекст EF Core
    /// </summary>
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
