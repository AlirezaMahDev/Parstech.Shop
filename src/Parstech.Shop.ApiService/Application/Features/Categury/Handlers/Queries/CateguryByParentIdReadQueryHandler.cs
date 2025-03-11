using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Categury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.Features.Categury.Requests.Queries;

namespace Shop.Application.Features.Categury.Handlers.Queries
{
    public class CateguryByParentIdReadQueryHandler : IRequestHandler<CateguryByParentIdReadQueryReq, List<CateguryDto>>
    {
        private ICateguryRepository _categuryRep;
        private IMapper _mapper;

        public CateguryByParentIdReadQueryHandler(ICateguryRepository categuryRep, IMapper mapper)
        {
            _categuryRep = categuryRep;
            _mapper = mapper;
        }
        public async Task<List<CateguryDto>> Handle(CateguryByParentIdReadQueryReq request, CancellationToken cancellationToken)
        {
            var list = await _categuryRep.GetCateguryByParentId(request.ParentId,null);
            return _mapper.Map<List<CateguryDto>>(list);
        }
    }

}
