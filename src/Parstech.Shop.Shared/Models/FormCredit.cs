namespace Parstech.Shop.Shared.Models;

public partial class FormCredit
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Family { get; set; } = null!;

    public string PersonalCode { get; set; } = null!;

    public string InternationalCode { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string State { get; set; } = null!;

    public long RequestPrice { get; set; }

    public DateTime CreateDate { get; set; }

    public string Status { get; set; } = null!;
}