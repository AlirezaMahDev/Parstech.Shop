using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Order.Handler.Queries;

public class FinallyOrdersOfUserByPagingQueryHandler : IRequestHandler<FinallyOrdersOfUserByPagingQueryReq, PagingDto>
{
    private readonly IOrderRepository _orderRep;
    private readonly IMapper _mapper;

    public FinallyOrdersOfUserByPagingQueryHandler(IOrderRepository orderRep, IMapper mapper)
    {
        _orderRep = orderRep;
        _mapper = mapper;
    }

    public async Task<PagingDto> Handle(FinallyOrdersOfUserByPagingQueryReq request,
        CancellationToken cancellationToken)
    {
        return await _orderRep.GetFinallyOrdersOfUserByPaging(request.Parameter.CurrentPage,
            request.Parameter.TakePage,
            request.Parameter.Filter,
            request.userId);
    }
}