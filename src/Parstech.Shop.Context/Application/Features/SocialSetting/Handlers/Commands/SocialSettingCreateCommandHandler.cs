﻿using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.SocialSetting.Requests.Commands;
using Parstech.Shop.Context.Application.Generator;

namespace Parstech.Shop.Context.Application.Features.SocialSetting.Handlers.Commands;

public class SocialSettingCreateCommandHandler : IRequestHandler<SocialSettingCreateCommandReq, Unit>
{
    private readonly ISocialSettingRepository _socialSettingRep;
    private readonly IMapper _mapper;


    public SocialSettingCreateCommandHandler(ISocialSettingRepository socialSettingRep, IMapper mapper)
    {
        _socialSettingRep = socialSettingRep;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(SocialSettingCreateCommandReq request, CancellationToken cancellationToken)
    {
            
        if (request.socialSettingDto.ImageFile != null)
        {
               
            try
            {
                request.socialSettingDto.Image = NameGenerator.GenerateUniqCode() + Path.GetExtension(request.socialSettingDto.ImageFile.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images", request.socialSettingDto.Image);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    request.socialSettingDto.ImageFile.CopyTo(stream);
                }

            }
            catch (Exception e)
            {
            }
        }
        var socialSetting = _mapper.Map<Domain.Models.SocialSetting>(request.socialSettingDto);

        socialSetting.SiteSettingId = 1;
        await _socialSettingRep.AddAsync(socialSetting);

            
        return Unit.Value ;
    }
}