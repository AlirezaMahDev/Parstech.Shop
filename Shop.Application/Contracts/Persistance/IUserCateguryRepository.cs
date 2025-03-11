using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contracts.Persistance
{
    public interface IUserCateguryRepository:IGenericRepository<UserCategury>
    {
        Task<bool> ExistUserInCategury(int userId);
        Task<UserCategury> GetUserCateguryByUserId(int userId);
    }
}
