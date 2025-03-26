using Parstech.Shop.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.AddServiceDefaults();
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
app.MapDefaultEndpoints();
app.MapReverseProxy();
app.Run();