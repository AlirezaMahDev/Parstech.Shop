using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.Web.Services.GrpcClients;

public interface IConfigAdminGrpcClient
{
    Task<CreditInfoDto> GetCreditOfNationalCodeAsync(int sellerId, string nationalCode);
    Task<ResponseDto> AddProductsByExcelAsync(string fileName);
    Task<string> GetApiDataAsync(string apiName, Dictionary<string, string> parameters);
    Task<List<WordpressProductDto>> GetProductsFromWordpressAsync(int pageNumber);
    Task<WordpressProductDto> GetProductFromWordpressByIdAsync(string productId);
    Task<List<WordpressCategoryDto>> GetCateguriesFromWordpressAsync(int pageNumber);
    Task<ResponseDto> FixProductStocksAsync();
    Task<ResponseDto> FixDublicateAsync();
    Task<ResponseDto> DatetimeChangeAsync();
    Task<ResponseDto> SetBestStockIdAsync();
    Task<ResponseDto> ExcelFixProductsAsync(string fileName);
    Task<ResponseDto> EditCateguriesOfProductsAsync(string fileName);
    Task<ResponseDto> AddUsersAndWalletCreditAsync(string fileName);
    Task<ResponseDto> UpdateUserWalletsCreditAsync(string fileName);
    Task<ResponseDto> FillProductCodeAsync(string fileName);
}