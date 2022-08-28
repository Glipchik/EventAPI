using EventAPI.Core.Entities;
using EventAPI.DAL.EntityConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EventAPI.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly IConfiguration _configuration;
        public DbSet<Event> Events { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public ApplicationDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _configuration.GetConnectionString("SqlServer");
            if (!string.IsNullOrEmpty(connectionString))
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EventConfiguration());
            builder.ApplyConfiguration(new IdentityUserConfiguration());
            builder.Entity<IdentityRole>(e => e.ToTable("Roles").HasNoKey());
            builder.Entity<IdentityUserRole<string>>(e => e.ToTable("UserRoles").HasNoKey());
            builder.Entity<IdentityUserClaim<string>>(e => e.ToTable("UserClaims").HasNoKey());
            builder.Entity<IdentityUserLogin<string>>(e => e.ToTable("UserLogins").HasNoKey());
            builder.Entity<IdentityUserToken<string>>(e => e.ToTable("UserTokens").HasNoKey());
            builder.Entity<IdentityRoleClaim<string>>(e => e.ToTable("RoleClaims").HasNoKey());
        }
    }
}
