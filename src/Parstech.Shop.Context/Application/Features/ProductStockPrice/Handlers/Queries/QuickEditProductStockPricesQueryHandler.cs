using AutoMapper;
using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;
using Parstech.Shop.Context.Application.DTOs.ProductStockPrice;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.ProductLog.Requests.Queries;
using Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Queries;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;
using Parstech.Shop.Context.Application.Validators.Product;

namespace Parstech.Shop.Context.Application.Features.ProductStockPrice.Handlers.Queries;

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
        ResponseDto Response = new();
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
                ProductRepresentationDto pr = new();

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