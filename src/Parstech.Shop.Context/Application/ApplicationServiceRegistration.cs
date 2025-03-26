using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

using Microsoft.AspNetCore.Builder;

using SixLaborsCaptcha.Mvc.Core;
using SixLabors.ImageSharp;
namespace Parstech.Shop.Context.Application;

public static class ApplicationServiceRegistration
{
    public static void ConfigureApplicationService(this WebApplicationBuilder builder)
    {
        //builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        builder.Services.AddDataProtection();
            
        builder.Services.AddSixLabCaptcha(x =>
        {
            x.FontFamilies = ["Arial"];
            x.DrawLines = 5;
            x.Width = 300;
            x.Height = 80;
            x.FontSize = 32;
            x.TextColor = [Color.Black];
            x.BackgroundColor = [Color.White];
            x.NoiseRateColor = [Color.Gray];
            x.NoiseRate = 100;
            x.MaxLineThickness = 1;
            x.MinLineThickness = 0.9f;
            x.MaxRotationDegrees = 2;
                
        });
    }
}