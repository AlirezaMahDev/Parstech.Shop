using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductStockPriceSection.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPriceSection.Handlers.Queries;

public class
    GetSectionOfProductStockPriceQueryHandler : IRequestHandler<GetSectionOfProductStockPriceQueryReq,
    ProdcutStockPriceSectionDto>
{
    #region Constractor

    private readonly IProductStockPriceSectionRepository _productStockPriceSectionRep;
    private readonly IProductStockPriceRepository _productStockPriceRep;
    private readonly IMapper _mapper;

    public GetSectionOfProductStockPriceQueryHandler(IMapper mapper,
        IProductStockPriceSectionRepository productStockPriceSectionRep,
        IProductStockPriceRepository productStockPriceRep)
    {
        _mapper = mapper;
        _productStockPriceSectionRep = productStockPriceSectionRep;
        _productStockPriceRep = productStockPriceRep;
    }

    #endregion

    public async Task<ProdcutStockPriceSectionDto> Handle(GetSectionOfProductStockPriceQueryReq request,
        CancellationToken cancellationToken)
    {
        ProdcutStockPriceSectionDto result = new();
        List<SectionDto> sections = new();
        List<Domain.Models.ProductStockPriceSection> list =
            await _productStockPriceSectionRep.GetSectionOfProductStockPrice(request.productStockPriceId);
        foreach (Domain.Models.ProductStockPriceSection item in list)
        {
            var dto = _mapper.Map<SectionDto>(item);
            dto.SectionName = item.Section.SectionName;
            sections.Add(dto);
        }

        result.ProdutSrockPriceId = request.productStockPriceId;

        Domain.Models.ProductStockPrice? productstockPrice =
            await _productStockPriceRep.GetAsync(request.productStockPriceId);
        result.ShowInDiscountPanels = productstockPrice.ShowInDiscountPanels;
        result.sections = sections;
        return result;
    }
}