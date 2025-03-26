using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parstech.Shop.Context.Application.Calculator;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRep;
    private readonly ITaxRepository _taxRep;

    public OrderDetailRepository(DatabaseContext context,
        IMapper mapper,
        IProductRepository productRep, ITaxRepository taxRep) : base(context)
    {
        _context = context;
        _mapper = mapper;
        _productRep = productRep;
        _taxRep = taxRep;
    }


    public async Task<List<OrderDetail>> GetOrderDetailsByOrderId(int orderId)
    {
        var list = await _context.OrderDetails.Where(u => u.OrderId == orderId).ToListAsync();
        return list;
    }
    public async Task CalculateOrderDetailTax(int orderId)
    {
        var orderDetails = await _context.OrderDetails.Where(z => z.OrderId == orderId).ToListAsync();
        foreach (var orderDetail in orderDetails)
        {
            //var product = await _context.Products.FirstOrDefaultAsync(z => z.Id == orderDetail.ProductId);
            //if(product.TaxId == 1)
            //{
            //    orderDetail.Tax = 0;
            //}
            //else
            //{
            //    var Price = ((int)orderDetail.Price - orderDetail.Discount) * orderDetail.Count;
            //    orderDetail.Tax = await _taxRep.TaxCalculate(Price);
            //}
            await UpdateAsync(orderDetail);
        }
    }

    public async Task<int> CountOfSaleByProductId(int productId)
    {
        // var sales=await _context.OrderDetails.Where(u => u.ProductId == productId).ToListAsync();
        //return sales.Count;
        return 0;
    }

    public async Task<OrderDetail> RefreshOrderDetail(int detailId)
    {
        var detail = await GetAsync(detailId);
        detail.DetailSum = detail.Price * detail.Count;
        detail.Tax = PersentCalculator.PersentCalculatorByPrice(detail.DetailSum, 9);
        detail.Total = detail.DetailSum + detail.Tax - detail.Discount;
        return await UpdateAsync(detail);
    }

    public async Task<bool> ProductIdExistInOrderDetails(int orderId, int productId)
    {
        if (await _context.OrderDetails.AnyAsync(u => u.OrderId == orderId && u.ProductStockPriceId == productId))
        {
            return true;
        }
        else
        {
            return false;
        }
            
    }

    public async Task<int> GetCountOfOrder(int orderId)
    {
        var orderDetails = await _context.OrderDetails.Where(u => u.OrderId == orderId).ToListAsync();
        return orderDetails.Count;
    }

    public async Task<bool> ExistOrderDetailforProductStockPrice(int ProductStockPricId)
    {
        if (await _context.OrderDetails.AnyAsync(u => u.ProductStockPriceId == ProductStockPricId))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}