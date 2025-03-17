using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Brand.Requests.Commands;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Services;

public class BrandService : BrandServiceBase
{
    private readonly IMediator _mediator;
    private readonly IBrandRepository _brandRepository;

    public BrandService(IMediator mediator, IBrandRepository brandRepository)
    {
        _mediator = mediator;
        _brandRepository = brandRepository;
    }

    public override async Task<BrandResponse> GetBrands(BrandsRequest request, ServerCallContext context)
    {
        try
        {
            var command = new BrandReadsCommandReq();
            void brands = await _mediator.Send(command);

            var response = new BrandResponse();
            response.Brands.AddRange(brands.Select(b => new Brand
            {
                Id = b.Id,
                Name = b.Name,
                LatinName = b.LatinName,
                Description = b.Description,
                Image = b.Image,
                IsActive = b.IsActive,
                Order = b.Order,
                Logo = b.Logo,
                Website = b.Website,
                Country = b.Country
            }));

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
            var brand = await _brandRepository.GetById(request.Id);
            if (brand == null)
            {
                throw new RpcException(new(StatusCode.NotFound, "Brand not found"));
            }

            return new Brand
            {
                Id = brand.Id,
                Name = brand.Name,
                LatinName = brand.LatinName,
                Description = brand.Description,
                Image = brand.Image,
                IsActive = brand.IsActive,
                Order = brand.Order,
                Logo = brand.Logo,
                Website = brand.Website,
                Country = brand.Country
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }
}