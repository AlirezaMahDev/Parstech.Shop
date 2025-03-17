using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Commands;

public record RahkaranProductCreateCommandReq(RahkaranProductDto dto) : IRequest<RahkaranProductDto>;