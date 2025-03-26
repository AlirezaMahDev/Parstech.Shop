using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Status;
using Parstech.Shop.Context.Application.Features.Status.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Status.Handler.Commands;

public class StatusReadCommandHandler : IRequestHandler<StatusReadCommandReq, List<StatusDto>>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IStatusRepository _statusRepo;
    public StatusReadCommandHandler(IStatusRepository statusRepo, IMapper mapper, IMediator mediator)
    {
        _statusRepo = statusRepo;
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<List<StatusDto>> Handle(StatusReadCommandReq request, CancellationToken cancellationToken)
    {
        var StatusList = await _statusRepo.GetAll();
        return _mapper.Map<List<StatusDto>>(StatusList);
    }
}