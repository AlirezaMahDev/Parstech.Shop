using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.ProductCategury.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductProperty.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductRelated.Requests.Commnads;
using Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

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

    public CopyProductQueryHandler(IProductRepository productRep,
        IMapper mapper,
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
        Shared.Models.Product? product = await _productRep.GetAsync(request.productId);
        ProductDto? newProduct = _mapper.Map<ProductDto>(product);
        newProduct.Id = 0;
        await _mediator.Send(new ProductCreateCommandReq(newProduct));

        List<Shared.Models.ProductCategury> productCategories =
            await _productCateguryRep.GetCateguriesByProduct(request.productId);
        foreach (Shared.Models.ProductCategury? item in productCategories)
        {
            ProductCateguryDto? newProductCategory = _mapper.Map<ProductCateguryDto>(item);
            newProductCategory.Id = 0;
            await _mediator.Send(new ProductCateguryCreateCommandReq(newProductCategory));
        }

        Shared.Models.ProductRepresentation productRepresentation =
            await _productRepresesntationRep.GetProductRepresentationOfProduct(request.productId);
        ProductRepresentationDto? newProductRepresentation =
            _mapper.Map<ProductRepresentationDto>(productRepresentation);
        newProductRepresentation.Id = 0;
        await _mediator.Send(new ProductRepresesntationCreateCommandReq(newProductRepresentation));

        List<Shared.Models.ProductProperty> productProperties =
            await _productPropertyRep.GetPropertiesByProduct(request.productId);
        foreach (Shared.Models.ProductProperty? item in productProperties)
        {
            ProductPropertyDto? newProductProperty = _mapper.Map<ProductPropertyDto>(item);
            newProductProperty.Id = 0;
            await _mediator.Send(new ProductPropertyCreateCommandReq(newProductProperty));
        }

        List<Shared.Models.ProductGallery> productGalleries =
            await _productGallleryRep.GetGalleriesByProduct(request.productId);
        foreach (Shared.Models.ProductGallery? item in productGalleries)
        {
            ProductGalleryDto? newProductGallery = _mapper.Map<ProductGalleryDto>(item);
            newProductGallery.Id = 0;
            await _mediator.Send(new ProductGalleryCreateCommandReq(newProductGallery));
        }

        if (request.related)
        {
            List<Shared.Models.ProductRelated> productRelated =
                await _productRelatedRep.GetRelatedProductsByProductId(request.productId);
            foreach (Shared.Models.ProductRelated? item in productRelated)
            {
                ProductRelatedDto? newProductRelated = _mapper.Map<ProductRelatedDto>(item);
                newProductRelated.Id = 0;
                await _mediator.Send(new CreateProductRelatedCommandReq(newProductRelated));
            }
        }
    }
}