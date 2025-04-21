using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductCategury;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.Features.ProductGallery.Requests.Queries;
using Shop.Application.Features.ProductProperty.Requests.Queries;

namespace Shop.Application.Features.ProductCategury.Handlers.Queries
{
    public class CateguriesOfProductQueryHandler : IRequestHandler<CateguriesOfProductQueryReq, List<ProductCateguryDto>>
    {
        private readonly IProductCateguryRepository _productCateguryRep;
        private readonly ICateguryRepository _categuryRep;
        private readonly IMapper _mapper;

        public CateguriesOfProductQueryHandler(IProductCateguryRepository productCateguryRep, ICateguryRepository categuryRep, IMapper mapper)
        {
            _productCateguryRep = productCateguryRep;
            _categuryRep = categuryRep;
            _mapper = mapper;
        }
        public async Task<List<ProductCateguryDto>> Handle(CateguriesOfProductQueryReq request, CancellationToken cancellationToken)
        {
            var result =await _productCateguryRep.GetCateguriesByProduct(request.productId);
            var FinalResult =new List<ProductCateguryDto>();
            foreach (var item in result)
            {
                var cat = await _categuryRep.GetAsync(item.CateguryId);
                var ProductCateguryDto=_mapper.Map<ProductCateguryDto>(item);
                ProductCateguryDto.GroupTitle =cat.GroupTitle;
                ProductCateguryDto.LatinGroupTitle =cat.LatinGroupTitle;
                ProductCateguryDto.IsParnet = cat.IsParnet;
                ProductCateguryDto.ParentId = cat.ParentId;
                FinalResult.Add(ProductCateguryDto);
            }
            return FinalResult;
        }
    }
}
