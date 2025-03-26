using MediatR;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.Rahkaran.Requests.Queries;

public record RahkaranFollowOrderFromApiQueryReq(int orderId):IRequest<ResponseDto>;