using Shop.Domain.Models;



namespace Shop.Application.Contracts.Persistance
{
	public interface IOrderStatusRepository:IGenericRepository<OrderStatus>
	{ 
		Task<OrderStatus?> GetActiveOrderStatuseByOrderId(int orderId);
		Task CancelActiveAllOrderStatusesByOrderId(int orderId);
		Task<bool> CheckCancelationStatusForOrder(int orderId);

		Task<List<OrderStatus>> GetByOrderId(int OrderId);
    }
}