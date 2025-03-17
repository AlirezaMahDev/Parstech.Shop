using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Excel.Requests.Queries;

public record UpdateUserWalletsByExcelQueryReq(string fileName) : IRequest<Unit>;