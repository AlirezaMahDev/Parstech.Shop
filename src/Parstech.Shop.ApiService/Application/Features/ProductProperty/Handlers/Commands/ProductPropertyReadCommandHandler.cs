using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductProperty.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductProperty.Handlers.Commands;

public class ProductPropertyReadCommandHandler : IRequestHandler<ProductPropertyReadCommandReq, ProductPropertyDto>
{
    private readonly IProductPropertyRepository _productPropertyRep;
    private readonly IMapper _mapper;

    public ProductPropertyReadCommandHandler(IProductPropertyRepository productPropertyRep, IMapper mapper)
    {
        _productPropertyRep = productPropertyRep;
        _mapper = mapper;
    }

    public async Task<ProductPropertyDto> Handle(ProductPropertyReadCommandReq request,
        CancellationToken cancellationToken)
    {
        Shared.Models.ProductProperty? pproperty = await _productPropertyRep.GetAsync(request.id);
        return _mapper.Map<ProductPropertyDto>(pproperty);
    }
}