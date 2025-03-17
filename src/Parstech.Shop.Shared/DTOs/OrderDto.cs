namespace Parstech.Shop.ApiService.Application.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Costumer { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateDateShamsi { get; set; }


        public string OrderCode { get; set; } = null!;

        public long OrderSum { get; set; }

        public long Shipping { get; set; }

        public long Tax { get; set; }

        public long Discount { get; set; }

        public long Total { get; set; }

        public bool IsFinaly { get; set; }

        public string? IntroCode { get; set; }

        public int IntroCoin { get; set; }

        public bool? ConfirmPayment { get; set; }

        public string? FactorFile { get; set; }

        public bool IsDelete { get; set; }

        public int TaxId { get; set; }

        public string Status { get; set; }
        public string StatusIcon { get; set; }
        public string PayType { get; set; }
        public string TypeName { get; set; }
        public string StatusName { get; set; }
    }

    public class OrderDetailShowDto
    {
        public List<OrderDetailDto> OrderDetailDto { get; set; }
        public OrderDto Order { get; set; }
        public UserBillingDto Costumer { get; set; }
        public OrderShippingDto OrderShipping { get; set; }
        public List<UserShippingDto> UserShippingList { get; set; }
        public int OrderShippingId { get; set; }
        public OrderCouponDto OrderCoupon { get; set; }
        public List<PayTypeDto> PayTypes { get; set; }
        public List<OrderPayDto> OrderPay { get; set; }
    }

    public class OrderForUserDto
    {
        public OrderDto Order { get; set; }
        public List<OrderDetailShowDto> OrderDetails { get; set; }
        public OrderCouponDto OrderCoupon { get; set; }
        public OrderPayDto OrderPay { get; set; }
        public OrderShippingDto OrderShipping { get; set; }
    }
}

public class OrderResponse
{
    public bool Status { get; set; }
    public string Message { get; set; }
    public long Discount { get; set; }
}

public class OrderFilterDto
{
    public List<storeFilterDto> stores { get; set; }
    public List<statusFilterDto> statuses { get; set; }
    public List<payFilterDto> pays { get; set; }
    public List<ordercodeFilterDto> ordercodes { get; set; }
    public List<customerFilterDto> customers { get; set; }
}

public class storeFilterDto
{
    public string StoreName { get; set; }
    public int UserStoreId { get; set; }
    public int UserId { get; set; }
}

public class customerFilterDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class statusFilterDto
{
    public int Id { get; set; }
    public string StatusName { get; set; }
    public string UserName { get; set; }
}

public class payFilterDto
{
    public int Id { get; set; }
    public string TypeName { get; set; }
}

public class ordercodeFilterDto
{
    public string OrderCode { get; set; }
}