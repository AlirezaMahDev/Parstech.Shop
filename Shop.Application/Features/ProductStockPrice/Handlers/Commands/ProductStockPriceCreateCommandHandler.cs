using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.ProductStockPrice;
using Shop.Application.DTOs.User;
using Shop.Application.Features.Product.Requests.Commands;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Application.Features.ProductGallery.Requests.Commands;
using Shop.Application.Features.ProductStockPrice.Requests.Commands;
using Shop.Application.Features.User.Requests.Commands;
using Shop.Application.Features.UserStore.Requests.Commands;

namespace Shop.Application.Features.ProductStockPrice.Handlers.Commands
{
    public class ProductStockPriceCreateCommandHandler : IRequestHandler<ProductStockPriceCreateCommandReq, ProductStockPriceDto>
    {
        private IProductRepository _productRep;
        private IProductStockPriceRepository _productStockRep;
        private IUserStoreRepository _userStoreRep;
        private IMapper _mapper;
        private IMediator _mediator;

        public ProductStockPriceCreateCommandHandler(IProductRepository productRep,
            IUserStoreRepository userStoreRep,
            IMapper mapper,
            IMediator mediator,
            IProductStockPriceRepository productStockRep)
        {
            _productRep = productRep;
            _userStoreRep = userStoreRep;
            _mapper = mapper;
            _mediator = mediator;
            _productStockRep = productStockRep;
        }
        public async Task<ProductStockPriceDto> Handle(ProductStockPriceCreateCommandReq request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Domain.Models.ProductStockPrice>(request.ProductStockPriceDto);

            //var Store =await _userStoreRep.GetAsync(product.StoreId);
            item.Product = null;
            item.Rep = null;
            var productResult=await _productStockRep.AddAsync(item);

           
            return _mapper.Map<ProductStockPriceDto>(productResult);
        }
    }
}
