using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Categury;
using Shop.Application.Features.Categury.Requests.Queries;

namespace Shop.Application.Features.Categury.Handlers.Queries
{
    public class CateguryParentsReadQueryHandler : IRequestHandler<CateguryParentsReadQueryReq, List<CateguryDto>>
    {
        private readonly ICateguryRepository _categuryRep;
        private readonly IMapper _mapper;

        public CateguryParentsReadQueryHandler(ICateguryRepository categuryRep, IMapper mapper)
        {
            _categuryRep = categuryRep;
            _mapper = mapper;
        }
        public async Task<List<CateguryDto>> Handle(CateguryParentsReadQueryReq request, CancellationToken cancellationToken)
        {
            var list =await _categuryRep.GetAllParentCategury();
            return _mapper.Map<List<CateguryDto>>(list);
        }
    }
}
