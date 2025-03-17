namespace Parstech.Shop.Shared.DTOs;

public class ParrentCateguryShowDto
{
    public int GroupId { get; set; }

    public string GroupTitle { get; set; } = null!;
    public string LatinGroupTitle { get; set; } = null!;
    public string? Image { get; set; }
    public string? BackImage { get; set; }
    public string? Alt { get; set; }
    public int? Row { get; set; }
    public List<SubParrentCateguryShowDto> Childs1 { get; set; } = new();
    public List<SubParrentCateguryShowDto> Childs2 { get; set; } = new();
    public List<SubParrentCateguryShowDto> Childs3 { get; set; } = new();
    public List<SubParrentCateguryShowDto> Childs4 { get; set; } = new();
}