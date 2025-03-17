using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.Web.Services;

public interface IRepresentationAdminGrpcClient
{
    #region Representation Operations

    Task<RepresentationDto> GetRepresentationByIdAsync(int representationId);
    Task<PagingDto> GetRepresentationsAsync(RepresentationParameterDto parameter);
    Task<ResponseDto> CreateRepresentationAsync(RepresentationDto representation);
    Task<ResponseDto> UpdateRepresentationAsync(RepresentationDto representation);
    Task<ResponseDto> DeleteRepresentationAsync(int representationId);

    #endregion

    #region Representation Type Operations

    Task<List<RepresentationTypeDto>> GetRepresentationTypesAsync();
    Task<RepresentationTypeDto> GetRepresentationTypeByIdAsync(int typeId);
    Task<ResponseDto> CreateRepresentationTypeAsync(RepresentationTypeDto type);
    Task<ResponseDto> UpdateRepresentationTypeAsync(RepresentationTypeDto type);
    Task<ResponseDto> DeleteRepresentationTypeAsync(int typeId);

    #endregion

    #region Product Representation Operations

    Task<List<ProductRepresentationDto>> GetProductRepresentationsAsync(int productId);
    Task<ProductRepresentationDto> GetProductRepresentationByIdAsync(int representationId);
    Task<ResponseDto> AddProductRepresentationAsync(ProductRepresentationDto representation);
    Task<ResponseDto> UpdateProductRepresentationAsync(ProductRepresentationDto representation);
    Task<ResponseDto> QuickAddProductRepresentationAsync(ProductRepresentationDto representation);
    Task<ResponseDto> DeleteProductRepresentationAsync(int representationId);

    #endregion

    #region Product Stock Price Operations

    Task<ProductStockPriceDto> GetProductStockPriceAsync(int stockPriceId);
    Task<ResponseDto> UpdateProductStockPriceAsync(ProductStockPriceDto stockPrice);

    #endregion

    #region Product Log Operations

    Task<PagingDto> GetProductLogsAsync(ProductLogParameterDto parameter);

    #endregion
}