using AutoMapper;
using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Convertor;
using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.DTOs.ProductLog;
using Parstech.Shop.Context.Application.Features.ProductLog.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.ProductLog.Handlers.Queries;

public class UniqProductLogPagingQueryHandler : IRequestHandler<UniqProductLogPagingQueryReq, PagingDto>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IProductLogRepository _producLogRep;
    private readonly IProductLogTypeRepository _productLogTypeRep;
    private readonly IUserBillingRepository _userBillingRep;

    public UniqProductLogPagingQueryHandler(IMediator mediator, IMapper mapper,
        IProductLogRepository producLogRep, IProductLogTypeRepository productLogTypeRep,
        IUserBillingRepository userBillingRep)
    {
        _mediator = mediator;
        _mapper = mapper;
        _producLogRep = producLogRep;
        _productLogTypeRep = productLogTypeRep;
        _userBillingRep = userBillingRep;
    }
    public async Task<PagingDto> Handle(UniqProductLogPagingQueryReq request, CancellationToken cancellationToken)
    {
        IList<ProductLogDto> productLogDto = new List<ProductLogDto>();

        var Sale = await _producLogRep.GetSaleProductLogWithProductId(request.parameter.ProductId);
        var Price = await _producLogRep.GetPriceProductLogWithProductId(request.parameter.ProductId);
        var Base = await _producLogRep.GetBaseProductLogWithProductId(request.parameter.ProductId);
        var Discount = await _producLogRep.GetDiscountProductLogWithProductId(request.parameter.ProductId);

        foreach (var product in Sale)
        {
            if (product.OldValue != product.NewValue)
            {
                ProductLogDto dto = new();
                dto = _mapper.Map<ProductLogDto>(product);
                dto.CreateDateShamsi = product.CreateDate.ToShamsi();
                var type = await _productLogTypeRep.GetAsync(product.ProductLogTypeId);
                dto.ProductLogTypeName = type.Name;
                var user = await _userBillingRep.GetUserBillingByUserId(dto.UserId);
                dto.UserName = $"{user.FirstName} {user.LastName}";
                productLogDto.Add(dto);
            }
        }
        foreach (var product in Base)
        {
            if (product.OldValue != product.NewValue)
            {
                ProductLogDto dto = new();
                dto = _mapper.Map<ProductLogDto>(product);
                dto.CreateDateShamsi = product.CreateDate.ToShamsi();
                var type = await _productLogTypeRep.GetAsync(product.ProductLogTypeId);
                dto.ProductLogTypeName = type.Name;
                var user = await _userBillingRep.GetUserBillingByUserId(dto.UserId);
                dto.UserName = $"{user.FirstName} {user.LastName}";
                productLogDto.Add(dto);
            }
        }
        foreach (var product in Price)
        {
            if (product.OldValue != product.NewValue)
            {
                ProductLogDto dto = new();
                dto = _mapper.Map<ProductLogDto>(product);
                dto.CreateDateShamsi = product.CreateDate.ToShamsi();
                var type = await _productLogTypeRep.GetAsync(product.ProductLogTypeId);
                dto.ProductLogTypeName = type.Name;
                var user = await _userBillingRep.GetUserBillingByUserId(dto.UserId);
                dto.UserName = $"{user.FirstName} {user.LastName}";
                productLogDto.Add(dto);
            }
        }
        foreach (var product in Discount)
        {
            if (product.OldValue != product.NewValue)
            {
                ProductLogDto dto = new();
                dto = _mapper.Map<ProductLogDto>(product);
                dto.CreateDateShamsi = product.CreateDate.ToShamsi();
                var type = await _productLogTypeRep.GetAsync(product.ProductLogTypeId);
                dto.ProductLogTypeName = type.Name;
                var user = await _userBillingRep.GetUserBillingByUserId(dto.UserId);
                dto.UserName = $"{user.FirstName} {user.LastName}";
                productLogDto.Add(dto);
            }
        }

        IQueryable<ProductLogDto> result = productLogDto.AsQueryable();
        PagingDto response = new();
        int skip = (request.parameter.CurrentPage - 1) * request.parameter.TakePage;
        response.CurrentPage = request.parameter.CurrentPage;
        int count = result.Count();
        response.PageCount = count / request.parameter.TakePage;
        response.List = result.Skip(skip).Take(request.parameter.TakePage).ToArray();
        return response;

    }
}