using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductProperty.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductProperty.Handlers.Commands;

public class ProductPropertyUpdateCommandHandler : IRequestHandler<ProductPropertyUpdateCommandReq, ProductPropertyDto>
{
    private readonly IProductPropertyRepository _productPropertyRep;
    private readonly IMapper _mapper;

    public ProductPropertyUpdateCommandHandler(IProductPropertyRepository productPropertyRep, IMapper mapper)
    {
        _productPropertyRep = productPropertyRep;
        _mapper = mapper;
    }

    public async Task<ProductPropertyDto> Handle(ProductPropertyUpdateCommandReq request,
        CancellationToken cancellationToken)
    {
        Shared.Models.ProductProperty? pproperty =
            _mapper.Map<Shared.Models.ProductProperty>(request.ProductPropertyDto);
        Shared.Models.ProductProperty result = await _productPropertyRep.UpdateAsync(pproperty);
        return _mapper.Map<ProductPropertyDto>(result);
    }
}