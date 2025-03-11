using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Application.DTOs.Reports;
using Shop.Domain.Models;
using Shop.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.Convertor;
using static System.Net.Mime.MediaTypeNames;
using Shop.Application.Dapper.Product.Queries;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.ProductStockPrice;
using Dapper;
using Shop.Application.DTOs.Product;
using Shop.Application.Dapper.OrderDetail.Queries;
using Shop.Application.DTOs.OrderDetail;

namespace Shop.Persistence.Repositories
{
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
            IndexCountsDto indexCountsDto = new IndexCountsDto();
            indexCountsDto.RepresentationsProductsForChart = new List<RepresentationsProducts>();
            indexCountsDto.RepresentationsProductsForMap = new List<RepresentationsSells>();

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
                RepresentationsProducts representationsProducts = new RepresentationsProducts();
                RepresentationsSells representationsSells = new RepresentationsSells();
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
}
