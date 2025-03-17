using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.PropertyCategury.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.PropertyCategury.Handlers.Commands;

public class PropertyCateguryReadCommandHandler : IRequestHandler<PropertyCateguryReadCommandReq, PropertyCateguryDto>
{
    private readonly IPropertyCateguryRepository _propertyCateguryRep;
    private readonly IMapper _mapper;

    public PropertyCateguryReadCommandHandler(IPropertyCateguryRepository propertyCateguryRep, IMapper mapper)
    {
        _propertyCateguryRep = propertyCateguryRep;
        _mapper = mapper;
    }

    public async Task<PropertyCateguryDto> Handle(PropertyCateguryReadCommandReq request,
        CancellationToken cancellationToken)
    {
        Domain.Models.PropertyCategury? item = await _propertyCateguryRep.GetAsync(request.Id);
        return _mapper.Map<PropertyCateguryDto>(item);
    }
}

public class
    PropertyCateguryReadsCommandHandler : IRequestHandler<PropertyCateguryReadsCommandReq, List<PropertyCateguryDto>>
{
    private readonly IPropertyCateguryRepository _propertyCateguryRep;
    private readonly IMapper _mapper;

    public PropertyCateguryReadsCommandHandler(IPropertyCateguryRepository propertyCateguryRep, IMapper mapper)
    {
        _propertyCateguryRep = propertyCateguryRep;
        _mapper = mapper;
    }

    public async Task<List<PropertyCateguryDto>> Handle(PropertyCateguryReadsCommandReq request,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<Domain.Models.PropertyCategury>? list = await _propertyCateguryRep.GetAll();
        return _mapper.Map<List<PropertyCateguryDto>>(list);
    }
}