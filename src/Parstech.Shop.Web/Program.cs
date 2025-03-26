using Grpc.Net.ClientFactory;

using Parstech.Shop.Web;
using Parstech.Shop.Web.Components;
using Parstech.Shop.ServiceDefaults;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyMethod()
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .WithOrigins("https://localhost:7040");
    });
});

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();

builder.Services.AddHttpClient<WeatherApiClient>(client =>
{
    client.BaseAddress = new("https+http://apiservice");
});

static void GrpcClientOptions(IServiceProvider provider, GrpcClientFactoryOptions options)
{
    options.Address = new("https+http://apiservice");
}

// builder.Services.AddGrpcClient<UserAuthService.UserAuthServiceClient>(GrpcClientOptions);

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.UseOutputCache();

app.UseRouting();
app.UseCors();
app.UseAuthorization();

app.MapDefaultEndpoints();
app.MapRazorPages();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();