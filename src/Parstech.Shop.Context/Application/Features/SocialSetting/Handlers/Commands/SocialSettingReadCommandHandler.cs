using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.SocialSetting;
using Parstech.Shop.Context.Application.Features.SocialSetting.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.SocialSetting.Handlers.Commands;

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
        var socialSetting = await _socialSettingRep.GetAsync(request.id);
        return _mapper.Map<SocialSettingDto>(socialSetting);
    }
}

public class SocialSettingListReadCommandHandler : IRequestHandler<SocialSettingListReadCommandReq, List<SocialSettingDto>>
{
    private readonly ISocialSettingRepository _socialSettingRep;
    private readonly IMapper _mapper;

    public SocialSettingListReadCommandHandler(ISocialSettingRepository socialSettingRep, IMapper mapper)
    {
        _socialSettingRep = socialSettingRep;
        _mapper = mapper;
    }

    public async Task<List<SocialSettingDto>> Handle(SocialSettingListReadCommandReq request, CancellationToken cancellationToken)
    {
        var socialSetting = await _socialSettingRep.GetAll();
        return _mapper.Map<List<SocialSettingDto>>(socialSetting);
    }
}