using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Dapper.Product.Queries;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

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
        string productQuery = $"select * from Product where ParentId={request.productId}";
        result.ProductDtos =
            DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query<ProductDto>(productQuery).ToList());

        result.ProductStockDtos = new();
        string productStockQuery = $"select* from ProductStockPrice where ProductId={request.productId} ";
        List<ProductStockPriceDto> ps = DapperHelper.ExecuteCommand(_connectionString,
            conn => conn.Query<ProductStockPriceDto>(productStockQuery).ToList());
        foreach (ProductStockPriceDto item in ps)
        {
            Domain.Models.UserStore? store = await _userStoreRep.GetAsync(item.StoreId);

            if (request.storeId == 0 || request.storeId == store.Id)
            {
            }
            else
            {
                continue;
            }

            Domain.Models.Representation? rep = await _representationRep.GetAsync(item.RepId);
            Domain.Models.Product? product = await _productRep.GetAsync(item.ProductId);
            item.StoreName = store.StoreName;
            item.RepName = rep.Name;
            item.ProductName = product.Name;
            item.TypeId = product.TypeId;
            result.ProductStockDtos.Add(item);
        }


        foreach (ProductDto item in result.ProductDtos)
        {
            string productStockQuery2 = $"select* from ProductStockPrice where ProductId={item.Id} ";
            List<ProductStockPriceDto> productStocks = DapperHelper.ExecuteCommand(_connectionString,
                conn => conn.Query<ProductStockPriceDto>(productStockQuery2).ToList());


            if (productStocks.Count > 0)
            {
                foreach (ProductStockPriceDto productStock in productStocks)
                {
                    Domain.Models.UserStore? store = await _userStoreRep.GetAsync(productStock.StoreId);

                    if (request.storeId == 0 || request.storeId == store.Id)
                    {
                    }
                    else
                    {
                        continue;
                    }

                    Domain.Models.Representation? rep = await _representationRep.GetAsync(productStock.RepId);
                    Domain.Models.Product? product = await _productRep.GetAsync(productStock.ProductId);
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