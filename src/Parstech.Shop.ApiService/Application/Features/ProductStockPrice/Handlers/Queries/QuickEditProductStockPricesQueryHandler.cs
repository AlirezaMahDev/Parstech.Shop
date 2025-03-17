using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductLog.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.User.Requests.Queries;
using Parstech.Shop.ApiService.Application.Validators.Product;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Handlers.Queries;

public class QuickEditProductStockPricesQueryHandler : IRequestHandler<QuickEditProductStockPricesQueryReq, ResponseDto>
{
    private readonly IProductRepository _productRep;
    private readonly IProductStockPriceRepository _productStockRep;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public QuickEditProductStockPricesQueryHandler(IProductRepository productRep,
        IProductStockPriceRepository productStockRep,
        IMediator mediator,
        IMapper mapper)
    {
        _productRep = productRep;
        _mediator = mediator;
        _mapper = mapper;
        _productStockRep = productStockRep;
    }

    public async Task<ResponseDto> Handle(QuickEditProductStockPricesQueryReq request,
        CancellationToken cancellationToken)
    {
        ResponseDto Response = new();
        foreach (QuickEditDto req in request.list)
        {
            void currentproductStock = await _mediator.Send(new ProductStockPriceReadCommandReq(req.id));

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
                Domain.Models.ProductStockPrice?
                    current = await _productStockRep.DapperGetProductStockPriceById(req.id);
                ProductStockPriceDto? currentDto = _mapper.Map<ProductStockPriceDto>(current);
                void edit = await _mediator.Send(new ProductStockPriceUpdateCommandReq(currentproductStock));
                await _mediator.Send(new PriceConflictsCreateLogQueryReq(request.userName, currentDto, edit));
            }

            if (currentproductStock.Quantity != req.quantity)
            {
                ProductRepresentationDto pr = new();

                void user = await _mediator.Send(new UserReadByUserNameQueryReq(request.userName));
                pr.UserId = user.Id;
                pr.Quantity = req.quantity;
                pr.ProductStockPriceId = req.id;
                void res = await _mediator.Send(new ProductRepresesntationQuickCreateCommandReq(pr));
            }


            Response.IsSuccessed = true;
        }

        return Response;
    }
}