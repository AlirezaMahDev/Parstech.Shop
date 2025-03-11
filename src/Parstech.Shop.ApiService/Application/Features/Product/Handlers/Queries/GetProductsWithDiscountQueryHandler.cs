using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Dapper.Helper;
using Shop.Application.Dapper.Product.Queries;
using Shop.Application.DTOs.Api;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Product.Handlers.Queries
{
	public class GetProductsWithDiscountQueryHandler : IRequestHandler<GetProductsWithDiscountQueryReq, List<ProductDto>>
	{
		private readonly IProductRepository _productRep;
		private readonly IMapper _mapper;
		private readonly IProductGallleryRepository _productGallleryRep;
		private readonly IProductStockPriceRepository _productStockRep;
        private readonly string _connectionString;
        private readonly IProductQueries _productQueries;

        public GetProductsWithDiscountQueryHandler(IProductRepository productRep, 
			IMapper mapper,
            IProductGallleryRepository productGallleryRep,
			IProductQueries productQueries,
			IConfiguration configuration,
			IProductStockPriceRepository productStockRep)
		{
			_productRep = productRep;
			_mapper = mapper;
			_productGallleryRep = productGallleryRep;
			_productStockRep = productStockRep;
			_productQueries = productQueries;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");

        }

		public async Task<List<ProductDto>> Handle(GetProductsWithDiscountQueryReq request, CancellationToken cancellationToken)
		{
			
			var query = $"SELECT ps.Id as ProductStockPriceId, ps.Id ,p.Id as ProductId,p.ParentId,p.Name,p.Code,p.TypeId,p.ShortLink,s.SectionId ,ps.SalePrice,ps.DiscountPrice,ps.DiscountDate, ps.StockStatus, ps.Quantity FROM dbo.ProductStockPrice as ps INNER JOIN dbo.Product as p ON p.Id = ps.ProductId INNER JOIN dbo.ProductStockPriceSection as s ON ps.Id = s.ProductStockPriceId where ps.DiscountPrice!=0 and s.SectionId={request.sectionId} and ps.Quantity>0 and ps.ShowInDiscountPanels=1 and p.IsActive=1 ORDER BY CreateDate Desc OFFSET 0 ROWS FETCH NEXT 40 ROWS ONLY";
			var products = DapperHelper.ExecuteCommand<List<ProductDto>>(_connectionString, conn => conn.Query<ProductDto>(query).ToList());

            //List<ProductListShowDto> productDto = new List<ProductListShowDto>();
            foreach (var item in products)
            {
               
                //var Dto = _mapper.Map<ProductListShowDto>(item);
                var image = DapperHelper.ExecuteCommand<Domain.Models.ProductGallery>(_connectionString, conn => conn.Query<Domain.Models.ProductGallery>(_productQueries.GetMainImage, new { @productId = item.ProductId }).FirstOrDefault());
                item.Image = image.ImageName;
                //ite.ProductStockPriceId = item.Id;
                //productDto.Add(Dto);
            }
   //      
			return products;

		}
	}
}
