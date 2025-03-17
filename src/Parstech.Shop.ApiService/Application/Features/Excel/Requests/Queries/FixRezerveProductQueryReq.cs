using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Excel.Requests.Queries;

public record FixRezerveProductQueryReq(string fileName) : IRequest<Unit>;