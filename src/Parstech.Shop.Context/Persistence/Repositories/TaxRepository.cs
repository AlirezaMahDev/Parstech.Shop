using Parstech.Shop.Context.Application.Calculator;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class TaxRepository : GenericRepository<Tax>, ITaxRepository
{
    private DatabaseContext _context;

    public TaxRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<long> TaxCalculate(long Price)
    {
        return (int)PersentCalculator.PersentCalculatorByPrice(Price,9);
    }
}