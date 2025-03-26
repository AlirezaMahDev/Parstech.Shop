using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Brand;
using Parstech.Shop.Context.Application.Features.Brand.Requests.Commands;
using Parstech.Shop.Context.Application.Generator;

namespace Parstech.Shop.Context.Application.Features.Brand.Handlers.Commands;

public class BrandCreateCommandHandler : IRequestHandler<BrandCreateCommandReq, BrandDto>
{
    private readonly IBrandRepository _brandRep; 
    private IMapper _mapper;
    private IMediator _mediator;

    public BrandCreateCommandHandler(IBrandRepository brandRep, IMapper mapper, IMediator mediator)
    {
        _brandRep = brandRep;
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<BrandDto> Handle(BrandCreateCommandReq request, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<Domain.Models.Brand>(request.BrandDto);

        if (request.BrandDto.BrandFile != null)
        {
            //string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images", siteSetting.Logo);
            //using (FileStream fs = new FileStream(tempFile, FileMode.Open)) { }
            try
            {
                item.BrandImage = NameGenerator.GenerateUniqCode() + Path.GetExtension(request.BrandDto.BrandFile.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images/Products", item.BrandImage);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    request.BrandDto.BrandFile.CopyTo(stream);
                }
                //File.Delete(tempFile);
            }
            catch (Exception e)
            {
            }
        }

        var Result = await _brandRep.AddAsync(item);
            

        return _mapper.Map<BrandDto>(Result);
    }
}