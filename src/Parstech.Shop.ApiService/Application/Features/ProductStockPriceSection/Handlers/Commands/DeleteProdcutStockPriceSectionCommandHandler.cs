using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductStockPriceSection.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPriceSection.Handlers.Commands;

public class
    DeleteProdcutStockPriceSectionCommandHandler : IRequestHandler<DeleteProdcutStockPriceSectionCommandReq,
    ResponseDto>
{
    #region Constractor

    private readonly IProductStockPriceSectionRepository _productStockPriceSectionRep;

    public DeleteProdcutStockPriceSectionCommandHandler(IProductStockPriceSectionRepository productStockPriceSectionRep)
    {
        _productStockPriceSectionRep = productStockPriceSectionRep;
    }

    #endregion

    public async Task<ResponseDto> Handle(DeleteProdcutStockPriceSectionCommandReq request,
        CancellationToken cancellationToken)
    {
        ResponseDto response = new();
        Domain.Models.ProductStockPriceSection? item = await _productStockPriceSectionRep.GetAsync(request.id);
        await _productStockPriceSectionRep.DeleteAsync(item);
        response.IsSuccessed = true;
        response.Object = request.id;
        return response;
    }
}