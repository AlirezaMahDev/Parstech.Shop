using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductStockPriceSection;
using Shop.Application.Features.ProductStockPriceSection.Requests.Queries;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductStockPriceSection.Handlers.Queries
{
    public class GetSectionOfProductStockPriceQueryHandler : IRequestHandler<GetSectionOfProductStockPriceQueryReq, ProdcutStockPriceSectionDto>
    {
        #region Constractor 
        private readonly IProductStockPriceSectionRepository _productStockPriceSectionRep;
        private readonly IProductStockPriceRepository _productStockPriceRep;
        private readonly IMapper _mapper;

        public GetSectionOfProductStockPriceQueryHandler(IMapper mapper, IProductStockPriceSectionRepository productStockPriceSectionRep, IProductStockPriceRepository productStockPriceRep)
        {
            
            _mapper = mapper;
            _productStockPriceSectionRep = productStockPriceSectionRep;
            _productStockPriceRep = productStockPriceRep;
        }
        #endregion
        public async Task<ProdcutStockPriceSectionDto> Handle(GetSectionOfProductStockPriceQueryReq request, CancellationToken cancellationToken)
        {
            ProdcutStockPriceSectionDto result = new ProdcutStockPriceSectionDto();
            List<SectionDto> sections = new List<SectionDto>();
            var list = await _productStockPriceSectionRep.GetSectionOfProductStockPrice(request.productStockPriceId);
            foreach (var item in list)
            {
                var dto = _mapper.Map<SectionDto>(item);
                dto.SectionName = item.Section.SectionName;
                sections.Add(dto);
            }
            result.ProdutSrockPriceId = request.productStockPriceId;
           
            var productstockPrice =await _productStockPriceRep.GetAsync(request.productStockPriceId);
            result.ShowInDiscountPanels = productstockPrice.ShowInDiscountPanels;
            result.sections = sections;
            return result;

        }
    }
}
