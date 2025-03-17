using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.Representation.Requests.Commands;

namespace Parstech.Shop.ApiService.Services;

public class RepresentationAdminGrpcService : RepresentationAdminService.RepresentationAdminServiceBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RepresentationAdminGrpcService> _logger;

    public RepresentationAdminGrpcService(IMediator mediator, ILogger<RepresentationAdminGrpcService> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    #region Representation Operations

    public override async Task<RepresentationDto> GetRepresentationById(RepresentationRequest request,
        ServerCallContext context)
    {
        try
        {
            void representation = await _mediator.Send(new RepresentationDetailQueryReq(request.RepresentationId));
            return MapToRepresentationDto(representation);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetRepresentationById: {Message}", ex.Message);
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<RepresentationPagingDto> GetRepresentations(RepresentationParameterRequest request,
        ServerCallContext context)
    {
        try
        {
            var parameter = new RepresentationParameterDto
            {
                CurrentPage = request.CurrentPage, TakePage = request.TakePage, SearchKey = request.SearchKey
            };

            void paging = await _mediator.Send(new RepresentationPagingQueryReq(parameter));

            var response = new RepresentationPagingDto
            {
                CurrentPage = paging.CurrentPage, PageCount = paging.PageCount, RowCount = paging.RowCount
            };

            foreach (var item in paging.List)
            {
                response.List.Add(MapToRepresentationDto(item));
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetRepresentations: {Message}", ex.Message);
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<ResponseDto> CreateRepresentation(RepresentationDto request, ServerCallContext context)
    {
        try
        {
            var representation = MapFromRepresentationDto(request);
            var createCommand = new RepresentationCreateCommandReq(representation);

            // Execute the command
            await _mediator.Send(createCommand);

            // Return success response
            return new() { IsSuccessed = true, Message = "نمایش با موفقیت ثبت شد" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateRepresentation: {Message}", ex.Message);
            return new() { IsSuccessed = false, Message = $"خطا در ثبت نمایش: {ex.Message}" };
        }
    }

    public override async Task<ResponseDto> UpdateRepresentation(RepresentationDto request, ServerCallContext context)
    {
        try
        {
            var representation = MapFromRepresentationDto(request);
            var updateCommand = new RepresentationUpdateCommandReq(representation);

            // Execute the command
            await _mediator.Send(updateCommand);

            // Return success response
            return new() { IsSuccessed = true, Message = "نمایش با موفقیت بروزرسانی شد" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UpdateRepresentation: {Message}", ex.Message);
            return new() { IsSuccessed = false, Message = $"خطا در بروزرسانی نمایش: {ex.Message}" };
        }
    }

    public override async Task<ResponseDto> DeleteRepresentation(RepresentationRequest request,
        ServerCallContext context)
    {
        try
        {
            var deleteCommand = new RepresentationDeleteCommandReq(request.RepresentationId);

            // Execute the command
            await _mediator.Send(deleteCommand);

            // Return success response
            return new() { IsSuccessed = true, Message = "نمایش با موفقیت حذف شد" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeleteRepresentation: {Message}", ex.Message);
            return new() { IsSuccessed = false, Message = $"خطا در حذف نمایش: {ex.Message}" };
        }
    }

    #endregion

    #region Representation Type Operations

    public override async Task<RepresentationTypeListResponse> GetRepresentationTypes(EmptyRequest request,
        ServerCallContext context)
    {
        try
        {
            void types = await _mediator.Send(new RepresentationTypeAllQueryReq());

            var response = new RepresentationTypeListResponse();
            foreach (var type in types)
            {
                response.Types.Add(MapToRepresentationTypeDto(type));
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetRepresentationTypes: {Message}", ex.Message);
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<RepresentationTypeDto> GetRepresentationTypeById(RepresentationTypeRequest request,
        ServerCallContext context)
    {
        try
        {
            void type = await _mediator.Send(new RepresentationTypeDetailQueryReq(request.TypeId));
            return MapToRepresentationTypeDto(type);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetRepresentationTypeById: {Message}", ex.Message);
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<ResponseDto> CreateRepresentationType(RepresentationTypeDto request,
        ServerCallContext context)
    {
        try
        {
            var type = MapFromRepresentationTypeDto(request);
            var createCommand = new RepresentationTypeCreateCommandReq(type);

            // Execute the command
            await _mediator.Send(createCommand);

            // Return success response
            return new() { IsSuccessed = true, Message = "نوع نمایش با موفقیت ثبت شد" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateRepresentationType: {Message}", ex.Message);
            return new() { IsSuccessed = false, Message = $"خطا در ثبت نوع نمایش: {ex.Message}" };
        }
    }

    public override async Task<ResponseDto> UpdateRepresentationType(RepresentationTypeDto request,
        ServerCallContext context)
    {
        try
        {
            var type = MapFromRepresentationTypeDto(request);
            var updateCommand = new RepresentationTypeUpdateCommandReq(type);

            // Execute the command
            await _mediator.Send(updateCommand);

            // Return success response
            return new() { IsSuccessed = true, Message = "نوع نمایش با موفقیت بروزرسانی شد" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UpdateRepresentationType: {Message}", ex.Message);
            return new() { IsSuccessed = false, Message = $"خطا در بروزرسانی نوع نمایش: {ex.Message}" };
        }
    }

    public override async Task<ResponseDto> DeleteRepresentationType(RepresentationTypeRequest request,
        ServerCallContext context)
    {
        try
        {
            var deleteCommand = new RepresentationTypeDeleteCommandReq(request.TypeId);

            // Execute the command
            await _mediator.Send(deleteCommand);

            // Return success response
            return new() { IsSuccessed = true, Message = "نوع نمایش با موفقیت حذف شد" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeleteRepresentationType: {Message}", ex.Message);
            return new() { IsSuccessed = false, Message = $"خطا در حذف نوع نمایش: {ex.Message}" };
        }
    }

    #endregion

    #region Product Representation Operations

    public override async Task<ProductRepresentationListResponse> GetProductRepresentations(ProductRequest request,
        ServerCallContext context)
    {
        try
        {
            void representations = await _mediator.Send(new ProductRepresentationsOfProductQueryReq(request.ProductId));

            var response = new ProductRepresentationListResponse();
            foreach (var rep in representations)
            {
                response.Representations.Add(MapToProductRepresentationDto(rep));
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetProductRepresentations: {Message}", ex.Message);
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<ResponseDto> AddProductRepresentation(ProductRepresentationDto request,
        ServerCallContext context)
    {
        try
        {
            var representation = MapFromProductRepresentationDto(request);
            var createCommand = new ProductRepresentationCreateCommandReq(representation);

            // Execute the command
            await _mediator.Send(createCommand);

            // Return success response
            return new() { IsSuccessed = true, Message = "نمایش محصول با موفقیت ثبت شد" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in AddProductRepresentation: {Message}", ex.Message);
            return new() { IsSuccessed = false, Message = $"خطا در ثبت نمایش محصول: {ex.Message}" };
        }
    }

    public override async Task<ResponseDto> QuickAddProductRepresentation(ProductRepresentationDto request,
        ServerCallContext context)
    {
        try
        {
            var representation = MapFromProductRepresentationDto(request);
            var quickAddCommand = new ProductRepresentationQuickCreateCommandReq(representation);

            // Execute the command
            await _mediator.Send(quickAddCommand);

            // Return success response
            return new() { IsSuccessed = true, Message = "نمایش محصول با موفقیت ثبت شد" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in QuickAddProductRepresentation: {Message}", ex.Message);
            return new() { IsSuccessed = false, Message = $"خطا در ثبت سریع نمایش محصول: {ex.Message}" };
        }
    }

    public override async Task<ResponseDto> DeleteProductRepresentation(ProductRepresentationRequest request,
        ServerCallContext context)
    {
        try
        {
            var deleteCommand = new ProductRepresentationDeleteCommandReq(request.RepresentationId);

            // Execute the command
            await _mediator.Send(deleteCommand);

            // Return success response
            return new() { IsSuccessed = true, Message = "نمایش محصول با موفقیت حذف شد" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeleteProductRepresentation: {Message}", ex.Message);
            return new() { IsSuccessed = false, Message = $"خطا در حذف نمایش محصول: {ex.Message}" };
        }
    }

    #endregion

    #region Product Stock Price Operations

    public override async Task<ProductStockPriceDto> GetProductStockPrice(ProductStockPriceRequest request,
        ServerCallContext context)
    {
        try
        {
            void stockPrice = await _mediator.Send(new ProductStockPriceDetailQueryReq(request.StockPriceId));
            return MapToProductStockPriceDto(stockPrice);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetProductStockPrice: {Message}", ex.Message);
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    public override async Task<ResponseDto> UpdateProductStockPrice(ProductStockPriceDto request,
        ServerCallContext context)
    {
        try
        {
            var stockPrice = MapFromProductStockPriceDto(request);
            var updateCommand = new ProductStockPriceUpdateCommandReq(stockPrice);

            // Execute the command
            await _mediator.Send(updateCommand);

            // Return success response
            return new() { IsSuccessed = true, Message = "قیمت محصول با موفقیت بروزرسانی شد" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UpdateProductStockPrice: {Message}", ex.Message);
            return new() { IsSuccessed = false, Message = $"خطا در بروزرسانی قیمت محصول: {ex.Message}" };
        }
    }

    #endregion

    #region Product Log Operations

    public override async Task<ProductLogPagingDto> GetProductLogs(ProductLogParameterRequest request,
        ServerCallContext context)
    {
        try
        {
            var parameter = new ProductLogParameterDto
            {
                ProductId = request.ProductId, CurrentPage = request.CurrentPage, TakePage = request.TakePage
            };

            void paging = await _mediator.Send(new ProductLogPagingQueryReq(parameter));

            var response = new ProductLogPagingDto
            {
                CurrentPage = paging.CurrentPage, PageCount = paging.PageCount, RowCount = paging.RowCount
            };

            foreach (var item in paging.List)
            {
                response.List.Add(MapToProductLogDto(item));
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetProductLogs: {Message}", ex.Message);
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    #endregion

    #region Mapping Methods

    private RepresentationDto MapToRepresentationDto(Shop.Application.DTOs.Representation.RepresentationDto source)
    {
        RepresentationDto result = new()
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

    private Parstech.Shop.Application.DTOs.Representation.RepresentationDto MapFromRepresentationDto(RepresentationDto source)
    {
        var result = new Shop.Application.DTOs.Representation.RepresentationDto
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

    private RepresentationTypeDto MapToRepresentationTypeDto(
        Shop.Application.DTOs.RepresentationType.RepresentationTypeDto source)
    {
        return new() { Id = source.Id, Name = source.Name ?? string.Empty };
    }

    private Parstech.Shop.Application.DTOs.RepresentationType.RepresentationTypeDto MapFromRepresentationTypeDto(
        RepresentationTypeDto source)
    {
        return new Shop.Application.DTOs.RepresentationType.RepresentationTypeDto
        {
            Id = source.Id, Name = source.Name
        };
    }

    private ProductRepresentationDto MapToProductRepresentationDto(
        Shop.Application.DTOs.ProductRepresentation.ProductRepresentationDto source)
    {
        return new()
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

    private Parstech.Shop.Application.DTOs.ProductRepresentation.ProductRepresentationDto MapFromProductRepresentationDto(
        ProductRepresentationDto source)
    {
        return new Shop.Application.DTOs.ProductRepresentation.ProductRepresentationDto
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

    private ProductStockPriceDto MapToProductStockPriceDto(
        Shop.Application.DTOs.ProductStockPrice.ProductStockPriceDto source)
    {
        ProductStockPriceDto result = new()
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

    private Parstech.Shop.Application.DTOs.ProductStockPrice.ProductStockPriceDto MapFromProductStockPriceDto(
        ProductStockPriceDto source)
    {
        var result = new Shop.Application.DTOs.ProductStockPrice.ProductStockPriceDto
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

    private ProductLogDto MapToProductLogDto(Shop.Application.DTOs.ProductLog.LogDto source)
    {
        ProductLogDto result = new()
        {
            Id = source.Id,
            ProductId = source.ProductId,
            ProductName = source.ProductName ?? string.Empty,
            Action = source.Action ?? string.Empty,
            Description = source.Description ?? string.Empty,
            CreateDateShamsi = source.CreateDateShamsi ?? string.Empty,
            UserName = source.UserName ?? string.Empty
        };

        if (source.CreateDate.HasValue)
        {
            result.CreateDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(
                source.CreateDate.Value.ToUniversalTime());
        }

        return result;
    }

    #endregion
}