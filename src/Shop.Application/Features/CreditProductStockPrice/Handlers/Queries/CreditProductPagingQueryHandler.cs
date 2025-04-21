using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Dapper.Helper;
using Shop.Application.Dapper.Product.Queries;
using Shop.Application.DTOs.CreditProductStockPrice;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Application.Features.CreditProductStockPrice.Requests.Queries;


namespace Shop.Application.Features.CreditProductStockPrice.Handlers.Queries
{


    public class CreditProductPagingQueryHandler : IRequestHandler<CreditProductPagingQueryReq, ProductRepresentationPagingDto>
    {
        private readonly IProductRepresesntationRepository _productRepresentationRep;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRep;
        private readonly IProductStockPriceRepository _productStockRep;
        private readonly IProductQueries _productQueries;
        private readonly string _connectionString;

        public CreditProductPagingQueryHandler(IProductRepresesntationRepository productRepresentationRep,
            IMapper mapper,
            IProductRepository productRep,
            IProductQueries productQueries,
            IConfiguration configuration,
            IProductStockPriceRepository productStockRep)
        {
            _productRepresentationRep = productRepresentationRep;
            _mapper = mapper;
            _productRep = productRep;
            _productStockRep = productStockRep;
            _productQueries = productQueries;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        public async Task<ProductRepresentationPagingDto> Handle(CreditProductPagingQueryReq request, CancellationToken cancellationToken)
        {
            ProductRepresentationPagingDto response = new ProductRepresentationPagingDto();
            string serach = "";
            string rep = "";


            if (request.ProductRepresenationParameterDto.RepId != 0)
            {
                rep = $"and ps.RepId={request.ProductRepresenationParameterDto.RepId}";
            }
            if (!string.IsNullOrEmpty(request.ProductRepresenationParameterDto.Filter))
            {
                serach = $" AND p.Id={request.ProductRepresenationParameterDto.Filter}";
            }
            
            
            int skip = (request.ProductRepresenationParameterDto.CurrentPage - 1) * request.ProductRepresenationParameterDto.TakePage;

            var query = $"select c.* ,ps.SalePrice,ps.DiscountPrice ,p.Name,p.code from CreditProductStockPrice as c LEFT join productstockPrice as ps on c.ProductStockPriceId=ps.Id LEFT join Product as p on ps.ProductId=p.Id where c.ProductStockPriceId is not null {rep} {serach}   ORDER BY c.Id Desc OFFSET {skip} ROWS FETCH NEXT {request.ProductRepresenationParameterDto.TakePage} ROWS ONLY";
           var productReps = DapperHelper.ExecuteCommand<List<CreditProductStockPriceDto>>(_connectionString, conn => conn.Query<CreditProductStockPriceDto>(query).ToList());




            var AllListquery = $"select c.Id from CreditProductStockPrice as c LEFT join productstockPrice as ps on c.ProductStockPriceId=ps.Id LEFT join Product as p on ps.ProductId=p.Id where c.ProductStockPriceId is not null {rep} {serach}   ORDER BY c.Id Desc ";
            var AllList = DapperHelper.ExecuteCommand<List<int>>(_connectionString, conn => conn.Query<int>(AllListquery).ToList());
            response.CurrentPage = request.ProductRepresenationParameterDto.CurrentPage;
            response.PageCount = AllList.Count() / request.ProductRepresenationParameterDto.TakePage;


            response.List = productReps.ToArray();

            return response;


        }
    }
}
