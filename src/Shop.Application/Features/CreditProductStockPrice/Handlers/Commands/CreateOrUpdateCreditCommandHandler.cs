using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.CreditProductStockPrice.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.CreditProductStockPrice.Handlers.Commands
{
    public class CreateOrUpdateCreditCommandHandler : IRequestHandler<CreateOrUpdateCreditCommandReq, ResponseDto>
    {
        private ICreditProductStockPriceReopsitory _creditRep;
        private IMapper _mapper;

        public CreateOrUpdateCreditCommandHandler(IMapper mapper, ICreditProductStockPriceReopsitory creditRep)
        {
            _mapper = mapper;
            _creditRep = creditRep;
        }

        public async Task<ResponseDto> Handle(CreateOrUpdateCreditCommandReq request, CancellationToken cancellationToken)
        {
            ResponseDto response = new ResponseDto();
            if (request.credit.Id != 0)
            {
                var item = _mapper.Map<Domain.Models.CreditProductStockPrice>(request.credit);
                
                var res=await _creditRep.UpdateAsync(item);
                response.IsSuccessed = true;
                response.Object = res;
                return response;
            }
            else
            {
                var item = _mapper.Map<Domain.Models.CreditProductStockPrice>(request.credit);
                var res = await _creditRep.AddAsync(item);
                response.IsSuccessed = true;
                response.Object = res;
                return response;
            }
            
        }
    }
}
