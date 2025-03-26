using Parstech.Shop.Context.Application.Dapper.Order.Commands;

namespace Parstech.Shop.Context.Persistence.Dapper.Order.Commands;

public class OrderCommand : IOrderCommand
{
    public string GetOrderByOrderCode => "select o.OrderId,o.UserId,o.CreateDate,o.OrderCode,o.OrderSum,o.Tax,o.Discount,o.Shipping,o.Total,o.IsFinaly,o.IntroCode,o.ConfirmPayment,o.FactorFile,o.IsDelete,o.TaxId,u.UserName from Orders as o inner join [User] as u ON o.UserId = u.Id where o.OrderCode=@orderCode";
}