using Grpc.Core;
using MediatR;
using Parstech.Shop.Shared.Protos.SiteSetting;
using Shop.Application.Features.SiteSeting.Requests.Queries;

namespace Parstech.Shop.ApiService.Services
{
    public class SiteSettingGrpcService : SiteSettingService.SiteSettingServiceBase
    {
        private readonly IMediator _mediator;
        
        public SiteSettingGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public override async Task<SettingAndSeoResponse> GetSettingAndSeo(SettingAndSeoRequest request, ServerCallContext context)
        {
            try
            {
                var settings = await _mediator.Send(new GetSettingAndSeoQueryReq());
                
                var response = new SettingAndSeoResponse
                {
                    SiteSetting = new SiteSettingItem
                    {
                        Id = settings.Setting.Id,
                        SiteName = settings.Setting.SiteName ?? string.Empty,
                        SiteUrl = settings.Setting.SiteUrl ?? string.Empty,
                        SiteEmail = settings.Setting.SiteEmail ?? string.Empty,
                        SiteTel = settings.Setting.SiteTel ?? string.Empty,
                        SiteFax = settings.Setting.SiteFax ?? string.Empty,
                        SiteAddress = settings.Setting.SiteAddress ?? string.Empty,
                        SiteAbout = settings.Setting.SiteAbout ?? string.Empty,
                        SiteLogo = settings.Setting.SiteLogo ?? string.Empty,
                        SiteFavicon = settings.Setting.SiteFavicon ?? string.Empty,
                        CopyRight = settings.Setting.CopyRight ?? string.Empty,
                        MapCode = settings.Setting.MapCode ?? string.Empty,
                        EnamadCode = settings.Setting.EnamadCode ?? string.Empty,
                        SamandehiCode = settings.Setting.SamandehiCode ?? string.Empty,
                        GoogleAnalyticsCode = settings.Setting.GoogleAnalyticsCode ?? string.Empty,
                        GoogleMasterCode = settings.Setting.GoogleMasterCode ?? string.Empty,
                        GoogleRecaptchaSiteKey = settings.Setting.GoogleRecaptchaSiteKey ?? string.Empty,
                        GoogleRecaptchaSecretKey = settings.Setting.GoogleRecaptchaSecretKey ?? string.Empty,
                        SmsUserName = settings.Setting.SmsUserName ?? string.Empty,
                        SmsPassword = settings.Setting.SmsPassword ?? string.Empty,
                        SmsNumber = settings.Setting.SmsNumber ?? string.Empty,
                        MerchantId = settings.Setting.MerchantId ?? string.Empty,
                        TerminalId = settings.Setting.TerminalId ?? string.Empty,
                        TerminalKey = settings.Setting.TerminalKey ?? string.Empty,
                        WalletMerchantId = settings.Setting.WalletMerchantId ?? string.Empty,
                        WalletTerminalId = settings.Setting.WalletTerminalId ?? string.Empty,
                        WalletTerminalKey = settings.Setting.WalletTerminalKey ?? string.Empty,
                        BnplMerchantId = settings.Setting.BnplMerchantId ?? string.Empty,
                        BnplTerminalId = settings.Setting.BnplTerminalId ?? string.Empty,
                        BnplTerminalKey = settings.Setting.BnplTerminalKey ?? string.Empty
                    },
                    SeoSetting = new SeoSettingItem
                    {
                        Id = settings.Seo.Id,
                        MetaTitle = settings.Seo.MetaTitle ?? string.Empty,
                        MetaDescription = settings.Seo.MetaDescription ?? string.Empty,
                        MetaKeywords = settings.Seo.MetaKeywords ?? string.Empty,
                        CanonicalUrl = settings.Seo.CanonicalUrl ?? string.Empty,
                        RobotsTxt = settings.Seo.RobotsTxt ?? string.Empty,
                        SchemaOrg = settings.Seo.SchemaOrg ?? string.Empty,
                        OpenGraphTitle = settings.Seo.OpenGraphTitle ?? string.Empty,
                        OpenGraphDescription = settings.Seo.OpenGraphDescription ?? string.Empty,
                        OpenGraphImage = settings.Seo.OpenGraphImage ?? string.Empty,
                        TwitterTitle = settings.Seo.TwitterTitle ?? string.Empty,
                        TwitterDescription = settings.Seo.TwitterDescription ?? string.Empty,
                        TwitterImage = settings.Seo.TwitterImage ?? string.Empty
                    }
                };
                
                return response;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
        }
    }
} 