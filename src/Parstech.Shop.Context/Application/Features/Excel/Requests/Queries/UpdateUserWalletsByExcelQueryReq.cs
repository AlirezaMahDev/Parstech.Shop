using MediatR;

namespace Parstech.Shop.Context.Application.Features.Excel.Requests.Queries;

public record UpdateUserWalletsByExcelQueryReq(string fileName) : IRequest<Unit>;