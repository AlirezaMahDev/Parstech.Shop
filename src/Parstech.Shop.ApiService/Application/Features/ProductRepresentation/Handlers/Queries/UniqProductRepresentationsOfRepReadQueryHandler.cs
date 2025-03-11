using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Application.Features.ProductRepresentation.Requests.Queries;

namespace Shop.Application.Features.Representation.Handlers.Queries
{
    public class UniqProductRepresentationsOfRepReadQueryHandler : IRequestHandler<UniqProductRepresentationsOfRepReadQueryReq, ProductRepresentationList>
    {
        private readonly IProductRepresesntationRepository _productRepresentationRep;
        private readonly IProductRepository _productRep;
        private readonly IMapper _mapper;

        public UniqProductRepresentationsOfRepReadQueryHandler(IProductRepresesntationRepository productRepresentationRep, IProductRepository productRep, IMapper mapper)
        {
            _productRepresentationRep = productRepresentationRep;
            _productRep = productRep;
            _mapper = mapper;
        }
        public async Task<ProductRepresentationList> Handle(UniqProductRepresentationsOfRepReadQueryReq request, CancellationToken cancellationToken)
        {
            List<ProductRepresentationDto> result = new List<ProductRepresentationDto>();
            var list =await _productRepresentationRep.GetUniqProductRepresentationFromRepId(request.repId);
            foreach (var item in list)
            {
                if (result.Any(u=>u.ProductStockPriceId ==item.ProductStockPriceId))
                {
                    
                }
                else
                {
                    item.Quantity =await _productRepresentationRep.GetLastQuantityFromProductRepresntation(item.ProductStockPriceId, request.repId);
                    var dto = _mapper.Map<ProductRepresentationDto>(item);
                    var product =await _productRep.GetAsync(item.ProductStockPriceId);
                    dto.ProductName = product.Name;
                    dto.ProductCode = product.Code;
                    result.Add(dto);
                }
            }

            ProductRepresentationList FinalResult = new ProductRepresentationList();
            FinalResult.RepId = request.repId;
            FinalResult.ProductRepresentationDtos = result;
            return FinalResult;
        }
    }
}
