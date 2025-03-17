using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderPay.Request.Queries;

public record ChoisePayTypeForCreateOrderPayQueryReq(
    int payTypeId,
    Domain.Models.Wallet wallet,
    Domain.Models.Order order) : IRequest<ResponseOrderPayDto>;