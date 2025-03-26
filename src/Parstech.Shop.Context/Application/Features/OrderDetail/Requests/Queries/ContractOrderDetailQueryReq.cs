using MediatR;
using Parstech.Shop.Context.Application.DTOs.Order;

namespace Parstech.Shop.Context.Application.Features.OrderDetail.Requests.Queries;

public record ContractOrderDetailQueryReq(Domain.Models.OrderDetail detail,string Store):IRequest<ContractDto>;