using MediatR;
using Parstech.Shop.Context.Application.DTOs.SiteSetting;

namespace Parstech.Shop.Context.Application.Features.SiteSeting.Requests.Queries;

public record GetSettingAndSeoQueryReq():IRequest<AllSettingAndSeoDto>;