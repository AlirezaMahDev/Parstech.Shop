using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Persistence.Context;

namespace Shop.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceService(this IServiceCollection Services,
            IConfiguration Configuration)
        {
            Services.AddDbContext<DatabaseContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

            var connectionString = Configuration.GetConnectionString("IdentityContextConnection");
            Services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(connectionString));
            Services.AddDatabaseDeveloperPageExceptionFilter();

            Services.Configure<IdentityOptions>(options =>
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

            Services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<IdentityContext>();
            

            #region Auth

            Services.AddAuthentication()
                .AddCookie(options =>
                {
                    options.Cookie.Expiration = TimeSpan.FromMinutes(60);
                    options.SlidingExpiration = true;
                });

            Services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
            Services.ConfigureApplicationCookie(option =>
            {

                option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                option.SlidingExpiration = true;
                // cookie setting
                option.LoginPath = "/Auth/Login";
                option.AccessDeniedPath = "/Auth/AccessDenied";

            });

            Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, options =>
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

            Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            #endregion

            return Services;
        }
    }
}
