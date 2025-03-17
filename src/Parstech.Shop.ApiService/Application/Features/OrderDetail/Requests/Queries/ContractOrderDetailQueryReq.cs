using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderDetail.Requests.Queries;

public record ContractOrderDetailQueryReq(Domain.Models.OrderDetail detail, string Store) : IRequest<ContractDto>;