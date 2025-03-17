using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.FormCredit.Requests.Commands;

public record CreateFormCreditCommandReq(FormCreditDto FormCreditDto) : IRequest<ResponseDto>;