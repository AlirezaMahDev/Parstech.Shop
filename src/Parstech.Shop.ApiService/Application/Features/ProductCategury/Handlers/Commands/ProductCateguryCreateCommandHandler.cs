using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductCategury.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductCategury.Handlers.Commands;

public class ProductCateguryCreateCommandHandler : IRequestHandler<ProductCateguryCreateCommandReq, ProductCateguryDto>
{
    private readonly IProductCateguryRepository _productCateguryRep;
    private readonly ICateguryRepository _categuryRep;
    private readonly IMapper _mapper;

    public ProductCateguryCreateCommandHandler(IProductCateguryRepository productCateguryRep,
        IMapper mapper,
        ICateguryRepository categuryRep)
    {
        _productCateguryRep = productCateguryRep;
        _mapper = mapper;
        _categuryRep = categuryRep;
    }

    public async Task<ProductCateguryDto> Handle(ProductCateguryCreateCommandReq request,
        CancellationToken cancellationToken)
    {
        Shared.Models.ProductCategury? pcategury =
            _mapper.Map<Shared.Models.ProductCategury>(request.ProductCateguryDto);
        if (await _productCateguryRep.ExistProductCategury(request.ProductCateguryDto))
        {
            Shared.Models.ProductCategury result = await _productCateguryRep.AddAsync(pcategury);
            Shared.Models.Categury? cat = await _categuryRep.GetAsync(pcategury.CateguryId);
            int? parentId = cat.ParentId;

            while (parentId != null)
            {
                Shared.Models.Categury? parent = await _categuryRep.GetAsync(parentId.Value);
                ProductCateguryDto psdto = new()
                {
                    ProductId = pcategury.ProductId, CateguryId = parent.ParentId.Value
                };
                if (await _productCateguryRep.ExistProductCategury(psdto))
                {
                    Shared.Models.ProductCategury? ps = _mapper.Map<Shared.Models.ProductCategury>(psdto);
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