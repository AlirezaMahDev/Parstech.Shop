using Dapper;

using MediatR;

using Microsoft.Extensions.Configuration;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.Dapper.Product.Queries;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.DTOs.ProductStockPrice;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Product.Handlers.Queries;

public class
    GetChildsAndProductStocksQueryHandler : IRequestHandler<GetChildsAndProductStocksQueryReq, ChildsAndStock>
{
    private readonly IProductQueries _productQueries;
    private readonly IProductRepository _productRep;
    private readonly IUserStoreRepository _userStoreRep;
    private readonly IRepresentationRepository _representationRep;
    private string _connectionString;

    public GetChildsAndProductStocksQueryHandler(IConfiguration configuration,
        IProductQueries productQueries,
        IRepresentationRepository representationRep,
        IUserStoreRepository userStoreRep,
        IProductRepository productRep)
    {
        _productQueries = productQueries;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
        _representationRep = representationRep;
        _userStoreRep = userStoreRep;
        _productRep = productRep;
    }

    public async Task<ChildsAndStock> Handle(GetChildsAndProductStocksQueryReq request,
        CancellationToken cancellationToken)
    {
        ChildsAndStock result = new();
        var productQuery = $"select * from Product where ParentId={request.productId}";
        result.ProductDtos = DapperHelper.ExecuteCommand<List<ProductDto>>(_connectionString,conn => conn.Query<ProductDto>(productQuery).ToList());

        result.ProductStockDtos = new();
        var productStockQuery = $"select* from ProductStockPrice where ProductId={request.productId} ";
        var ps = DapperHelper.ExecuteCommand<List<ProductStockPriceDto>>(_connectionString,conn => conn.Query<ProductStockPriceDto>(productStockQuery).ToList());
        foreach (var item in ps)
        {
            var store = await _userStoreRep.GetAsync(item.StoreId);

            if (request.storeId == 0 || request.storeId == store.Id)
            {
            }
            else
            {
                continue;
            }

            var rep = await _representationRep.GetAsync(item.RepId);
            var product = await _productRep.GetAsync(item.ProductId);
            item.StoreName = store.StoreName;
            item.RepName = rep.Name;
            item.ProductName = product.Name;
            item.TypeId = product.TypeId;
            result.ProductStockDtos.Add(item);
        }


        foreach (var item in result.ProductDtos)
        {
            var productStockQuery2 = $"select* from ProductStockPrice where ProductId={item.Id} ";
            var productStocks = DapperHelper.ExecuteCommand<List<ProductStockPriceDto>>(_connectionString, conn => conn.Query<ProductStockPriceDto>(productStockQuery2).ToList());


            if (productStocks.Count > 0)
            {
                foreach (var productStock in productStocks)
                {
                    var store = await _userStoreRep.GetAsync(productStock.StoreId);

                    if (request.storeId == 0 || request.storeId == store.Id)
                    {
                    }
                    else
                    {
                        continue;
                    }

                    var rep = await _representationRep.GetAsync(productStock.RepId);
                    var product = await _productRep.GetAsync(productStock.ProductId);
                    productStock.StoreName = store.StoreName;
                    productStock.RepName = rep.Name;
                    productStock.ProductName = product.Name;
                    productStock.TypeId = product.TypeId;
                    result.ProductStockDtos.Add(productStock);
                }
            }
        }

        return result;
    }
}