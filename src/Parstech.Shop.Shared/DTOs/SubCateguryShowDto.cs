namespace Parstech.Shop.Shared.DTOs;

public class SubCateguryShowDto
{
    public int GroupId { get; set; }

    public string GroupTitle { get; set; } = null!;

    public int? ParentId { get; set; }
    public string LatinGroupTitle { get; set; } = null!;
    public string? Image { get; set; }
    public string? BackImage { get; set; }
    public string? Alt { get; set; }
    public int? Row { get; set; }
    public List<SubCateguryShowDto> Childs { get; set; }
}