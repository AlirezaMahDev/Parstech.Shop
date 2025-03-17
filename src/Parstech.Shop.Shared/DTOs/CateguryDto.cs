namespace Parstech.Shop.ApiService.Application.DTOs;

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

public class SubParrentCateguryShowDto
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

public class CategurySelectDto
{
    public int GroupId { get; set; }
    public string GroupTitle { get; set; }
    public bool IsParnet { get; set; }
}