using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.Calculator;
using Shop.Application.Contracts.Persistance;
using Shop.Domain.Models;
using Shop.Persistence.Context;
using Shop.Persistence.Repositories;

namespace Shop.Persistence.Repositories
{
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
}
