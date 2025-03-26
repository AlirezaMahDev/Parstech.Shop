namespace Parstech.Shop.Context.Application.Enum;

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