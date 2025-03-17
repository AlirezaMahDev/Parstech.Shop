using Parstech.Shop.ApiService.Application.Calculator;
using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

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