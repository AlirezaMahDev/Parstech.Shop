using Dapper;

using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Dapper.ProductStockPrice.Commands;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class ProductStockPriceRepository : GenericRepository<ProductStockPrice>, IProductStockPriceRepository
{
    private readonly DatabaseContext _context;

    private readonly IProductStockPriceCommand _commandText;
    private readonly string _connectionString;

    public ProductStockPriceRepository(DatabaseContext context,
        IConfiguration configuration,
        IProductStockPriceCommand commandText) : base(context)
    {
        _context = context;
        _commandText = commandText;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public async Task<int> GetFirstProductStockPriceIdFromProductId(int productId)
    {
        if (await _context.ProductStockPrices.AnyAsync(u => u.ProductId == productId))
        {
            ProductStockPrice? pr = await _context.ProductStockPrices.FirstOrDefaultAsync(u =>
                u.ProductId == productId && u.SalePrice > 0 && u.Quantity > 0);
            if (pr == null)
            {
                pr = await _context.ProductStockPrices.FirstOrDefaultAsync(u => u.ProductId == productId);
            }

            return pr.Id;
        }
        else
        {
            return 0;
        }
    }

    public async Task<List<ProductStockPrice>> GetAllByProductId(int productId)
    {
        List<ProductStockPrice> result = new();
        result = await _context.ProductStockPrices.Where(u => u.ProductId == productId).ToListAsync();
        return result;
    }

    public async Task<ProductStockPrice?> DapperGetProductStockPriceById(int id)
    {
        ProductStockPrice? query = DapperHelper.ExecuteCommand<ProductStockPrice>(_connectionString,
            conn => conn.Query<ProductStockPrice>(_commandText.GetProductStockPriceById, new { @Id = id })
                .SingleOrDefault());

        return query;
    }

    public async Task<ProductStockPrice?> GetProductStockByProductIdAndStoreId(int productId, int storeId)
    {
        ProductStockPrice? productStockPrice =
            await _context.ProductStockPrices.FirstOrDefaultAsync(u =>
                u.ProductId == productId && u.StoreId == storeId);
        return productStockPrice;
    }

    public async Task<bool> ExistStockForProductIdAndStore(int productId, int storeId)
    {
        if (await _context.ProductStockPrices.AnyAsync(u => u.ProductId == productId && u.StoreId == storeId))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> ExistStockForParentProduct(int productId)
    {
        Product? product = await _context.Products.FindAsync(productId);

        if (product.ParentId == null)
        {
            return true;
        }

        if (await _context.ProductStockPrices.AnyAsync(u => u.ProductId == product.ParentId))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<List<ProductStockPriceDto>> GetAllStock(int productId, bool UserCateguryExist)
    {
        List<ProductStockPriceDto> Result = new();
        //var product =await _productRep.GetAsync(productId);
        string mainProductQuery = $"select* from Product where Id={productId} ";
        ProductDto product = DapperHelper.ExecuteCommand<ProductDto>(_connectionString,
            conn => conn.Query<ProductDto>(mainProductQuery).FirstOrDefault());


        string UserCtaeguryCondition = "";
        if (!UserCateguryExist && product.TypeId != 1)
        {
            UserCtaeguryCondition = "AND CateguryOfUserId IS NULL";
        }

        string productStockQuery =
            $"select* from ProductStockPrice where ProductId={productId} {UserCtaeguryCondition}";
        List<ProductStockPriceDto> prodcuctStocks = DapperHelper.ExecuteCommand(_connectionString,
            conn => conn.Query<ProductStockPriceDto>(productStockQuery).ToList());
        Result.AddRange(prodcuctStocks);

        string productQuery = $"select * from Product where ParentId={productId} ";
        List<ProductDto> childs =
            DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query<ProductDto>(productQuery).ToList());
        foreach (ProductDto item in childs)
        {
            string ChildStockQuery =
                $"select* from ProductStockPrice where ProductId={item.Id} {UserCtaeguryCondition}";
            List<ProductStockPriceDto> ChildStocks = DapperHelper.ExecuteCommand(_connectionString,
                conn => conn.Query<ProductStockPriceDto>(ChildStockQuery).ToList());
            Result.AddRange(ChildStocks);
        }

        foreach (ProductStockPriceDto item in Result)
        {
            if (item.DiscountPrice > 0)
            {
                item.BestPriceForBestStock = item.DiscountPrice;
            }
            else
            {
                item.BestPriceForBestStock = item.SalePrice;
            }
        }

        return Result;
    }

    public async Task<int?> GetBestStockId(int productId)
    {
        List<ProductStockPriceDto>? stocks = await GetAllStock(productId, false);
        List<ProductStockPriceDto>? Catstocks = await GetAllStock(productId, true);
        if (stocks.Count == 0 || stocks == null)
        {
            return null;
        }

        ProductStockPriceDto? stock = stocks.Where(u => u.Quantity > 0 && u.SalePrice > 0)
            .OrderBy(u => u.BestPriceForBestStock)
            .FirstOrDefault();
        if (stock != null)
        {
            return stock.Id;
        }
        else
        {
            ProductStockPriceDto? notExistStock = stocks.FirstOrDefault();
            if (notExistStock != null)
            {
                return notExistStock.Id;
            }
        }

        return null;
    }

    public async Task<int?> GetBestStockUserCateguryId(int productId)
    {
        List<ProductStockPriceDto>? stocks = await GetAllStock(productId, true);
        if (stocks.Count == 0 || stocks == null)
        {
            return null;
        }

        ProductStockPriceDto? stock = stocks.Where(u => u.Quantity > 0 && u.SalePrice > 0)
            .OrderBy(u => u.BestPriceForBestStock)
            .FirstOrDefault();
        if (stock != null)
        {
            return stock.Id;
        }
        else
        {
            ProductStockPriceDto? notExistStock = stocks.FirstOrDefault();
            if (notExistStock != null)
            {
                return notExistStock.Id;
            }
        }

        return null;
    }
}