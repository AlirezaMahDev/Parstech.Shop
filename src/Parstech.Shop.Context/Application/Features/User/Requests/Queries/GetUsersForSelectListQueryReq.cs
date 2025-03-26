using MediatR;

using Parstech.Shop.Context.Application.DTOs.User;

namespace Parstech.Shop.Context.Application.Features.User.Requests.Queries;

public record GetUsersForSelectListQueryReq():IRequest<List<UserForSelectListDto>>;