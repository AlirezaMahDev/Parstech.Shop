using MediatR;

using Parstech.Shop.Context.Application.DTOs.Section;

namespace Parstech.Shop.Context.Application.Features.Section.Requests.Queries;

public record SectionAndDetailsReadsQueryReq(int? storeId) : IRequest<List<SectionAndDetailsDto>>;
public record SectionAndDetailsReadQueryReq(int Olaviat) : IRequest<SectionAndDetailsDto>;
public record SectionAndDetailsReadByIdQueryReq(int id) : IRequest<SectionAndDetailsDto>;
public record SectionAndDetailsReadStoreQueryReq(string userName) : IRequest<SectionAndDetailsDto>;