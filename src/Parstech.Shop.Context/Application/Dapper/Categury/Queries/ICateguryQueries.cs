namespace Parstech.Shop.Context.Application.Dapper.Categury.Queries;

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