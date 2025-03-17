using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Section.Requests.Commands;

public record SectionReadCommandReq(int id) : IRequest<SectionDto>;