namespace Parstech.Shop.Context.Application.Dapper.ProductProperty.Queries;

public interface IProductPropertyQueries
{
    public string GetCommonByTwoProductId { get; }
    public string GetOneByProductIdAndPropertyId { get; }
    public string GeAllByProductId { get; }
    public string GeAllCompareUserProductsByUserId { get; }
    public string GeAllFavoriteUserProductsByUserId { get; }

}