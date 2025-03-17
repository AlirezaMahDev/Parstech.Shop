using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Excel.Requests.Queries;

public record EditCateguriesOfProductQueryReq(string fileName) : IRequest<Unit>;