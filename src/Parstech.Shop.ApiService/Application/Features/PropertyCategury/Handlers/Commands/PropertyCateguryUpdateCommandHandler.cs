using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.PropertyCategury.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.PropertyCategury.Handlers.Commands;

public class
    PropertyCateguryUpdateCommandHandler : IRequestHandler<PropertyCateguryUpdateCommandReq, PropertyCateguryDto>
{
    private readonly IPropertyCateguryRepository _propertyCatRep;
    private readonly IMapper _mapper;

    public PropertyCateguryUpdateCommandHandler(IPropertyCateguryRepository propertyCatRep, IMapper mapper)
    {
        _propertyCatRep = propertyCatRep;
        _mapper = mapper;
    }

    public async Task<PropertyCateguryDto> Handle(PropertyCateguryUpdateCommandReq request,
        CancellationToken cancellationToken)
    {
        Shared.Models.PropertyCategury? item = _mapper.Map<Shared.Models.PropertyCategury>(request.PropertyCateguryDto);
        Shared.Models.PropertyCategury itemResult = await _propertyCatRep.UpdateAsync(item);
        return _mapper.Map<PropertyCateguryDto>(itemResult);
    }
}