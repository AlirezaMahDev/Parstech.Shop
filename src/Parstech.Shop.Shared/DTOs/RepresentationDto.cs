namespace Parstech.Shop.ApiService.Application.DTOs;

public class RepresentationDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? StateId { get; set; }
}

public class QuickEditDto
{
    public int id { get; set; }

    public long price { get; set; }

    public int quantity { get; set; }
}