using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Queries;

public record RahakaranAllQueryReq(int orderId) : IRequest<RahkaranAllDto>;