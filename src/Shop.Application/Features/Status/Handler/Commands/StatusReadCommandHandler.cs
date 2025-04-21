using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.Status;
using Shop.Application.Features.Status.Requests.Commands;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Status.Handler.Commands
{
	public class StatusReadCommandHandler : IRequestHandler<StatusReadCommandReq, List<StatusDto>>
	{
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;
		private readonly IStatusRepository _statusRepo;
		public StatusReadCommandHandler(IStatusRepository statusRepo, IMapper mapper, IMediator mediator)
		{
			_statusRepo = statusRepo;
			_mapper = mapper;
			_mediator = mediator;
		}
		public async Task<List<StatusDto>> Handle(StatusReadCommandReq request, CancellationToken cancellationToken)
		{
			var StatusList = await _statusRepo.GetAll();
			return _mapper.Map<List<StatusDto>>(StatusList);
		}
	}
}
