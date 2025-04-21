using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.Paging;
using Shop.Application.Features.Order.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Handler.Queries
{
    public class FinallyOrdersOfUserByPagingQueryHandler : IRequestHandler<FinallyOrdersOfUserByPagingQueryReq, PagingDto>
    {
        private readonly IOrderRepository _orderRep;
        private readonly IMapper _mapper;

        public FinallyOrdersOfUserByPagingQueryHandler(IOrderRepository orderRep, IMapper mapper)
        {
            _orderRep = orderRep;
            _mapper = mapper;
        }
        public async Task<PagingDto> Handle(FinallyOrdersOfUserByPagingQueryReq request, CancellationToken cancellationToken)
        {
            return await _orderRep.GetFinallyOrdersOfUserByPaging(request.Parameter.CurrentPage, request.Parameter.TakePage, request.Parameter.Filter, request.userId);
        }

    }
}