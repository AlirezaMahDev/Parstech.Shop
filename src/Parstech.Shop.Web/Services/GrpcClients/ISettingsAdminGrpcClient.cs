using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.Web.Services.GrpcClients;

public interface ISettingsAdminGrpcClient
{
    // Site settings operations
    Task<SiteDto> GetSiteSettingsAsync(int id);
    Task<ResponseDto> UpdateSiteSettingsAsync(int id, SiteDto siteDto);

    // SEO operations
    Task<SeoDto> GetSeoSettingsAsync(int id);
    Task<ResponseDto> UpdateSeoSettingsAsync(int id, SeoDto seoDto);

    // Social links operations
    Task<SocialDto> GetSocialLinksAsync(int id);
    Task<ResponseDto> UpdateSocialLinksAsync(int id, SocialDto socialDto);

    // Sections operations
    Task<Shop.Application.DTOs.Paging.PageingDto<SectionDto>> GetSectionsAsync(
        Shop.Application.DTOs.Paging.ParameterDto parameter);

    Task<ResponseDto> CreateSectionAsync(SectionDto sectionDto);
    Task<ResponseDto> UpdateSectionAsync(SectionDto sectionDto);
    Task<ResponseDto> DeleteSectionAsync(int id);

    // Representation types operations
    Task<Shop.Application.DTOs.Paging.PageingDto<RepresentationTypeDto>> GetRepresentationTypesAsync(
        Shop.Application.DTOs.Paging.ParameterDto parameter);

    Task<ResponseDto> CreateRepresentationTypeAsync(RepresentationTypeDto typeDto);
    Task<ResponseDto> UpdateRepresentationTypeAsync(RepresentationTypeDto typeDto);
    Task<ResponseDto> DeleteRepresentationTypeAsync(int id);

    // Representations operations
    Task<Shop.Application.DTOs.Paging.PageingDto<RepresentationDto>> GetRepresentationsAsync(
        RepresentationParameterDto parameter);

    Task<ResponseDto> CreateRepresentationAsync(RepresentationDto representationDto);
    Task<ResponseDto> UpdateRepresentationAsync(RepresentationDto representationDto);
    Task<ResponseDto> DeleteRepresentationAsync(int id);
}