using MediatR;
using Parstech.Shop.Context.Application.DTOs.Section;

namespace Parstech.Shop.Context.Application.Features.Section.Requests.Queries;

public record DiscountSectionsSelectQueryReq():IRequest<List<SectionDto>>;