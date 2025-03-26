using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class SocialSettingRepository : GenericRepository<SocialSetting>, ISocialSettingRepository
{
    private readonly DatabaseContext _context;

    public SocialSettingRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }
}