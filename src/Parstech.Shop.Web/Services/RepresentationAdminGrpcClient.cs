using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.Web.Services;

public class RepresentationAdminGrpcClient : GrpcClientBase, IRepresentationAdminGrpcClient
{
    private readonly RepresentationAdminService.RepresentationAdminServiceClient _client;

    public RepresentationAdminGrpcClient(IConfiguration configuration) : base(configuration)
    {
        _client = new RepresentationAdminService.RepresentationAdminServiceClient(Channel);
    }

    #region Representation Operations

    public async Task<RepresentationDto> GetRepresentationByIdAsync(int representationId)
    {
        var request = new RepresentationRequest { RepresentationId = representationId };
        var response = await _client.GetRepresentationByIdAsync(request);

        return MapToRepresentationDto(response);
    }

    public async Task<PagingDto> GetRepresentationsAsync(RepresentationParameterDto parameter)
    {
        var request = new RepresentationParameterRequest
        {
            CurrentPage = parameter.CurrentPage,
            TakePage = parameter.TakePage,
            SearchKey = parameter.SearchKey ?? string.Empty
        };

        var response = await _client.GetRepresentationsAsync(request);

        var result = new PagingDto
        {
            CurrentPage = response.CurrentPage,
            PageCount = response.PageCount,
            RowCount = response.RowCount,
            List = response.List.Select(r => MapToRepresentationDto(r)).ToList<object>()
        };

        return result;
    }

    public async Task<ResponseDto> CreateRepresentationAsync(RepresentationDto representation)
    {
        var request = MapFromRepresentationDto(representation);
        var response = await _client.CreateRepresentationAsync(request);

        return MapToResponseDto(response);
    }

    public async Task<ResponseDto> UpdateRepresentationAsync(RepresentationDto representation)
    {
        var request = MapFromRepresentationDto(representation);
        var response = await _client.UpdateRepresentationAsync(request);

        return MapToResponseDto(response);
    }

    public async Task<ResponseDto> DeleteRepresentationAsync(int representationId)
    {
        var request = new RepresentationRequest { RepresentationId = representationId };
        var response = await _client.DeleteRepresentationAsync(request);

        return MapToResponseDto(response);
    }

    #endregion

    #region Representation Type Operations

    public async Task<List<RepresentationTypeDto>> GetRepresentationTypesAsync()
    {
        var request = new EmptyRequest();
        var response = await _client.GetRepresentationTypesAsync(request);

        var result = new List<RepresentationTypeDto>();
        foreach (var type in response.Types)
        {
            result.Add(MapToRepresentationTypeDto(type));
        }

        return result;
    }

    public async Task<RepresentationTypeDto> GetRepresentationTypeByIdAsync(int typeId)
    {
        var request = new RepresentationTypeRequest { TypeId = typeId };
        var response = await _client.GetRepresentationTypeByIdAsync(request);

        return MapToRepresentationTypeDto(response);
    }

    public async Task<ResponseDto> CreateRepresentationTypeAsync(RepresentationTypeDto type)
    {
        var request = MapFromRepresentationTypeDto(type);
        var response = await _client.CreateRepresentationTypeAsync(request);

        return MapToResponseDto(response);
    }

    public async Task<ResponseDto> UpdateRepresentationTypeAsync(RepresentationTypeDto type)
    {
        var request = MapFromRepresentationTypeDto(type);
        var response = await _client.UpdateRepresentationTypeAsync(request);

        return MapToResponseDto(response);
    }

    public async Task<ResponseDto> DeleteRepresentationTypeAsync(int typeId)
    {
        var request = new RepresentationTypeRequest { TypeId = typeId };
        var response = await _client.DeleteRepresentationTypeAsync(request);

        return MapToResponseDto(response);
    }

    #endregion

    #region Product Representation Operations

    public async Task<List<ProductRepresentationDto>> GetProductRepresentationsAsync(int productId)
    {
        var request = new ProductRequest { ProductId = productId };
        var response = await _client.GetProductRepresentationsAsync(request);

        var result = new List<ProductRepresentationDto>();
        foreach (var rep in response.Representations)
        {
            result.Add(MapToProductRepresentationDto(rep));
        }

        return result;
    }

    public async Task<ProductRepresentationDto> GetProductRepresentationByIdAsync(int representationId)
    {
        var request = new ProductRepresentationRequest { RepresentationId = representationId };
        var response = await _client.GetProductRepresentationByIdAsync(request);

        return MapToProductRepresentationDto(response);
    }

    public async Task<ResponseDto> AddProductRepresentationAsync(ProductRepresentationDto representation)
    {
        var request = MapFromProductRepresentationDto(representation);
        var response = await _client.AddProductRepresentationAsync(request);

        return MapToResponseDto(response);
    }

    public async Task<ResponseDto> UpdateProductRepresentationAsync(ProductRepresentationDto representation)
    {
        var request = MapFromProductRepresentationDto(representation);
        var response = await _client.UpdateProductRepresentationAsync(request);

        return MapToResponseDto(response);
    }

    public async Task<ResponseDto> QuickAddProductRepresentationAsync(ProductRepresentationDto representation)
    {
        var request = MapFromProductRepresentationDto(representation);
        var response = await _client.QuickAddProductRepresentationAsync(request);

        return MapToResponseDto(response);
    }

    public async Task<ResponseDto> DeleteProductRepresentationAsync(int representationId)
    {
        var request = new ProductRepresentationRequest { RepresentationId = representationId };
        var response = await _client.DeleteProductRepresentationAsync(request);

        return MapToResponseDto(response);
    }

    #endregion

    #region Product Stock Price Operations

    public async Task<ProductStockPriceDto> GetProductStockPriceAsync(int stockPriceId)
    {
        var request = new ProductStockPriceRequest { StockPriceId = stockPriceId };
        var response = await _client.GetProductStockPriceAsync(request);

        return MapToProductStockPriceDto(response);
    }

    public async Task<ResponseDto> UpdateProductStockPriceAsync(ProductStockPriceDto stockPrice)
    {
        var request = MapFromProductStockPriceDto(stockPrice);
        var response = await _client.UpdateProductStockPriceAsync(request);

        return MapToResponseDto(response);
    }

    #endregion

    #region Product Log Operations

    public async Task<PagingDto> GetProductLogsAsync(ProductLogParameterDto parameter)
    {
        var request = new ProductLogParameterRequest
        {
            ProductId = parameter.ProductId, CurrentPage = parameter.CurrentPage, TakePage = parameter.TakePage
        };

        var response = await _client.GetProductLogsAsync(request);

        var result = new PagingDto
        {
            CurrentPage = response.CurrentPage,
            PageCount = response.PageCount,
            RowCount = response.RowCount,
            List = response.List.Select(l => MapToLogDto(l)).ToList<object>()
        };

        return result;
    }

    #endregion

    #region Mapping Methods

    private RepresentationDto MapToRepresentationDto(Shop.Shared.Protos.RepresentationAdmin.RepresentationDto source)
    {
        var result = new RepresentationDto
        {
            Id = source.Id,
            Name = source.Name,
            RepresentationTypeId = source.RepresentationTypeId,
            RepresentationTypeName = source.RepresentationTypeName,
            CreateDateShamsi = source.CreateDateShamsi,
            CreatedBy = source.CreatedBy
        };

        if (source.CreateDate != null)
        {
            result.CreateDate = source.CreateDate.ToDateTime();
        }

        return result;
    }

    private Parstech.Shop.Shared.Protos.RepresentationAdmin.RepresentationDto MapFromRepresentationDto(RepresentationDto source)
    {
        var result = new Parstech.Shop.Shared.Protos.RepresentationAdmin.RepresentationDto
        {
            Id = source.Id,
            Name = source.Name ?? string.Empty,
            RepresentationTypeId = source.RepresentationTypeId,
            RepresentationTypeName = source.RepresentationTypeName ?? string.Empty,
            CreateDateShamsi = source.CreateDateShamsi ?? string.Empty,
            CreatedBy = source.CreatedBy ?? string.Empty
        };

        if (source.CreateDate.HasValue)
        {
            result.CreateDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(
                source.CreateDate.Value.ToUniversalTime());
        }

        return result;
    }

    private RepresentationTypeDto MapToRepresentationTypeDto(
        Shop.Shared.Protos.RepresentationAdmin.RepresentationTypeDto source)
    {
        return new() { Id = source.Id, Name = source.Name };
    }

    private Parstech.Shop.Shared.Protos.RepresentationAdmin.RepresentationTypeDto MapFromRepresentationTypeDto(
        RepresentationTypeDto source)
    {
        return new Parstech.Shop.Shared.Protos.RepresentationAdmin.RepresentationTypeDto
        {
            Id = source.Id, Name = source.Name ?? string.Empty
        };
    }

    private ProductRepresentationDto MapToProductRepresentationDto(
        Shop.Shared.Protos.RepresentationAdmin.ProductRepresentationDto source)
    {
        return new()
        {
            Id = source.Id,
            ProductId = source.ProductId,
            RepresentationId = source.RepresentationId,
            RepresentationName = source.RepresentationName,
            RepresentationTypeId = source.RepresentationTypeId,
            RepresentationTypeName = source.RepresentationTypeName,
            Value = source.Value,
            UserId = source.UserId
        };
    }

    private Parstech.Shop.Shared.Protos.RepresentationAdmin.ProductRepresentationDto MapFromProductRepresentationDto(
        ProductRepresentationDto source)
    {
        return new Parstech.Shop.Shared.Protos.RepresentationAdmin.ProductRepresentationDto
        {
            Id = source.Id,
            ProductId = source.ProductId,
            RepresentationId = source.RepresentationId,
            RepresentationName = source.RepresentationName ?? string.Empty,
            RepresentationTypeId = source.RepresentationTypeId,
            RepresentationTypeName = source.RepresentationTypeName ?? string.Empty,
            Value = source.Value ?? string.Empty,
            UserId = source.UserId ?? string.Empty
        };
    }

    private ProductStockPriceDto MapToProductStockPriceDto(
        Shop.Shared.Protos.RepresentationAdmin.ProductStockPriceDto source)
    {
        var result = new ProductStockPriceDto
        {
            Id = source.Id,
            ProductId = source.ProductId,
            StoreId = source.StoreId,
            StoreName = source.StoreName,
            Price = source.Price,
            TextPrice = source.TextPrice,
            SalePrice = source.SalePrice,
            TextSalePrice = source.TextSalePrice,
            DiscountPrice = source.DiscountPrice,
            TextDiscountPrice = source.TextDiscountPrice,
            BasePrice = source.BasePrice,
            TextBasePrice = source.TextBasePrice,
            DiscountDateShamsi = source.DiscountDateShamsi,
            Quantity = source.Quantity,
            MaximumSaleInOrder = source.MaximumSaleInOrder,
            RepresentationId = source.RepresentationId,
            RepresentationName = source.RepresentationName,
            TaxId = source.TaxId,
            QuantityPerBundle = source.QuantityPerBundle
        };

        if (source.DiscountDate != null)
        {
            result.DiscountDate = source.DiscountDate.ToDateTime();
        }

        return result;
    }

    private Parstech.Shop.Shared.Protos.RepresentationAdmin.ProductStockPriceDto MapFromProductStockPriceDto(
        ProductStockPriceDto source)
    {
        var result = new Parstech.Shop.Shared.Protos.RepresentationAdmin.ProductStockPriceDto
        {
            Id = source.Id,
            ProductId = source.ProductId,
            StoreId = source.StoreId,
            StoreName = source.StoreName ?? string.Empty,
            Price = source.Price,
            TextPrice = source.TextPrice ?? string.Empty,
            SalePrice = source.SalePrice,
            TextSalePrice = source.TextSalePrice ?? string.Empty,
            DiscountPrice = source.DiscountPrice,
            TextDiscountPrice = source.TextDiscountPrice ?? string.Empty,
            BasePrice = source.BasePrice,
            TextBasePrice = source.TextBasePrice ?? string.Empty,
            DiscountDateShamsi = source.DiscountDateShamsi ?? string.Empty,
            Quantity = source.Quantity,
            MaximumSaleInOrder = source.MaximumSaleInOrder,
            RepresentationId = source.RepresentationId,
            RepresentationName = source.RepresentationName ?? string.Empty,
            TaxId = source.TaxId,
            QuantityPerBundle = source.QuantityPerBundle
        };

        if (source.DiscountDate.HasValue)
        {
            result.DiscountDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(
                source.DiscountDate.Value.ToUniversalTime());
        }

        return result;
    }

    private LogDto MapToLogDto(Shop.Shared.Protos.RepresentationAdmin.ProductLogDto source)
    {
        var result = new LogDto
        {
            Id = source.Id,
            ProductId = source.ProductId,
            ProductName = source.ProductName,
            Action = source.Action,
            Description = source.Description,
            CreateDateShamsi = source.CreateDateShamsi,
            UserName = source.UserName
        };

        if (source.CreateDate != null)
        {
            result.CreateDate = source.CreateDate.ToDateTime();
        }

        return result;
    }

    private ResponseDto MapToResponseDto(Shop.Shared.Protos.RepresentationAdmin.ResponseDto source)
    {
        var result = new ResponseDto
        {
            IsSuccessed = source.IsSuccessed, Message = source.Message, Object = source.Object
        };

        if (source.Errors != null && source.Errors.Count > 0)
        {
            result.Errors = new();
            foreach (var error in source.Errors)
            {
                result.Errors.Add(new(
                    error.PropertyName,
                    error.ErrorMessage
                ));
            }
        }

        return result;
    }

    #endregion
}