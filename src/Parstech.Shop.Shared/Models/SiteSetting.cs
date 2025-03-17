namespace Parstech.Shop.Shared.Models;

public partial class SiteSetting
{
    public int Id { get; set; }

    public string? SiteName { get; set; }

    public string Title { get; set; } = null!;

    public string Logo { get; set; } = null!;

    public string? LogoAlt { get; set; }

    public string? Author { get; set; }

    public string? Owner { get; set; }

    public string? Keywords { get; set; }

    public string? Description { get; set; }

    public string? Canonical { get; set; }

    public string? OgType { get; set; }

    public string? OgTitle { get; set; }

    public string? OgDescription { get; set; }

    public string? OgImage { get; set; }

    public string? OgUrl { get; set; }

    public string? OgSiteName { get; set; }

    public string? RobotsIndex { get; set; }

    public string? RobotsFollow { get; set; }

    public string? Enamad { get; set; }

    public string? EtaemadElectronic { get; set; }

    public virtual ICollection<SocialSetting> SocialSettings { get; set; } = new List<SocialSetting>();
}