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
using Shop.Application.Features.ProductCategury.Requests.Commands;
using Shop.Application.Features.ProductGallery.Requests.Commands;
using Shop.Application.Features.ProductProperty.Requests.Commands;

namespace Shop.Application.Features.ProductCategury.Handlers.Commands
{
    public class ProductCateguryCreateCommandHandler : IRequestHandler<ProductCateguryCreateCommandReq, ProductCateguryDto>
    {
        private readonly IProductCateguryRepository _productCateguryRep;
        private readonly ICateguryRepository _categuryRep;
        private readonly IMapper _mapper;

        public ProductCateguryCreateCommandHandler(IProductCateguryRepository productCateguryRep, IMapper mapper, ICateguryRepository categuryRep)
        {
            _productCateguryRep = productCateguryRep;
            _mapper = mapper;
            _categuryRep = categuryRep;
        }
        public async Task<ProductCateguryDto> Handle(ProductCateguryCreateCommandReq request, CancellationToken cancellationToken)
        {
            var pcategury = _mapper.Map<Domain.Models.ProductCategury>(request.ProductCateguryDto);
            if (await _productCateguryRep.ExistProductCategury(request.ProductCateguryDto))
            {
                var result = await _productCateguryRep.AddAsync(pcategury);
                var cat =await _categuryRep.GetAsync(pcategury.CateguryId);
                int? parentId = cat.ParentId;

                while (parentId != null)
                {
                    var parent =await _categuryRep.GetAsync(parentId.Value);
                    ProductCateguryDto psdto =new ProductCateguryDto()
                    {
                        ProductId=pcategury.ProductId,
                        CateguryId=parent.ParentId.Value,
                    };
                    if (await _productCateguryRep.ExistProductCategury(psdto))
                    {
                        var ps = _mapper.Map<Domain.Models.ProductCategury>(psdto);
                        await _productCateguryRep.AddAsync(ps);
                    }
                       
                    parentId = parent.ParentId;
                }

                return _mapper.Map<ProductCateguryDto>(result);
            }
            else
            {
                return request.ProductCateguryDto;
            }
            
        }
    }
}
