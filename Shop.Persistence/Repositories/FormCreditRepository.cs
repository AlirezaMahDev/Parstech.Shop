using Microsoft.EntityFrameworkCore;
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
    public class FormCreditRepository:GenericRepository<FormCredit>,IFormCreditRepository
    {
        private readonly DatabaseContext _context;
        public FormCreditRepository(DatabaseContext context):base(context)
        {
            _context = context;
        }

        public async Task<List<FormCredit>> Search(string Filter, string FromDate,string ToDate)
        {
            IQueryable<FormCredit> Result = _context.FormCredits;
            if (Filter != null && Filter != "")
            {
                Result = Result.Where(u =>
                u.Name.Contains(Filter) ||
                u.Family.Contains(Filter));
            }
            
            return await Result.OrderByDescending(u => u.Id).ToListAsync();
        }
    }
}
