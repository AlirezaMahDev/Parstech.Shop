using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.UserProduct.Requests.Query;

public record GetCmparisonProductsOfUsersQueryReq(string userName) : IRequest<List<CompareDto>>;