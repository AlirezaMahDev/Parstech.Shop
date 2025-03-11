using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Convertor;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.ProductLog;
using Shop.Application.Features.ProductLog.Requests.Queries;
using Shop.Domain.Models;

namespace Shop.Application.Features.ProductLog.Handlers.Queries
{
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
                    ProductLogDto dto = new ProductLogDto();
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
                    ProductLogDto dto = new ProductLogDto();
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
                    ProductLogDto dto = new ProductLogDto();
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
                    ProductLogDto dto = new ProductLogDto();
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
            PagingDto response = new PagingDto();
            int skip = (request.parameter.CurrentPage - 1) * request.parameter.TakePage;
            response.CurrentPage = request.parameter.CurrentPage;
            int count = result.Count();
            response.PageCount = count / request.parameter.TakePage;
            response.List = result.Skip(skip).Take(request.parameter.TakePage).ToArray();
            return response;

        }
    }
}
