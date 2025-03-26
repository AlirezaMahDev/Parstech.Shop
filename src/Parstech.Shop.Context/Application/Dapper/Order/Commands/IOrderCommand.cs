namespace Parstech.Shop.Context.Application.Dapper.Order.Commands;

public interface IOrderCommand
{
    string GetOrderByOrderCode { get; }
}