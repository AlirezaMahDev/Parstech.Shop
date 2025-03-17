using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Excel.Requests.Queries;

public record AddUsersAndWalletsByExcelQueryReq(string fileName) : IRequest<Unit>;