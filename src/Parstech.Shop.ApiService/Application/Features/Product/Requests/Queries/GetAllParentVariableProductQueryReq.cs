using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

public record GetAllParentVariableProductQueryReq(string filter) : IRequest<List<ProductSelectDto>>;