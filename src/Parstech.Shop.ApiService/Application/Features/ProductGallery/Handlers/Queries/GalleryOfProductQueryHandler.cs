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
using Shop.Application.Features.ProductGallery.Requests.Queries;
using Shop.Application.Features.ProductProperty.Requests.Queries;

namespace Shop.Application.Features.ProductGallery.Handlers.Queries
{
    public class GalleryOfProductQueryHandler : IRequestHandler<GalleryOfProductQueryReq, ProductGalleryDto>
    {
        private readonly IProductGallleryRepository _productGallleryRep;
        private readonly IMapper _mapper;

        public GalleryOfProductQueryHandler(IProductGallleryRepository productGallleryRep, IMapper mapper)
        {
            _productGallleryRep = productGallleryRep;
            _mapper = mapper;
        }
        public async Task<ProductGalleryDto> Handle(GalleryOfProductQueryReq request, CancellationToken cancellationToken)
        {
            var result =await _productGallleryRep.GetGalleryByProduct(request.productId);
            return _mapper.Map<ProductGalleryDto>(result);
        }
    }
}
