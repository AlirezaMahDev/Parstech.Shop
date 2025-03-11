using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.Categury.Requests.Commands;

namespace Shop.Application.Features.Categury.Handlers.Commands
{
    public class CateguryUpdateCommandHandler : IRequestHandler<CateguryUpdateCommandReq, CateguryDto>
    {
        private readonly ICateguryRepository _categuryRep;
        private readonly IMapper _mapper;

        public CateguryUpdateCommandHandler(ICateguryRepository categuryRep, IMapper mapper)
        {
            _categuryRep = categuryRep;
            _mapper = mapper;
        }
        public async Task<CateguryDto> Handle(CateguryUpdateCommandReq request, CancellationToken cancellationToken)
        {
            var categury = _mapper.Map<Domain.Models.Categury>(request.CateguryDto);
            var categuryResult = await _categuryRep.UpdateAsync(categury);
            return _mapper.Map<CateguryDto>(categuryResult);
        }
    }
}
