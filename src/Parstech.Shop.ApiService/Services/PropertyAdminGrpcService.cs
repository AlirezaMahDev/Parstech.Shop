using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Property.Requests.Queries;

namespace Parstech.Shop.ApiService.Services;

public class PropertyAdminGrpcService : PropertyAdminService.PropertyAdminServiceBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PropertyAdminGrpcService> _logger;

    public PropertyAdminGrpcService(IMediator mediator, ILogger<PropertyAdminGrpcService> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override async Task<PropertyDto> GetPropertyById(PropertyRequest request, ServerCallContext context)
    {
        try
        {
            void property = await _mediator.Send(new PropertyDetailQueryReq(request.PropertyId));

            if (property == null)
            {
                return new();
            }

            PropertyDto response = new()
            {
                Id = property.Id,
                Title = property.Title,
                SortOrder = property.SortOrder,
                IsColor = property.IsColor,
                IsActive = property.IsActive
            };

            if (property.Categories != null)
            {
                foreach (var category in property.Categories)
                {
                    response.Categories.Add(new CategoryDto
                    {
                        Id = category.Id,
                        Title = category.Title,
                        Image = category.Image ?? string.Empty,
                        Slug = category.Slug ?? string.Empty,
                        ParentId = category.ParentId,
                        IsActive = category.IsActive,
                        SortOrder = category.SortOrder
                    });
                }
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting property by ID: {PropertyId}", request.PropertyId);
            throw new RpcException(new(StatusCode.Internal, "Error retrieving property"));
        }
    }

    public override async Task<PropertyPagingDto> GetProperties(PropertyParameterRequest request,
        ServerCallContext context)
    {
        try
        {
            PropertyParameterDto? parameter = new()
            {
                PageIndex = request.PageIndex, PageSize = request.PageSize, SearchKey = request.SearchKey
            };

            void paging = await _mediator.Send(new PropertyPagingQueryReq(parameter));

            var response = new PropertyPagingDto
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                TotalPages = paging.TotalPages,
                TotalCount = paging.TotalCount,
                HasPreviousPage = paging.HasPreviousPage,
                HasNextPage = paging.HasNextPage
            };

            if (paging.Data != null)
            {
                foreach (var property in paging.Data)
                {
                    PropertyDto propertyDto = new()
                    {
                        Id = property.Id,
                        Title = property.Title,
                        SortOrder = property.SortOrder,
                        IsColor = property.IsColor,
                        IsActive = property.IsActive
                    };

                    if (property.Categories != null)
                    {
                        foreach (var category in property.Categories)
                        {
                            propertyDto.Categories.Add(new CategoryDto
                            {
                                Id = category.Id,
                                Title = category.Title,
                                Image = category.Image ?? string.Empty,
                                Slug = category.Slug ?? string.Empty,
                                ParentId = category.ParentId,
                                IsActive = category.IsActive,
                                SortOrder = category.SortOrder
                            });
                        }
                    }

                    response.Data.Add(propertyDto);
                }
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error getting properties with parameters: {PageIndex}, {PageSize}, {SearchKey}",
                request.PageIndex,
                request.PageSize,
                request.SearchKey);
            throw new RpcException(new(StatusCode.Internal, "Error retrieving properties"));
        }
    }

    public override async Task<ResponseDto> CreateProperty(PropertyRequest request, ServerCallContext context)
    {
        try
        {
            PropertyDto? property = new()
            {
                Title = request.Title,
                SortOrder = request.SortOrder,
                IsColor = request.IsColor,
                IsActive = request.IsActive
            };

            void result = await _mediator.Send(new CreatePropertyCommandReq(property));

            return new() { Status = result.Status, Message = result.Message, Code = result.Code };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating property: {Title}", request.Title);
            return new() { Status = false, Message = "An error occurred while creating the property", Code = 500 };
        }
    }

    public override async Task<ResponseDto> UpdateProperty(PropertyRequest request, ServerCallContext context)
    {
        try
        {
            PropertyDto? property = new()
            {
                Id = request.PropertyId,
                Title = request.Title,
                SortOrder = request.SortOrder,
                IsColor = request.IsColor,
                IsActive = request.IsActive
            };

            void result = await _mediator.Send(new UpdatePropertyCommandReq(property));

            return new() { Status = result.Status, Message = result.Message, Code = result.Code };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating property: {PropertyId}", request.PropertyId);
            return new() { Status = false, Message = "An error occurred while updating the property", Code = 500 };
        }
    }

    public override async Task<ResponseDto> DeleteProperty(PropertyRequest request, ServerCallContext context)
    {
        try
        {
            void result = await _mediator.Send(new DeletePropertyCommandReq(request.PropertyId));

            return new() { Status = result.Status, Message = result.Message, Code = result.Code };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting property: {PropertyId}", request.PropertyId);
            return new() { Status = false, Message = "An error occurred while deleting the property", Code = 500 };
        }
    }

    public override async Task<CategoryListDto> GetAllCategories(EmptyRequest request, ServerCallContext context)
    {
        try
        {
            void categories = await _mediator.Send(new CateguryAllQueryReq());

            var response = new CategoryListDto();

            if (categories != null)
            {
                foreach (var category in categories)
                {
                    response.Data.Add(new CategoryDto
                    {
                        Id = category.Id,
                        Title = category.Title,
                        Image = category.Image ?? string.Empty,
                        Slug = category.Slug ?? string.Empty,
                        ParentId = category.ParentId,
                        IsActive = category.IsActive,
                        SortOrder = category.SortOrder
                    });
                }
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all categories");
            throw new RpcException(new(StatusCode.Internal, "Error retrieving categories"));
        }
    }

    public override async Task<ResponseDto> CreateOrUpdatePropertyCategory(PropertyCategoryRequest request,
        ServerCallContext context)
    {
        try
        {
            void result =
                await _mediator.Send(
                    new CreateOrUpdatePropertyCateguryCommandReq(request.PropertyId, request.CategoryId));

            return new() { Status = result.Status, Message = result.Message, Code = result.Code };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error creating/updating property category: Property {PropertyId}, Category {CategoryId}",
                request.PropertyId,
                request.CategoryId);
            return new()
            {
                Status = false,
                Message = "An error occurred while creating/updating the property category",
                Code = 500
            };
        }
    }
}