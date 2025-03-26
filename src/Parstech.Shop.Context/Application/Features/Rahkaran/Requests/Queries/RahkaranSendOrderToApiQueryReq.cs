using MediatR;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.Rahkaran.Requests.Queries;

public record RahkaranSendOrderToApiQueryReq(int orderId):IRequest<ResponseDto>;