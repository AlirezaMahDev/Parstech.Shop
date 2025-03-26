using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.ProductStockPriceSection.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.ProductStockPriceSection.Handlers.Commands;

public class DeleteProdcutStockPriceSectionCommandHandler : IRequestHandler<DeleteProdcutStockPriceSectionCommandReq, ResponseDto>
{
    #region Constractor 
    private readonly IProductStockPriceSectionRepository _productStockPriceSectionRep;

    public DeleteProdcutStockPriceSectionCommandHandler(IProductStockPriceSectionRepository productStockPriceSectionRep)
    {
        _productStockPriceSectionRep = productStockPriceSectionRep;
    }
    #endregion
    public async Task<ResponseDto> Handle(DeleteProdcutStockPriceSectionCommandReq request, CancellationToken cancellationToken)
    {
        ResponseDto response=new();
        var item =await _productStockPriceSectionRep.GetAsync(request.id);
        await _productStockPriceSectionRep.DeleteAsync(item);
        response.IsSuccessed = true;
        response.Object = request.id;
        return response;
    }
}