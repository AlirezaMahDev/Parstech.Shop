using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.CouponType;
using Shop.Application.DTOs.Status;
using Shop.Application.Features.CouponType.Requests.Commands;
using Shop.Application.Features.Status.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.CouponType.Handlers.Commands
{
    public class CouponTypeReadCommandHandler : IRequestHandler<CouponTypeReadCommandReq, List<CouponTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ICouponTypeRepository _couponTypeRepo;
        public CouponTypeReadCommandHandler(ICouponTypeRepository couponTypeRepo, IMapper mapper, IMediator mediator)
        {
            _couponTypeRepo = couponTypeRepo;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<List<CouponTypeDto>> Handle(CouponTypeReadCommandReq request, CancellationToken cancellationToken)
        {
            var couponTypeList = await _couponTypeRepo.GetAll();
            return _mapper.Map<List<CouponTypeDto>>(couponTypeList);
        }
    }
}
