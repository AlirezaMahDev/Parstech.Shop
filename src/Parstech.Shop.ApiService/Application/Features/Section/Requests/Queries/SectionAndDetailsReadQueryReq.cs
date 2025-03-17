using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Section.Requests.Queries;

public record SectionAndDetailsReadsQueryReq(int? storeId) : IRequest<List<SectionAndDetailsDto>>;

public record SectionAndDetailsReadQueryReq(int Olaviat) : IRequest<SectionAndDetailsDto>;

public record SectionAndDetailsReadByIdQueryReq(int id) : IRequest<SectionAndDetailsDto>;

public record SectionAndDetailsReadStoreQueryReq(string userName) : IRequest<SectionAndDetailsDto>;