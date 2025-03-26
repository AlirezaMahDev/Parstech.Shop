using MediatR;

namespace Parstech.Shop.Context.Application.Features.Excel.Requests.Queries;

public record AddUsersAndWalletsByExcelQueryReq(string fileName) :IRequest<Unit>;