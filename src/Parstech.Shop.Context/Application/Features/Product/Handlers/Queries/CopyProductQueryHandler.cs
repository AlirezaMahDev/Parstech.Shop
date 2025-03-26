using AutoMapper;
using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.ProductRelated.Requests.Commnads;
using Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Commands;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.DTOs.ProductCategury;
using Parstech.Shop.Context.Application.DTOs.ProductGallery;
using Parstech.Shop.Context.Application.DTOs.ProductProperty;
using Parstech.Shop.Context.Application.DTOs.ProductRelated;
using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;
using Parstech.Shop.Context.Application.Features.Product.Requests.Commands;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;
using Parstech.Shop.Context.Application.Features.ProductCategury.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductGallery.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductProperty.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Product.Handlers.Queries;

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