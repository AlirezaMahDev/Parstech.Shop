﻿using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.SectionDetail;
using Parstech.Shop.Context.Application.Features.SectionDetail.Requests.Commands;
using Parstech.Shop.Context.Application.Generator;

namespace Parstech.Shop.Context.Application.Features.SectionDetail.Handlers.Commands;

public class SectionDetailUpdateCommandHandler : IRequestHandler<SectionDetailUpdateCommandReq, SectionDetailDto>
{
    private ISectionDetailRepository _sectionDetailRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public SectionDetailUpdateCommandHandler(ISectionDetailRepository sectionDetailRep, IMapper mapper, IMediator madiiator)
    {
        _sectionDetailRep = sectionDetailRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }

    public async Task<SectionDetailDto> Handle(SectionDetailUpdateCommandReq request, CancellationToken cancellationToken)
    {
        var sectionDetail = await _sectionDetailRep.GetAsync(request.SectionDetailDto.Id);
        //if (request.SectionDetailDto.Image == null)
        //{
        //    request.SectionDetailDto.Image = sectionDetail.Image;
        //}
           
        //request.SectionDetailDto.BackgroundImage = sectionDetail.BackgroundImage;
        if (request.SectionDetailDto.ImageFile != null)
        {

            try
            {
                request.SectionDetailDto.Image = NameGenerator.GenerateUniqCode() + Path.GetExtension(request.SectionDetailDto.ImageFile.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images", request.SectionDetailDto.Image);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    request.SectionDetailDto.ImageFile.CopyTo(stream);
                }
                if (sectionDetail.Image != null)
                {
                    string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images", sectionDetail.Image);

                    try
                    {
                        using (FileStream fs = new(tempFile, FileMode.Open)) { }
                        File.Delete(tempFile);
                    }
                    catch (Exception e)
                    {
                    }

                        
                }
            }
            catch (Exception e)
            {
            }
        }
            
        if (request.SectionDetailDto.BackgroundImageFile != null)
        {

            try
            {
                request.SectionDetailDto.BackgroundImage = NameGenerator.GenerateUniqCode() + Path.GetExtension(request.SectionDetailDto.BackgroundImageFile.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images", request.SectionDetailDto.BackgroundImage);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    request.SectionDetailDto.BackgroundImageFile.CopyTo(stream);
                }
                if (sectionDetail.BackgroundImage != null)
                {
                    string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images", sectionDetail.BackgroundImage);
                    using (FileStream fs = new(tempFile, FileMode.Open)) { }
                    File.Delete(tempFile);
                }
            }
            catch (Exception e)
            {
            }
        }
        sectionDetail = _mapper.Map(request.SectionDetailDto, sectionDetail);
        await _sectionDetailRep.UpdateAsync(sectionDetail);
        var result = await _madiiator.Send(new SectionDetailReadCommandReq(sectionDetail.Id));
        return result;
    }
}