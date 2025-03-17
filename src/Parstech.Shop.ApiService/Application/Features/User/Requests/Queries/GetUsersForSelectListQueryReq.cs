using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.User.Requests.Queries;

public record GetUsersForSelectListQueryReq() : IRequest<List<UserForSelectListDto>>;