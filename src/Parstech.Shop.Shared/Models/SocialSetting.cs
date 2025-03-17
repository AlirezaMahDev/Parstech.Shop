namespace Parstech.Shop.Shared.Models;

public partial class SocialSetting
{
    public int Id { get; set; }

    public int SiteSettingId { get; set; }

    public string SocialName { get; set; } = null!;

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public string? Site { get; set; }

    public virtual SiteSetting SiteSetting { get; set; } = null!;
}