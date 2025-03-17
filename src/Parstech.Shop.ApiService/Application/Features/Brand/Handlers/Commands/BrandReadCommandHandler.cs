using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Brand.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.Brand.Handlers.Commands;

public class BrandReadCommandHandler : IRequestHandler<BrandReadCommandReq, BrandDto>
{
    private IBrandRepository _brandRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public BrandReadCommandHandler(IBrandRepository brandRep, IMapper mapper, IMediator madiiator)
    {
        _brandRep = brandRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }

    public async Task<BrandDto> Handle(BrandReadCommandReq request, CancellationToken cancellationToken)
    {
        Domain.Models.Brand? brand = await _brandRep.GetAsync(request.id);
        return _mapper.Map<BrandDto>(brand);
    }
}

public class BrandReadsCommandHandler : IRequestHandler<BrandReadsCommandReq, List<BrandDto>>
{
    private IBrandRepository _brandRep;
    private IMapper _mapper;
    private IMediator _madiiator;

    public BrandReadsCommandHandler(IBrandRepository brandRep, IMapper mapper, IMediator madiiator)
    {
        _brandRep = brandRep;
        _mapper = mapper;
        _madiiator = madiiator;
    }

    public async Task<List<BrandDto>> Handle(BrandReadsCommandReq request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Domain.Models.Brand>? brands = await _brandRep.GetAll();
        return _mapper.Map<List<BrandDto>>(brands);
    }
}