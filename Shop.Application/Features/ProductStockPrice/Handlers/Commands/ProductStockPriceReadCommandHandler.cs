using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Humanizer;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Convertor;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductStockPrice;
using Shop.Application.DTOs.Section;
using Shop.Application.DTOs.User;
using Shop.Application.Features.Product.Requests.Commands;
using Shop.Application.Features.ProductStockPrice.Requests.Commands;
using Shop.Application.Features.Section.Requests.Commands;
using Shop.Application.Features.User.Requests.Commands;

namespace Shop.Application.Features.ProductStockPrice.Handlers.Commands
{
    public class ProductStockPriceReadCommandHandler : IRequestHandler<ProductStockPriceReadCommandReq, ProductStockPriceDto>
    {

        private IProductStockPriceRepository _productStockRep;
        private readonly string _connectionString;
        private IMapper _mapper;
        private IMediator _madiiator;

        public ProductStockPriceReadCommandHandler(IProductStockPriceRepository productStockRep,
            IMapper mapper,
            IMediator madiiator,IConfiguration configuration)
        {
            _productStockRep= productStockRep;
            _mapper = mapper;
            _madiiator = madiiator;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }


        public async Task<ProductStockPriceDto> Handle(ProductStockPriceReadCommandReq request, CancellationToken cancellationToken)
        {
            ProductStockPriceDto result = new ProductStockPriceDto();
            //var product = await _productRep.GetAsync(request.id);
            //var product = await _productStockRep.GetAsync(request.id);

            var query = $"Select* From ProductStockPrice Where Id={request.id}";
            var product = DapperHelper.ExecuteCommand<ProductStockPriceDto>(_connectionString, conn => conn.Query<ProductStockPriceDto>(query).FirstOrDefault());


            result = _mapper.Map<ProductStockPriceDto>(product);
            result.TextPrice = product.Price.ToString();
            result.TextSalePrice = product.SalePrice.ToString();
            result.TextDiscountPrice = product.DiscountPrice.ToString();
            result.TextBasePrice = product.BasePrice.ToString();
            if(product.DiscountDate != null)
            {
                result.DiscountDateShamsi = product.DiscountDate.Value.ToShamsi();
            }
            
            return result;
        }
    }
}
