using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;
using Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.ProductRepresentation.Handlers.Queries;

public class ProductRepresentationChartQueryHandler : IRequestHandler<ProductRepresentationChartQueryReq, List<ProductRepresenationChartDto>>
{
    private readonly IProductRepresesntationRepository _productRepresentationRep;

    public ProductRepresentationChartQueryHandler(IProductRepresesntationRepository productRepresentationRep)
    {
        _productRepresentationRep = productRepresentationRep;
    }
    public async Task<List<ProductRepresenationChartDto>> Handle(ProductRepresentationChartQueryReq request, CancellationToken cancellationToken)
    {
        List<ProductRepresenationChartDto> Result = new();
        var enter = await _productRepresentationRep.GetCountEnterProductRepresentationWithProductId(request.ProductId);
        ProductRepresenationChartDto enterdto = new()
        {
            Name = "ورود به انبار",
            Color = "",
            Count = enter
        };
        var getOut = await _productRepresentationRep.GetCountGetoutProductRepresentationWithProductId(request.ProductId);
        ProductRepresenationChartDto getoutdto = new()
        {
            Name = "خروج از انبار (فروش)",
            Color = "",
            Count = getOut
        };
        var returns = await _productRepresentationRep.GetCountReturnProductRepresentationWithProductId(request.ProductId);
        ProductRepresenationChartDto returnsdto = new()
        {
            Name = "برگشت از فروش (عودت)",
            Color = "",
            Count = returns
        };
        var manualy = await _productRepresentationRep.GetCountEnterManualyProductRepresentationWithProductId(request.ProductId);
        ProductRepresenationChartDto manualydto = new()
        {
            Name = "ورود به انبار به صورت دستی (رفع مغایرت)",
            Color = "",
            Count = manualy
        };
        Result.Add(enterdto);
        Result.Add(getoutdto);
        Result.Add(returnsdto);
        Result.Add(manualydto);
        return Result;
    }
}