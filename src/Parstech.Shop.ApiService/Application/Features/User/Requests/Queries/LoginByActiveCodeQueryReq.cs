using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.User.Requests.Queries;

public record LoginByActiveCodeQueryReq(string Mobile, string ActiveCode) : IRequest<ResponseDto>;