using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

public class
    GetProductsByCategoryByPagingQueryHandler : IRequestHandler<GetProductsByCategoryByPagingQueryReq,
    ProductPageingDto>
{
    private readonly IProductRepository _productRep;
    private readonly IMapper _mapper;
    private readonly IBrandRepository _brandRep;
    private readonly IProductGallleryRepository _gallleryRep;
    private readonly IProductTypeRepository _productTypeRep;
    private readonly IUserStoreRepository _userStoreRep;
    private readonly IProductCateguryRepository _productCateguryRep;

    public GetProductsByCategoryByPagingQueryHandler(IProductRepository productRep,
        IMapper mapper,
        IBrandRepository brandRep,
        IProductGallleryRepository gallleryRep,
        IProductTypeRepository productTypeRep,
        IUserStoreRepository userStoreRep,
        IProductCateguryRepository productCateguryRep)
    {
        _productRep = productRep;
        _mapper = mapper;
        _brandRep = brandRep;
        _gallleryRep = gallleryRep;
        _productTypeRep = productTypeRep;
        _userStoreRep = userStoreRep;
        _productCateguryRep = productCateguryRep;
    }

    public async Task<ProductPageingDto> Handle(GetProductsByCategoryByPagingQueryReq request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Domain.Models.Product>? all = await _productRep.GetAll();
        List<Domain.Models.Product>?
            products = all.Where(z => z.TypeId == request.productTypeId && z.IsActive).ToList();
        IList<ProductListShowDto> productDto = new List<ProductListShowDto>();
        foreach (Domain.Models.Product product in products)
        {
            List<Domain.Models.ProductCategury> productCategories =
                await _productCateguryRep.GetCateguriesByProduct(product.Id);
            Domain.Models.ProductCategury productCategory = new();
            foreach (Domain.Models.ProductCategury item in productCategories)
            {
                if (item.CateguryId == request.categoryId)
                {
                    productCategory = item;
                }
            }

            ProductListShowDto x = new();
            x = _mapper.Map<ProductListShowDto>(product);

            Domain.Models.ProductGallery? pic = await _gallleryRep.GetMainImageOfProduct(product.Id);
            x.Image = pic.ImageName;
            productDto.Add(x);
        }

        IQueryable<ProductListShowDto> result = productDto.AsQueryable();

        ProductPageingDto response = new();

        if (!string.IsNullOrEmpty(request.Parameter.Filter))
        {
            result = result.Where(p =>
                p.Name.Contains(request.Parameter.Filter) ||
                p.CateguryName.Contains(request.Parameter.Filter));
        }

        int skip = (request.Parameter.CurrentPage - 1) * request.Parameter.TakePage;

        response.CurrentPage = request.Parameter.CurrentPage;
        int count = result.Count();
        response.PageCount = count / request.Parameter.TakePage;


        response.ProductDtos = result.Skip(skip).Take(request.Parameter.TakePage).ToArray();

        return response;
    }
}