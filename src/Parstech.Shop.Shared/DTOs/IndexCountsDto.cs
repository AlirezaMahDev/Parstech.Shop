namespace Parstech.Shop.Shared.DTOs;

public class IndexCountsDto
{
    public string Time { get; set; }
    public int UserCount { get; set; }
    public int ProductCount { get; set; }
    public int IsLoadOrderCount { get; set; }
    public int AllTransactionsCount { get; set; }
    public int CoinTransactionsCount { get; set; }
    public int WalletTransactionsCount { get; set; }
    public int FacilitiesTransactionsCount { get; set; }
    public int PishFactorCount { get; set; }
    public int SouratHesabCount { get; set; }
    public List<RepresentationsSells> RepresentationsProductsForMap { get; set; }
    public List<RepresentationsProducts> RepresentationsProductsForChart { get; set; }
}