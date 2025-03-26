using MediatR;

namespace Parstech.Shop.Context.Application.Features.Api.Requests.Queries;

public record GetCreditOfNationalCodeQueryReq(int userId,string nationalCode):IRequest<int>;