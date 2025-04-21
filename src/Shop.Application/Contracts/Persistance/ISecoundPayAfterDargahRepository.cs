using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contracts.Persistance
{
    public interface ISecoundPayAfterDargahRepository:IGenericRepository<SecoundPayAfterDargah>
    {
        Task<SecoundPayAfterDargah>GetByOrderId(int orderId);
    }
}
