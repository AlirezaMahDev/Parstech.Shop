﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using FluentValidation.Validators;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shop.Application.Calculator;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Convertor;
using Shop.Application.Dapper.Helper;
using Shop.Application.Dapper.Product.Commands;
using Shop.Application.Dapper.Product.Queries;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductStockPrice;
using Shop.Application.DTOs.Property;
using Shop.Application.DTOs.User;
using Shop.Domain.Models;
using Shop.Persistence.Context;
using static Shop.Persistence.Repositories.ProductRepository;

namespace Shop.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DatabaseContext _context;
        private readonly IBrandRepository _brandRep;
        private readonly ICateguryRepository _categuryRep;
        private readonly IProductGallleryRepository _GalleryRep;
        private readonly IProductCateguryRepository _prodyctCateguryRep;
        private readonly IUserStoreRepository _userStoreRep;
        private readonly IProductTypeRepository _productTypeRep;
        private readonly IProductStockPriceRepository _productStockRep;
        private readonly IMapper _mapper;

        private readonly IProductCommand _commandText;
        private readonly IProductQueries _queriesText;
        private readonly string _connectionString;

        public ProductRepository(DatabaseContext context,
            IBrandRepository brandRep,
            IProductGallleryRepository galleryRep,
            IUserStoreRepository userStoreRep,
            IProductTypeRepository productTypeRep,
            IProductCateguryRepository prodyctCateguryRep,
            ICateguryRepository categuryRep,
            IMapper mapper,
            IConfiguration configuration,
            IProductCommand commandText,
            IProductQueries queriesText,
            IProductStockPriceRepository productStockRep) : base(context)
        {

            _context = context;
            _brandRep = brandRep;
            _GalleryRep = galleryRep;
            _userStoreRep = userStoreRep;
            _productTypeRep = productTypeRep;
            _prodyctCateguryRep = prodyctCateguryRep;
            _categuryRep = categuryRep;
            _mapper = mapper;

            _commandText = commandText;
            _queriesText = queriesText;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
            _productStockRep = productStockRep;
        }




        public async Task<List<Product>> GetAllParentBundleProduct(string filter)
        {
            var products = await _context.Products.Where(a => a.TypeId == 4).ToListAsync();
            if (!string.IsNullOrEmpty(filter))
            {
                products = (List<Product>)products.Where(p =>
                    (p.Name.Contains(filter) ||
                     (p.Code.Contains(filter))));
            }
            return products;
        }

        public async Task<List<Product>> GetAllParentVariableProduct(string filter)
        {
            var products = await _context.Products.Where(a => a.TypeId == 2).ToListAsync();
            if (products.Count > 0 && !string.IsNullOrEmpty(filter))
            {
                products = products.Where(p =>
                    (p.Name.Contains(filter) ||
                     p.Code.Contains(filter))).ToList();
            }
            return products;
        }

        public async Task<Product?> GetProductByShortLink(string shortLink)
        {
            var item = await _context.Products.FirstOrDefaultAsync(u => u.ShortLink == shortLink);
            return item;
        }

        public async Task<List<Product>> GetProductsByParrentId(int parrentId)
        {
            var list = await _context.Products.Where(u => u.ParentId == parrentId).ToListAsync();
            return list;
        }

        public async Task<List<Product>> GetSomeOfProductByCategury(int take, int categuryId)
        {
            var result = new List<Product>();
            var productCateguries = await _prodyctCateguryRep.GetProductCateguriesByCateguryId(categuryId);

            foreach (var item in productCateguries)
            {
                var product = await GetAsync(item.ProductId);


                result.Add(product);
            }
            return result.OrderByDescending(u => u.CreateDate).Take(take).ToList();
        }

        public async Task<List<Product>> SearchProducts(string Filter, int take)
        {
            IQueryable<Product> Result = _context.Products;
            if (Filter != null && Filter != "")
            {
                Result = Result.Where(u =>
                u.Name.Contains(Filter, StringComparison.InvariantCultureIgnoreCase) ||
                u.Code.Contains(Filter));
            }
            return await Result.Where(u => u.TypeId != 2 && u.TypeId != 5 && u.IsActive).OrderByDescending(u => u.CreateDate).Take(take).ToListAsync();
        }
        public async Task<bool> ProductShortLinkExist(string shortLink)
        {
            var check = await _context.Products.AnyAsync(z => z.ShortLink == shortLink);
            if (check)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<Product> GetProductByCode(string code)
        {
            Product result = new Product();
            if (await _context.Products.AnyAsync(u => u.Code == code))
            {
                result = await _context.Products.FirstOrDefaultAsync(u => u.Code == code);

            }
            else
            {
                result.Id = 0;

            }
            return result;
        }
        public async Task<Product> GetProductsByName(string name)
        {
            var result = new Product();
            result = await _context.Products.FirstOrDefaultAsync(u => u.Name == name);

            return result;
        }

        public async Task<Product> GetProductByVosit(int visit)
        {
            Product result = new Product();
            if (await _context.Products.AnyAsync(u => u.Visit == visit))
            {
                result = await _context.Products.FirstOrDefaultAsync(u => u.Visit == visit);

            }
            else
            {
                result.Id = 0;

            }
            return result;
        }

        public async Task<bool> IsChild(int productId)
        {
            var item = await GetAsync(productId);
            if (item.ParentId != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<Product?> DapperGetProductById(int id)
        {
            var query = DapperHelper.ExecuteCommand<Product>(_connectionString, conn => conn.Query<Product>(_commandText.GetProductById, new { @Id = id }).SingleOrDefault());

            return query;
        }
        public async Task<ProductDto> ConvertProduct(ProductStockPrice product)
        {
            var productItem = await GetAsync(product.ProductId);
            var pdto = _mapper.Map<ProductDto>(productItem);
            pdto.ProductStockPriceId = product.Id;

            pdto.Price = product.Price;
            pdto.SalePrice = product.SalePrice;
            pdto.DiscountPrice = product.DiscountPrice;
            pdto.BasePrice = product.BasePrice;
            pdto.StockStatus = product.StockStatus;
            pdto.Quantity = product.Quantity;
            pdto.MaximumSaleInOrder = product.MaximumSaleInOrder;
            pdto.StoreId = product.StoreId;
            pdto.RepId = product.RepId;
            pdto.TaxId = product.TaxId;
            return pdto;
        }
        public async Task<ProductListShowDto> ConvertProductForShow(ProductStockPrice product)
        {
            var productItem = await GetAsync(product.ProductId);
            var pdto = _mapper.Map<ProductListShowDto>(productItem);
            pdto.ProductStockPriceId = product.Id;


            pdto.SalePrice = product.SalePrice;
            pdto.DiscountPrice = product.DiscountPrice;
            pdto.Quantity = product.Quantity;

            return pdto;
        }
        public async Task<List<DapperProductDto>> DapperGetProductsByPage(int skip, int take)
        {

            //string sql = "SELECT Product.Name, Product.LatinName, Product.Code, Product.ShortDescription, Product.ShortLink, Product.TypeId, ProductStockPrice.Id, ProductStockPrice.ProductId, ProductStockPrice.SalePrice,ProductStockPrice.DiscountPrice, ProductStockPrice.StockStatus, ProductStockPrice.Quantity, ProductStockPrice.MaximumSaleInOrder, ProductStockPrice.StoreId, ProductStockPrice.RepId FROM Product INNER JOIN ProductStockPrice ON Product.Id = ProductStockPrice.ProductId WHERE TypeId!=3 ORDER BY Id Desc OFFSET 0 ROWS FETCH NEXT 30 ROWS ONLY;";
            //var connecttion = new SqlConnection(_connectionString);
            //var list = connecttion.Query<DapperProductDto>(sql);
            //var result=list.ToList();
            var query = DapperHelper.ExecuteCommand<List<DapperProductDto>>(_connectionString, conn => conn.Query<DapperProductDto>(_queriesText.GetListByGroup).ToList());

            return query;
        }

        

        public async Task RefreshBestStockProduct(int productId)
        {
            
            var item = await GetAsync(productId);
            item.BestStockId = await _productStockRep.GetBestStockId(item.Id);
            item.BestStockUserCateguryId = await _productStockRep.GetBestStockUserCateguryId(item.Id);
            _context.Products.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task RefreshAllBestStockProduct()
        {
            var list = await GetAll();
            foreach (var item in list)
            {
                item.BestStockId = await _productStockRep.GetBestStockId(item.Id);
                item.BestStockUserCateguryId = await _productStockRep.GetBestStockUserCateguryId(item.Id);
                _context.Products.Update(item);
            }
            //var item = await GetAsync(productId);

            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductDto>> GetProductsPaging(string JoinType,string search,string categury, string condition, string orderBy, string paging)
        {
            var query = $"SELECT p.Id AS ProductId,p.ShortLink ,p.Name ,p.ParentId AS ParentProductId,p.TypeId ,sp.Id AS ProductStockPriceId,sp.DiscountPrice,p.CreateDate,sp.SalePrice,sp.DiscountPrice,sp.Quantity,sp.CateguryOfUserId,sp.CateguryOfUserType,us.StoreName,us.Id AS StoreId,p.BestStockId,p.BestStockUserCateguryId FROM dbo.Product p LEFT JOIN dbo.ProductStockPrice sp ON {JoinType} = sp.Id LEFT JOIN dbo.UserStore us ON sp.StoreId = us.Id WHERE sp.Id IS NOT NULL AND {condition} {categury} {search} {orderBy} {paging}";
            var list = DapperHelper.ExecuteCommand<List<ProductDto>>(_connectionString, conn => conn.Query<ProductDto>(query).ToList());
            return list;
        }

       
    }
}
