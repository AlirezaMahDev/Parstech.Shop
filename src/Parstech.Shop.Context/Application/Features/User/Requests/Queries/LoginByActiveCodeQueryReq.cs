using MediatR;

using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.User.Requests.Queries;

public record LoginByActiveCodeQueryReq(string Mobile,string ActiveCode) : IRequest<ResponseDto>;