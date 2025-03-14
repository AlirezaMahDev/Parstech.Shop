using Shop.Persistence.Context;
using Shop.Application;
using Shop.Infrastructure;
using Shop.Persistence;
using Parstech.Shop.ApiService.Services;
using Parstech.Shop.ApiService.MapperProfiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePersistenceService(builder.Configuration);
builder.Services.ConfigureInfrustructureService();
builder.Services.ConfigureApplicationService(builder.Configuration);

// Add gRPC services
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
    options.MaxReceiveMessageSize = 16 * 1024 * 1024; // 16 MB
    options.MaxSendMessageSize = 16 * 1024 * 1024; // 16 MB
});

builder.Services.AddGrpcReflection();

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();
builder.Services.AddSqlServer<DatabaseContext>("database");

// Add AutoMapper profiles
builder.Services.AddAutoMapper(config => 
{
    config.AddProfile<GrpcMapper>();
    // Apply extensions
    OrderShippingMapperExtensions.AddOrderShippingMappings(config);
});

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("https://localhost:7040")
            .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
    });
});

var app = builder.Build();

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseMigrationsEndPoint();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseRouting();
app.UseGrpcWeb(); // Enable gRPC-Web middleware
app.UseAuthorization();

// Configure gRPC service endpoints
app.MapGrpcService<ProductService>().EnableGrpcWeb();
app.MapGrpcService<OrderService>().EnableGrpcWeb();
app.MapGrpcService<UserService>().EnableGrpcWeb();
app.MapGrpcService<UserShippingService>().EnableGrpcWeb();
app.MapGrpcService<OrderShippingService>().EnableGrpcWeb();
app.MapGrpcService<SectionService>();
app.MapGrpcService<UserProductService>();

// Map gRPC services
app.MapGrpcService<CategoryGrpcService>();
app.MapGrpcService<BrandGrpcService>();
app.MapGrpcService<UserStoreGrpcService>();
app.MapGrpcReflectionService();
app.MapGrpcService<ProductDetailService>();
app.MapGrpcService<ProductGalleryService>();
app.MapGrpcService<TorobService>();

// Map new gRPC services
app.MapGrpcService<OrderCheckoutService>().EnableGrpcWeb();
app.MapGrpcService<CouponGrpcService>().EnableGrpcWeb();
app.MapGrpcService<WalletGrpcService>().EnableGrpcWeb();
app.MapGrpcService<ShippingGrpcService>().EnableGrpcWeb();
app.MapGrpcService<PaymentGrpcService>().EnableGrpcWeb();
app.MapGrpcService<UserProfileGrpcService>().EnableGrpcWeb();
app.MapGrpcService<UserPreferencesGrpcService>().EnableGrpcWeb();

app.MapDefaultEndpoints();
app.MapStaticAssets();
app.MapControllers();

app.Run();
