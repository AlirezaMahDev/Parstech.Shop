using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.SiteSeting.Requests.Commands;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Features.SiteSeting.Handlers.Commands;

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
        SiteSetting? siteSetting = await _siteSettingRep.GetAsync(request.id);
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
        SiteSetting? siteSetting = await _siteSettingRep.GetAsync(request.id);
        return _mapper.Map<SeoDto>(siteSetting);
    }
}