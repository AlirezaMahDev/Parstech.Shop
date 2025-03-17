using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Api.Requests.Queries;

public record FixDuplicateProductsByCodeQueryReq() : IRequest<ResponseDto>;