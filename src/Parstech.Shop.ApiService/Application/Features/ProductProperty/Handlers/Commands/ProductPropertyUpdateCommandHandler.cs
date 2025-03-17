using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductProperty.Requests.Commands;

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
        Domain.Models.ProductProperty? pproperty =
            _mapper.Map<Domain.Models.ProductProperty>(request.ProductPropertyDto);
        Domain.Models.ProductProperty? result = await _productPropertyRep.UpdateAsync(pproperty);
        return _mapper.Map<ProductPropertyDto>(result);
    }
}