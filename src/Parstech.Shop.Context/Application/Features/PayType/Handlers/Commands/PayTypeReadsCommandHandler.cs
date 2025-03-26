using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.PayType;
using Parstech.Shop.Context.Application.Features.PayType.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.PayType.Handlers.Commands;

public class PayTypeReadsCommandHandler : IRequestHandler<PayTypeReadsCommandReq, List<PayTypeDto>>
{
    private readonly IPayTypeRepository _payTypeRep;
    private readonly IMapper _mapper;
    public PayTypeReadsCommandHandler(IPayTypeRepository payTypeRep,IMapper mapper)
    {
        _payTypeRep=payTypeRep;
        _mapper=mapper;
    }
    public async Task<List<PayTypeDto>> Handle(PayTypeReadsCommandReq request, CancellationToken cancellationToken)
    {
        if (request.justactive)
        {
            var list = await _payTypeRep.GetActiveList();
            return _mapper.Map<List<PayTypeDto>>(list);
        }
        else
        {
            var list = await _payTypeRep.GetAll();
            return _mapper.Map<List<PayTypeDto>>(list);
        }
            
    }
}