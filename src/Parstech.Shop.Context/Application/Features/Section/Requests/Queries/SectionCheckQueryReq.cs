using MediatR;

namespace Parstech.Shop.Context.Application.Features.Section.Requests.Queries;

public record SectionCheckQueryReq(int id) : IRequest<bool>;