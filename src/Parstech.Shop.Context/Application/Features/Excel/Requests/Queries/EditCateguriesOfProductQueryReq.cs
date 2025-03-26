using MediatR;

namespace Parstech.Shop.Context.Application.Features.Excel.Requests.Queries;

public record EditCateguriesOfProductQueryReq(string fileName):IRequest<Unit>;