using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Brand;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.Features.Brand.Requests.Commands;
using Shop.Application.Features.ProductGallery.Requests.Commands;
using Shop.Application.Generator;
using Shop.Domain.Models;

namespace Shop.Application.Features.Brand.Handlers.Commands
{
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
}
