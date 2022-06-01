using JogTracker.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JogTracker.Database
{
    public class ApplicationContext : IdentityDbContext<UserEntity>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserEntity>()
                .HasOne(u => u.RefreshToken)
                .WithOne(t => t.User)
                .HasForeignKey<RefreshTokenEntity>(t => t.UserId);

            builder.Entity<JogEntity>()
                .HasOne(j => j.User)
                .WithMany(u => u.Jogs)
                .HasForeignKey(j => j.UserId);
        }

        public DbSet<UserEntity> ApplicationUsers { get; set; }
        public DbSet<JogEntity> Jogs { get; set; }
    }
}
