using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.SocialSetting;
using Shop.Application.Features.SiteSeting.Requests.Commands;
using Shop.Application.Features.SocialSetting.Requests.Commands;
using Shop.Application.Generator;

namespace Shop.Application.Features.SocialSetting.Handlers.Commands
{
    public class SocialSettingUpdateCommandHandler : IRequestHandler<SocialSettingUpdateCommandReq, SocialSettingDto>
    {
        private readonly ISocialSettingRepository _socialSettingRep;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SocialSettingUpdateCommandHandler(ISocialSettingRepository socialSettingRep, IMapper mapper, IMediator mediator)
        {
            _socialSettingRep = socialSettingRep;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<SocialSettingDto> Handle(SocialSettingUpdateCommandReq request, CancellationToken cancellationToken)
        {
            var socialSetting = await _socialSettingRep.GetAsync(request.socialSettingDto.Id);
            
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
                    if (socialSetting.Image != null)
                    {
                        string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images", socialSetting.Image);
                        using (FileStream fs = new FileStream(tempFile, FileMode.Open)) { }
                        File.Delete(tempFile);
                    }
                }
                catch (Exception e)
                {
                }
            }

            _mapper.Map(request.socialSettingDto, socialSetting);
            await _socialSettingRep.UpdateAsync(socialSetting);
            var result = await _mediator.Send(new SocialSettingReadCommandReq(socialSetting.Id));
            return result;
        }
    }
}
