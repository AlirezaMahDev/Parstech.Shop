using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Enum
{
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
}
