using Grpc.Core;

using Parstech.Shop.ApiService.Application.Dapper.ProductProperty.Queries;

namespace Parstech.Shop.ApiService.Services;

public class ProductPropertyGrpcService : PropertyAdminService.PropertyAdminServiceBase
{
    private readonly GetProductPropertiesByParrentIdQueryHandler _getProductPropertiesHandler;

    public ProductPropertyGrpcService(IConfiguration configuration)
    {
        _getProductPropertiesHandler = new GetProductPropertiesByParrentIdQueryHandler(configuration);
    }

    public override async Task<GetPropertiesByProductIdResponse> GetPropertiesByProductId(
        GetPropertiesByProductIdRequest request, ServerCallContext context)
    {
        try
        {
            var properties = await _getProductPropertiesHandler.ExecuteAsync(request.ProductId);
            
            var response = new GetPropertiesByProductIdResponse();
            
            foreach (var property in properties)
            {
                response.Properties.Add(new PropertyDto
                {
                    Id = property.Id,
                    Title = property.Title ?? string.Empty,
                    Value = property.Value ?? string.Empty,
                    CreateDay = property.CreateDay ?? string.Empty,
                    UpdateDay = property.UpdateDay ?? string.Empty,
                    ParrentId = property.ParrentId,
                    IsEnable = property.IsEnable,
                    OrderShow = property.OrderShow,
                    Status = property.Status
                });
            }
            
            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }
} 