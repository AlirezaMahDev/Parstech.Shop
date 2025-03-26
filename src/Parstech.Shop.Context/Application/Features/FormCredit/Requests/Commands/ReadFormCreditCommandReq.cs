using MediatR;
using Parstech.Shop.Context.Application.DTOs.FormCredit;

namespace Parstech.Shop.Context.Application.Features.FormCredit.Requests.Commands;

public record ReadFormCreditCommandReq(int Id):IRequest<FormCreditDto>;
public record ReadFormCreditsCommandReq():IRequest<List<FormCreditDto>>;