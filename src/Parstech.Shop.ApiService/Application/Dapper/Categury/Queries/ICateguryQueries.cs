namespace Parstech.Shop.ApiService.Application.Dapper.Categury.Queries;

public interface ICateguryQueries
{
    string GetBlankCateguries { get; }
    string GetParrentCateguries { get; }
    string GetChalidOfParrentCateguries { get; }
    string GetChalidOfChildCateguries { get; }
    string GetMenuParrentCateguries { get; }
    string GetMenuChalidOfParrentCateguries { get; }
    string GetMenuChalidOfChildCateguries { get; }
}