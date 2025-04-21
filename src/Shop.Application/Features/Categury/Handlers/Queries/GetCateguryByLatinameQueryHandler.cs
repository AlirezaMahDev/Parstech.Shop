using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Categury;
using Shop.Application.Features.Categury.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Categury.Handlers.Queries
{
    public class GetCateguryByLatinameQueryHandler : IRequestHandler<GetCateguryByLatinameQueryReq, CateguryDto>
    {
        private readonly ICateguryRepository _categuryRep;
        private readonly IMapper _mapper;
        public GetCateguryByLatinameQueryHandler(ICateguryRepository categuryRep,IMapper mapper)
        {
            _categuryRep = categuryRep;
            _mapper = mapper;
        }
        public async Task<CateguryDto> Handle(GetCateguryByLatinameQueryReq request, CancellationToken cancellationToken)
        {
            var item =await _categuryRep.GetCateguryByLatinName(request.latinName);
            return _mapper.Map<CateguryDto>(item);
        }
    }
}
