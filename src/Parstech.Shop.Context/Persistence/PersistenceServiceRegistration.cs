using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence;

public static class PersistenceServiceRegistration
{
    public static void ConfigurePersistenceService(this WebApplicationBuilder builder)
    {
        builder.AddSqlServerDbContext<DatabaseContext>("database");
        builder.AddSqlServerDbContext<IdentityContext>("database");

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 0;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(43200);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        });
            
            

        //Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        //    .AddEntityFrameworkStores<IdentityContext>();

        builder.Services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<IdentityContext>();
            

        #region Auth

        builder.Services.AddAuthentication()
            .AddCookie(options =>
            {
                options.Cookie.Expiration = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
            });

        builder.Services.AddSession(options => {
            options.IdleTimeout = TimeSpan.FromMinutes(60);
        });
        builder.Services.ConfigureApplicationCookie(option =>
        {

            option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            option.SlidingExpiration = true;
            // cookie setting
            option.LoginPath = "/Auth/Login";
            option.AccessDeniedPath = "/Auth/AccessDenied";

        });

        builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, options =>
        {
            options.LoginPath = "/Auth/Login";
        });

        #endregion

        #region COOKIE
            
        //Services.AddAuthentication()
        //    .AddCookie(options =>
        //    {
        //        options.Cookie.Expiration = TimeSpan.FromMinutes(30);
        //    });

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
        });
        #endregion
    }
}