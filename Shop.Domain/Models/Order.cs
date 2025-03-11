using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateTime CreateDate { get; set; }

    public string OrderCode { get; set; } = null!;

    public long OrderSum { get; set; }

    public long Tax { get; set; }

    public long Discount { get; set; }

    public long Shipping { get; set; }

    public long Total { get; set; }

    public bool IsFinaly { get; set; }

    public string? IntroCode { get; set; }

    public int IntroCoin { get; set; }

    public bool ConfirmPayment { get; set; }

    public string? FactorFile { get; set; }

    public bool IsDelete { get; set; }

    public int TaxId { get; set; }

    public virtual ICollection<OrderCoupon> OrderCoupons { get; set; } = new List<OrderCoupon>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<OrderPay> OrderPays { get; set; } = new List<OrderPay>();

    public virtual ICollection<OrderShipping> OrderShippings { get; set; } = new List<OrderShipping>();

    public virtual ICollection<OrderStatus> OrderStatuses { get; set; } = new List<OrderStatus>();

    public virtual Tax TaxNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
