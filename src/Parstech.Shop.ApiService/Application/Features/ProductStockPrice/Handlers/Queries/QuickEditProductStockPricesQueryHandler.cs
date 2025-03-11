using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Convertor;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Application.DTOs.ProductStockPrice;
using Shop.Application.DTOs.Representation;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.ProductLog.Requests.Queries;
using Shop.Application.Features.ProductRepresentation.Requests.Commands;
using Shop.Application.Features.ProductStockPrice.Requests.Commands;
using Shop.Application.Features.ProductStockPrice.Requests.Queries;
using Shop.Application.Features.User.Requests.Queries;
using Shop.Application.Validators.Product;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductStockPrice.Handlers.Queries
{

    public class QuickEditProductStockPricesQueryHandler : IRequestHandler<QuickEditProductStockPricesQueryReq, ResponseDto>
    {
        private readonly IProductRepository _productRep;
        private readonly IProductStockPriceRepository _productStockRep;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public QuickEditProductStockPricesQueryHandler(IProductRepository productRep,
            IProductStockPriceRepository productStockRep,
            IMediator mediator, IMapper mapper)
        {
            _productRep = productRep;
            _mediator = mediator;
            _mapper = mapper;
            _productStockRep = productStockRep;
        }

        public async Task<ResponseDto> Handle(QuickEditProductStockPricesQueryReq request, CancellationToken cancellationToken)
        {
            ResponseDto Response = new ResponseDto();
            foreach (var req in request.list)
            {


               
                var currentproductStock = await _mediator.Send(new ProductStockPriceReadCommandReq(req.id));
                #region Validator
                var validator = new ProductPriceValidator();
                var valid = validator.Validate(currentproductStock);
                if (!valid.IsValid)
                {
                    Response.IsSuccessed = false;
                    Response.Errors = valid.Errors;

                    return Response;
                }
                #endregion

                if (currentproductStock.SalePrice != req.price)
                {
                    currentproductStock.SalePrice = req.price;
                    //currentproductStock.Quantity = req.quantity;
                    var current = await _productStockRep.DapperGetProductStockPriceById(req.id);
                    var currentDto = _mapper.Map<ProductStockPriceDto>(current);
                    var edit = await _mediator.Send(new ProductStockPriceUpdateCommandReq(currentproductStock));
                    await _mediator.Send(new PriceConflictsCreateLogQueryReq(request.userName, currentDto, edit));
                }
                if (currentproductStock.Quantity != req.quantity)
                {
                    ProductRepresentationDto pr = new ProductRepresentationDto();

                    var user = await _mediator.Send(new UserReadByUserNameQueryReq(request.userName));
                    pr.UserId = user.Id;
                    pr.Quantity = req.quantity;
                    pr.ProductStockPriceId = req.id;
                    var res = await _mediator.Send(new ProductRepresesntationQuickCreateCommandReq(pr));
                }
                
               

                Response.IsSuccessed = true;
                
               
            }
            return Response;
        }
    }
}
