using MediatR;
using Microsoft.AspNetCore.Http;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Queries;

public record FacilityPaymentByExcelQueryReq(IFormFile file):IRequest<ResponseDto>;