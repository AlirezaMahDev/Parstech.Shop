namespace Parstech.Shop.Shared.DTOs;

public class CateguryDto
{
    public int GroupId { get; set; }

    public string GroupTitle { get; set; } = null!;

    public bool IsDelete { get; set; }

    public int? ParentId { get; set; }
    public string ParentTitle { get; set; } = null!;

    public string LatinGroupTitle { get; set; } = null!;

    public string? ChangeByUserName { get; set; }

    public DateTime? LastChangeTime { get; set; }

    public bool IsParnet { get; set; }

    public bool Show { get; set; }
    public string? Image { get; set; }
    public string? BackImage { get; set; }
    public string? Alt { get; set; }
    public int Row { get; set; }
    public int Sath { get; set; }
}