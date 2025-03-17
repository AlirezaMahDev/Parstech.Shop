using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.SectionDetail.Requests.Commands;

public record SectionDetailUpdateCommandReq(SectionDetailDto SectionDetailDto) : IRequest<SectionDetailDto>;