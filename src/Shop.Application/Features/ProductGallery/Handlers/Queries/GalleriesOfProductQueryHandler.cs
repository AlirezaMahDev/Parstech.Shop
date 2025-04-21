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
    public class GalleriesOfProductQueryHandler : IRequestHandler<GalleriesOfProductQueryReq, List<ProductGalleryDto>>
    {
        private readonly IProductRepository _productRep;
        private readonly IProductGallleryRepository _productGallleryRep;
        private readonly IMapper _mapper;

        public GalleriesOfProductQueryHandler(IProductGallleryRepository productGallleryRep,
            IProductRepository productRep,IMapper mapper)
        {
            _productGallleryRep = productGallleryRep;
            _productRep = productRep;
            _mapper = mapper;
        }
        public async Task<List<ProductGalleryDto>> Handle(GalleriesOfProductQueryReq request, CancellationToken cancellationToken)
        {
            var product =await _productRep.GetAsync(request.productId);
            if (product.TypeId == 3)
            {
                var parrent = await _productRep.GetAsync(product.ParentId.Value);
                var result = await _productGallleryRep.GetGalleriesByProduct(parrent.Id);
                return _mapper.Map<List<ProductGalleryDto>>(result);
            }
            else
            {
                var result = await _productGallleryRep.GetGalleriesByProduct(request.productId);
                return _mapper.Map<List<ProductGalleryDto>>(result);
            }
            
            
        }
    }
}
