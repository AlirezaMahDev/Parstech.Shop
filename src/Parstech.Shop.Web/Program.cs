using Parstech.Shop.Web;
using Parstech.Shop.Web.Components;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyMethod().AllowAnyMethod().WithOrigins("https://localhost:7040");
    });
});

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();

builder.Services.AddHttpClient<WeatherApiClient>(client =>
    {
        client.BaseAddress = new("https+http://apiservice");
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.UseOutputCache();

app.UseRouting();
app.UseAuthorization();

app.MapDefaultEndpoints();
app.MapRazorPages();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
