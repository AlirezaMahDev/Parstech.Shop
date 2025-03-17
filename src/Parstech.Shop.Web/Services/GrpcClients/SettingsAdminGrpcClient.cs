using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.Web.Services.GrpcClients;

public class SettingsAdminGrpcClient : GrpcClientBase, ISettingsAdminGrpcClient
{
    private readonly SettingsAdminService.SettingsAdminServiceClient _client;

    public SettingsAdminGrpcClient(IConfiguration configuration) : base(configuration)
    {
        _client = new SettingsAdminService.SettingsAdminServiceClient(Channel);
    }

    #region Site Settings

    public async Task<SiteDto> GetSiteSettingsAsync(int id)
    {
        var request = new SiteSettingsRequest { Id = id };
        var response = await _client.GetSiteSettingsAsync(request);

        if (!response.Status)
        {
            throw new($"Failed to get site settings: {response.Message}");
        }

        return new SiteDto
        {
            Id = response.Site.Id,
            SiteName = response.Site.SiteName,
            SiteTitle = response.Site.SiteTitle,
            SiteDescription = response.Site.SiteDescription,
            SiteCopyright = response.Site.SiteCopyright,
            SiteAddress = response.Site.SiteAddress,
            SitePhone = response.Site.SitePhone,
            SiteEmail = response.Site.SiteEmail,
            SiteLogo = response.Site.SiteLogo,
            SiteFavicon = response.Site.SiteFavicon,
            SiteSlogan = response.Site.SiteSlogan,
            SiteTerms = response.Site.SiteTerms,
            SiteAbout = response.Site.SiteAbout,
            SitePrivacy = response.Site.SitePrivacy,
            EnableRegistration = response.Site.EnableRegistration,
            EnableBlog = response.Site.EnableBlog,
            EnableContact = response.Site.EnableContact,
            EnableShop = response.Site.EnableShop,
            CurrencySymbol = response.Site.CurrencySymbol,
            CurrencyName = response.Site.CurrencyName
        };
    }

    public async Task<ResponseDto> UpdateSiteSettingsAsync(int id, SiteDto siteDto)
    {
        var request = new UpdateSiteSettingsRequest
        {
            Id = id,
            Site = new SiteSettingsDto
            {
                Id = siteDto.Id,
                SiteName = siteDto.SiteName,
                SiteTitle = siteDto.SiteTitle,
                SiteDescription = siteDto.SiteDescription,
                SiteCopyright = siteDto.SiteCopyright,
                SiteAddress = siteDto.SiteAddress,
                SitePhone = siteDto.SitePhone,
                SiteEmail = siteDto.SiteEmail,
                SiteLogo = siteDto.SiteLogo,
                SiteFavicon = siteDto.SiteFavicon,
                SiteSlogan = siteDto.SiteSlogan,
                SiteTerms = siteDto.SiteTerms,
                SiteAbout = siteDto.SiteAbout,
                SitePrivacy = siteDto.SitePrivacy,
                EnableRegistration = siteDto.EnableRegistration,
                EnableBlog = siteDto.EnableBlog,
                EnableContact = siteDto.EnableContact,
                EnableShop = siteDto.EnableShop,
                CurrencySymbol = siteDto.CurrencySymbol,
                CurrencyName = siteDto.CurrencyName
            }
        };

        var response = await _client.UpdateSiteSettingsAsync(request);

        return new ResponseDto { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    #endregion

    #region SEO Settings

    public async Task<SeoDto> GetSeoSettingsAsync(int id)
    {
        var request = new SeoSettingsRequest { Id = id };
        var response = await _client.GetSeoSettingsAsync(request);

        if (!response.Status)
        {
            throw new($"Failed to get SEO settings: {response.Message}");
        }

        return new SeoDto
        {
            Id = response.Seo.Id,
            MetaTitle = response.Seo.MetaTitle,
            MetaDescription = response.Seo.MetaDescription,
            MetaKeywords = response.Seo.MetaKeywords,
            CanonicalUrl = response.Seo.CanonicalUrl,
            RobotsTxt = response.Seo.RobotsTxt,
            GoogleAnalytics = response.Seo.GoogleAnalytics,
            GoogleTagManager = response.Seo.GoogleTagManager,
            GoogleSiteVerification = response.Seo.GoogleSiteVerification,
            BingSiteVerification = response.Seo.BingSiteVerification,
            EnableSitemap = response.Seo.EnableSitemap,
            EnableSchemaMarkup = response.Seo.EnableSchemaMarkup,
            CustomHeadScripts = response.Seo.CustomHeadScripts,
            CustomFooterScripts = response.Seo.CustomFooterScripts
        };
    }

    public async Task<ResponseDto> UpdateSeoSettingsAsync(int id, SeoDto seoDto)
    {
        var request = new UpdateSeoSettingsRequest
        {
            Id = id,
            Seo = new SeoSettingsDto
            {
                Id = seoDto.Id,
                MetaTitle = seoDto.MetaTitle,
                MetaDescription = seoDto.MetaDescription,
                MetaKeywords = seoDto.MetaKeywords,
                CanonicalUrl = seoDto.CanonicalUrl,
                RobotsTxt = seoDto.RobotsTxt,
                GoogleAnalytics = seoDto.GoogleAnalytics,
                GoogleTagManager = seoDto.GoogleTagManager,
                GoogleSiteVerification = seoDto.GoogleSiteVerification,
                BingSiteVerification = seoDto.BingSiteVerification,
                EnableSitemap = seoDto.EnableSitemap,
                EnableSchemaMarkup = seoDto.EnableSchemaMarkup,
                CustomHeadScripts = seoDto.CustomHeadScripts,
                CustomFooterScripts = seoDto.CustomFooterScripts
            }
        };

        var response = await _client.UpdateSeoSettingsAsync(request);

        return new ResponseDto { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    #endregion

    #region Social Links

    public async Task<SocialDto> GetSocialLinksAsync(int id)
    {
        var request = new SocialLinksRequest { Id = id };
        var response = await _client.GetSocialLinksAsync(request);

        if (!response.Status)
        {
            throw new($"Failed to get social links: {response.Message}");
        }

        return new SocialDto
        {
            Id = response.Social.Id,
            Facebook = response.Social.Facebook,
            Twitter = response.Social.Twitter,
            Instagram = response.Social.Instagram,
            Linkedin = response.Social.Linkedin,
            Youtube = response.Social.Youtube,
            Pinterest = response.Social.Pinterest,
            Telegram = response.Social.Telegram,
            Whatsapp = response.Social.Whatsapp,
            EnableSocialLogin = response.Social.EnableSocialLogin,
            EnableFacebookLogin = response.Social.EnableFacebookLogin,
            EnableGoogleLogin = response.Social.EnableGoogleLogin
        };
    }

    public async Task<ResponseDto> UpdateSocialLinksAsync(int id, SocialDto socialDto)
    {
        var request = new UpdateSocialLinksRequest
        {
            Id = id,
            Social = new SocialLinksDto
            {
                Id = socialDto.Id,
                Facebook = socialDto.Facebook,
                Twitter = socialDto.Twitter,
                Instagram = socialDto.Instagram,
                Linkedin = socialDto.Linkedin,
                Youtube = socialDto.Youtube,
                Pinterest = socialDto.Pinterest,
                Telegram = socialDto.Telegram,
                Whatsapp = socialDto.Whatsapp,
                EnableSocialLogin = socialDto.EnableSocialLogin,
                EnableFacebookLogin = socialDto.EnableFacebookLogin,
                EnableGoogleLogin = socialDto.EnableGoogleLogin
            }
        };

        var response = await _client.UpdateSocialLinksAsync(request);

        return new ResponseDto { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    #endregion

    #region Sections

    public async Task<Shop.Application.DTOs.Paging.PageingDto<SectionDto>> GetSectionsAsync(
        Shop.Application.DTOs.Paging.ParameterDto parameter)
    {
        var request = new SectionsRequest
        {
            SearchKey = parameter.SearchKey, PageId = parameter.PageId, Take = parameter.Take
        };

        var response = await _client.GetSectionsAsync(request);

        var result = new Shop.Application.DTOs.Paging.PageingDto<SectionDto>
        {
            Status = response.Status,
            Message = response.Message,
            Code = response.Code,
            TotalRow = response.TotalRow,
            PageId = response.PageId,
            Take = response.Take,
            Items = new List<SectionDto>()
        };

        foreach (var section in response.Items)
        {
            result.Items.Add(new SectionDto
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

        return result;
    }

    public async Task<ResponseDto> CreateSectionAsync(SectionDto sectionDto)
    {
        var request = new CreateSectionRequest
        {
            Section = new SectionDto
            {
                Title = sectionDto.Title,
                Description = sectionDto.Description,
                ImageUrl = sectionDto.ImageUrl,
                DisplayOrder = sectionDto.DisplayOrder,
                IsActive = sectionDto.IsActive,
                SectionType = sectionDto.SectionType,
                LinkUrl = sectionDto.LinkUrl,
                LinkText = sectionDto.LinkText,
                IsFeatured = sectionDto.IsFeatured
            }
        };

        var response = await _client.CreateSectionAsync(request);

        return new ResponseDto { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<ResponseDto> UpdateSectionAsync(SectionDto sectionDto)
    {
        var request = new UpdateSectionRequest
        {
            Section = new SectionDto
            {
                Id = sectionDto.Id,
                Title = sectionDto.Title,
                Description = sectionDto.Description,
                ImageUrl = sectionDto.ImageUrl,
                DisplayOrder = sectionDto.DisplayOrder,
                IsActive = sectionDto.IsActive,
                SectionType = sectionDto.SectionType,
                LinkUrl = sectionDto.LinkUrl,
                LinkText = sectionDto.LinkText,
                IsFeatured = sectionDto.IsFeatured
            }
        };

        var response = await _client.UpdateSectionAsync(request);

        return new ResponseDto { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<ResponseDto> DeleteSectionAsync(int id)
    {
        var request = new DeleteSectionRequest { Id = id };
        var response = await _client.DeleteSectionAsync(request);

        return new ResponseDto { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    #endregion

    #region Representation Types

    public async Task<Shop.Application.DTOs.Paging.PageingDto<RepresentationTypeDto>> GetRepresentationTypesAsync(
        Shop.Application.DTOs.Paging.ParameterDto parameter)
    {
        var request = new RepresentationTypesRequest
        {
            SearchKey = parameter.SearchKey, PageId = parameter.PageId, Take = parameter.Take
        };

        var response = await _client.GetRepresentationTypesAsync(request);

        var result = new Shop.Application.DTOs.Paging.PageingDto<RepresentationTypeDto>
        {
            Status = response.Status,
            Message = response.Message,
            Code = response.Code,
            TotalRow = response.TotalRow,
            PageId = response.PageId,
            Take = response.Take,
            Items = new List<RepresentationTypeDto>()
        };

        foreach (var type in response.Items)
        {
            result.Items.Add(new RepresentationTypeDto
            {
                Id = type.Id,
                Title = type.Title,
                IsActive = type.IsActive,
                DisplayOrder = type.DisplayOrder,
                CreatedDate = type.CreatedDate
            });
        }

        return result;
    }

    public async Task<ResponseDto> CreateRepresentationTypeAsync(RepresentationTypeDto typeDto)
    {
        var request = new CreateRepresentationTypeRequest
        {
            Type = new RepresentationTypeDto
            {
                Title = typeDto.Title, IsActive = typeDto.IsActive, DisplayOrder = typeDto.DisplayOrder
            }
        };

        var response = await _client.CreateRepresentationTypeAsync(request);

        return new ResponseDto { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<ResponseDto> UpdateRepresentationTypeAsync(RepresentationTypeDto typeDto)
    {
        var request = new UpdateRepresentationTypeRequest
        {
            Type = new RepresentationTypeDto
            {
                Id = typeDto.Id,
                Title = typeDto.Title,
                IsActive = typeDto.IsActive,
                DisplayOrder = typeDto.DisplayOrder
            }
        };

        var response = await _client.UpdateRepresentationTypeAsync(request);

        return new ResponseDto { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<ResponseDto> DeleteRepresentationTypeAsync(int id)
    {
        var request = new DeleteRepresentationTypeRequest { Id = id };
        var response = await _client.DeleteRepresentationTypeAsync(request);

        return new ResponseDto { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    #endregion

    #region Representations

    public async Task<Shop.Application.DTOs.Paging.PageingDto<RepresentationDto>> GetRepresentationsAsync(
        RepresentationParameterDto parameter)
    {
        var request = new RepresentationsRequest
        {
            SearchKey = parameter.SearchKey,
            PageId = parameter.PageId,
            Take = parameter.Take,
            TypeId = parameter.TypeId
        };

        var response = await _client.GetRepresentationsAsync(request);

        var result = new Shop.Application.DTOs.Paging.PageingDto<RepresentationDto>
        {
            Status = response.Status,
            Message = response.Message,
            Code = response.Code,
            TotalRow = response.TotalRow,
            PageId = response.PageId,
            Take = response.Take,
            Items = new List<RepresentationDto>()
        };

        foreach (var representation in response.Items)
        {
            var repDto = new RepresentationDto
            {
                Id = representation.Id,
                Title = representation.Title,
                TypeId = representation.TypeId,
                TypeTitle = representation.TypeTitle,
                IsActive = representation.IsActive,
                DiscountPercent = representation.DiscountPercent,
                DisplayOrder = representation.DisplayOrder,
                CreatedDate = representation.CreatedDate,
                ProductIds = representation.ProductIds.ToList()
            };

            result.Items.Add(repDto);
        }

        return result;
    }

    public async Task<ResponseDto> CreateRepresentationAsync(RepresentationDto representationDto)
    {
        var request = new CreateRepresentationRequest
        {
            Representation = new RepresentationDto
            {
                Title = representationDto.Title,
                TypeId = representationDto.TypeId,
                IsActive = representationDto.IsActive,
                DiscountPercent = representationDto.DiscountPercent,
                DisplayOrder = representationDto.DisplayOrder
            }
        };

        if (representationDto.ProductIds != null)
        {
            foreach (var productId in representationDto.ProductIds)
            {
                request.Representation.ProductIds.Add(productId);
            }
        }

        var response = await _client.CreateRepresentationAsync(request);

        return new ResponseDto { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<ResponseDto> UpdateRepresentationAsync(RepresentationDto representationDto)
    {
        var request = new UpdateRepresentationRequest
        {
            Representation = new RepresentationDto
            {
                Id = representationDto.Id,
                Title = representationDto.Title,
                TypeId = representationDto.TypeId,
                IsActive = representationDto.IsActive,
                DiscountPercent = representationDto.DiscountPercent,
                DisplayOrder = representationDto.DisplayOrder
            }
        };

        if (representationDto.ProductIds != null)
        {
            foreach (var productId in representationDto.ProductIds)
            {
                request.Representation.ProductIds.Add(productId);
            }
        }

        var response = await _client.UpdateRepresentationAsync(request);

        return new ResponseDto { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    public async Task<ResponseDto> DeleteRepresentationAsync(int id)
    {
        var request = new DeleteRepresentationRequest { Id = id };
        var response = await _client.DeleteRepresentationAsync(request);

        return new ResponseDto { Status = response.Status, Message = response.Message, Code = response.Code };
    }

    #endregion
}