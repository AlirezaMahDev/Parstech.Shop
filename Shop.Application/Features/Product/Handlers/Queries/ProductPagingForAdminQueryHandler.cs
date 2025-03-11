using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.User;
using Shop.Application.Features.User.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.Features.Product.Requests.Queries;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Domain.Models;
using Shop.Application.Enum;
using Shop.Application.Dapper.Product.Queries;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dapper.Helper;
using Dapper;
using System.Reflection.Metadata;
using Shop.Application.Convertor;

namespace Shop.Application.Features.Product.Handlers.Queries
{

    public class ProductPagingForAdminQueryHandler : IRequestHandler<ProductPagingForAdminQueryReq, ProductPageingDto>
    {
        private readonly IProductRepository _productRep;
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRep;
        private readonly IProductGallleryRepository _gallleryRep;
        private readonly IProductTypeRepository _productTypeRep;
        private readonly IProductCateguryRepository _productCateguryRep;
        private readonly IUserStoreRepository _userStoreRep;
        private readonly ICateguryRepository _categuryRep;
        private readonly IOrderDetailRepository _orderDetailep;
        private readonly IRepresentationRepository _representationRep;
        private readonly IProductStockPriceRepository _productStockRep;
        private readonly IProductQueries _productQueries;
        private readonly string _connectionString;

        public ProductPagingForAdminQueryHandler(IProductRepository productRep,
            IMapper mapper, IBrandRepository brandRep, IProductGallleryRepository gallleryRep,
            IProductTypeRepository productTypeRep,
            IUserStoreRepository userStoreRep,
            ICateguryRepository categuryRep,
            IOrderDetailRepository orderDetailep,
            IProductCateguryRepository productCateguryRep,
            IProductStockPriceRepository productStockRep,
            IProductQueries productQueries,
            IConfiguration configuration,
            IRepresentationRepository representationRep)
        {
            _productRep = productRep;
            _mapper = mapper;
            _brandRep = brandRep;
            _gallleryRep = gallleryRep;
            _productTypeRep = productTypeRep;
            _userStoreRep = userStoreRep;
            _categuryRep = categuryRep;
            _orderDetailep = orderDetailep;
            _productCateguryRep = productCateguryRep;
            _representationRep = representationRep;
            _productStockRep = productStockRep;
            _productQueries = productQueries;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        public async Task<ProductPageingDto> Handle(ProductPagingForAdminQueryReq request, CancellationToken cancellationToken)
        {
            IList<ProductDto> productDto = new List<ProductDto>();
            ProductPageingDto response = new ProductPageingDto();

            string serach = "";
            string categury = "";
            string isActive = "";
            //if (!string.IsNullOrEmpty(request.ProductParameterDto.Filter))
            //{

            //var words = GetWordFromString.GetWords(request.ProductParameterDto.Filter);
            //var sb = new StringBuilder();
            //if (words.Count() > 1)
            //{
            //    sb.Append($"\"{request.ProductParameterDto.Filter}*\"OR");
            //}

            //foreach (var word in words)
            //{

            //    var lastItem = words.Last();
            //    if (word == lastItem)
            //    {
            //        sb.Append($"\"{word}*\"");
            //    }
            //    else
            //    {
            //        sb.Append($"\"{word}*\" OR");
            //    }
            //}
            //var serach = $"CONTAINS(dbo.Product.Name, '{sb}')";


            //string query = $"SELECT dbo.Product.Id,dbo.Product.IsActive,dbo.Product.CreateDate, dbo.Product.Name,dbo.Product.SingleSale, dbo.Product.LatinName, dbo.Product.Code, dbo.Product.ShortDescription, dbo.Product.ShortLink, dbo.Product.TypeId FROM dbo.Product where TypeId!=3 and  {serach}";
            //var searched2 = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(query).ToList());

            //response.Searched = searched2.Count();
            //foreach (var item in searched2)
            //{
            //    var PDto = _mapper.Map<ProductDto>(item);


            //    PDto.CateguryName = item.GroupTitle;
            //    PDto.BrandName = item.BrandTitle;
            //    PDto.LatinBrandName = item.LatinBrandTitle;

            //    var image = DapperHelper.ExecuteCommand<Domain.Models.ProductGallery>(_connectionString, conn => conn.Query<Domain.Models.ProductGallery>(_productQueries.GetMainImage, new { @productId = item.Id }).FirstOrDefault());
            //    if (image != null)
            //    {
            //        PDto.Image = image.ImageName;
            //    }




            //    var CountSale = await _orderDetailep.CountOfSaleByProductId(PDto.Id);
            //    PDto.CountSale = CountSale;

            //    var Type = await _productTypeRep.GetAsync(PDto.TypeId);
            //    PDto.TypeName = Type.TypeName;



            //    //var Pdto = _mapper.Map<ProductDto>(item);
            //    PDto.ProductStockPriceId = item.Id;
            //    PDto.ProductId = item.Id;
            //    //productDto.Add(Pdto);
            //    PDto.CreateDateShamsi = item.CreateDate.ToShamsi();
            //    productDto.Add(PDto);

            //}
            //response.CurrentPage = request.ProductParameterDto.CurrentPage;

            //response.PageCount = 0;

            //response.ProductDtos = productDto.ToArray();
            //return response;
            //}
            if (!string.IsNullOrEmpty(request.ProductParameterDto.Filter))
            {
                //var words = GetWordFromString.GetWords(request.ProductParameterDto.Filter);
                //var sb = new StringBuilder();
                //if (words.Count() > 1)
                //{
                //    sb.Append($"\"{request.ProductParameterDto.Filter}*\"OR");
                //}

                //foreach (var word in words)
                //{

                //    var lastItem = words.Last();
                //    if (word == lastItem)
                //    {
                //        sb.Append($"\"{word}*\"");
                //    }
                //    else
                //    {
                //        sb.Append($"\"{word}*\" OR");
                //    }
                //}
                serach = $" AND dbo.Product.Id={request.ProductParameterDto.Filter}";
            }
            if (request.ProductParameterDto.Categury!=null) {
                categury = $"AND EXISTS (SELECT 1 FROM dbo.ProductCategury WHERE ProductId = dbo.Product.Id AND CateguryId = {request.ProductParameterDto.Categury}) ";
            }
            if (request.ProductParameterDto.Type != null)
            {
                switch (request.ProductParameterDto.Type)
                {
                    case "Active":
                        isActive = $"AND IsActive=1";
                        break;
                    case "NotActive":
                        isActive = $"AND IsActive=0";
                        break;
                }
                
            }

            var products = new List<DapperProductDto>();
            int skip = (request.ProductParameterDto.CurrentPage - 1) * request.ProductParameterDto.TakePage;
            var query = $"SELECT dbo.Product.Id,dbo.Product.IsActive,dbo.Product.CreateDate, dbo.Product.Name,dbo.Product.SingleSale, dbo.Product.LatinName, dbo.Product.Code, dbo.Product.ShortDescription, dbo.Product.ShortLink, dbo.Product.TypeId FROM dbo.Product where TypeId!=3 {categury} {isActive} {serach} ORDER BY CreateDate Desc OFFSET {skip} ROWS FETCH NEXT {request.ProductParameterDto.TakePage} ROWS ONLY";
            products = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(query).ToList());
            //products = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(_productQueries.GetProductsPagingForAdmin, new { @skip = skip, @take = request.ProductParameterDto.TakePage }).ToList());


            foreach (var product in products)
            {

                var PDto = _mapper.Map<ProductDto>(product);


                PDto.CateguryName = product.GroupTitle;
                PDto.BrandName = product.BrandTitle;
                PDto.LatinBrandName = product.LatinBrandTitle;
                PDto.ProductId = product.Id;
                var image = DapperHelper.ExecuteCommand<Domain.Models.ProductGallery>(_connectionString, conn => conn.Query<Domain.Models.ProductGallery>(_productQueries.GetMainImage, new { @productId = product.Id }).FirstOrDefault());
                if (image != null)
                {
                    PDto.Image = image.ImageName;
                }


                if (PDto.TypeId == 2)
                {

                    var variations = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(_productQueries.GetFirstVariation, new { @parentId = product.Id }).ToList());
                    if (variations.Count > 0)
                    {
                        var variation = variations.FirstOrDefault();
                        PDto.ProductStockPriceId = variation.Id;
                        PDto.Quantity = variation.Quantity;
                        PDto.SalePrice = variation.SalePrice;
                        PDto.DiscountPrice = variation.DiscountPrice;
                    }
                    else
                    {
                        PDto.ProductStockPriceId = product.Id;
                    }
                }
                else
                {
                    PDto.ProductStockPriceId = product.Id;
                }
                //var Rep = await _representationRep.GetAsync(PDto.RepId);
                //PDto.RepName = Rep.Name;

                //if (request.ProductParameterDto.Categury != null)
                //{
                //    var Categury = await _categuryRep.GetCateguryByLatinName(request.ProductParameterDto.Categury);
                //    if (Categury != null)
                //    {
                //        var ProductHaveCategury = await _productCateguryRep.ProductHaveCategury(PDto.Id, Categury.GroupId);
                //        if (ProductHaveCategury)
                //        {
                //            PDto.CateguryName = Categury.GroupTitle;
                //            PDto.CateguryLatinName = Categury.LatinGroupTitle;

                //        }
                //    }

                //}
                var CountSale = await _orderDetailep.CountOfSaleByProductId(PDto.Id);
                PDto.CountSale = CountSale;

                var Type = await _productTypeRep.GetAsync(PDto.TypeId);
                PDto.TypeName = Type.TypeName;
                PDto.CreateDateShamsi = product.CreateDate.ToShamsi();
                productDto.Add(PDto);


            }

            //IQueryable<ProductDto> result = productDto.AsQueryable();


            var AllListquery = $"SELECT dbo.Product.Id FROM dbo.Product where TypeId!=3 {categury} {isActive} {serach} ORDER BY CreateDate Desc";
            var AllList = DapperHelper.ExecuteCommand<List<int>>(_connectionString, conn => conn.Query<int>(AllListquery).ToList());

            //var AllList = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(_productQueries.GetAllList).ToList());
            response.CurrentPage = request.ProductParameterDto.CurrentPage;

            response.PageCount = AllList.Count() / request.ProductParameterDto.TakePage;


            





            
            

            response.ProductDtos = productDto.ToArray();

            return response;

        }
    }
}
