using MediatR;

using Parstech.Shop.Context.Application.DTOs.Rahkaran;

namespace Parstech.Shop.Context.Application.Features.Rahkaran.Requests.Commands;

public record RahkaranUserUpdateCommandReq(RahkaranUserDto dto) : IRequest<RahkaranUserDto>;