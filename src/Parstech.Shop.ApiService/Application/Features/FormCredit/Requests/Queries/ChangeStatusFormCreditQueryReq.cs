using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.FormCredit.Requests.Queries;

public record ChangeStatusFormCreditQueryReq(int Id, string Type) : IRequest<ResponseDto>;