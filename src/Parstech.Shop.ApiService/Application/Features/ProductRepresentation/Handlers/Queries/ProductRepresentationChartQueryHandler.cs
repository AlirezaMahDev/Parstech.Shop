using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Application.Features.ProductRepresentation.Requests.Queries;

namespace Shop.Application.Features.ProductRepresentation.Handlers.Queries
{
    public class ProductRepresentationChartQueryHandler : IRequestHandler<ProductRepresentationChartQueryReq, List<ProductRepresenationChartDto>>
    {
        private readonly IProductRepresesntationRepository _productRepresentationRep;

        public ProductRepresentationChartQueryHandler(IProductRepresesntationRepository productRepresentationRep)
        {
            _productRepresentationRep = productRepresentationRep;
        }
        public async Task<List<ProductRepresenationChartDto>> Handle(ProductRepresentationChartQueryReq request, CancellationToken cancellationToken)
        {
            List<ProductRepresenationChartDto> Result = new List<ProductRepresenationChartDto>();
            var enter = await _productRepresentationRep.GetCountEnterProductRepresentationWithProductId(request.ProductId);
            ProductRepresenationChartDto enterdto = new ProductRepresenationChartDto()
            {
                Name = "ورود به انبار",
                Color = "",
                Count = enter
            };
            var getOut = await _productRepresentationRep.GetCountGetoutProductRepresentationWithProductId(request.ProductId);
            ProductRepresenationChartDto getoutdto = new ProductRepresenationChartDto()
            {
                Name = "خروج از انبار (فروش)",
                Color = "",
                Count = getOut
            };
            var returns = await _productRepresentationRep.GetCountReturnProductRepresentationWithProductId(request.ProductId);
            ProductRepresenationChartDto returnsdto = new ProductRepresenationChartDto()
            {
                Name = "برگشت از فروش (عودت)",
                Color = "",
                Count = returns
            };
            var manualy = await _productRepresentationRep.GetCountEnterManualyProductRepresentationWithProductId(request.ProductId);
            ProductRepresenationChartDto manualydto = new ProductRepresenationChartDto()
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
}
