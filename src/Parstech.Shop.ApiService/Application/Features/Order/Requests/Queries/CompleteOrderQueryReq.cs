﻿using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;

public record CompleteOrderQueryReq(int orderId, int orderShippingId, int payTypeId, int? transactionId, int? month)
    : IRequest<ResponseDto>;