using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Reports;
using Parstech.Shop.Context.Persistence.Context;
using Parstech.Shop.Context.Application.Convertor;
using Parstech.Shop.Context.Application.Dapper.Product.Queries;
using Microsoft.Extensions.Configuration;
using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.DTOs.ProductStockPrice;
using Dapper;

using Parstech.Shop.Context.Application.Dapper.OrderDetail.Queries;
using Parstech.Shop.Context.Application.DTOs.OrderDetail;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class ReporstRepository : GenericRepository<IndexCountsDto>, IReporstRepository
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;
    private readonly IProductQueries _productQueries;
    private readonly IOrderDetailQueries _orderDetailQueries;
    private readonly string _connectionString;
    public ReporstRepository(DatabaseContext context,
        IMapper mapper,
        IProductQueries productQueries,
        IConfiguration configuration,
        IOrderDetailQueries orderDetailQueries) : base(context)
    {
        _context = context;
        _mapper = mapper;
        _productQueries = productQueries;
        _orderDetailQueries= orderDetailQueries;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public async Task<IndexCountsDto> GetIndexCounts()
    {
        IndexCountsDto indexCountsDto = new();
        indexCountsDto.RepresentationsProductsForChart = new();
        indexCountsDto.RepresentationsProductsForMap = new();

        indexCountsDto.Time = $"{DateTime.Now.ToShortTimeString()}-{DateTime.Now.ToShamsi()}";
        indexCountsDto.UserCount = _context.Users.Count();
        indexCountsDto.ProductCount = _context.Products.Count();
        indexCountsDto.IsLoadOrderCount = _context.OrderStatuses.Where(a => a.StatusId == 1).Count();
        indexCountsDto.AllTransactionsCount = _context.WalletTransactions.Count();
        indexCountsDto.CoinTransactionsCount = _context.WalletTransactions.Where(a => a.Type == "Coin").Count();
        indexCountsDto.WalletTransactionsCount = _context.WalletTransactions.Where(a => a.Type == "Amount").Count();
        indexCountsDto.FacilitiesTransactionsCount = _context.WalletTransactions.Where(a => a.Type == "Fecilities").Count();
        indexCountsDto.PishFactorCount = _context.Orders.Where(a => a.IsFinaly == true && a.ConfirmPayment == false).Count();
        indexCountsDto.SouratHesabCount = _context.Orders.Where(a => a.IsFinaly == true && a.ConfirmPayment == true).Count();
        var representations = await _context.Representations.ToListAsync();
            
        foreach (var item in representations)
        {
            RepresentationsProducts representationsProducts = new();
            RepresentationsSells representationsSells = new();
            representationsSells.RepresentationSells = 0;
            representationsProducts.RepresentationProducts = 0;

            var products = DapperHelper.ExecuteCommand<List<ProductStockPriceDto>>(_connectionString, conn => conn.Query<ProductStockPriceDto>(_productQueries.GetProductStockPriceByRepId, new { @repId = item.Id }).ToList());

                
            //var products = await _context.ProductStockPrices.Where(z => z.RepId == item.Id).ToListAsync();
            foreach (var x in products)
            {
                representationsProducts.RepresentationProducts += x.Quantity;
                //var orderDetails = await _context.OrderDetails.Where(z => z.ProductStockPriceId == x.Id).ToListAsync();
                var orderDetails = DapperHelper.ExecuteCommand<List<OrderDetailDto>>(_connectionString, conn => conn.Query<OrderDetailDto>(_orderDetailQueries.GetOrderDetailOfProductStockPriceId, new { @productId = x.Id }).ToList());

                foreach (var y in orderDetails)
                {
                    representationsSells.RepresentationSells += y.Total;
                }
            }
            representationsProducts.RepresentationName = item.Name;
            representationsSells.RepresentationName = item.Name;

            var State = await _context.States.FindAsync(item.StateId);
                
            representationsSells.latitude = State.Latitude.Value;
            representationsSells.longitude = State.Longitude.Value;
               
            indexCountsDto.RepresentationsProductsForChart.Add(representationsProducts);
            indexCountsDto.RepresentationsProductsForMap.Add(representationsSells);
               
        }
        return indexCountsDto;
    }
}