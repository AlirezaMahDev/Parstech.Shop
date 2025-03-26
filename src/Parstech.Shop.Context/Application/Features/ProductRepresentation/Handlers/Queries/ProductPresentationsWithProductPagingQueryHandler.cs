using AutoMapper;
using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Convertor;
using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;
using Parstech.Shop.Context.Application.Features.Product.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Queries;
using Parstech.Shop.Context.Application.Features.Representation.Requests.Commands;
using Parstech.Shop.Context.Application.Features.RepresentationType.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.ProductRepresentation.Handlers.Queries;

public class ProductPresentationsWithProductPagingQueryHandler : IRequestHandler<ProductPresentationsWithProductPagingQueryReq, PagingDto>
{
    private readonly IProductRepresesntationRepository _productRepresentationRep;
    private readonly IProductRepository _productRep;
    private readonly IProductStockPriceRepository _productStockRep;
    private readonly IRepresentationRepository _representationRep;
    private readonly IRepresentationTypeRepository _representationTypeRep;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUserBillingRepository _userBillingRep;

    public ProductPresentationsWithProductPagingQueryHandler(IProductRepresesntationRepository productRepresentationRep, IProductStockPriceRepository productStockRep,IProductRepository productRep, IRepresentationRepository representationRep, IRepresentationTypeRepository representationTypeRep, IMapper mapper, IMediator mediator, IUserBillingRepository userBillingRep)
    {
        _productRepresentationRep = productRepresentationRep;
        _productRep = productRep;
        _representationRep = representationRep;
        _representationTypeRep = representationTypeRep;
        _mapper = mapper;
        _mediator = mediator;
        _userBillingRep = userBillingRep;
        _productStockRep = productStockRep;
    }
    public async Task<PagingDto> Handle(ProductPresentationsWithProductPagingQueryReq request, CancellationToken cancellationToken)
    {
        IList<ProductRepresentationDto> productDto = new List<ProductRepresentationDto>();
        var productReps = await _productRepresentationRep.GetAll();
        var ProductRepList = productReps.Where(z => z.ProductStockPriceId == request.Parameter.ProductId);
        foreach (var item in ProductRepList)
        {
            var PDto = _mapper.Map<ProductRepresentationDto>(item);
            var ps=await _productStockRep.GetAsync(item.ProductStockPriceId);
            var Product = await _mediator.Send(new ProductReadCommandReq(ps.ProductId));
            PDto.ProductName = Product.Name;

            var type = await _mediator.Send(new RepresentationTypeReadCommandReq(item.TypeId));
            PDto.Type = type.Type;

            var representation = await _mediator.Send(new RepresentationReadCommandReq(item.RepresntationId));
            PDto.RepresntationName = representation.Name;

            var userBilling = await _userBillingRep.GetUserBillingByUserId(item.UserId);
            PDto.UserName = $"{userBilling.FirstName} {userBilling.LastName}";
            PDto.CreateDateShamsi = item.CreateDate.ToShamsi();
            productDto.Add(PDto);
        }

        var list = productDto.AsQueryable();
        PagingDto response = new();
        int skip = (request.Parameter.CurrentPage - 1) * request.Parameter.TakePage;
        response.CurrentPage = request.Parameter.CurrentPage;
        int count = list.Count();
        response.PageCount = count / request.Parameter.TakePage;
        response.List = list.Skip(skip).Take(request.Parameter.TakePage).ToArray();
        return response;



    }
}