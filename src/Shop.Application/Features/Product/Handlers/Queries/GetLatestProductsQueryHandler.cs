using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
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
	public class GetLatestProductsQueryHandler : IRequestHandler<GetLatestProductsQueryReq, List<ProductListShowDto>>
	{
		private readonly IProductRepository _productRep;
		private readonly IProductStockPriceRepository _productStockRep;
		private readonly IMapper _mapper;
        private readonly IProductGallleryRepository _productGallleryRep;

        public GetLatestProductsQueryHandler(IProductRepository productRep, IMapper mapper,
            IProductGallleryRepository productGallleryRep, IProductStockPriceRepository productStockRep)
		{
			_productRep = productRep;
			_mapper = mapper;
			_productGallleryRep = productGallleryRep;
            _productStockRep = productStockRep;

        }
		public async Task<List<ProductListShowDto>> Handle(GetLatestProductsQueryReq request, CancellationToken cancellationToken)
		{
            var allProducts = await _productRep.GetAll();
            var products = allProducts.Where(z => z.TypeId == request.productTypeId &&z.IsActive);
            List<ProductListShowDto> productDto = new List<ProductListShowDto>();
            foreach (var product in products)
            {
                
                ProductListShowDto x = new ProductListShowDto();
                x = _mapper.Map<ProductListShowDto>(product);
                //x.DiscountPrice = (product.Price - product.DiscountPrice);
                var pic = await _productGallleryRep.GetMainImageOfProduct(product.Id);
                x.Image = pic.ImageName;

                productDto.Add(x);
            }
            return productDto.OrderByDescending(u => u.Id).Take(request.take).ToList();

		}
	}
}
