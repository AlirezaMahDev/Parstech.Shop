using MediatR;

using Parstech.Shop.Context.Application.DTOs.User;

namespace Parstech.Shop.Context.Application.Features.User.Requests.Queries;

public record GetUserInfoQueryReq(string userName, string position) : IRequest<UserInfoDto>;