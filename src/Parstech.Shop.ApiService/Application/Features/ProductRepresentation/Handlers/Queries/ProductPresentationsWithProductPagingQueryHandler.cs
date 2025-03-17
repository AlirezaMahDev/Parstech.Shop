using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Convertor;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.Representation.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.RepresentationType.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Handlers.Queries;

public class
    ProductPresentationsWithProductPagingQueryHandler : IRequestHandler<ProductPresentationsWithProductPagingQueryReq,
    PagingDto>
{
    private readonly IProductRepresesntationRepository _productRepresentationRep;
    private readonly IProductRepository _productRep;
    private readonly IProductStockPriceRepository _productStockRep;
    private readonly IRepresentationRepository _representationRep;
    private readonly IRepresentationTypeRepository _representationTypeRep;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUserBillingRepository _userBillingRep;

    public ProductPresentationsWithProductPagingQueryHandler(IProductRepresesntationRepository productRepresentationRep,
        IProductStockPriceRepository productStockRep,
        IProductRepository productRep,
        IRepresentationRepository representationRep,
        IRepresentationTypeRepository representationTypeRep,
        IMapper mapper,
        IMediator mediator,
        IUserBillingRepository userBillingRep)
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

    public async Task<PagingDto> Handle(ProductPresentationsWithProductPagingQueryReq request,
        CancellationToken cancellationToken)
    {
        IList<ProductRepresentationDto> productDto = new List<ProductRepresentationDto>();
        IReadOnlyList<Shared.Models.ProductRepresentation> productReps = await _productRepresentationRep.GetAll();
        IEnumerable<Shared.Models.ProductRepresentation> ProductRepList =
            productReps.Where(z => z.ProductStockPriceId == request.Parameter.ProductId);
        foreach (Shared.Models.ProductRepresentation item in ProductRepList)
        {
            ProductRepresentationDto? PDto = _mapper.Map<ProductRepresentationDto>(item);
            Shared.Models.ProductStockPrice? ps = await _productStockRep.GetAsync(item.ProductStockPriceId);
            var Product = await _mediator.Send(new ProductReadCommandReq(ps.ProductId));
            PDto.ProductName = Product.Name;

            var type = await _mediator.Send(new RepresentationTypeReadCommandReq(item.TypeId));
            PDto.Type = type.Type;

            var representation = await _mediator.Send(new RepresentationReadCommandReq(item.RepresntationId));
            PDto.RepresntationName = representation.Name;

            Shared.Models.UserBilling? userBilling = await _userBillingRep.GetUserBillingByUserId(item.UserId);
            PDto.UserName = $"{userBilling.FirstName} {userBilling.LastName}";
            PDto.CreateDateShamsi = item.CreateDate.ToShamsi();
            productDto.Add(PDto);
        }

        IQueryable<ProductRepresentationDto> list = productDto.AsQueryable();
        PagingDto response = new();
        int skip = (request.Parameter.CurrentPage - 1) * request.Parameter.TakePage;
        response.CurrentPage = request.Parameter.CurrentPage;
        int count = list.Count();
        response.PageCount = count / request.Parameter.TakePage;
        response.List = list.Skip(skip).Take(request.Parameter.TakePage).ToArray();
        return response;
    }
}