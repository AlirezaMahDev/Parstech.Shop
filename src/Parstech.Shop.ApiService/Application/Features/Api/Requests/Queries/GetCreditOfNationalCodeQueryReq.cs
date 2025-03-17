using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Api.Requests.Queries;

public record GetCreditOfNationalCodeQueryReq(int userId, string nationalCode) : IRequest<int>;