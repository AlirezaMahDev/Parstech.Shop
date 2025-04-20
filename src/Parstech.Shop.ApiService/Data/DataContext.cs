using Microsoft.EntityFrameworkCore;

namespace Parstech.Shop.ApiService.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
}
