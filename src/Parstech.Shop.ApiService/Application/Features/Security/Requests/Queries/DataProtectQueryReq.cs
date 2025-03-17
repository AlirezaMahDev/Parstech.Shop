using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Security.Requests.Queries;

public record DataProtectQueryReq(string value, string type) : IRequest<ResponseDto>;