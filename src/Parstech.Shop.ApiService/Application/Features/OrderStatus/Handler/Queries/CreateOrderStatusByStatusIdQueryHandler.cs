using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.OrderStatus;
using Shop.Application.Features.OrderStatus.Requests.Commands;
using Shop.Application.Features.OrderStatus.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderStatus.Handler.Queries
{
    public class CreateOrderStatusByStatusIdQueryHandler : IRequestHandler<CreateOrderStatusByStatusIdQueryReq, Unit>
    {

        private readonly IStatusRepository _statusRep;
        private readonly IUserRepository _userRep;

        private readonly IMediator _mediator;
        public CreateOrderStatusByStatusIdQueryHandler(
            IUserRepository userRep,
            IStatusRepository statusRep,
            IMediator mediator)
        {

            _statusRep = statusRep;
            _userRep= userRep;
            _mediator = mediator;
        }
        public async Task<Unit> Handle(CreateOrderStatusByStatusIdQueryReq request, CancellationToken cancellationToken)
        {
            var status = await _statusRep.GetStatusByLatinName(request.Latinstatus);
            var user = await _userRep.GetAsync(request.userId);
            OrderStatusDto item = new OrderStatusDto();
            item.StatusId = status.Id;
            item.OrderId = request.orderId;
            item.CreateDate = DateTime.Now;
            item.CreateBy = user.UserName;

            await _mediator.Send(new OrderStatusCreatCommandReq(item));
            return Unit.Value;
        }
    }
}
