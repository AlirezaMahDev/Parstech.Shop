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
	public class StatusRepository : GenericRepository<Status>, IStatusRepository
	{
		private DatabaseContext _context;
		public StatusRepository(DatabaseContext context) : base(context)
		{
			_context = context;
		}

        public async Task<Status?> GetStatusByLatinName(string name)
        {
          return await _context.Statuses.FirstOrDefaultAsync(u=>u.StatusLatinName == name);	
        }
    }
}
