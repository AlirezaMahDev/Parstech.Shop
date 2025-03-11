using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Models;

namespace Shop.Application.Contracts.Persistance
{
    public interface IRoleRepository:IGenericRepository<Irole>
    {
        Task DeleteIdentityRole(string id);
        Task<Irole> GetIdentityRole(string id);
    }
}
