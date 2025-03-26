using MediatR;

namespace Parstech.Shop.Context.Application.Features.Excel.Requests.Queries;

public record FillCodeOfProductsByExcelQueryReq(string fileName):IRequest<Unit>;