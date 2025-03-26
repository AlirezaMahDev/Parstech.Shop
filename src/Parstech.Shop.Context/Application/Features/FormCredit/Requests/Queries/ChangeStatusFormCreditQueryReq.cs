using MediatR;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.FormCredit.Requests.Queries;

public record ChangeStatusFormCreditQueryReq(int Id,string Type):IRequest<ResponseDto>;