using System.Reflection;

using SixLaborsCaptcha.Mvc.Core;

using SixLabors.ImageSharp;

namespace Parstech.Shop.ApiService.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection ConfigureApplicationService(this IServiceCollection Services,
        IConfiguration Configuration)
    {
        //Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        Services.AddAutoMapper(Assembly.GetExecutingAssembly());
        Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        Services.AddDataProtection();

        Services.AddSixLabCaptcha(x =>
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
        return Services;
    }
}