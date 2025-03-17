using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Queries;

public record RahkaranFollowOrderFromApiQueryReq(int orderId) : IRequest<ResponseDto>;