using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Features.SiteSeting.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.SocialSetting.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Services;

public class SettingsAdminGrpcService : SettingsAdminService.SettingsAdminServiceBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SettingsAdminGrpcService> _logger;

    public SettingsAdminGrpcService(
        IMediator mediator,
        ILogger<SettingsAdminGrpcService> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Site Settings

    public override async Task<SiteSettingsResponse> GetSiteSettings(SiteSettingsRequest request,
        ServerCallContext context)
    {
        try
        {
            var siteSettings = await _mediator.Send(new SiteSettingReadCommandReq(request.Id));

            var response = new SiteSettingsResponse
            {
                Status = true,
                Message = "Site settings retrieved successfully",
                Code = 200,
                Site = new SiteSettingsDto
                {
                    Id = siteSettings.Id,
                    SiteName = siteSettings.SiteName,
                    SiteTitle = siteSettings.SiteTitle,
                    SiteDescription = siteSettings.SiteDescription,
                    SiteCopyright = siteSettings.SiteCopyright,
                    SiteAddress = siteSettings.SiteAddress,
                    SitePhone = siteSettings.SitePhone,
                    SiteEmail = siteSettings.SiteEmail,
                    SiteLogo = siteSettings.SiteLogo,
                    SiteFavicon = siteSettings.SiteFavicon,
                    SiteSlogan = siteSettings.SiteSlogan,
                    SiteTerms = siteSettings.SiteTerms,
                    SiteAbout = siteSettings.SiteAbout,
                    SitePrivacy = siteSettings.SitePrivacy,
                    EnableRegistration = siteSettings.EnableRegistration,
                    EnableBlog = siteSettings.EnableBlog,
                    EnableContact = siteSettings.EnableContact,
                    EnableShop = siteSettings.EnableShop,
                    CurrencySymbol = siteSettings.CurrencySymbol,
                    CurrencyName = siteSettings.CurrencyName
                }
            };

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving site settings for ID: {Id}", request.Id);
            return new SiteSettingsResponse
            {
                Status = false, Message = "Error retrieving site settings: " + ex.Message, Code = 500
            };
        }
    }

    public override async Task<ResponseDto> UpdateSiteSettings(UpdateSiteSettingsRequest request,
        ServerCallContext context)
    {
        try
        {
            SiteDto siteDto = new()
            {
                Id = request.Site.Id,
                SiteName = request.Site.SiteName,
                SiteTitle = request.Site.SiteTitle,
                SiteDescription = request.Site.SiteDescription,
                SiteCopyright = request.Site.SiteCopyright,
                SiteAddress = request.Site.SiteAddress,
                SitePhone = request.Site.SitePhone,
                SiteEmail = request.Site.SiteEmail,
                SiteLogo = request.Site.SiteLogo,
                SiteFavicon = request.Site.SiteFavicon,
                SiteSlogan = request.Site.SiteSlogan,
                SiteTerms = request.Site.SiteTerms,
                SiteAbout = request.Site.SiteAbout,
                SitePrivacy = request.Site.SitePrivacy,
                EnableRegistration = request.Site.EnableRegistration,
                EnableBlog = request.Site.EnableBlog,
                EnableContact = request.Site.EnableContact,
                EnableShop = request.Site.EnableShop,
                CurrencySymbol = request.Site.CurrencySymbol,
                CurrencyName = request.Site.CurrencyName
            };

            var result = await _mediator.Send(new SiteSettingUpdateCommandReq(request.Id, siteDto));

            return new() { Status = result.Status, Message = result.Message, Code = result.Code };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating site settings for ID: {Id}", request.Id);
            return new() { Status = false, Message = "Error updating site settings: " + ex.Message, Code = 500 };
        }
    }

    #endregion

    #region SEO Settings

    public override async Task<SeoSettingsResponse> GetSeoSettings(SeoSettingsRequest request,
        ServerCallContext context)
    {
        try
        {
            var seoSettings = await _mediator.Send(new SeoSettingReadCommandReq(request.Id));

            var response = new SeoSettingsResponse
            {
                Status = true,
                Message = "SEO settings retrieved successfully",
                Code = 200,
                Seo = new SeoSettingsDto
                {
                    Id = seoSettings.Id,
                    MetaTitle = seoSettings.MetaTitle,
                    MetaDescription = seoSettings.MetaDescription,
                    MetaKeywords = seoSettings.MetaKeywords,
                    CanonicalUrl = seoSettings.CanonicalUrl,
                    RobotsTxt = seoSettings.RobotsTxt,
                    GoogleAnalytics = seoSettings.GoogleAnalytics,
                    GoogleTagManager = seoSettings.GoogleTagManager,
                    GoogleSiteVerification = seoSettings.GoogleSiteVerification,
                    BingSiteVerification = seoSettings.BingSiteVerification,
                    EnableSitemap = seoSettings.EnableSitemap,
                    EnableSchemaMarkup = seoSettings.EnableSchemaMarkup,
                    CustomHeadScripts = seoSettings.CustomHeadScripts,
                    CustomFooterScripts = seoSettings.CustomFooterScripts
                }
            };

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving SEO settings for ID: {Id}", request.Id);
            return new SeoSettingsResponse
            {
                Status = false, Message = "Error retrieving SEO settings: " + ex.Message, Code = 500
            };
        }
    }

    public override async Task<ResponseDto> UpdateSeoSettings(UpdateSeoSettingsRequest request,
        ServerCallContext context)
    {
        try
        {
            SeoDto seoDto = new()
            {
                Id = request.Seo.Id,
                MetaTitle = request.Seo.MetaTitle,
                MetaDescription = request.Seo.MetaDescription,
                MetaKeywords = request.Seo.MetaKeywords,
                CanonicalUrl = request.Seo.CanonicalUrl,
                RobotsTxt = request.Seo.RobotsTxt,
                GoogleAnalytics = request.Seo.GoogleAnalytics,
                GoogleTagManager = request.Seo.GoogleTagManager,
                GoogleSiteVerification = request.Seo.GoogleSiteVerification,
                BingSiteVerification = request.Seo.BingSiteVerification,
                EnableSitemap = request.Seo.EnableSitemap,
                EnableSchemaMarkup = request.Seo.EnableSchemaMarkup,
                CustomHeadScripts = request.Seo.CustomHeadScripts,
                CustomFooterScripts = request.Seo.CustomFooterScripts
            };

            var result = await _mediator.Send(new SeoSettingUpdateCommandReq(request.Id, seoDto));

            return new() { Status = result.Status, Message = result.Message, Code = result.Code };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating SEO settings for ID: {Id}", request.Id);
            return new() { Status = false, Message = "Error updating SEO settings: " + ex.Message, Code = 500 };
        }
    }

    #endregion

    #region Social Links

    public override async Task<SocialLinksResponse> GetSocialLinks(SocialLinksRequest request,
        ServerCallContext context)
    {
        try
        {
            var socialLinks = await _mediator.Send(new SocialSettingReadCommandReq(request.Id));

            var response = new SocialLinksResponse
            {
                Status = true,
                Message = "Social links retrieved successfully",
                Code = 200,
                Social = new SocialLinksDto
                {
                    Id = socialLinks.Id,
                    Facebook = socialLinks.Facebook,
                    Twitter = socialLinks.Twitter,
                    Instagram = socialLinks.Instagram,
                    Linkedin = socialLinks.Linkedin,
                    Youtube = socialLinks.Youtube,
                    Pinterest = socialLinks.Pinterest,
                    Telegram = socialLinks.Telegram,
                    Whatsapp = socialLinks.Whatsapp,
                    EnableSocialLogin = socialLinks.EnableSocialLogin,
                    EnableFacebookLogin = socialLinks.EnableFacebookLogin,
                    EnableGoogleLogin = socialLinks.EnableGoogleLogin
                }
            };

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving social links for ID: {Id}", request.Id);
            return new SocialLinksResponse
            {
                Status = false, Message = "Error retrieving social links: " + ex.Message, Code = 500
            };
        }
    }

    public override async Task<ResponseDto> UpdateSocialLinks(UpdateSocialLinksRequest request,
        ServerCallContext context)
    {
        try
        {
            var socialDto = new SocialDto
            {
                Id = request.Social.Id,
                Facebook = request.Social.Facebook,
                Twitter = request.Social.Twitter,
                Instagram = request.Social.Instagram,
                Linkedin = request.Social.Linkedin,
                Youtube = request.Social.Youtube,
                Pinterest = request.Social.Pinterest,
                Telegram = request.Social.Telegram,
                Whatsapp = request.Social.Whatsapp,
                EnableSocialLogin = request.Social.EnableSocialLogin,
                EnableFacebookLogin = request.Social.EnableFacebookLogin,
                EnableGoogleLogin = request.Social.EnableGoogleLogin
            };

            var result = await _mediator.Send(new SocialSettingUpdateCommandReq(request.Id, socialDto));

            return new() { Status = result.Status, Message = result.Message, Code = result.Code };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating social links for ID: {Id}", request.Id);
            return new() { Status = false, Message = "Error updating social links: " + ex.Message, Code = 500 };
        }
    }

    #endregion

    #region Sections

    public override async Task<SectionsResponse> GetSections(SectionsRequest request, ServerCallContext context)
    {
        try
        {
            var parameter = new Shop.Application.DTOs.Paging.ParameterDto
            {
                PageId = request.PageId, Take = request.Take, SearchKey = request.SearchKey
            };

            var result = await _mediator.Send(new GetSectionsPagingQueryReq(parameter));

            var response = new SectionsResponse
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code,
                TotalRow = result.TotalRow,
                PageId = result.PageId,
                Take = result.Take
            };

            foreach (var section in result.Items)
            {
                response.Items.Add(new SectionDto
                {
                    Id = section.Id,
                    Title = section.Title,
                    Description = section.Description,
                    ImageUrl = section.ImageUrl,
                    DisplayOrder = section.DisplayOrder,
                    IsActive = section.IsActive,
                    SectionType = section.SectionType,
                    LinkUrl = section.LinkUrl,
                    LinkText = section.LinkText,
                    IsFeatured = section.IsFeatured,
                    CreatedDate = section.CreatedDate
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving sections");
            return new SectionsResponse
            {
                Status = false, Message = "Error retrieving sections: " + ex.Message, Code = 500
            };
        }
    }

    public override async Task<ResponseDto> CreateSection(CreateSectionRequest request, ServerCallContext context)
    {
        try
        {
            var sectionDto = new SectionDto
            {
                Title = request.Section.Title,
                Description = request.Section.Description,
                ImageUrl = request.Section.ImageUrl,
                DisplayOrder = request.Section.DisplayOrder,
                IsActive = request.Section.IsActive,
                SectionType = request.Section.SectionType,
                LinkUrl = request.Section.LinkUrl,
                LinkText = request.Section.LinkText,
                IsFeatured = request.Section.IsFeatured
            };

            var result = await _mediator.Send(new CreateSectionCommandReq(sectionDto));

            return new() { Status = result.Status, Message = result.Message, Code = result.Code };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating section");
            return new() { Status = false, Message = "Error creating section: " + ex.Message, Code = 500 };
        }
    }

    public override async Task<ResponseDto> UpdateSection(UpdateSectionRequest request, ServerCallContext context)
    {
        try
        {
            var sectionDto = new SectionDto
            {
                Id = request.Section.Id,
                Title = request.Section.Title,
                Description = request.Section.Description,
                ImageUrl = request.Section.ImageUrl,
                DisplayOrder = request.Section.DisplayOrder,
                IsActive = request.Section.IsActive,
                SectionType = request.Section.SectionType,
                LinkUrl = request.Section.LinkUrl,
                LinkText = request.Section.LinkText,
                IsFeatured = request.Section.IsFeatured
            };

            var result = await _mediator.Send(new UpdateSectionCommandReq(sectionDto));

            return new() { Status = result.Status, Message = result.Message, Code = result.Code };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating section with ID: {Id}", request.Section.Id);
            return new() { Status = false, Message = "Error updating section: " + ex.Message, Code = 500 };
        }
    }

    public override async Task<ResponseDto> DeleteSection(DeleteSectionRequest request, ServerCallContext context)
    {
        try
        {
            var result = await _mediator.Send(new DeleteSectionCommandReq(request.Id));

            return new() { Status = result.Status, Message = result.Message, Code = result.Code };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting section with ID: {Id}", request.Id);
            return new() { Status = false, Message = "Error deleting section: " + ex.Message, Code = 500 };
        }
    }

    #endregion

    #region Representation Types

    public override async Task<RepresentationTypesResponse> GetRepresentationTypes(RepresentationTypesRequest request,
        ServerCallContext context)
    {
        try
        {
            var parameter = new Shop.Application.DTOs.Paging.ParameterDto
            {
                PageId = request.PageId, Take = request.Take, SearchKey = request.SearchKey
            };

            var result = await _mediator.Send(new GetRepresentationTypesPagingQueryReq(parameter));

            var response = new RepresentationTypesResponse
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code,
                TotalRow = result.TotalRow,
                PageId = result.PageId,
                Take = result.Take
            };

            foreach (var type in result.Items)
            {
                response.Items.Add(new RepresentationTypeDto
                {
                    Id = type.Id,
                    Title = type.Title,
                    IsActive = type.IsActive,
                    DisplayOrder = type.DisplayOrder,
                    CreatedDate = type.CreatedDate
                });
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving representation types");
            return new RepresentationTypesResponse
            {
                Status = false, Message = "Error retrieving representation types: " + ex.Message, Code = 500
            };
        }
    }

    public override async Task<ResponseDto> CreateRepresentationType(CreateRepresentationTypeRequest request,
        ServerCallContext context)
    {
        try
        {
            RepresentationTypeDto typeDto = new()
            {
                Title = request.Type.Title,
                IsActive = request.Type.IsActive,
                DisplayOrder = request.Type.DisplayOrder
            };

            var result = await _mediator.Send(new CreateRepresentationTypeCommandReq(typeDto));

            return new() { Status = result.Status, Message = result.Message, Code = result.Code };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating representation type");
            return new() { Status = false, Message = "Error creating representation type: " + ex.Message, Code = 500 };
        }
    }

    public override async Task<ResponseDto> UpdateRepresentationType(UpdateRepresentationTypeRequest request,
        ServerCallContext context)
    {
        try
        {
            RepresentationTypeDto typeDto = new()
            {
                Id = request.Type.Id,
                Title = request.Type.Title,
                IsActive = request.Type.IsActive,
                DisplayOrder = request.Type.DisplayOrder
            };

            var result = await _mediator.Send(new UpdateRepresentationTypeCommandReq(typeDto));

            return new() { Status = result.Status, Message = result.Message, Code = result.Code };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating representation type with ID: {Id}", request.Type.Id);
            return new() { Status = false, Message = "Error updating representation type: " + ex.Message, Code = 500 };
        }
    }

    public override async Task<ResponseDto> DeleteRepresentationType(DeleteRepresentationTypeRequest request,
        ServerCallContext context)
    {
        try
        {
            var result = await _mediator.Send(new DeleteRepresentationTypeCommandReq(request.Id));

            return new() { Status = result.Status, Message = result.Message, Code = result.Code };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting representation type with ID: {Id}", request.Id);
            return new() { Status = false, Message = "Error deleting representation type: " + ex.Message, Code = 500 };
        }
    }

    #endregion

    #region Representations

    public override async Task<RepresentationsResponse> GetRepresentations(RepresentationsRequest request,
        ServerCallContext context)
    {
        try
        {
            var parameter = new Shop.Application.DTOs.Representation.RepresentationParameterDto
            {
                PageId = request.PageId, Take = request.Take, SearchKey = request.SearchKey, TypeId = request.TypeId
            };

            var result = await _mediator.Send(new GetRepresentationsPagingQueryReq(parameter));

            var response = new RepresentationsResponse
            {
                Status = result.Status,
                Message = result.Message,
                Code = result.Code,
                TotalRow = result.TotalRow,
                PageId = result.PageId,
                Take = result.Take
            };

            foreach (var representation in result.Items)
            {
                RepresentationDto repDto = new()
                {
                    Id = representation.Id,
                    Title = representation.Title,
                    TypeId = representation.TypeId,
                    TypeTitle = representation.TypeTitle,
                    IsActive = representation.IsActive,
                    DiscountPercent = representation.DiscountPercent,
                    DisplayOrder = representation.DisplayOrder,
                    CreatedDate = representation.CreatedDate
                };

                if (representation.ProductIds != null)
                {
                    foreach (var productId in representation.ProductIds)
                    {
                        repDto.ProductIds.Add(productId);
                    }
                }

                response.Items.Add(repDto);
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving representations");
            return new RepresentationsResponse
            {
                Status = false, Message = "Error retrieving representations: " + ex.Message, Code = 500
            };
        }
    }

    public override async Task<ResponseDto> CreateRepresentation(CreateRepresentationRequest request,
        ServerCallContext context)
    {
        try
        {
            RepresentationDto representationDto = new()
            {
                Title = request.Representation.Title,
                TypeId = request.Representation.TypeId,
                IsActive = request.Representation.IsActive,
                DiscountPercent = request.Representation.DiscountPercent,
                DisplayOrder = request.Representation.DisplayOrder,
                ProductIds = request.Representation.ProductIds.ToList()
            };

            var result = await _mediator.Send(new CreateRepresentationCommandReq(representationDto));

            return new() { Status = result.Status, Message = result.Message, Code = result.Code };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating representation");
            return new() { Status = false, Message = "Error creating representation: " + ex.Message, Code = 500 };
        }
    }

    public override async Task<ResponseDto> UpdateRepresentation(UpdateRepresentationRequest request,
        ServerCallContext context)
    {
        try
        {
            RepresentationDto representationDto = new()
            {
                Id = request.Representation.Id,
                Title = request.Representation.Title,
                TypeId = request.Representation.TypeId,
                IsActive = request.Representation.IsActive,
                DiscountPercent = request.Representation.DiscountPercent,
                DisplayOrder = request.Representation.DisplayOrder,
                ProductIds = request.Representation.ProductIds.ToList()
            };

            var result = await _mediator.Send(new UpdateRepresentationCommandReq(representationDto));

            return new() { Status = result.Status, Message = result.Message, Code = result.Code };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating representation with ID: {Id}", request.Representation.Id);
            return new() { Status = false, Message = "Error updating representation: " + ex.Message, Code = 500 };
        }
    }

    public override async Task<ResponseDto> DeleteRepresentation(DeleteRepresentationRequest request,
        ServerCallContext context)
    {
        try
        {
            var result = await _mediator.Send(new DeleteRepresentationCommandReq(request.Id));

            return new() { Status = result.Status, Message = result.Message, Code = result.Code };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting representation with ID: {Id}", request.Id);
            return new() { Status = false, Message = "Error deleting representation: " + ex.Message, Code = 500 };
        }
    }

    #endregion
}