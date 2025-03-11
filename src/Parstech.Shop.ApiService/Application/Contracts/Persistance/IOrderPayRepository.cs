using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contracts.Persistance
{
    public interface IOrderPayRepository:IGenericRepository<OrderPay>
    {
        Task<OrderPay> GetByOrderId(int OrderId);
        Task<bool> HasOrderPay(int OrderId);
        Task<List<OrderPay>> GetListByOrderId(int OrderId);
    }
}
