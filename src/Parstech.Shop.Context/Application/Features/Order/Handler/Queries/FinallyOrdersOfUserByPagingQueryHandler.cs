using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.Features.Order.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Order.Handler.Queries;

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