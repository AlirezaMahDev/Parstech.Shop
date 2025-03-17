using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Convertor;
using Parstech.Shop.ApiService.Application.Features.ProductLog.Requests.Queries;
using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Features.ProductLog.Handlers.Queries;

public class UniqProductLogPagingQueryHandler : IRequestHandler<UniqProductLogPagingQueryReq, PagingDto>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IProductLogRepository _producLogRep;
    private readonly IProductLogTypeRepository _productLogTypeRep;
    private readonly IUserBillingRepository _userBillingRep;

    public UniqProductLogPagingQueryHandler(IMediator mediator,
        IMapper mapper,
        IProductLogRepository producLogRep,
        IProductLogTypeRepository productLogTypeRep,
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

        List<Shared.Models.ProductLog> Sale =
            await _producLogRep.GetSaleProductLogWithProductId(request.parameter.ProductId);
        List<Shared.Models.ProductLog> Price =
            await _producLogRep.GetPriceProductLogWithProductId(request.parameter.ProductId);
        List<Shared.Models.ProductLog> Base =
            await _producLogRep.GetBaseProductLogWithProductId(request.parameter.ProductId);
        List<Shared.Models.ProductLog> Discount =
            await _producLogRep.GetDiscountProductLogWithProductId(request.parameter.ProductId);

        foreach (Shared.Models.ProductLog product in Sale)
        {
            if (product.OldValue != product.NewValue)
            {
                ProductLogDto dto = new();
                dto = _mapper.Map<ProductLogDto>(product);
                dto.CreateDateShamsi = product.CreateDate.ToShamsi();
                ProductLogType? type = await _productLogTypeRep.GetAsync(product.ProductLogTypeId);
                dto.ProductLogTypeName = type.Name;
                Shared.Models.UserBilling? user = await _userBillingRep.GetUserBillingByUserId(dto.UserId);
                dto.UserName = $"{user.FirstName} {user.LastName}";
                productLogDto.Add(dto);
            }
        }

        foreach (Shared.Models.ProductLog product in Base)
        {
            if (product.OldValue != product.NewValue)
            {
                ProductLogDto dto = new();
                dto = _mapper.Map<ProductLogDto>(product);
                dto.CreateDateShamsi = product.CreateDate.ToShamsi();
                ProductLogType? type = await _productLogTypeRep.GetAsync(product.ProductLogTypeId);
                dto.ProductLogTypeName = type.Name;
                Shared.Models.UserBilling? user = await _userBillingRep.GetUserBillingByUserId(dto.UserId);
                dto.UserName = $"{user.FirstName} {user.LastName}";
                productLogDto.Add(dto);
            }
        }

        foreach (Shared.Models.ProductLog product in Price)
        {
            if (product.OldValue != product.NewValue)
            {
                ProductLogDto dto = new();
                dto = _mapper.Map<ProductLogDto>(product);
                dto.CreateDateShamsi = product.CreateDate.ToShamsi();
                ProductLogType? type = await _productLogTypeRep.GetAsync(product.ProductLogTypeId);
                dto.ProductLogTypeName = type.Name;
                Shared.Models.UserBilling? user = await _userBillingRep.GetUserBillingByUserId(dto.UserId);
                dto.UserName = $"{user.FirstName} {user.LastName}";
                productLogDto.Add(dto);
            }
        }

        foreach (Shared.Models.ProductLog product in Discount)
        {
            if (product.OldValue != product.NewValue)
            {
                ProductLogDto dto = new();
                dto = _mapper.Map<ProductLogDto>(product);
                dto.CreateDateShamsi = product.CreateDate.ToShamsi();
                ProductLogType? type = await _productLogTypeRep.GetAsync(product.ProductLogTypeId);
                dto.ProductLogTypeName = type.Name;
                Shared.Models.UserBilling? user = await _userBillingRep.GetUserBillingByUserId(dto.UserId);
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