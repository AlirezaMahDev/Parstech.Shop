using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.SocialSetting.Requests.Commands;
using Parstech.Shop.ApiService.Application.Generator;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.SocialSetting.Handlers.Commands;

public class SocialSettingUpdateCommandHandler : IRequestHandler<SocialSettingUpdateCommandReq, SocialSettingDto>
{
    private readonly ISocialSettingRepository _socialSettingRep;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public SocialSettingUpdateCommandHandler(ISocialSettingRepository socialSettingRep,
        IMapper mapper,
        IMediator mediator)
    {
        _socialSettingRep = socialSettingRep;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<SocialSettingDto> Handle(SocialSettingUpdateCommandReq request,
        CancellationToken cancellationToken)
    {
        Shared.Models.SocialSetting? socialSetting = await _socialSettingRep.GetAsync(request.socialSettingDto.Id);

        if (request.socialSettingDto.ImageFile != null)
        {
            try
            {
                request.socialSettingDto.Image = NameGenerator.GenerateUniqCode() +
                                                 Path.GetExtension(request.socialSettingDto.ImageFile.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/Shared/Images",
                    request.socialSettingDto.Image);
                using (FileStream stream = new(imagePath, FileMode.Create))
                {
                    request.socialSettingDto.ImageFile.CopyTo(stream);
                }

                if (socialSetting.Image != null)
                {
                    string tempFile = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot/Shared/Images",
                        socialSetting.Image);
                    using (FileStream fs = new(tempFile, FileMode.Open)) { }

                    File.Delete(tempFile);
                }
            }
            catch (Exception e)
            {
            }
        }

        _mapper.Map(request.socialSettingDto, socialSetting);
        await _socialSettingRep.UpdateAsync(socialSetting);
        SocialSettingDto result = await _mediator.Send(new SocialSettingReadCommandReq(socialSetting.Id));
        return result;
    }
}