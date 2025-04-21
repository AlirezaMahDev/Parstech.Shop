using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Product.Handlers.Queries
{
    public class GetProductsByCategoryByPagingQueryHandler : IRequestHandler<GetProductsByCategoryByPagingQueryReq, ProductPageingDto>
    {
        private readonly IProductRepository _productRep;
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRep;
        private readonly IProductGallleryRepository _gallleryRep;
        private readonly IProductTypeRepository _productTypeRep;
        private readonly IUserStoreRepository _userStoreRep;
        private readonly IProductCateguryRepository _productCateguryRep;

        public GetProductsByCategoryByPagingQueryHandler(IProductRepository productRep,
            IMapper mapper, IBrandRepository brandRep, IProductGallleryRepository gallleryRep,
            IProductTypeRepository productTypeRep, IUserStoreRepository userStoreRep,
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

        public async Task<ProductPageingDto> Handle(GetProductsByCategoryByPagingQueryReq request, CancellationToken cancellationToken)
        {
            var all = await _productRep.GetAll();
            var products = all.Where(z => z.TypeId == request.productTypeId&&z.IsActive).ToList();
            IList<ProductListShowDto> productDto = new List<ProductListShowDto>();
            foreach (var product in products)
            {
                var productCategories = await _productCateguryRep.GetCateguriesByProduct(product.Id);
                Domain.Models.ProductCategury productCategory = new Domain.Models.ProductCategury();
                foreach (var item in productCategories)
                {
                    if (item.CateguryId == request.categoryId)
                    {
                        productCategory = item;
                    }
                }

                ProductListShowDto x = new ProductListShowDto();
                x = _mapper.Map<ProductListShowDto>(product);
                
                var pic = await _gallleryRep.GetMainImageOfProduct(product.Id);
                x.Image = pic.ImageName;
                productDto.Add(x);
            }
            var result = productDto.AsQueryable();

            ProductPageingDto response = new ProductPageingDto();

            if (!string.IsNullOrEmpty(request.Parameter.Filter))
            {
                result = result.Where(p =>
                    (p.Name.Contains(request.Parameter.Filter) ||
                     (p.CateguryName.Contains(request.Parameter.Filter))));
            }
            int skip = (request.Parameter.CurrentPage - 1) * request.Parameter.TakePage;

            response.CurrentPage = request.Parameter.CurrentPage;
            int count = result.Count();
            response.PageCount = count / request.Parameter.TakePage;


            response.ProductDtos = result.Skip(skip).Take(request.Parameter.TakePage).ToArray();

            return response;
        }
    }
}
