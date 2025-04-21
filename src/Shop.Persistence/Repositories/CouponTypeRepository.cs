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
    public class CouponTypeRepository : GenericRepository<CouponType>, ICouponTypeRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public CouponTypeRepository(DatabaseContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
