using MediatR;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.Security.Requests.Queries;

public record DataProtectQueryReq(string value,string type):IRequest<ResponseDto>;