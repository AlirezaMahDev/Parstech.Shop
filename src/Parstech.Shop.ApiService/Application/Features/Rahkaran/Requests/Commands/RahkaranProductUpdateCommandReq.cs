using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Commands;

public record RahkaranProductUpdateCommandReq(RahkaranProductDto dto) : IRequest<RahkaranProductDto>;