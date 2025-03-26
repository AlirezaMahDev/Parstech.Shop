using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.SiteSetting;
using Parstech.Shop.Context.Application.Features.SiteSeting.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.SiteSeting.Handlers.Commands;

public class SiteSettingReadCommandHandler : IRequestHandler<SiteSettingReadCommandReq, SiteDto>
{
    #region Constractor
    private ISiteSettingRepository _siteSettingRep;
    private readonly IMapper _mapper;

    public SiteSettingReadCommandHandler(ISiteSettingRepository siteSettingRep, IMapper mapper)
    {
        _siteSettingRep = siteSettingRep;
        _mapper = mapper;
    }



    #endregion
        

    public async Task<SiteDto> Handle(SiteSettingReadCommandReq request, CancellationToken cancellationToken)
    {
        var siteSetting = await _siteSettingRep.GetAsync(request.id);
        return _mapper.Map<SiteDto>(siteSetting);
    }
}

public class SeoSettingReadCommandHandler : IRequestHandler<SeoSettingReadCommandReq, SeoDto>
{
    #region Constractor
    private ISiteSettingRepository _siteSettingRep;
    private readonly IMapper _mapper;

    public SeoSettingReadCommandHandler(ISiteSettingRepository siteSettingRep, IMapper mapper)
    {
        _siteSettingRep = siteSettingRep;
        _mapper = mapper;
    }

    #endregion
    public async Task<SeoDto> Handle(SeoSettingReadCommandReq request, CancellationToken cancellationToken)
    {
        var siteSetting = await _siteSettingRep.GetAsync(request.id);
        return _mapper.Map<SeoDto>(siteSetting);
    }
}