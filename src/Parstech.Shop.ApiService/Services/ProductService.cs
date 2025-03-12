using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Parstech.Shop.Shared.Protos.Product;
using Shop.Application.Features.Product.Requests.Commands;
using Shop.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.ApiService.Services
{
    public class ProductService : Parstech.Shop.Shared.Protos.Product.ProductService.ProductServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<Product> GetProduct(ProductRequest request, ServerCallContext context)
        {
            var product = await _mediator.Send(new ProductReadCommandReq(request.ProductId));
            return _mapper.Map<Product>(product);
        }

        public override async Task<ProductPaging> GetProducts(ProductPagingRequest request, ServerCallContext context)
        {
            var parameters = _mapper.Map<Shop.Application.DTOs.Product.ProductParameterDto>(request.Parameter);
            var result = await _mediator.Send(new IntegratedProductsPagingQueryReq(
                _mapper.Map<Shop.Application.DTOs.Product.ProductSearchParameterDto>(request.Parameter), 
                request.UserName));

            var response = new ProductPaging
            {
                CurrentPage = result.CurrentPage,
                PageCount = result.PageCount,
                Searched = result.Searched
            };

            foreach (var product in result.ProductList)
            {
                response.ProductList.Add(_mapper.Map<Product>(product));
            }

            return response;
        }

        public override async Task<ProductPaging> SearchProducts(ProductSearchParameter request, ServerCallContext context)
        {
            var result = await _mediator.Send(new SearchProductQueryReq(request.Filter, request.Take));
            
            var response = new ProductPaging
            {
                CurrentPage = 1,
                PageCount = 1,
                Searched = result.Count()
            };

            foreach (var product in result)
            {
                response.ProductList.Add(_mapper.Map<Product>(product));
            }

            return response;
        }

        public override async Task<ProductDetailShow> GetProductDetails(ProductRequest request, ServerCallContext context)
        {
            var productDetail = await _mediator.Send(new ProductDetailQueryReq(request.ProductId, null));
            return _mapper.Map<ProductDetailShow>(productDetail);
        }
    }
}
