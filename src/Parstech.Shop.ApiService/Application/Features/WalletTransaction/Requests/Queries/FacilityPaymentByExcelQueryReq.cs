using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;

public record FacilityPaymentByExcelQueryReq(IFormFile file) : IRequest<ResponseDto>;