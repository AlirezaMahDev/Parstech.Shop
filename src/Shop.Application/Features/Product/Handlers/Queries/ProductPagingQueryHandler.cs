﻿using MediatR;
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
using Microsoft.EntityFrameworkCore;
using Shop.Application.DTOs.Api;
using System.Collections;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static OfficeOpenXml.ExcelErrorValue;

namespace Shop.Application.Features.Product.Handlers.Queries
{

    public class ProductPagingQueryHandler : IRequestHandler<ProductPagingQueryReq, ProductPageingDto>
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

        public ProductPagingQueryHandler(IProductRepository productRep,
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
        public async Task<ProductPageingDto> Handle(ProductPagingQueryReq request, CancellationToken cancellationToken)
        {

            IList<ProductDto> productDto = new List<ProductDto>();
            var products = new List<DapperProductDto>();
            int skip = (request.ProductParameterDto.CurrentPage - 1) * request.ProductParameterDto.TakePage;
            if (request.ProductParameterDto.Categury != null)
            {
                products = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(_productQueries.GetListPagingByGroup, new { @categuryLatinName = request.ProductParameterDto.Categury, @skip = skip, @take = request.ProductParameterDto.TakePage }).ToList());

            }
            else
            {
                products = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(_productQueries.GetAllListPaging, new { @skip = skip, @take = request.ProductParameterDto.TakePage }).ToList());

            }
            foreach (var product in products)
            {
                if (product.TypeId == 5 && !product.SingleSale && request.ProductParameterDto.Categury != null)
                {
                    continue;
                }
                else
                {
                    var PDto = _mapper.Map<ProductDto>(product);


                    PDto.CateguryName = product.GroupTitle;
                    PDto.BrandName = product.BrandTitle;
                    PDto.LatinBrandName = product.LatinBrandTitle;

                    var image = DapperHelper.ExecuteCommand<Domain.Models.ProductGallery>(_connectionString, conn => conn.Query<Domain.Models.ProductGallery>(_productQueries.GetMainImage, new { @productId = product.ProductId }).FirstOrDefault());
                    if (image != null)
                    {
                        PDto.Image = image.ImageName;
                    }


                    if (PDto.TypeId == 2)
                    {

                        var variations = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(_productQueries.GetFirstVariation, new { @parentId = product.ProductId }).ToList());
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
                    var Rep = await _representationRep.GetAsync(PDto.RepId);
                    PDto.RepName = Rep.Name;

                    if (request.ProductParameterDto.Categury != null)
                    {
                        var Categury = await _categuryRep.GetCateguryByLatinName(request.ProductParameterDto.Categury);
                        if (Categury != null)
                        {
                            var ProductHaveCategury = await _productCateguryRep.ProductHaveCategury(PDto.Id, Categury.GroupId);
                            if (ProductHaveCategury)
                            {
                                PDto.CateguryName = Categury.GroupTitle;
                                PDto.CateguryLatinName = Categury.LatinGroupTitle;

                            }
                        }

                    }
                    var CountSale = await _orderDetailep.CountOfSaleByProductId(PDto.Id);
                    PDto.CountSale = CountSale;

                    var Type = await _productTypeRep.GetAsync(PDto.TypeId);
                    PDto.TypeName = Type.TypeName;
                    productDto.Add(PDto);
                }

            }

            IQueryable<ProductDto> result = productDto.AsQueryable();


            if (request.ProductParameterDto.Store != null)
            {
                result = result.Where(p =>
                        (p.StoreName.Contains(request.ProductParameterDto.Store)
                        || (p.LatinStoreName.Contains(request.ProductParameterDto.Store))));
            }
            //if (request.ProductParameterDto.Categury != null)
            //{
            //    result = result.Where(p =>
            //            (p.CateguryLatinName == (request.ProductParameterDto.Categury)));
            //}
            //if (request.ProductParameterDto.Brand != null)
            //{
            //    result = result.Where(p =>
            //            (p.BrandName.Contains(request.ProductParameterDto.Brand)
            //            || (p.LatinBrandName.Contains(request.ProductParameterDto.Brand))));
            //}


            switch (request.ProductParameterDto.Type)
            {
                case "Top":
                    result = result.OrderByDescending(u => u.CountSale);
                    break;
                case "New":
                    result = result.OrderByDescending(u => u.CreateDate);
                    break;
                case "LowPrice":
                    result = result.OrderBy(u => u.SalePrice);
                    break;
                case "HighPrice":
                    result = result.OrderByDescending(u => u.SalePrice);
                    break;
                default:
                    break;
            }



            ProductPageingDto response = new ProductPageingDto();

            var AllList = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(_productQueries.GetAllList).ToList());

            if (!string.IsNullOrEmpty(request.ProductParameterDto.Filter))
            {

                productDto = new List<ProductDto>();
                var searched = AllList.Where(p =>
                    (p.Name.Contains(request.ProductParameterDto.Filter) ||
                    (p.StoreName.Contains(request.ProductParameterDto.Filter) ||
                     (p.Code == request.ProductParameterDto.Filter))
                     )).ToList();

                foreach (var item in searched)
                {
                    var PDto = _mapper.Map<ProductDto>(item);


                    PDto.CateguryName = item.GroupTitle;
                    PDto.BrandName = item.BrandTitle;
                    PDto.LatinBrandName = item.LatinBrandTitle;

                    var image = DapperHelper.ExecuteCommand<Domain.Models.ProductGallery>(_connectionString, conn => conn.Query<Domain.Models.ProductGallery>(_productQueries.GetMainImage, new { @productId = item.ProductId }).FirstOrDefault());
                    if (image != null)
                    {
                        PDto.Image = image.ImageName;
                    }



                    var Rep = await _representationRep.GetAsync(PDto.RepId);
                    PDto.RepName = Rep.Name;

                    if (request.ProductParameterDto.Categury != null)
                    {
                        var Categury = await _categuryRep.GetCateguryByLatinName(request.ProductParameterDto.Categury);
                        if (Categury != null)
                        {
                            var ProductHaveCategury = await _productCateguryRep.ProductHaveCategury(PDto.Id, Categury.GroupId);
                            if (ProductHaveCategury)
                            {
                                PDto.CateguryName = Categury.GroupTitle;
                                PDto.CateguryLatinName = Categury.LatinGroupTitle;

                            }
                        }

                    }
                    var CountSale = await _orderDetailep.CountOfSaleByProductId(PDto.Id);
                    PDto.CountSale = CountSale;

                    var Type = await _productTypeRep.GetAsync(PDto.TypeId);
                    PDto.TypeName = Type.TypeName;



                    //var Pdto = _mapper.Map<ProductDto>(item);
                    PDto.ProductStockPriceId = item.Id;
                    //productDto.Add(Pdto);
                    productDto.Add(PDto);

                }
                result = productDto.AsQueryable();
            }






            response.CurrentPage = request.ProductParameterDto.CurrentPage;

            var CountOfResult = result.ToList();
            response.PageCount = CountOfResult.Count() / request.ProductParameterDto.TakePage;


            response.ProductDtos = result.ToArray();

            return response;

        }
    }


    public class ProductPagingSarachOrStoreQueryHandler : IRequestHandler<ProductPagingSarachOrStoreQueryReq, ProductPageingDto>
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

        public ProductPagingSarachOrStoreQueryHandler(IProductRepository productRep,
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
        public class MyObject
        {
            public string Name { get; set; }
            // دیگر فیلدها و خصوصیات
        }
        public async Task<ProductPageingDto> Handle(ProductPagingSarachOrStoreQueryReq request, CancellationToken cancellationToken)
        {

            ProductPageingDto response = new ProductPageingDto();
            var products = new List<ProductDto>();

            //سرچ محصول
            if (!string.IsNullOrEmpty(request.ProductParameterDto.Filter))
            {
                var query = $"SELECT dbo.Product.Name,dbo.Product.ParentId,dbo.Product.SingleSale, dbo.Product.LatinName, dbo.Product.Code, dbo.Product.ShortDescription, dbo.Product.ShortLink, dbo.Product.TypeId, dbo.ProductStockPrice.Id as ProductStockPriceId, dbo.ProductStockPrice.ProductId, dbo.ProductStockPrice.SalePrice,dbo.ProductStockPrice.DiscountPrice, dbo.ProductStockPrice.StockStatus, dbo.ProductStockPrice.Quantity, dbo.ProductStockPrice.MaximumSaleInOrder,dbo.Brand.BrandId,dbo.Brand.BrandTitle,dbo.Brand.LatinBrandTitle,dbo.UserStore.LatinStoreName, dbo.ProductStockPrice.StoreId, dbo.ProductStockPrice.RepId,dbo.UserStore.StoreName ,dbo.ProductCategury.CateguryId FROM dbo.Product INNER JOIN dbo.ProductStockPrice ON dbo.Product.Id = dbo.ProductStockPrice.ProductId INNER JOIN dbo.UserStore ON dbo.ProductStockPrice.StoreId = dbo.UserStore.Id INNER JOIN dbo.Brand ON dbo.Product.BrandId = dbo.Brand.BrandId INNER JOIN dbo.ProductCategury on dbo.Product.Id=dbo.ProductCategury.ProductId WHERE dbo.Product.TypeId!=3 AND dbo.Product.IsActive=1 ORDER BY CreateDate Desc";

                var list = DapperHelper.ExecuteCommand<List<ProductDto>>(_connectionString, conn => conn.Query<ProductDto>(query).ToList());



                var searched = list.Where(p =>
                    (p.Name.Contains(request.ProductParameterDto.Filter, StringComparison.InvariantCultureIgnoreCase) ||

                     (p.Code == request.ProductParameterDto.Filter)
                     )).ToList();

                foreach (var item in searched)
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
                    var pstockId = await _productStockRep.GetFirstProductStockPriceIdFromProductId(item.Id);
                    dto.ProductStockPriceId = pstockId;
                    //var pic = await _productGallleryRep.GetMainImageOfProduct(item.Id);
                    //dto.Image = pic.ImageName;
                    products.Add(dto);

                }



            }


            if (request.ProductParameterDto.Store != null)
            {
                var store = await _userStoreRep.GetStoreByLatinName(request.ProductParameterDto.Store);

                var query = $"SELECT dbo.Product.Name,dbo.Product.ParentId,dbo.Product.SingleSale, dbo.Product.LatinName, dbo.Product.Code, dbo.Product.ShortDescription, dbo.Product.ShortLink, dbo.Product.TypeId, dbo.ProductStockPrice.Id as ProductStockPriceId, dbo.ProductStockPrice.ProductId, dbo.ProductStockPrice.SalePrice,dbo.ProductStockPrice.DiscountPrice, dbo.ProductStockPrice.StockStatus, dbo.ProductStockPrice.Quantity, dbo.ProductStockPrice.MaximumSaleInOrder,dbo.Brand.BrandId,dbo.Brand.BrandTitle,dbo.Brand.LatinBrandTitle,dbo.UserStore.LatinStoreName, dbo.ProductStockPrice.StoreId, dbo.ProductStockPrice.RepId,dbo.UserStore.StoreName FROM dbo.Product INNER JOIN dbo.ProductStockPrice ON dbo.Product.Id = dbo.ProductStockPrice.ProductId INNER JOIN dbo.UserStore ON dbo.ProductStockPrice.StoreId = dbo.UserStore.Id INNER JOIN dbo.Brand ON dbo.Product.BrandId = dbo.Brand.BrandId where ProductStockPrice.StoreId={store.Id} AND dbo.Product.TypeId!=2 and dbo.Product.IsActive=1 ORDER BY CreateDate Desc OFFSET {request.ProductParameterDto.Skip} ROWS FETCH NEXT 24 ROWS ONLY";
                products = DapperHelper.ExecuteCommand<List<ProductDto>>(_connectionString, conn => conn.Query<ProductDto>(query).ToList());

            }
            //else
            //{
            //var query = "SELECT dbo.Product.Name,dbo.Product.ParentId,dbo.Product.SingleSale, dbo.Product.LatinName, dbo.Product.Code, dbo.Product.ShortDescription, dbo.Product.ShortLink, dbo.Product.TypeId, dbo.ProductStockPrice.Id as ProductStockPriceId, dbo.ProductStockPrice.ProductId, dbo.ProductStockPrice.SalePrice,dbo.ProductStockPrice.DiscountPrice, dbo.ProductStockPrice.StockStatus, dbo.ProductStockPrice.Quantity, dbo.ProductStockPrice.MaximumSaleInOrder,dbo.Brand.BrandId,dbo.Brand.BrandTitle,dbo.Brand.LatinBrandTitle,dbo.UserStore.LatinStoreName, dbo.ProductStockPrice.StoreId, dbo.ProductStockPrice.RepId,dbo.UserStore.StoreName FROM dbo.Product INNER JOIN dbo.ProductStockPrice ON dbo.Product.Id = dbo.ProductStockPrice.ProductId INNER JOIN dbo.UserStore ON dbo.ProductStockPrice.StoreId = dbo.UserStore.Id INNER JOIN dbo.Brand ON dbo.Product.BrandId = dbo.Brand.BrandId where dbo.Product.TypeId!=2 ORDER BY CreateDate Desc";

            //products = DapperHelper.ExecuteCommand<List<ProductDto>>(_connectionString, conn => conn.Query<ProductDto>(query).ToList());
            //}






            //تکراری ها
            #region Duplicate

            var duplicates = products
            .GroupBy(x => x.ProductId)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);

            foreach (var item in duplicates)
            {
                var p = products.Where(u => u.ProductId == item).ToList();
                foreach (var dub in p.Skip(1))
                {
                    products.Remove(dub);
                }

            }
            #endregion

            foreach (var product in products)
            {
                if (product.ProductId == 0)
                {
                    product.ProductId = product.Id;
                }
                else
                {
                    product.Id = product.ProductId;
                }

                if (product.TypeId == 5 && !product.SingleSale)
                {
                    continue;
                }
                else
                {
                    var id = 0;
                    if (product.TypeId == 3 || product.TypeId == 5)
                    {
                        id = product.ParentId.Value;
                    }
                    else
                    {
                        id = product.ProductId;
                    }
                    var image = DapperHelper.ExecuteCommand<Domain.Models.ProductGallery>(_connectionString, conn => conn.Query<Domain.Models.ProductGallery>(_productQueries.GetMainImage, new { @productId = id }).FirstOrDefault());
                    if (image != null)
                    {
                        product.Image = image.ImageName;
                    }

                    if (product.ProductStockPriceId == 0)
                    {
                        var pid = await _productStockRep.GetFirstProductStockPriceIdFromProductId(product.Id);
                        product.ProductStockPriceId = pid;
                    }
                }


                if (product.TypeId == 2)
                {
                    var variationquery = $"SELECT dbo.Product.Id,dbo.ProductStockPrice.SalePrice,dbo.ProductStockPrice.DiscountDate from dbo.Product inner join dbo.ProductStockPrice on dbo.Product.Id=dbo.ProductStockPrice.ProductId where dbo.Product.ParentId={product.Id}";

                    var variations = DapperHelper.ExecuteCommand<List<ProductDto>>(_connectionString, conn => conn.Query<ProductDto>(variationquery).ToList());
                    if (variations.Count > 0)
                    {
                        product.SalePrice = variations[0].SalePrice;
                        product.DiscountPrice = variations[0].DiscountPrice;
                    }

                }

            }

            IQueryable<ProductDto> result = products.AsQueryable();



            response.ProductDtos = result.ToArray();

            return response;

        }
    }



    public class ProductPagingCateguryQueryHandler : IRequestHandler<ProductPagingCateguryQueryReq, ProductPageingDto>
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
        private readonly SqlConnection _connection;

        public ProductPagingCateguryQueryHandler(IProductRepository productRep,
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
            _connection = new SqlConnection(configuration.GetConnectionString("DatabaseConnection"));
        }

        public async Task<ProductPageingDto> Handle(ProductPagingCateguryQueryReq request, CancellationToken cancellationToken)
        {
            //var parmeters = new DynamicParameters();
            //parmeters.Add("@CateguryId", 821);
            //var r= await _connection.ExecuteAsync("GetProductsByCateguryPagination", parmeters, commandType: CommandType.StoredProcedure);

            ////List<ProductDto> castResults = _connection.Query<ProductDto>(
            ////                   "GetProductsByCateguryPagination",
            ////commandType: CommandType.StoredProcedure).ToList();

            //var results = _connection.Query("GetProductsByCateguryPagination", parmeters, commandType: CommandType.StoredProcedure).ToList();
            //results.ForEach(r => Console.WriteLine($"{r.OrderID} {r.Subtotal}"));


            ProductPageingDto response = new ProductPageingDto();
            var products = new List<ProductDto>();
            var categury = await _categuryRep.GetCateguryByLatinName(request.ProductParameterDto.Categury);
            var storeQuery = "";
            var minQuery = "";
            var maxQuery = "";
            var existQuery = "";
            var orderBy = "";
            var exist = "";
            var minPrice = "";
            var maxPrice = "";
            if (request.ProductParameterDto.Store != null)
            {
                var store = await _userStoreRep.GetStoreByLatinName(request.ProductParameterDto.Store);
                storeQuery = $"AND StoreId={store.Id}";
            }
            if (request.ProductParameterDto.Exist)
            {
                existQuery = "and  COALESCE(bp.TotalQuantity, sp.Quantity)>0";
            }
            if (request.ProductParameterDto.MinPrice != 0)
            {
                minQuery = $"and COALESCE(bp.BestSalePrice, sp.SalePrice)>={request.ProductParameterDto.MinPrice}";
            }
            if (request.ProductParameterDto.MaxPrice != 0)
            {
                maxQuery = $"and COALESCE(bp.BestSalePrice, sp.SalePrice)<={request.ProductParameterDto.MaxPrice}";
            }



            //if (request.ProductParameterDto.MinPrice > 0)
            //{
            //    minPrice = $"And dbo.ProductStockPrice.SalePrice>={request.ProductParameterDto.MinPrice}";
            //}
            //if (request.ProductParameterDto.MaxPrice > 0)
            //{
            //    maxPrice = $"And dbo.ProductStockPrice.SalePrice<={request.ProductParameterDto.MaxPrice}";
            //}

            switch (request.ProductParameterDto.Type)
            {
                case "Top":
                    orderBy = "ORDER BY FinalQuantity DESC, FinalPrice DESC ";
                    break;
                case "New":
                    orderBy = "ORDER BY p.CreateDate Desc";
                    break;
                case "LowPrice":
                    orderBy = "ORDER BY FinalQuantity DESC, FinalPrice ASC ";
                    break;
                case "HighPrice":
                    orderBy = "ORDER BY FinalQuantity DESC, FinalPrice DESC ";
                    break;
                default:
                    orderBy = "ORDER BY dbo.ProductStockPrice.SalePrice ASC";
                    break;
            }

            var query = $"WITH BestPrices AS ( SELECT p2.Id AS ParentProductId, MIN(sp3.SalePrice) AS BestSalePrice, MIN(sp3.DiscountPrice) AS BestDiscountPrice, SUM(sp3.Quantity) AS TotalQuantity FROM dbo.Product p2 INNER JOIN dbo.Product p3 ON p3.ParentId = p2.Id AND p3.TypeId = 3 INNER JOIN dbo.ProductStockPrice sp3 ON sp3.ProductId = p3.Id WHERE sp3.Quantity > 0 GROUP BY p2.Id ) SELECT p.Id AS ProductId, p.ShortLink , p.Name , p.ParentId AS ParentProductId, p.TypeId , sp.Id AS ProductStockPriceId, sp.DiscountPrice, p.CreateDate, COALESCE(bp.BestSalePrice, sp.SalePrice) AS FinalPrice, COALESCE(bp.BestDiscountPrice, sp.DiscountPrice) AS FinalDiscountPrice, COALESCE(bp.TotalQuantity, sp.Quantity) AS FinalQuantity, us.StoreName, us.Id AS StoreId FROM dbo.Product p INNER JOIN dbo.ProductStockPrice sp ON sp.ProductId = p.Id INNER JOIN dbo.UserStore us ON sp.StoreId = us.Id LEFT JOIN BestPrices bp ON p.Id = bp.ParentProductId WHERE p.IsActive = 1 AND (p.TypeId = 1 OR p.TypeId = 2 or p.TypeId=5) AND EXISTS (SELECT 1 FROM dbo.ProductCategury WHERE ProductId = p.Id AND CateguryId = {categury.GroupId}) {storeQuery} {existQuery} {minQuery} {maxQuery} {orderBy} OFFSET {request.ProductParameterDto.Skip} ROWS FETCH NEXT 24 ROWS ONLY;";
            //var query = $"WITH BestPrices AS ( SELECT p2.Id AS ParentProductId, MIN(sp3.SalePrice) AS BestSalePrice, SUM(sp3.Quantity) AS TotalQuantity FROM dbo.Product p2 INNER JOIN dbo.Product p3 ON p3.ParentId = p2.Id AND p3.TypeId = 3 INNER JOIN dbo.ProductStockPrice sp3 ON sp3.ProductId = p3.Id WHERE sp3.Quantity > 0 GROUP BY p2.Id ) SELECT p.Id AS ProductId, p.ShortLink , p.Name , p.ParentId AS ParentProductId, p.TypeId , sp.Id AS ProductStockPriceId, sp.DiscountPrice, p.CreateDate, COALESCE(bp.BestSalePrice, sp.SalePrice) AS FinalPrice, COALESCE(bp.TotalQuantity, sp.Quantity) AS FinalQuantity, us.StoreName, us.Id AS StoreId FROM dbo.Product p INNER JOIN dbo.ProductStockPrice sp ON sp.ProductId = p.Id INNER JOIN dbo.UserStore us ON sp.StoreId = us.Id LEFT JOIN BestPrices bp ON p.Id = bp.ParentProductId WHERE p.IsActive = 1 AND (p.TypeId = 1 OR p.TypeId = 2 or p.TypeId=5) AND EXISTS (SELECT 1 FROM dbo.ProductCategury WHERE ProductId = p.Id AND CateguryId = {categury.GroupId}) {storeQuery} {existQuery} {minQuery} {maxQuery} {orderBy} OFFSET {request.ProductParameterDto.Skip} ROWS FETCH NEXT 24 ROWS ONLY;";
            //var query = $"SELECT dbo.Product.Name,dbo.Product.ParentId,dbo.Product.SingleSale, dbo.Product.LatinName, dbo.Product.Code, dbo.Product.ShortDescription, dbo.Product.ShortLink, dbo.Product.TypeId, dbo.ProductStockPrice.Id as ProductStockPriceId, dbo.ProductStockPrice.ProductId, dbo.ProductStockPrice.SalePrice,dbo.ProductStockPrice.DiscountPrice, dbo.ProductStockPrice.StockStatus, dbo.ProductStockPrice.Quantity, dbo.ProductStockPrice.MaximumSaleInOrder,dbo.Brand.BrandId,dbo.Brand.BrandTitle,dbo.Brand.LatinBrandTitle,dbo.UserStore.LatinStoreName, dbo.ProductStockPrice.StoreId, dbo.ProductStockPrice.RepId,dbo.UserStore.StoreName ,dbo.ProductCategury.CateguryId FROM dbo.Product INNER JOIN dbo.ProductStockPrice ON dbo.Product.Id = dbo.ProductStockPrice.ProductId INNER JOIN dbo.UserStore ON dbo.ProductStockPrice.StoreId = dbo.UserStore.Id INNER JOIN dbo.Brand ON dbo.Product.BrandId = dbo.Brand.BrandId INNER JOIN dbo.ProductCategury on dbo.Product.Id=dbo.ProductCategury.ProductId WHERE EXISTS (SELECT Id FROM Product WHERE Id = ProductCategury.ProductId AND ProductCategury.CateguryId={categury.GroupId})AND dbo.Product.TypeId!=3 AND dbo.Product.IsActive=1 {storeQuery} {existQuery} {minQuery} {maxQuery} {exist} {minPrice}{maxPrice} {orderBy} OFFSET {request.ProductParameterDto.Skip} ROWS FETCH NEXT 24 ROWS ONLY;";

            var productslist = DapperHelper.ExecuteCommand<List<ProductPaginationCateguryDto>>(_connectionString, conn => conn.Query<ProductPaginationCateguryDto>(query).ToList());
            
            //تکراری ها
            #region Duplicate

            var duplicates = productslist
            .GroupBy(x => x.ProductId)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);

            foreach (var item in duplicates)
            {
                var p = productslist.Where(u => u.ProductId == item).ToList();
                var pid = p[0].ProductStockPriceId;
                var price = p[0].FinalPrice;
                foreach (var pd in p)
                {
                    if (pd.FinalPrice < price)
                    {
                        price = pd.FinalPrice;
                        pid = pd.ProductStockPriceId;
                    }
                }
                var idP = p.Min(item => item.FinalPrice);
                productslist.RemoveAll(p => p.ProductId == item && p.ProductStockPriceId != pid);
            }
            #endregion
            foreach (var product in productslist)
            {
                var image = DapperHelper.ExecuteCommand<Domain.Models.ProductGallery>(_connectionString, conn => conn.Query<Domain.Models.ProductGallery>(_productQueries.GetMainImage, new { @productId = product.ProductId }).FirstOrDefault());
                if (image != null)
                {
                    product.Image = image.ImageName;
                }

                if (product.TypeId == 2)
                {
                    var variationquery = $"SELECT dbo.Product.Id,dbo.ProductStockPrice.Id as ProductStockPriceId ,dbo.ProductStockPrice.SalePrice,dbo.ProductStockPrice.DiscountDate from dbo.Product inner join dbo.ProductStockPrice on dbo.Product.Id=dbo.ProductStockPrice.ProductId where dbo.Product.ParentId={product.ProductId} ORDER BY dbo.ProductStockPrice.Quantity Desc";

                    var variations = DapperHelper.ExecuteCommand<List<ProductPaginationCateguryDto>>(_connectionString, conn => conn.Query<ProductPaginationCateguryDto>(variationquery).ToList());

                    if (variations.Count > 0)
                    {
                        var bestItem = variations.Where(u => u.Quantity > 0).OrderBy(u => u.SalePrice).FirstOrDefault();
                        if (bestItem != null)
                        {

                            product.ProductStockPriceId = bestItem.ProductStockPriceId;
                        }
                        else
                        {

                            product.ProductStockPriceId = variations[0].ProductStockPriceId;
                        }
                    }

                }
            }



            //foreach (var product in products)
            //{
            //    if (product.ProductId == 0)
            //    {
            //        product.ProductId = product.Id;
            //    }
            //    else
            //    {
            //        product.Id = product.ProductId;
            //    }

            //    if (product.TypeId == 5 && !product.SingleSale)
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        var id = 0;
            //        if (product.TypeId == 3 || product.TypeId == 5)
            //        {
            //            id = product.ParentId.Value;
            //        }
            //        else
            //        {
            //            id = product.ProductId;
            //        }
            //        var image = DapperHelper.ExecuteCommand<Domain.Models.ProductGallery>(_connectionString, conn => conn.Query<Domain.Models.ProductGallery>(_productQueries.GetMainImage, new { @productId = id }).FirstOrDefault());
            //        if (image != null)
            //        {
            //            product.Image = image.ImageName;
            //        }

            //        if (product.ProductStockPriceId == 0)
            //        {
            //            var pid = await _productStockRep.GetFirstProductStockPriceIdFromProductId(product.Id);
            //            product.ProductStockPriceId = pid;
            //        }
            //    }




            //}

            //IQueryable<ProductDto> result = products.AsQueryable();



            response.ProductDtos = productslist.ToArray();

            return response;

        }
    }

}

