using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.UserShipping;
using Shop.Domain.Models;

namespace Shop.Application.Contracts.Persistance
{
    public interface IUserShippingRepository:IGenericRepository<UserShipping>
    {
        Task<List<UserShippingDto>> GetShippingOfUser(int userId);
        Task<UserShipping> GetFirstShippingOfUser(int userId);
        
    }
}
