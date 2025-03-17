using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.PayType.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.PayType.Handlers.Commands;

public class PayTypeReadsCommandHandler : IRequestHandler<PayTypeReadsCommandReq, List<PayTypeDto>>
{
    private readonly IPayTypeRepository _payTypeRep;
    private readonly IMapper _mapper;

    public PayTypeReadsCommandHandler(IPayTypeRepository payTypeRep, IMapper mapper)
    {
        _payTypeRep = payTypeRep;
        _mapper = mapper;
    }

    public async Task<List<PayTypeDto>> Handle(PayTypeReadsCommandReq request, CancellationToken cancellationToken)
    {
        if (request.justactive)
        {
            List<Shared.Models.PayType> list = await _payTypeRep.GetActiveList();
            return _mapper.Map<List<PayTypeDto>>(list);
        }
        else
        {
            IReadOnlyList<Shared.Models.PayType> list = await _payTypeRep.GetAll();
            return _mapper.Map<List<PayTypeDto>>(list);
        }
    }
}