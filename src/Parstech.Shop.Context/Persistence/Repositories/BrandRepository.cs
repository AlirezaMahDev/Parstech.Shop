using AutoMapper;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class BrandRepository:GenericRepository<Brand>,IBrandRepository
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public BrandRepository(DatabaseContext context, IMapper mapper) : base(context)
    {
        _context = context;
        _mapper = mapper;
    }
    public int GetCountOfBrands()
    {

        return _context.Brands.Count();
    }
}