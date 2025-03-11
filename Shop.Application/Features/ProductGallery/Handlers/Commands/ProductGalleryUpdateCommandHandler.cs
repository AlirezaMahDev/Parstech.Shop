using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.Features.ProductGallery.Requests.Commands;
using Shop.Application.Features.ProductProperty.Requests.Commands;

namespace Shop.Application.Features.ProductGallery.Handlers.Commands
{
    public class ProductGalleryUpdateCommandHandler : IRequestHandler<ProductGalleryUpdateCommandReq, ProductGalleryDto>
    {
        private readonly IProductGallleryRepository _productGallleryRep;
        private readonly IMapper _mapper;

        public ProductGalleryUpdateCommandHandler(IProductGallleryRepository productGallleryRep, IMapper mapper)
        {
            _productGallleryRep = productGallleryRep;
            _mapper = mapper;
        }
        public async Task<ProductGalleryDto> Handle(ProductGalleryUpdateCommandReq request, CancellationToken cancellationToken)
        {
            var pgallery = _mapper.Map<Domain.Models.ProductGallery>(request.ProductGalleryDto);
            var result =await _productGallleryRep.UpdateAsync(pgallery);
            return _mapper.Map<ProductGalleryDto>(result);
        }
    }
}
