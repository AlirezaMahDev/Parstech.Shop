using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderDetail.Requests.Queries;

public record ContractOrderDetailQueryReq(Shared.Models.OrderDetail detail, string Store) : IRequest<ContractDto>;