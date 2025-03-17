using AutoMapper;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class BrandRepository : GenericRepository<Brand>, IBrandRepository
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