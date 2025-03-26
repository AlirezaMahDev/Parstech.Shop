using AutoMapper;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

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