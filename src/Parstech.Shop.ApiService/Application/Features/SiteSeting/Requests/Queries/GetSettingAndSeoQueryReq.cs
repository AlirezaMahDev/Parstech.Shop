using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.SiteSeting.Requests.Queries;

public record GetSettingAndSeoQueryReq() : IRequest<AllSettingAndSeoDto>;