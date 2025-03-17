using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductStockPriceSection.Requests.Commands;

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
        Domain.Models.ProductStockPriceSection item = new();
        item.ProductStockPriceId = request.productStockPriceId;
        item.SectionId = request.sectionId;
        Domain.Models.ProductStockPriceSection additem = await _productStockPriceSectionRep.AddAsync(item);

        response.IsSuccessed = true;
        response.Object = additem;
        return response;
    }
}