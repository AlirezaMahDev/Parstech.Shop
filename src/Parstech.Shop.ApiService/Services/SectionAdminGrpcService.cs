using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Features.Section.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.Section.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.SectionDetail.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Services;

public class SectionAdminGrpcService : SectionAdminService.SectionAdminServiceBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SectionAdminGrpcService> _logger;

    public SectionAdminGrpcService(IMediator mediator, ILogger<SectionAdminGrpcService> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Get all sections and details for a store
    /// </summary>
    public override async Task<SectionAndDetailsListResponse> GetSectionsAndDetails(SectionRequest request,
        ServerCallContext context)
    {
        try
        {
            var result =
                await _mediator.Send(new SectionAndDetailsReadsQueryReq(request.StoreId > 0 ? request.StoreId : null));
            var response = new SectionAndDetailsListResponse();

            foreach (var section in result)
            {
                response.Sections.Add(MapToSectionAndDetailsDto(section));
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting sections and details");
            throw new RpcException(new(StatusCode.Internal, "An error occurred while retrieving sections and details"));
        }
    }

    /// <summary>
    /// Get section by ID
    /// </summary>
    public override async Task<SectionDto> GetSectionById(SectionIdRequest request, ServerCallContext context)
    {
        try
        {
            var section = await _mediator.Send(new SectionReadCommandReq(request.SectionId));
            if (section == null)
            {
                throw new RpcException(new(StatusCode.NotFound, $"Section with ID {request.SectionId} not found"));
            }

            return MapToSectionDto(section);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting section by ID {SectionId}", request.SectionId);
            throw new RpcException(new(StatusCode.Internal, "An error occurred while retrieving the section"));
        }
    }

    /// <summary>
    /// Get section detail by ID
    /// </summary>
    public override async Task<SectionDetailDto> GetSectionDetailById(SectionDetailIdRequest request,
        ServerCallContext context)
    {
        try
        {
            var sectionDetail = await _mediator.Send(new SectionDetailReadCommandReq(request.SectionDetailId));
            if (sectionDetail == null)
            {
                throw new RpcException(new(StatusCode.NotFound,
                    $"Section detail with ID {request.SectionDetailId} not found"));
            }

            return MapToSectionDetailDto(sectionDetail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting section detail by ID {SectionDetailId}", request.SectionDetailId);
            throw new RpcException(new(StatusCode.Internal, "An error occurred while retrieving the section detail"));
        }
    }

    /// <summary>
    /// Create new section
    /// </summary>
    public override async Task<ResponseDto> CreateSection(SectionDto request, ServerCallContext context)
    {
        try
        {
            var sectionDto = MapFromSectionDto(request);
            var result = await _mediator.Send(new SectionCreateCommandReq(sectionDto));

            return new() { IsSuccessed = true, Message = "آیتم جدید با موفقیت افزوده شد", Object = result.ToString() };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating section");
            return new() { IsSuccessed = false, Message = "خطا در ایجاد قسمت جدید" };
        }
    }

    /// <summary>
    /// Update existing section
    /// </summary>
    public override async Task<ResponseDto> UpdateSection(SectionDto request, ServerCallContext context)
    {
        try
        {
            var sectionDto = MapFromSectionDto(request);
            var result = await _mediator.Send(new SectionUpdateCommandReq(sectionDto));

            return new() { IsSuccessed = true, Message = "ویرایش با موفقیت انجام شد", Object = result.ToString() };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating section with ID {SectionId}", request.Id);
            return new() { IsSuccessed = false, Message = "خطا در بروزرسانی قسمت" };
        }
    }

    /// <summary>
    /// Create new section detail
    /// </summary>
    public override async Task<ResponseDto> CreateSectionDetail(SectionDetailDto request, ServerCallContext context)
    {
        try
        {
            var sectionDetailDto = MapFromSectionDetailDto(request);
            var result = await _mediator.Send(new SectionDetailCreateCommandReq(sectionDetailDto));

            return new() { IsSuccessed = true, Message = "آیتم جدید با موفقیت افزوده شد", Object = result.ToString() };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating section detail");
            return new() { IsSuccessed = false, Message = "خطا در ایجاد جزئیات قسمت" };
        }
    }

    /// <summary>
    /// Update existing section detail
    /// </summary>
    public override async Task<ResponseDto> UpdateSectionDetail(SectionDetailDto request, ServerCallContext context)
    {
        try
        {
            var sectionDetailDto = MapFromSectionDetailDto(request);
            var result = await _mediator.Send(new SectionDetailUpdateCommandReq(sectionDetailDto));

            return new() { IsSuccessed = true, Message = "ویرایش با موفقیت انجام شد", Object = result.ToString() };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating section detail with ID {SectionDetailId}", request.Id);
            return new() { IsSuccessed = false, Message = "خطا در بروزرسانی جزئیات قسمت" };
        }
    }

    /// <summary>
    /// Delete section
    /// </summary>
    public override async Task<ResponseDto> DeleteSection(SectionIdRequest request, ServerCallContext context)
    {
        try
        {
            var canDelete = await _mediator.Send(new SectionCheckQueryReq(request.SectionId));
            if (!canDelete)
            {
                return new()
                {
                    IsSuccessed = false,
                    Message = "تا زمانی که قسمت اصلی سایت دارای زیر مجموعه باشد امکان حذف آن وجود ندارد"
                };
            }

            await _mediator.Send(new SectionDeleteCommandReq(request.SectionId));

            return new() { IsSuccessed = true, Message = "آیتم با موفقیت حذف شد" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting section with ID {SectionId}", request.SectionId);
            return new() { IsSuccessed = false, Message = "خطا در حذف قسمت" };
        }
    }

    /// <summary>
    /// Delete section detail
    /// </summary>
    public override async Task<ResponseDto> DeleteSectionDetail(SectionDetailIdRequest request,
        ServerCallContext context)
    {
        try
        {
            await _mediator.Send(new SectionDetailDeleteCommandReq(request.SectionDetailId));

            return new() { IsSuccessed = true, Message = "آیتم با موفقیت حذف شد" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting section detail with ID {SectionDetailId}", request.SectionDetailId);
            return new() { IsSuccessed = false, Message = "خطا در حذف جزئیات قسمت" };
        }
    }

    /// <summary>
    /// Check if section can be deleted
    /// </summary>
    public override async Task<BoolResponse> CheckSectionCanBeDeleted(SectionIdRequest request,
        ServerCallContext context)
    {
        try
        {
            var result = await _mediator.Send(new SectionCheckQueryReq(request.SectionId));
            return new BoolResponse { Result = result };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if section can be deleted, ID {SectionId}", request.SectionId);
            throw new RpcException(new(StatusCode.Internal,
                "An error occurred while checking if section can be deleted"));
        }
    }

    #region Mapping Methods

    private SectionAndDetailsDto MapToSectionAndDetailsDto(Shop.Application.DTOs.Section.SectionAndDetailsDto source)
    {
        SectionAndDetailsDto result = new()
        {
            Id = source.Id,
            Title = source.Title ?? string.Empty,
            Description = source.Description ?? string.Empty,
            Image = source.Image ?? string.Empty,
            Link = source.Link ?? string.Empty,
            IsActive = source.IsActive,
            Sort = source.Sort,
            StoreId = source.StoreId,
            ParentId = source.ParentId
        };

        if (source.Details != null)
        {
            foreach (var detail in source.Details)
            {
                result.Details.Add(MapToSectionDetailDto(detail));
            }
        }

        return result;
    }

    private SectionDto MapToSectionDto(Shop.Application.DTOs.Section.SectionDto source)
    {
        return new()
        {
            Id = source.Id,
            Title = source.Title ?? string.Empty,
            Description = source.Description ?? string.Empty,
            Image = source.Image ?? string.Empty,
            Link = source.Link ?? string.Empty,
            IsActive = source.IsActive,
            Sort = source.Sort,
            StoreId = source.StoreId,
            ParentId = source.ParentId
        };
    }

    private Parstech.Shop.Application.DTOs.Section.SectionDto MapFromSectionDto(SectionDto source)
    {
        return new Parstech.Shop.Application.DTOs.Section.SectionDto
        {
            Id = source.Id,
            Title = source.Title,
            Description = source.Description,
            Image = source.Image,
            Link = source.Link,
            IsActive = source.IsActive,
            Sort = source.Sort,
            StoreId = source.StoreId,
            ParentId = source.ParentId
        };
    }

    private SectionDetailDto MapToSectionDetailDto(Shop.Application.DTOs.SectionDetail.SectionDetailDto source)
    {
        return new()
        {
            Id = source.Id,
            Title = source.Title ?? string.Empty,
            Description = source.Description ?? string.Empty,
            Image = source.Image ?? string.Empty,
            Link = source.Link ?? string.Empty,
            IsActive = source.IsActive,
            Sort = source.Sort,
            SectionId = source.SectionId
        };
    }

    private Parstech.Shop.Application.DTOs.SectionDetail.SectionDetailDto MapFromSectionDetailDto(SectionDetailDto source)
    {
        return new Parstech.Shop.Application.DTOs.SectionDetail.SectionDetailDto
        {
            Id = source.Id,
            Title = source.Title,
            Description = source.Description,
            Image = source.Image,
            Link = source.Link,
            IsActive = source.IsActive,
            Sort = source.Sort,
            SectionId = source.SectionId
        };
    }

    #endregion
}