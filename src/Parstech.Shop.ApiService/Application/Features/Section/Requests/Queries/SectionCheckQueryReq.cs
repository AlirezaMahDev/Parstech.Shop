using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Section.Requests.Queries;

public record SectionCheckQueryReq(int id) : IRequest<bool>;