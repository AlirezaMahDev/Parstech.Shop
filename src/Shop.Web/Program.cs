using Shop.Application;
using Shop.Application.Url;
using Shop.Infrastructure;
using Shop.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddSeqEndpoint("seq");
builder.AddRedisOutputCache("cache");
builder.AddRedisDistributedCache("cache");

builder.Services.AddRouting(options =>
{
    options.LowercaseQueryStrings = true;
    options.LowercaseUrls = true;
});
// Add services to the container.
builder.ConfigurePersistenceService();
builder.Services.ConfigureInfrustructureService();
builder.Services.ConfigureApplicationService(builder.Configuration);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var url = BaseUrl.GetUrl();
builder.Services.AddCors(option =>
{
    option.AddPolicy("Me", policy =>
    {
        policy.AllowAnyMethod().AllowAnyMethod().WithOrigins(url);

    });
});
builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
//builder.Services.Configure<FormOptions>(options =>
//{
//    options.MultipartBodyLengthLimit = 52428800; // 50MB
//});
var app = builder.Build();

app.UseMigrationsEndPoint();
//if (app.Environment.IsDevelopment())
//{
//    app.UseMigrationsEndPoint();
//}
//else
//{
//    app.UseExceptionHandler("/Error");
//    app.UseHsts();
//}
//app.UseMiddleware<CountryCheckMiddleware>();
app.UseCors("Me");
app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // منطق اصلاح نام فایل (مثلاً تبدیل به حروف کوچک)
        var path = ctx.File.PhysicalPath;
        if (!File.Exists(path))
        {
            path = path.ToLower();
            if (File.Exists(path))
            {
                ctx.Context.Response.Redirect(path);
            }
        }
    }
});

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
