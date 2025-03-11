using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Convertor;
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
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Shop.Application.Features.Product.Handlers.Queries
{
    public class SearchProductQueryHandler : IRequestHandler<SearchProductQueryReq, List<ProductDto>>
    {
        private readonly IProductRepository _productRep;
        private readonly IProductGallleryRepository _productGallleryRep;
        private readonly IMapper _mapper;
        private IProductQueries _productQueries;
        private string _connectionString;
        private IProductStockPriceRepository _productStockPriceRep;
        public SearchProductQueryHandler(IProductRepository productRep,
            IMapper mapper ,
            IProductQueries productQueries,
            IConfiguration configuration,
            IProductGallleryRepository productGallleryRep,
            IProductStockPriceRepository productStockPriceRep)
        {
            _productRep = productRep;
            _mapper = mapper;
            _productGallleryRep = productGallleryRep;
            _productQueries = productQueries;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
            _productStockPriceRep = productStockPriceRep;
        }
        public async Task<List<ProductDto>> Handle(SearchProductQueryReq request, CancellationToken cancellationToken)
        {
            var Result=new List<ProductDto>();
            //var list =await _productRep.SearchProducts(request.Filter,request.Take);

            var list = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(_productQueries.GetAllList).ToList());

            if (!string.IsNullOrEmpty(request.Filter))
            {

                
                var searched = list.Where(p =>
                    (p.Name.Contains(request.Filter, StringComparison.InvariantCultureIgnoreCase) ||
                (p.Code == request.Filter)
                     )).ToList();


                

                foreach (var item in searched.Take(request.Take))
                {
                    var id = 0;
                    if (item.ParentId != null) { id = item.ParentId.Value; }
                    else { id = item.Id; }
                    var dto = _mapper.Map<ProductDto>(item);
                    var image = DapperHelper.ExecuteCommand<Domain.Models.ProductGallery>(_connectionString, conn => conn.Query<Domain.Models.ProductGallery>(_productQueries.GetMainImage, new { @productId = id }).FirstOrDefault());

                    if (image != null)
                    {
                        dto.Image = image.ImageName;
                    }
                    var pstockId = await _productStockPriceRep.GetFirstProductStockPriceIdFromProductId(item.Id);
                    dto.ProductStockPriceId = pstockId;
                    //var pic = await _productGallleryRep.GetMainImageOfProduct(item.Id);
                    //dto.Image = pic.ImageName;
                    Result.Add(dto);

                }
               
            }


            
            return Result;
        }
    }
}
