using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contracts.Persistance
{
    public interface IUserProductRepository:IGenericRepository<UserProduct>
    {

        Task<List<UserProduct?>>GetUserProductsByUsername(string userName,string type);
        Task<bool>ExistFourUserProductByUserName(string userName ,string type);
        
    }
}
