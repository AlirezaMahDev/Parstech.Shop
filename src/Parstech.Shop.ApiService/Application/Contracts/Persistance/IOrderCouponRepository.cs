using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contracts.Persistance
{
    public interface IOrderCouponRepository: IGenericRepository<OrderCoupon>
    {
        Task<bool> CouponExistInOrder(int orderId, int couponId);
        Task<bool> CouponExistInOrderCoupon(int couponId);
        Task<bool> OrderHaveCoupon(int orderId);
        Task<OrderCoupon?> GetByOrderId(int orderId);
        Task CheckAndDelete(int orderId);
        Task<bool> ExistInOrder(int orderId);
    }
}
