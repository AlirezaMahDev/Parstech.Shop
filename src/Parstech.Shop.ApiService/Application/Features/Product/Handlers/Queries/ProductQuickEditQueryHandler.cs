using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.Product.Requests.Queries;

namespace Shop.Application.Features.Product.Handlers.Queries
{
    public class ProductQuickEditQueryHandler : IRequestHandler<ProductQuickEditQueryReq, ProductDto>
    {

        private readonly IProductRepository _productRep;
        private readonly IUserStoreRepository _userStoreRep;
        private readonly IProductStockPriceRepository _productStockPriceRep;
        private readonly IMapper _mapper;

        public ProductQuickEditQueryHandler(IProductRepository productRep,
            IUserStoreRepository userStoreRep,
            IProductStockPriceRepository productStockPriceRep,
            IMapper mapper)
        {
            _productRep = productRep;
            _userStoreRep = userStoreRep;
            _productStockPriceRep = productStockPriceRep;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(ProductQuickEditQueryReq request, CancellationToken cancellationToken)
        {
            var product =await _productRep.GetAsync(request.ProductQuickEditDto.ProductId);
            product.Name = request.ProductQuickEditDto.Name;
            product.LatinName = request.ProductQuickEditDto.LatinName;
            product.Code = request.ProductQuickEditDto.Code;
            product.TaxId = request.ProductQuickEditDto.TaxId;
            product.BrandId = request.ProductQuickEditDto.BrandId;
            product.TypeId = request.ProductQuickEditDto.TypeId;
            product.ParentId = request.ProductQuickEditDto.ParentId;
            product.Score = request.ProductQuickEditDto.Score;
            product.ParentId=request.ProductQuickEditDto.ParentId;
            var result =await _productRep.UpdateAsync(product);


            var prosuctStock =await _productStockPriceRep.GetAsync(request.ProductQuickEditDto.Id);
            var store =await _userStoreRep.GetAsync(request.ProductQuickEditDto.StoreId);
            prosuctStock.StoreId = store.Id;
            prosuctStock.QuantityPerBundle = request.ProductQuickEditDto.QuantityPerBundle;
            prosuctStock.RepId = store.RepId;

            await _productStockPriceRep.UpdateAsync(prosuctStock);
            return _mapper.Map<ProductDto>(result);

        }
    }
}
