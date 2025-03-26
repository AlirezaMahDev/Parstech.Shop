using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;
using Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.ProductRepresentation.Handlers.Queries;

public class UniqProductRepresentationsOfRepReadQueryHandler : IRequestHandler<UniqProductRepresentationsOfRepReadQueryReq, ProductRepresentationList>
{
    private readonly IProductRepresesntationRepository _productRepresentationRep;
    private readonly IProductRepository _productRep;
    private readonly IMapper _mapper;

    public UniqProductRepresentationsOfRepReadQueryHandler(IProductRepresesntationRepository productRepresentationRep, IProductRepository productRep, IMapper mapper)
    {
        _productRepresentationRep = productRepresentationRep;
        _productRep = productRep;
        _mapper = mapper;
    }
    public async Task<ProductRepresentationList> Handle(UniqProductRepresentationsOfRepReadQueryReq request, CancellationToken cancellationToken)
    {
        List<ProductRepresentationDto> result = new();
        var list =await _productRepresentationRep.GetUniqProductRepresentationFromRepId(request.repId);
        foreach (var item in list)
        {
            if (result.Any(u=>u.ProductStockPriceId ==item.ProductStockPriceId))
            {
                    
            }
            else
            {
                item.Quantity =await _productRepresentationRep.GetLastQuantityFromProductRepresntation(item.ProductStockPriceId, request.repId);
                var dto = _mapper.Map<ProductRepresentationDto>(item);
                var product =await _productRep.GetAsync(item.ProductStockPriceId);
                dto.ProductName = product.Name;
                dto.ProductCode = product.Code;
                result.Add(dto);
            }
        }

        ProductRepresentationList FinalResult = new();
        FinalResult.RepId = request.repId;
        FinalResult.ProductRepresentationDtos = result;
        return FinalResult;
    }
}