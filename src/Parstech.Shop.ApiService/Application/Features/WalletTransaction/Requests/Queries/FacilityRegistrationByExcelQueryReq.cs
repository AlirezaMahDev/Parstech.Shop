using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.WalletTransaction.Requests.Queries;

public record FacilityRegistrationByExcelQueryReq(string type, IFormFile file) : IRequest<ResponseDto>;