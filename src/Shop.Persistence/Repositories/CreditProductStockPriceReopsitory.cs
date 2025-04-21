using AutoMapper;
using Shop.Application.Contracts.Persistance;
using Shop.Domain.Models;
using Shop.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Repositories
{
    public class CreditProductStockPriceReopsitory: GenericRepository<CreditProductStockPrice>,ICreditProductStockPriceReopsitory
    {
        private readonly DatabaseContext _context;
        
        public CreditProductStockPriceReopsitory(DatabaseContext context) : base(context)
        {
            _context = context;
           
        }
    }
}
