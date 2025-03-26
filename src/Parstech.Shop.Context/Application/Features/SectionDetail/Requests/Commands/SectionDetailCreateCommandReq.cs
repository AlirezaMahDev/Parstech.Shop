using MediatR;

using Parstech.Shop.Context.Application.DTOs.SectionDetail;

namespace Parstech.Shop.Context.Application.Features.SectionDetail.Requests.Commands;

public record SectionDetailCreateCommandReq(SectionDetailDto SectionDetailDto) : IRequest<SectionDetailDto>;