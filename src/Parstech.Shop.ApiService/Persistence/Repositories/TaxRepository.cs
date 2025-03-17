using Parstech.Shop.ApiService.Application.Calculator;
using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class TaxRepository : GenericRepository<Tax>, ITaxRepository
{
    private DatabaseContext _context;

    public TaxRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<long> TaxCalculate(long Price)
    {
        return (int)PersentCalculator.PersentCalculatorByPrice(Price, 9);
    }
}