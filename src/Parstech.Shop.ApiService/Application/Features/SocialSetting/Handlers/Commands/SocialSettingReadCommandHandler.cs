using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.SocialSetting.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.SocialSetting.Handlers.Commands;

public class SocialSettingReadCommandHandler : IRequestHandler<SocialSettingReadCommandReq, SocialSettingDto>
{
    private readonly ISocialSettingRepository _socialSettingRep;
    private readonly IMapper _mapper;

    public SocialSettingReadCommandHandler(ISocialSettingRepository socialSettingRep, IMapper mapper)
    {
        _socialSettingRep = socialSettingRep;
        _mapper = mapper;
    }

    public async Task<SocialSettingDto> Handle(SocialSettingReadCommandReq request, CancellationToken cancellationToken)
    {
        Shared.Models.SocialSetting? socialSetting = await _socialSettingRep.GetAsync(request.id);
        return _mapper.Map<SocialSettingDto>(socialSetting);
    }
}

public class
    SocialSettingListReadCommandHandler : IRequestHandler<SocialSettingListReadCommandReq, List<SocialSettingDto>>
{
    private readonly ISocialSettingRepository _socialSettingRep;
    private readonly IMapper _mapper;

    public SocialSettingListReadCommandHandler(ISocialSettingRepository socialSettingRep, IMapper mapper)
    {
        _socialSettingRep = socialSettingRep;
        _mapper = mapper;
    }

    public async Task<List<SocialSettingDto>> Handle(SocialSettingListReadCommandReq request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Shared.Models.SocialSetting> socialSetting = await _socialSettingRep.GetAll();
        return _mapper.Map<List<SocialSettingDto>>(socialSetting);
    }
}