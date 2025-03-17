using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.PayType.Requests.Commands;

public record PayTypeReadsCommandReq(bool justactive) : IRequest<List<PayTypeDto>>;