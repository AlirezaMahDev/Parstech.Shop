using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Security.Requests.Queries;

public record DataProtectQueryReq(string value, string type) : IRequest<ResponseDto>;