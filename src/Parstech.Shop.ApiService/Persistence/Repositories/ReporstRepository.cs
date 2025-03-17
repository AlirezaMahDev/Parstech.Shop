using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Dapper;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Dapper.OrderDetail.Queries;
using Parstech.Shop.ApiService.Application.Dapper.Product.Queries;
using Parstech.Shop.ApiService.Application.Convertor;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

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
        _orderDetailQueries = orderDetailQueries;
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
        indexCountsDto.FacilitiesTransactionsCount =
            _context.WalletTransactions.Where(a => a.Type == "Fecilities").Count();
        indexCountsDto.PishFactorCount =
            _context.Orders.Where(a => a.IsFinaly == true && a.ConfirmPayment == false).Count();
        indexCountsDto.SouratHesabCount =
            _context.Orders.Where(a => a.IsFinaly == true && a.ConfirmPayment == true).Count();
        List<Representation> representations = await _context.Representations.ToListAsync();

        foreach (Representation item in representations)
        {
            RepresentationsProducts representationsProducts = new();
            RepresentationsSells representationsSells = new();
            representationsSells.RepresentationSells = 0;
            representationsProducts.RepresentationProducts = 0;

            List<ProductStockPriceDto> products = DapperHelper.ExecuteCommand(_connectionString,
                conn => conn
                    .Query<ProductStockPriceDto>(_productQueries.GetProductStockPriceByRepId, new { @repId = item.Id })
                    .ToList());


            //var products = await _context.ProductStockPrices.Where(z => z.RepId == item.Id).ToListAsync();
            foreach (ProductStockPriceDto x in products)
            {
                representationsProducts.RepresentationProducts += x.Quantity;
                //var orderDetails = await _context.OrderDetails.Where(z => z.ProductStockPriceId == x.Id).ToListAsync();
                List<OrderDetailDto> orderDetails = DapperHelper.ExecuteCommand(_connectionString,
                    conn => conn.Query<OrderDetailDto>(_orderDetailQueries.GetOrderDetailOfProductStockPriceId,
                            new { @productId = x.Id })
                        .ToList());

                foreach (OrderDetailDto y in orderDetails)
                {
                    representationsSells.RepresentationSells += y.Total;
                }
            }

            representationsProducts.RepresentationName = item.Name;
            representationsSells.RepresentationName = item.Name;

            State? State = await _context.States.FindAsync(item.StateId);

            representationsSells.latitude = State.Latitude.Value;
            representationsSells.longitude = State.Longitude.Value;

            indexCountsDto.RepresentationsProductsForChart.Add(representationsProducts);
            indexCountsDto.RepresentationsProductsForMap.Add(representationsSells);
        }

        return indexCountsDto;
    }
}