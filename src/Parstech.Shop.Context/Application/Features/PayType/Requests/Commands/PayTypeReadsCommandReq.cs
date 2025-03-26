using MediatR;

using Parstech.Shop.Context.Application.DTOs.PayType;

namespace Parstech.Shop.Context.Application.Features.PayType.Requests.Commands;

public record PayTypeReadsCommandReq(bool justactive):IRequest<List<PayTypeDto>>;