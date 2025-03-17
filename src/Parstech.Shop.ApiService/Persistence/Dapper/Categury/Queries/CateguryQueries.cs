using Parstech.Shop.ApiService.Application.Dapper.Categury.Queries;

namespace Parstech.Shop.ApiService.Persistence.Dapper.Categury.Queries;

public class CateguryQueries : ICateguryQueries
{
    public string GetBlankCateguries =>
        "select* from Categury where ParentId IS NULL AND isParnet=0 order by Categury.Show desc";

    public string GetParrentCateguries =>
        "select* from Categury where ParentId IS NULL AND isParnet=1 order by Categury.Show desc";

    public string GetChalidOfParrentCateguries =>
        "select* from Categury where ParentId=@parentId AND isParnet=1 order by Categury.Show desc";

    public string GetChalidOfChildCateguries =>
        "select* from Categury where ParentId=@parentId AND isParnet=0 order by Categury.Show desc";


    //menu
    public string GetMenuParrentCateguries =>
        "select* from Categury where Show=1 AND ParentId IS NULL AND isParnet=1 order by Categury.Row Asc";

    public string GetMenuChalidOfParrentCateguries =>
        "select* from Categury where Show=1 AND ParentId=@parentId AND isParnet=1 AND Row=@row order by Categury.Show desc";

    public string GetMenuChalidOfChildCateguries =>
        "select* from Categury where Show=1 AND ParentId=@parentId AND isParnet=0 order by Categury.Show desc";
}