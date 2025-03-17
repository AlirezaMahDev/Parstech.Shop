using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductType.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.ProductType.Handlers.Commands;

public class ProductTypeReadCommandHandler : IRequestHandler<ProductTypeReadCommandReq, ProductTypeDto>
{
    private IProductTypeRepository _productTypeRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public ProductTypeReadCommandHandler(IProductTypeRepository productTypeRep, IMapper mapper, IMediator madiiator)
    {
        _productTypeRep = productTypeRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }

    public async Task<ProductTypeDto> Handle(ProductTypeReadCommandReq request, CancellationToken cancellationToken)
    {
        Domain.Models.ProductType? type = await _productTypeRep.GetAsync(request.id);
        return _mapper.Map<ProductTypeDto>(type);
    }
}

public class ProductTypeReadsCommandHandler : IRequestHandler<ProductTypeReadsCommandReq, List<ProductTypeDto>>
{
    private IProductTypeRepository _productTypeRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public ProductTypeReadsCommandHandler(IProductTypeRepository productTypeRep, IMapper mapper, IMediator madiiator)
    {
        _productTypeRep = productTypeRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }

    public async Task<List<ProductTypeDto>> Handle(ProductTypeReadsCommandReq request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Domain.Models.ProductType>? types = await _productTypeRep.GetAll();
        return _mapper.Map<List<ProductTypeDto>>(types);
    }
}