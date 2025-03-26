using MediatR;
using Microsoft.AspNetCore.Http;
using Parstech.Shop.Context.Application.DTOs.Response;

namespace Parstech.Shop.Context.Application.Features.WalletTransaction.Requests.Queries;

public record FacilityRegistrationByExcelQueryReq(string type,IFormFile file):IRequest<ResponseDto>;