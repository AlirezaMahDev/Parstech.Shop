using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.FormCredit.Requests.Commands;

public record ReadFormCreditCommandReq(int Id) : IRequest<FormCreditDto>;

public record ReadFormCreditsCommandReq() : IRequest<List<FormCreditDto>>;