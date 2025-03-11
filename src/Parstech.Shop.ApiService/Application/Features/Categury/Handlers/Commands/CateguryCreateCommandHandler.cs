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
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.Features.Categury.Requests.Commands;
using Shop.Application.Features.ProductGallery.Requests.Commands;

namespace Shop.Application.Features.Categury.Handlers.Commands
{
    public class CateguryCreateCommandHandler : IRequestHandler<CateguryCreateCommandReq, CateguryDto>
    {
        private readonly ICateguryRepository _categuryRep;
        private readonly IMapper _mapper;

        public CateguryCreateCommandHandler(ICateguryRepository categuryRep, IMapper mapper)
        {
            _categuryRep = categuryRep;
            _mapper = mapper;
        }

        public async Task<CateguryDto> Handle(CateguryCreateCommandReq request, CancellationToken cancellationToken)
        {
            var categury = _mapper.Map<Domain.Models.Categury>(request.CateguryDto);
            categury.Image = "05.png";
            var categuryResult = await _categuryRep.AddAsync(categury);

            return _mapper.Map<CateguryDto>(categuryResult);
        }
    }
}
