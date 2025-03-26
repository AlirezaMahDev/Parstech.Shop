using MediatR;
using Parstech.Shop.Context.Application.DTOs.Section;

namespace Parstech.Shop.Context.Application.Features.Section.Requests.Commands;

public record SectionReadCommandReq(int id) : IRequest<SectionDto>;