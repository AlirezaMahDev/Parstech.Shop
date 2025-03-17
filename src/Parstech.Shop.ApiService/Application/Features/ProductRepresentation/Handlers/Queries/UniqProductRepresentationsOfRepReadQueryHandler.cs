using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Handlers.Queries;

public class
    UniqProductRepresentationsOfRepReadQueryHandler : IRequestHandler<UniqProductRepresentationsOfRepReadQueryReq,
    ProductRepresentationList>
{
    private readonly IProductRepresesntationRepository _productRepresentationRep;
    private readonly IProductRepository _productRep;
    private readonly IMapper _mapper;

    public UniqProductRepresentationsOfRepReadQueryHandler(IProductRepresesntationRepository productRepresentationRep,
        IProductRepository productRep,
        IMapper mapper)
    {
        _productRepresentationRep = productRepresentationRep;
        _productRep = productRep;
        _mapper = mapper;
    }

    public async Task<ProductRepresentationList> Handle(UniqProductRepresentationsOfRepReadQueryReq request,
        CancellationToken cancellationToken)
    {
        List<ProductRepresentationDto> result = new();
        List<Domain.Models.ProductRepresentation> list =
            await _productRepresentationRep.GetUniqProductRepresentationFromRepId(request.repId);
        foreach (Domain.Models.ProductRepresentation item in list)
        {
            if (result.Any(u => u.ProductStockPriceId == item.ProductStockPriceId))
            {
            }
            else
            {
                item.Quantity =
                    await _productRepresentationRep.GetLastQuantityFromProductRepresntation(item.ProductStockPriceId,
                        request.repId);
                ProductRepresentationDto? dto = _mapper.Map<ProductRepresentationDto>(item);
                Domain.Models.Product? product = await _productRep.GetAsync(item.ProductStockPriceId);
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