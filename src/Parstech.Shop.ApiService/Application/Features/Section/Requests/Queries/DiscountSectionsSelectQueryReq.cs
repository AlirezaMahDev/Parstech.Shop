using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Section.Requests.Queries;

public record DiscountSectionsSelectQueryReq() : IRequest<List<SectionDto>>;