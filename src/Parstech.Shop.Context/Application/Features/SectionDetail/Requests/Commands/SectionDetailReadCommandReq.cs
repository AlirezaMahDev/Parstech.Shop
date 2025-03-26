using MediatR;

using Parstech.Shop.Context.Application.DTOs.SectionDetail;

namespace Parstech.Shop.Context.Application.Features.SectionDetail.Requests.Commands;

public record SectionDetailReadCommandReq(int id) : IRequest<SectionDetailDto>;