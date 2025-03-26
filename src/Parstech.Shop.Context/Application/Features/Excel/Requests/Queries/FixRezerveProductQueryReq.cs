using MediatR;

namespace Parstech.Shop.Context.Application.Features.Excel.Requests.Queries;

public record FixRezerveProductQueryReq(string fileName):IRequest<Unit>;