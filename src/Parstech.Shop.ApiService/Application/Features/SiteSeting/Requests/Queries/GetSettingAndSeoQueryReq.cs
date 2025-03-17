using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.SiteSeting.Requests.Queries;

public record GetSettingAndSeoQueryReq() : IRequest<AllSettingAndSeoDto>;