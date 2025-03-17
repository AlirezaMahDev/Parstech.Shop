using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Commands;

public record RahkaranOrderUpdateCommandReq(RahkaranOrderDto dto) : IRequest<RahkaranOrderDto>;