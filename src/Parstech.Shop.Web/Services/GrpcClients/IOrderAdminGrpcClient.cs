using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.OrderDetail;
using Shop.Application.DTOs.OrderPay;
using Shop.Application.DTOs.OrderShipping;
using Shop.Application.DTOs.OrderStatus;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Rahkaran;
using Shop.Application.DTOs.Response;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public interface IOrderAdminGrpcClient
    {
        Task<List<OrderFilterDataDto>> GetOrderFiltersAsync(string filter);
        Task<ResponsePagingDto<OrderDto, OrderParameterDto>> GetOrdersPagingAsync(OrderParameterDto parameter);
        Task<OrderDetailDto> GetOrderDetailsAsync(int orderId);
        Task<List<OrderStatusDto>> GetOrderStatusesAsync(int orderId);
        Task<ResponseDto> CreateOrderStatusAsync(OrderStatusDto orderStatus, IFormFile file);
        Task<ResponseDto> ChangeOrderShippingAsync(OrderShippingDto shipping);
        Task<ResponseDto> CompleteOrderByAdminAsync(int orderId, string typeName, int month);
        Task<byte[]> GenerateOrderWordFileAsync(int orderId, out string fileName);
        Task<List<OrderPayDto>> GetOrderPaysAsync(int orderId);
        Task<ResponseDto> AddOrderPayAsync(OrderPayDto orderPay);
        Task<ResponseDto> DeleteOrderPayAsync(int payId);
        Task<RahkaranOrderDto> GetRahkaranOrderAsync(int orderId);
        Task<RahkaranUserDto> GetRahkaranUserAsync(int userId);
        Task<RahkaranProductDto> GetRahkaranProductAsync(int productId);
        Task<ResponseDto> SendOrderToRahkaranAsync(int orderId);
        Task<ResponseDto> FollowOrderFromRahkaranAsync(int orderId);
        Task<ResponseDto> CreateRahkaranOrderAsync(RahkaranOrderDto order);
        Task<ResponseDto> CreateRahkaranUserAsync(RahkaranUserDto user);
        Task<ResponseDto> CreateRahkaranProductAsync(RahkaranProductDto product);
    }
} 