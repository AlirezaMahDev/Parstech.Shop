using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Brand.Requests.Commands;
using Parstech.Shop.ApiService.Application.Generator;

namespace Parstech.Shop.ApiService.Application.Features.Brand.Handlers.Commands;

public class BrandUpdateCommandHandler : IRequestHandler<BrandUpdateCommandReq, BrandDto>
{
    private readonly IBrandRepository _brandRep;
    private IMapper _mapper;
    private IMediator _mediator;

    public BrandUpdateCommandHandler(IBrandRepository brandRep, IMapper mapper, IMediator mediator)
    {
        _brandRep = brandRep;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<BrandDto> Handle(BrandUpdateCommandReq request, CancellationToken cancellationToken)
    {
        Domain.Models.Brand? item = _mapper.Map<Domain.Models.Brand>(request.BrandDto);
        if (request.BrandDto.BrandFile != null)
        {
            string tempFile = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot/Shared/Images/Products",
                item.BrandImage);
            using (FileStream fs = new(tempFile, FileMode.Open)) { }

            try
            {
                // 2. بررسی فرمت فایل
                string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                string fileExtension = Path.GetExtension(request.BrandDto.BrandFile.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return null;
                }

                // 3. بررسی اندازه فایل (مثلاً حداکثر 5 مگابایت)
                int maxFileSize = 5 * 1024 * 1024; // 5 MB
                if (request.BrandDto.BrandFile.Length > maxFileSize)
                {
                    return null;
                }

                item.BrandImage = NameGenerator.GenerateUniqCode() +
                                  Path.GetExtension(request.BrandDto.BrandFile.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/Shared/Images/Products",
                    item.BrandImage);
                using (FileStream stream = new(imagePath, FileMode.Create))
                {
                    request.BrandDto.BrandFile.CopyTo(stream);
                }

                File.Delete(tempFile);
            }
            catch (Exception e)
            {
            }
        }

        Domain.Models.Brand Result = await _brandRep.UpdateAsync(item);
        return _mapper.Map<BrandDto>(Result);
    }
}