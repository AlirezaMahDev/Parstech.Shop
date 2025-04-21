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
    public class ProductGalleryReadCommandHandler : IRequestHandler<ProductGalleryReadCommandReq, ProductGalleryDto>
    {
        private readonly IProductGallleryRepository _productGallleryRep;
        private readonly IMapper _mapper;

        public ProductGalleryReadCommandHandler(IProductGallleryRepository productGallleryRep, IMapper mapper)
        {
            _productGallleryRep = productGallleryRep;
            _mapper = mapper;
        }
        public async Task<ProductGalleryDto> Handle(ProductGalleryReadCommandReq request, CancellationToken cancellationToken)
        {
            var pgallery =await _productGallleryRep.GetAsync(request.id);
            return _mapper.Map<ProductGalleryDto>(pgallery);
        }
    }
}
