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
    public class CateguryReadByIdQueryHandler : IRequestHandler<CateguryReadByIdQueryReq, CateguryDto>
    {
        private readonly ICateguryRepository _categuryRep;
        private readonly IMapper _mapper;

        public CateguryReadByIdQueryHandler(ICateguryRepository categuryRep, IMapper mapper)
        {
            _categuryRep = categuryRep;
            _mapper = mapper;
        }

        public async Task<CateguryDto> Handle(CateguryReadByIdQueryReq request, CancellationToken cancellationToken)
        {
            var cat =await _categuryRep.GetAsync(request.id);
            return _mapper.Map<CateguryDto>(cat);

        }
    }
}
