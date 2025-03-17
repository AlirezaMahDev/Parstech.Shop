using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Section.Requests.Commands;

public record SectionDeleteCommandReq(int id) : IRequest<Unit>;