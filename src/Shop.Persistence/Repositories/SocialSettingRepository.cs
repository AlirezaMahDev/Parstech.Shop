using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.Contracts.Persistance;
using Shop.Domain.Models;
using Shop.Persistence.Context;

namespace Shop.Persistence.Repositories
{
    public class SocialSettingRepository : GenericRepository<SocialSetting>, ISocialSettingRepository
    {
        private readonly DatabaseContext _context;

        public SocialSettingRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
