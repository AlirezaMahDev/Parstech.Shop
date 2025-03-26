using MediatR;
using Parstech.Shop.Context.Application.DTOs.Rahkaran;

namespace Parstech.Shop.Context.Application.Features.Rahkaran.Requests.Queries;

public record RahakaranAllQueryReq(int orderId):IRequest<RahkaranAllDto>;