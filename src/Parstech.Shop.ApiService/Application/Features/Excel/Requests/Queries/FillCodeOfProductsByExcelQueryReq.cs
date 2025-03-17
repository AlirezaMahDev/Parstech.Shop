using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Excel.Requests.Queries;

public record FillCodeOfProductsByExcelQueryReq(string fileName) : IRequest<Unit>;