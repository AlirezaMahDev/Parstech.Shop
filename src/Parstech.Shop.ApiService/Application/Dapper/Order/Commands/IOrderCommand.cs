namespace Parstech.Shop.ApiService.Application.Dapper.Order.Commands;

public interface IOrderCommand
{
    string GetOrderByOrderCode { get; }
}