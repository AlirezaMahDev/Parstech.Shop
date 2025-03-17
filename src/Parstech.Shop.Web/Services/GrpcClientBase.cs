using Grpc.Net.Client;
using Grpc.Net.Client.Web;

namespace Parstech.Shop.Web.Services;

public abstract class GrpcClientBase
{
    protected readonly GrpcChannel Channel;

    protected GrpcClientBase(IConfiguration configuration)
    {
        string apiServiceUrl = configuration["ApiServiceUrl"] ?? "https://localhost:7156";

        HttpClientHandler httpClientHandler = new();
        GrpcWebHandler grpcWebHandler = new(GrpcWebMode.GrpcWeb, httpClientHandler);

        Channel = GrpcChannel.ForAddress(apiServiceUrl, new() { HttpClient = new(grpcWebHandler) });
    }
}