using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.SectionDetail.Requests.Commands;
using Parstech.Shop.ApiService.Application.Generator;

namespace Parstech.Shop.ApiService.Application.Features.SectionDetail.Handlers.Commands;

public class SectionDetailCreateCommandHandler : IRequestHandler<SectionDetailCreateCommandReq, SectionDetailDto>
{
    private ISectionDetailRepository _sectionDetailRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public SectionDetailCreateCommandHandler(ISectionDetailRepository sectionDetailRep,
        IMapper mapper,
        IMediator madiiator)
    {
        _sectionDetailRep = sectionDetailRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }

    public async Task<SectionDetailDto> Handle(SectionDetailCreateCommandReq request,
        CancellationToken cancellationToken)
    {
        if (request.SectionDetailDto.ImageFile != null)
        {
            try
            {
                // 2. بررسی فرمت فایل
                string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                string fileExtension = Path.GetExtension(request.SectionDetailDto.ImageFile.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return null;
                }

                // 3. بررسی اندازه فایل (مثلاً حداکثر 5 مگابایت)
                int maxFileSize = 5 * 1024 * 1024; // 5 MB
                if (request.SectionDetailDto.ImageFile.Length > maxFileSize)
                {
                    return null;
                }

                request.SectionDetailDto.Image = NameGenerator.GenerateUniqCode() +
                                                 Path.GetExtension(request.SectionDetailDto.ImageFile.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/Shared/Images",
                    request.SectionDetailDto.Image);
                using (FileStream stream = new(imagePath, FileMode.Create))
                {
                    request.SectionDetailDto.ImageFile.CopyTo(stream);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        if (request.SectionDetailDto.BackgroundImageFile != null)
        {
            try
            {
                // 2. بررسی فرمت فایل
                string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                string fileExtension =
                    Path.GetExtension(request.SectionDetailDto.BackgroundImageFile.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return null;
                }

                // 3. بررسی اندازه فایل (مثلاً حداکثر 5 مگابایت)
                int maxFileSize = 5 * 1024 * 1024; // 5 MB
                if (request.SectionDetailDto.BackgroundImageFile.Length > maxFileSize)
                {
                    return null;
                }

                request.SectionDetailDto.BackgroundImage = NameGenerator.GenerateUniqCode() +
                                                           Path.GetExtension(request.SectionDetailDto
                                                               .BackgroundImageFile.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/Shared/Images",
                    request.SectionDetailDto.BackgroundImage);
                using (FileStream stream = new(imagePath, FileMode.Create))
                {
                    request.SectionDetailDto.BackgroundImageFile.CopyTo(stream);
                }
            }
            catch (Exception e)
            {
            }
        }

        Domain.Models.SectionDetail? sectionDetail = _mapper.Map<Domain.Models.SectionDetail>(request.SectionDetailDto);
        await _sectionDetailRep.AddAsync(sectionDetail);
        SectionDetailDto result = await _madiiator.Send(new SectionDetailReadCommandReq(sectionDetail.Id));
        return result;
    }
}