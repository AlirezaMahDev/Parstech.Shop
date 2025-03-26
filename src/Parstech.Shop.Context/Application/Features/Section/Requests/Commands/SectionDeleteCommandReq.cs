using MediatR;

namespace Parstech.Shop.Context.Application.Features.Section.Requests.Commands;

public record SectionDeleteCommandReq(int id) : IRequest<Unit>;