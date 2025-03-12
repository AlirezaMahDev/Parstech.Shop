using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using System.Net.Http;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public abstract class GrpcClientBase
    {
        protected readonly GrpcChannel Channel;

        protected GrpcClientBase(IConfiguration configuration)
        {
            var apiServiceUrl = configuration["ApiServiceUrl"] ?? "https://localhost:7156";

            var httpClientHandler = new HttpClientHandler();
            var grpcWebHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, httpClientHandler);

            Channel = GrpcChannel.ForAddress(apiServiceUrl, new GrpcChannelOptions
            {
                HttpClient = new HttpClient(grpcWebHandler)
            });
        }
    }
}
