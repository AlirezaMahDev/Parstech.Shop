using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductStockPriceSection.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPriceSection.Handlers.Commands;

public class
    CreateProdcutStockPriceSectionCommandHandler : IRequestHandler<CreateProdcutStockPriceSectionCommandReq,
    ResponseDto>
{
    #region Constractor

    private readonly IProductStockPriceSectionRepository _productStockPriceSectionRep;

    public CreateProdcutStockPriceSectionCommandHandler(IProductStockPriceSectionRepository productStockPriceSectionRep)
    {
        _productStockPriceSectionRep = productStockPriceSectionRep;
    }

    #endregion

    public async Task<ResponseDto> Handle(CreateProdcutStockPriceSectionCommandReq request,
        CancellationToken cancellationToken)
    {
        ResponseDto response = new();
        Shared.Models.ProductStockPriceSection item = new();
        item.ProductStockPriceId = request.productStockPriceId;
        item.SectionId = request.sectionId;
        Shared.Models.ProductStockPriceSection additem = await _productStockPriceSectionRep.AddAsync(item);

        response.IsSuccessed = true;
        response.Object = additem;
        return response;
    }
}