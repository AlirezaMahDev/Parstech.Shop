using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Brand;
using Parstech.Shop.Context.Application.Features.Brand.Requests.Commands;
using Parstech.Shop.Context.Application.Generator;

namespace Parstech.Shop.Context.Application.Features.Brand.Handlers.Commands;

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
        var item = _mapper.Map<Domain.Models.Brand>(request.BrandDto);
        if (request.BrandDto.BrandFile != null)
        {
            string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images/Products", item.BrandImage);
            using (FileStream fs = new(tempFile, FileMode.Open)) { }
            try
            {
                // 2. بررسی فرمت فایل
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var fileExtension = Path.GetExtension(request.BrandDto.BrandFile.FileName).ToLower();
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
                item.BrandImage = NameGenerator.GenerateUniqCode() + Path.GetExtension(request.BrandDto.BrandFile.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images/Products", item.BrandImage);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    request.BrandDto.BrandFile.CopyTo(stream);
                }
                File.Delete(tempFile);
            }
            catch (Exception e)
            {
            }
        }
        var Result = await _brandRep.UpdateAsync(item);
        return _mapper.Map<BrandDto>(Result);
    }
}