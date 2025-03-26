using MediatR;

using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.Api.Requests.Queries;

public record FixDuplicateProductsByCodeQueryReq():IRequest<ResponseDto>;