using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.User.Requests.Queries;

public record LoginByActiveCodeQueryReq(string Mobile, string ActiveCode) : IRequest<ResponseDto>;