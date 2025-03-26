using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.ProductStockPriceSection.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.ProductStockPriceSection.Handlers.Commands;

public class CreateProdcutStockPriceSectionCommandHandler : IRequestHandler<CreateProdcutStockPriceSectionCommandReq, ResponseDto>
{
    #region Constractor 
    private readonly IProductStockPriceSectionRepository _productStockPriceSectionRep;

    public CreateProdcutStockPriceSectionCommandHandler(IProductStockPriceSectionRepository productStockPriceSectionRep)
    {
        _productStockPriceSectionRep = productStockPriceSectionRep;
    }
    #endregion
    public async Task<ResponseDto> Handle(CreateProdcutStockPriceSectionCommandReq request, CancellationToken cancellationToken)
    {
        ResponseDto response = new();
        var item = new Domain.Models.ProductStockPriceSection();
        item.ProductStockPriceId = request.productStockPriceId;
        item.SectionId = request.sectionId;
        var additem = await _productStockPriceSectionRep.AddAsync(item);
           
        response.IsSuccessed = true;
        response.Object = additem;
        return response;
    }
}