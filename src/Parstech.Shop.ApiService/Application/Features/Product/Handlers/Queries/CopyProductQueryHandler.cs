using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductCategury;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.DTOs.ProductRelated;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Application.Features.Product.Requests.Commands;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Application.Features.ProductCategury.Requests.Commands;
using Shop.Application.Features.ProductGallery.Requests.Commands;
using Shop.Application.Features.ProductProperty.Requests.Commands;
using Shop.Application.Features.ProductRelated.Requests.Commnads;
using Shop.Application.Features.ProductRepresentation.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Product.Handlers.Queries
{
    public class CopyProductQueryHandler : IRequestHandler<CopyProductQueryReq>
    {
        private readonly IProductRepository _productRep;
        private readonly IProductCateguryRepository _productCateguryRep;
        private readonly IProductRepresesntationRepository _productRepresesntationRep;
        private readonly IProductPropertyRepository _productPropertyRep;
        private readonly IProductGallleryRepository _productGallleryRep;
        private readonly IProductRelatedRepository _productRelatedRep;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CopyProductQueryHandler(IProductRepository productRep, IMapper mapper,
            IProductCateguryRepository productCateguryRep,
            IProductRepresesntationRepository productRepresesntationRep,
            IProductPropertyRepository productPropertyRep,
            IProductGallleryRepository productGallleryRep,
            IProductRelatedRepository productRelatedRep,
            IMediator mediator)
        {
            _productRep = productRep;
            _mapper = mapper;
            _productCateguryRep = productCateguryRep;
            _productRepresesntationRep = productRepresesntationRep;
            _productPropertyRep = productPropertyRep;
            _productGallleryRep = productGallleryRep;
            _productRelatedRep = productRelatedRep;
            _mediator = mediator;
        }
        public async Task Handle(CopyProductQueryReq request, CancellationToken cancellationToken)
        {
            //error
            var product = await _productRep.GetAsync(request.productId);
            var newProduct = _mapper.Map<ProductDto>(product);
            newProduct.Id = 0;
            await _mediator.Send(new ProductCreateCommandReq(newProduct));

            var productCategories = await _productCateguryRep.GetCateguriesByProduct(request.productId);
            foreach (var item in productCategories)
            {
                var newProductCategory = _mapper.Map<ProductCateguryDto>(item);
                newProductCategory.Id = 0;
                await _mediator.Send(new ProductCateguryCreateCommandReq(newProductCategory));
            }

            var productRepresentation = await _productRepresesntationRep.GetProductRepresentationOfProduct(request.productId);
            var newProductRepresentation = _mapper.Map<ProductRepresentationDto>(productRepresentation);
            newProductRepresentation.Id = 0;
            await _mediator.Send(new ProductRepresesntationCreateCommandReq(newProductRepresentation));

            var productProperties = await _productPropertyRep.GetPropertiesByProduct(request.productId);
            foreach(var item in productProperties)
            {
                var newProductProperty = _mapper.Map<ProductPropertyDto>(item);
                newProductProperty.Id = 0;
                await _mediator.Send(new ProductPropertyCreateCommandReq(newProductProperty));
            }

            var productGalleries = await _productGallleryRep.GetGalleriesByProduct(request.productId);
            foreach (var item in productGalleries)
            {
                var newProductGallery = _mapper.Map<ProductGalleryDto>(item);
                newProductGallery.Id = 0;
                await _mediator.Send(new ProductGalleryCreateCommandReq(newProductGallery));
            }

            if (request.related)
            {
                var productRelated = await _productRelatedRep.GetRelatedProductsByProductId(request.productId);
                foreach (var item in productRelated)
                {
                    var newProductRelated = _mapper.Map<ProductRelatedDto>(item);
                    newProductRelated.Id = 0;
                    await _mediator.Send(new CreateProductRelatedCommandReq(newProductRelated));
                }
            }

        }
    }
}
