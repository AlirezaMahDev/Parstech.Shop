using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class ProductLogRepository : GenericRepository<ProductLog>, IProductLogRepository
{
    private readonly DatabaseContext _context;
    private readonly IProductLogTypeRepository _productLogTypeRep;
    private readonly IUserBillingRepository _userBillingRep;
    private readonly IMapper _mapper;

    public ProductLogRepository(DatabaseContext context,
        IProductLogTypeRepository productLogTypeRep,
        IUserBillingRepository userBillingRep,
        IMapper mapper) : base(context)
    {
        _context = context;
        _productLogTypeRep = productLogTypeRep;
        _userBillingRep = userBillingRep;
        _mapper = mapper;
    }

    public async Task<List<ProductLog>> GetBaseProductLogWithProductId(int productId)
    {
        return await _context.ProductLogs.Where(u => u.ProductStockPriceId == productId && u.ProductLogTypeId == 4)
            .ToListAsync();
    }

    public async Task<List<ProductLog>> GetDiscountProductLogWithProductId(int productId)
    {
        return await _context.ProductLogs.Where(u => u.ProductStockPriceId == productId && u.ProductLogTypeId == 3)
            .ToListAsync();
    }

    public async Task<List<ProductLog>> GetSaleProductLogWithProductId(int productId)
    {
        return await _context.ProductLogs.Where(u => u.ProductStockPriceId == productId && u.ProductLogTypeId == 2)
            .ToListAsync();
    }

    public async Task<List<ProductLog>> GetPriceProductLogWithProductId(int productId)
    {
        return await _context.ProductLogs.Where(u => u.ProductStockPriceId == productId && u.ProductLogTypeId == 1)
            .ToListAsync();
    }

    public async Task<List<ProductLog>> GetProductLogWithProductId(int productId)
    {
        return await _context.ProductLogs.Where(u => u.ProductStockPriceId == productId).ToListAsync();
    }
}