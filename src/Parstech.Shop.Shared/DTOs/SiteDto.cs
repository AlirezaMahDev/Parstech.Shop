using Microsoft.AspNetCore.Http;

namespace Parstech.Shop.Shared.DTOs;

public class SiteDto
{
    public string? SiteName { get; set; }

    public string Title { get; set; } = null!;

    public IFormFile LogoFile { get; set; } = null!;
    public string Logo { get; set; } = null!;

    public string? LogoAlt { get; set; }

    public IFormFile? EnamadFile { get; set; }
    public string? Enamad { get; set; }

    public IFormFile? EtaemadElectronicFile { get; set; }
    public string? EtaemadElectronic { get; set; }
}