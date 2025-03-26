using Microsoft.AspNetCore.Builder;

using Parstech.Shop.Context.Application;
using Parstech.Shop.Context.Infrastructure;
using Parstech.Shop.Context.Persistence;

namespace Parstech.Shop.Context;

public static class ContextExtensions
{
    public static void AddContext(this WebApplicationBuilder builder)
    {
        builder.ConfigureApplicationService();
        builder.ConfigureInfrustructureService();
        builder.ConfigurePersistenceService();
    }
}