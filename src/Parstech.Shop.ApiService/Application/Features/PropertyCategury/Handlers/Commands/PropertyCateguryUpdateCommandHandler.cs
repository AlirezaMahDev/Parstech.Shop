using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.PropertyCategury.Requests.Commands;

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
        Domain.Models.PropertyCategury? item = _mapper.Map<Domain.Models.PropertyCategury>(request.PropertyCateguryDto);
        Domain.Models.PropertyCategury? itemResult = await _propertyCatRep.UpdateAsync(item);
        return _mapper.Map<PropertyCateguryDto>(itemResult);
    }
}