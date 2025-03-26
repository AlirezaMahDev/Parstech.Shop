using MediatR;
using Parstech.Shop.Context.Application.DTOs.OrderPay;

namespace Parstech.Shop.Context.Application.Features.OrderPay.Request.Queries;

public record ChoisePayTypeForCreateOrderPayQueryReq(int payTypeId,Domain.Models.Wallet wallet, Domain.Models.Order order):IRequest<ResponseOrderPayDto>;