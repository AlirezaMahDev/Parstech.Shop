namespace Parstech.Shop.ApiService.Application.Enum;

public enum OrderStatusType
{
    OrderRegister,
    OrderDoing,
    OrderReview,
    OrderAwaitingPayment,
    CancellationOrderPayment,
    OrderCanceled,
    OrderCompleted,
    OrderReturned,
    SendToCustomer
}