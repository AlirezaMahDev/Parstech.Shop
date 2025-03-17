using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Features.Brand.Requests.Commands;
using Parstech.Shop.Shared.Protos.Brand;

using Brand = Parstech.Shop.Shared.Models.Brand;

namespace Parstech.Shop.ApiService.Services;

public class BrandGrpcService : BrandService.BrandServiceBase
{
    private readonly IMediator _mediator;

    public BrandGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<BrandResponse> GetBrands(BrandsRequest request, ServerCallContext context)
    {
        try
        {
            var brands = await _mediator.Send(new BrandsQueryReq());

            var response = new BrandResponse();
            foreach (var brand in brands)
            {
                response.Brands.Add(new Brand
                {
                    Id = brand.Id,
                    Name = brand.Name ?? string.Empty,
                    LatinName = brand.LatinName ?? string.Empty,
                    Description = brand.Description ?? string.Empty,
                    Image = brand.Image ?? string.Empty,
                    IsActive = brand.IsActive,
                    Order = brand.Order,
                    Logo = brand.Logo ?? string.Empty,
                    Website = brand.Website ?? string.Empty,
                    Country = brand.Country ?? string.Empty
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<Brand> GetBrandById(BrandByIdRequest request, ServerCallContext context)
    {
        try
        {
            var brand = await _mediator.Send(new BrandReadCommandReq(request.Id));

            return new()
            {
                Id = brand.Id,
                Name = brand.Name ?? string.Empty,
                LatinName = brand.LatinName ?? string.Empty,
                Description = brand.Description ?? string.Empty,
                Image = brand.Image ?? string.Empty,
                IsActive = brand.IsActive,
                Order = brand.Order,
                Logo = brand.Logo ?? string.Empty,
                Website = brand.Website ?? string.Empty,
                Country = brand.Country ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }
}