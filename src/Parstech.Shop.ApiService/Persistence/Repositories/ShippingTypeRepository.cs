﻿using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class ShippingTypeRepository : GenericRepository<ShippingType>, IShippingTypeRepository
{
    private readonly DatabaseContext _context;

    public ShippingTypeRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}