namespace Parstech.Shop.ApiService.Domain.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string? UserId { get; set; }

    public string? Avatar { get; set; }

    public DateTime LastLoginDate { get; set; }

    public bool SendSms { get; set; }

    public bool IsDelete { get; set; }

    public string? ActiveCode { get; set; }

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ProductComment> ProductComments { get; set; } = new List<ProductComment>();

    public virtual ICollection<ProductLog> ProductLogs { get; set; } = new List<ProductLog>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<UserBilling> UserBillings { get; set; } = new List<UserBilling>();

    public virtual ICollection<UserCategury> UserCateguries { get; set; } = new List<UserCategury>();

    public virtual ICollection<UserShipping> UserShippings { get; set; } = new List<UserShipping>();

    public virtual ICollection<UserStore> UserStores { get; set; } = new List<UserStore>();

    public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}