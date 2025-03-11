using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.PayType;
using Shop.Application.Features.PayType.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.PayType.Handlers.Commands
{
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
}
