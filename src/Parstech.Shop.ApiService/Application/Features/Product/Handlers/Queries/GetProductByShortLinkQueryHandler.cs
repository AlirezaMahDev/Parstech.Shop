using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

public class GetProductByShortLinkQueryHandler : IRequestHandler<GetProductByShortLinkQueryReq, ProductDto>
{
    private readonly IProductRepository _productRep;
    private readonly IMapper _mapper;

    public GetProductByShortLinkQueryHandler(IProductRepository productRep, IMapper mapper)
    {
        _productRep = productRep;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductByShortLinkQueryReq request, CancellationToken cancellationToken)
    {
        Domain.Models.Product? item = await _productRep.GetProductByShortLink(request.shortLink);
        if (item.TypeId == 3)
        {
            Domain.Models.Product? parrent = await _productRep.GetAsync(item.ParentId.Value);
            item = await _productRep.GetProductByShortLink(parrent.ShortLink);
        }

        return _mapper.Map<ProductDto>(item);
    }
}