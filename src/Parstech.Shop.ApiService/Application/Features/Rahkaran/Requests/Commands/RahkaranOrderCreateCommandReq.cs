using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Commands;

public record RahkaranOrderCreateCommandReq(RahkaranOrderDto dto) : IRequest<RahkaranOrderDto>;