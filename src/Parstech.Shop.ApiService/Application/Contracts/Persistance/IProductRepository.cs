﻿using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<List<Product>> GetAllParentBundleProduct(string filter);
    Task<List<Product>> GetAllParentVariableProduct(string filter);
    Task<List<Product>> GetSomeOfProductByCategury(int take, int categuryId);
    Task<Product?> GetProductByShortLink(string shortLink);
    Task<List<Product>> GetProductsByParrentId(int parrentId);
    Task<List<Product>> SearchProducts(string Filter, int take);
    Task<bool> ProductShortLinkExist(string shortLink);
    Task<Product> GetProductByCode(string code);
    Task<Product> GetProductByVosit(int visit);
    Task<bool> IsChild(int productId);
    Task<Product?> DapperGetProductById(int id);
    Task<ProductDto> ConvertProduct(ProductStockPrice product);
    Task<ProductListShowDto> ConvertProductForShow(ProductStockPrice product);
    Task<List<DapperProductDto>> DapperGetProductsByPage(int skip, int take);
    Task<Product> GetProductsByName(string name);

    Task RefreshBestStockProduct(int productId);
    Task RefreshAllBestStockProduct();

    Task<List<ProductDto>> GetProductsPaging(string JoinType,
        string search,
        string categury,
        string condition,
        string orderBy,
        string paging);
}