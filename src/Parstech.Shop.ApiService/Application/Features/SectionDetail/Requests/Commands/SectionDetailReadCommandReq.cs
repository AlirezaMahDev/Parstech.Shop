﻿using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.SectionDetail.Requests.Commands;

public record SectionDetailReadCommandReq(int id) : IRequest<SectionDetailDto>;