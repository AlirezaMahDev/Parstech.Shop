using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Convertor;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductLog.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.ProductLog.Handlers.Queries;

public class ProductLogReadsFromProductIdQueryHandler : IRequestHandler<ProductLogReadsFromProductIdQueryReq, LogDto>
{
    private readonly IProductLogRepository _productLogRep;
    private readonly IProductLogTypeRepository _productLogTypeRep;
    private readonly IMapper _mapper;

    public ProductLogReadsFromProductIdQueryHandler(IProductLogRepository productLogRep,
        IProductLogTypeRepository productLogTypeRep,
        IMapper mapper)
    {
        _productLogRep = productLogRep;
        _productLogTypeRep = productLogTypeRep;
        _mapper = mapper;
    }

    public async Task<LogDto> Handle(ProductLogReadsFromProductIdQueryReq request, CancellationToken cancellationToken)
    {
        LogDto Result = new();
        List<Domain.Models.ProductLog>
            SaleLogs = await _productLogRep.GetSaleProductLogWithProductId(request.productId);
        List<Domain.Models.ProductLog>
            BaseLogs = await _productLogRep.GetBaseProductLogWithProductId(request.productId);
        List<Domain.Models.ProductLog> PriceLogs =
            await _productLogRep.GetPriceProductLogWithProductId(request.productId);
        List<Domain.Models.ProductLog> DiscountLogs =
            await _productLogRep.GetDiscountProductLogWithProductId(request.productId);
        Result.SaleLogDtos = _mapper.Map<List<ProductLogDto>>(SaleLogs);
        Result.BaseLogDtos = _mapper.Map<List<ProductLogDto>>(BaseLogs);
        Result.DiscountLogDtos = _mapper.Map<List<ProductLogDto>>(DiscountLogs);

        List<ProductLogDto> prices = new();
        foreach (Domain.Models.ProductLog priceLog in PriceLogs)
        {
            ProductLogDto? dto = _mapper.Map<ProductLogDto>(priceLog);
            dto.CreateDateShamsi = priceLog.CreateDate.ToShamsi();
            prices.Add(dto);
        }

        Result.PriceLogDtos = prices;
        return Result;
    }
}