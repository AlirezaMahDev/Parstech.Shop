using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.Web.Services.GrpcClients;

public class SectionAdminGrpcClient : GrpcClientBase
{
    private readonly SectionAdminService.SectionAdminServiceClient _client;

    public SectionAdminGrpcClient(IConfiguration configuration) : base(configuration)
    {
        _client = new SectionAdminService.SectionAdminServiceClient(Channel);
    }

    /// <summary>
    /// Get all sections and details for a store
    /// </summary>
    public async Task<List<SectionAndDetailsDto>> GetSectionsAndDetailsAsync(int? storeId = null)
    {
        var request = new SectionRequest { StoreId = storeId ?? 0 };
        var response = await _client.GetSectionsAndDetailsAsync(request);

        var result = new List<SectionAndDetailsDto>();
        foreach (var section in response.Sections)
        {
            result.Add(MapToSectionAndDetailsDto(section));
        }

        return result;
    }

    /// <summary>
    /// Get section by ID
    /// </summary>
    public async Task<SectionDto> GetSectionByIdAsync(int sectionId)
    {
        var request = new SectionIdRequest { SectionId = sectionId };
        var response = await _client.GetSectionByIdAsync(request);

        return MapToSectionDto(response);
    }

    /// <summary>
    /// Get section detail by ID
    /// </summary>
    public async Task<SectionDetailDto> GetSectionDetailByIdAsync(int sectionDetailId)
    {
        var request = new SectionDetailIdRequest { SectionDetailId = sectionDetailId };
        var response = await _client.GetSectionDetailByIdAsync(request);

        return MapToSectionDetailDto(response);
    }

    /// <summary>
    /// Create a new section
    /// </summary>
    public async Task<ResponseDto> CreateSectionAsync(SectionDto section)
    {
        var request = MapFromSectionDto(section);
        var response = await _client.CreateSectionAsync(request);

        return MapToResponseDto(response);
    }

    /// <summary>
    /// Update an existing section
    /// </summary>
    public async Task<ResponseDto> UpdateSectionAsync(SectionDto section)
    {
        var request = MapFromSectionDto(section);
        var response = await _client.UpdateSectionAsync(request);

        return MapToResponseDto(response);
    }

    /// <summary>
    /// Create a new section detail
    /// </summary>
    public async Task<ResponseDto> CreateSectionDetailAsync(SectionDetailDto sectionDetail)
    {
        var request = MapFromSectionDetailDto(sectionDetail);
        var response = await _client.CreateSectionDetailAsync(request);

        return MapToResponseDto(response);
    }

    /// <summary>
    /// Update an existing section detail
    /// </summary>
    public async Task<ResponseDto> UpdateSectionDetailAsync(SectionDetailDto sectionDetail)
    {
        var request = MapFromSectionDetailDto(sectionDetail);
        var response = await _client.UpdateSectionDetailAsync(request);

        return MapToResponseDto(response);
    }

    /// <summary>
    /// Delete a section
    /// </summary>
    public async Task<ResponseDto> DeleteSectionAsync(int sectionId)
    {
        var request = new SectionIdRequest { SectionId = sectionId };
        var response = await _client.DeleteSectionAsync(request);

        return MapToResponseDto(response);
    }

    /// <summary>
    /// Delete a section detail
    /// </summary>
    public async Task<ResponseDto> DeleteSectionDetailAsync(int sectionDetailId)
    {
        var request = new SectionDetailIdRequest { SectionDetailId = sectionDetailId };
        var response = await _client.DeleteSectionDetailAsync(request);

        return MapToResponseDto(response);
    }

    /// <summary>
    /// Check if a section can be deleted
    /// </summary>
    public async Task<bool> CheckSectionCanBeDeletedAsync(int sectionId)
    {
        var request = new SectionIdRequest { SectionId = sectionId };
        var response = await _client.CheckSectionCanBeDeletedAsync(request);

        return response.Result;
    }

    #region Mapping Methods

    private SectionAndDetailsDto MapToSectionAndDetailsDto(Shared.Protos.SectionAdmin.SectionAndDetailsDto source)
    {
        var result = new SectionAndDetailsDto
        {
            Id = source.Id,
            Title = source.Title,
            Description = source.Description,
            Image = source.Image,
            Link = source.Link,
            IsActive = source.IsActive,
            Sort = source.Sort,
            StoreId = source.StoreId,
            ParentId = source.ParentId,
            Details = new List<SectionDetailDto>()
        };

        foreach (var detail in source.Details)
        {
            result.Details.Add(MapToSectionDetailDto(detail));
        }

        return result;
    }

    private SectionDto MapToSectionDto(Shared.Protos.SectionAdmin.SectionDto source)
    {
        return new SectionDto
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

    private Shared.Protos.SectionAdmin.SectionDto MapFromSectionDto(SectionDto source)
    {
        return new Shared.Protos.SectionAdmin.SectionDto
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

    private SectionDetailDto MapToSectionDetailDto(Shared.Protos.SectionAdmin.SectionDetailDto source)
    {
        return new SectionDetailDto
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

    private Shared.Protos.SectionAdmin.SectionDetailDto MapFromSectionDetailDto(SectionDetailDto source)
    {
        return new Shared.Protos.SectionAdmin.SectionDetailDto
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

    private ResponseDto MapToResponseDto(Shared.Protos.SectionAdmin.ResponseDto source)
    {
        var result = new ResponseDto
        {
            IsSuccessed = source.IsSuccessed, Message = source.Message, Object = source.Object
        };

        if (source.Errors != null && source.Errors.Count > 0)
        {
            result.Errors = new List<FluentValidation.Results.ValidationFailure>();
            foreach (var error in source.Errors)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(
                    error.PropertyName,
                    error.ErrorMessage
                ));
            }
        }

        return result;
    }

    #endregion
}