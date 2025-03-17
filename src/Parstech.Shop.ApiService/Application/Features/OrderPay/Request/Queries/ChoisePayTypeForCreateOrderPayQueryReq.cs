using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderPay.Request.Queries;

public record ChoisePayTypeForCreateOrderPayQueryReq(
    int payTypeId,
    Shared.Models.Wallet wallet,
    Shared.Models.Order order) : IRequest<ResponseOrderPayDto>;