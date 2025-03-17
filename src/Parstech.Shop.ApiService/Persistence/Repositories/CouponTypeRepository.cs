using AutoMapper;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

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