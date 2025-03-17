using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.SectionDetail.Requests.Commands;

public record SectionDetailDeleteCommandReq(int id) : IRequest<Unit>;