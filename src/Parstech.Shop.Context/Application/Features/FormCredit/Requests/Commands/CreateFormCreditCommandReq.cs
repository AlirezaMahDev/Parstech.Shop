using MediatR;
using Parstech.Shop.Context.Application.DTOs.FormCredit;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.FormCredit.Requests.Commands;

public record CreateFormCreditCommandReq(FormCreditDto FormCreditDto):IRequest<ResponseDto>;