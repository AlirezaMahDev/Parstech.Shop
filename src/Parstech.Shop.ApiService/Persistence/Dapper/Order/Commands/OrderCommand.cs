using Shop.Application.Dapper.Order.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Dapper.Order.Commands
{
    public class OrderCommand : IOrderCommand
    {
        public string GetOrderByOrderCode => "select o.OrderId,o.UserId,o.CreateDate,o.OrderCode,o.OrderSum,o.Tax,o.Discount,o.Shipping,o.Total,o.IsFinaly,o.IntroCode,o.ConfirmPayment,o.FactorFile,o.IsDelete,o.TaxId,u.UserName from Orders as o inner join [User] as u ON o.UserId = u.Id where o.OrderCode=@orderCode";
    }
}
