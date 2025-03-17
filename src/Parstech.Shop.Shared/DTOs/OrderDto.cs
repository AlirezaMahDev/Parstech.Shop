namespace Parstech.Shop.Shared.DTOs
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
}