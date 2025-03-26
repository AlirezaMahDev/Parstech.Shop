using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductCategury;
using Parstech.Shop.Context.Application.Features.ProductCategury.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.ProductCategury.Handlers.Commands;

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
                ProductCateguryDto psdto =new()
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