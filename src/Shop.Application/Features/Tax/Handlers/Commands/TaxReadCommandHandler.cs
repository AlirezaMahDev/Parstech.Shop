using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductType;
using Shop.Application.Features.ProductType.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.Tax;
using Shop.Application.Features.Tax.Requests.Commands;

namespace Shop.Application.Features.Tax.Handlers.Commands
{
    public class TaxReadCommandHandler : IRequestHandler<TaxReadCommandReq, TaxDto>
    {
        private ITaxRepository _taxRep;
        private IMapper _mapper;
        private IMediator _madiiator;

        public TaxReadCommandHandler(ITaxRepository taxRep, IMapper mapper, IMediator madiiator)
        {
            _taxRep = taxRep;
            _mapper = mapper;
            _madiiator = madiiator;
        }

        public async Task<TaxDto> Handle(TaxReadCommandReq request, CancellationToken cancellationToken)
        {
            var tax = await _taxRep.GetAsync(request.id);
            return _mapper.Map<TaxDto>(tax);
        }
    }
    public class TaxReadsCommandHandler : IRequestHandler<TaxReadsCommandReq, List<TaxDto>>
    {
        private ITaxRepository _taxRep;
        private IMapper _mapper;
        private IMediator _madiiator;

        public TaxReadsCommandHandler(ITaxRepository taxRep, IMapper mapper, IMediator madiiator)
        {
            _taxRep = taxRep;
            _mapper = mapper;
            _madiiator = madiiator;
        }

        public async Task<List<TaxDto>> Handle(TaxReadsCommandReq request, CancellationToken cancellationToken)
        {
            var tax = await _taxRep.GetAll();
            return _mapper.Map<List<TaxDto>>(tax);
        }
    }

}
