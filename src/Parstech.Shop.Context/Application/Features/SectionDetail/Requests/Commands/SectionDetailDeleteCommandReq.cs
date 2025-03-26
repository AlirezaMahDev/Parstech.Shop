using MediatR;

namespace Parstech.Shop.Context.Application.Features.SectionDetail.Requests.Commands;

public record SectionDetailDeleteCommandReq(int id) : IRequest<Unit>;