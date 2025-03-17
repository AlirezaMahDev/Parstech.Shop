using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Parstech.Shop.ApiService.Persistence.Context;

public class IdentityContext : IdentityDbContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Override default AspNet Identity table names
        modelBuilder.Entity<IdentityUser>(entity => { entity.ToTable("IUsers"); });
        modelBuilder.Entity<IdentityRole>(entity => { entity.ToTable("IRoles"); });
        modelBuilder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("IUserRoles"); });
        modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("IUserClaims"); });
        modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("IUserLogins"); });
        modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("IUserTokens"); });
        modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("IRoleClaims"); });
    }
}