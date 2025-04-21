using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Convertor;
using Shop.Application.DTOs.ProductLog;
using Shop.Application.Features.ProductLog.Requests.Queries;

namespace Shop.Application.Features.ProductLog.Handlers.Queries
{
    public class ProductLogReadsFromProductIdQueryHandler : IRequestHandler<ProductLogReadsFromProductIdQueryReq, LogDto>
    {
        private readonly IProductLogRepository _productLogRep;
        private readonly IProductLogTypeRepository _productLogTypeRep;
        private readonly IMapper _mapper;

        public ProductLogReadsFromProductIdQueryHandler(IProductLogRepository productLogRep, IProductLogTypeRepository productLogTypeRep, IMapper mapper)
        {
            _productLogRep = productLogRep;
            _productLogTypeRep = productLogTypeRep;
            _mapper = mapper;
        }
        public async Task<LogDto> Handle(ProductLogReadsFromProductIdQueryReq request, CancellationToken cancellationToken)
        {
            LogDto Result = new LogDto();
            var SaleLogs = await _productLogRep.GetSaleProductLogWithProductId(request.productId);
            var BaseLogs = await _productLogRep.GetBaseProductLogWithProductId(request.productId);
            var PriceLogs = await _productLogRep.GetPriceProductLogWithProductId(request.productId);
            var DiscountLogs = await _productLogRep.GetDiscountProductLogWithProductId(request.productId);
            Result.SaleLogDtos = _mapper.Map<List<ProductLogDto>>(SaleLogs);
            Result.BaseLogDtos = _mapper.Map<List<ProductLogDto>>(BaseLogs);
            Result.DiscountLogDtos = _mapper.Map<List<ProductLogDto>>(DiscountLogs);

            List<ProductLogDto> prices = new List<ProductLogDto>();
            foreach (var priceLog in PriceLogs)
            {
                var dto = _mapper.Map<ProductLogDto>(priceLog);
                dto.CreateDateShamsi = priceLog.CreateDate.ToShamsi();
                prices.Add(dto);
            }

            Result.PriceLogDtos = prices;
            return Result;
        }
    }
}
