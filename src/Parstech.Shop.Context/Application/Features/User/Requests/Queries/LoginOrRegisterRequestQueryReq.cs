using MediatR;

using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.User.Requests.Queries;

public record LoginOrRegisterRequestQueryReq(string Mobile) :IRequest<ResponseDto>;