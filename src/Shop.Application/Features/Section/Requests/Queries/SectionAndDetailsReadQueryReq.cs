using MediatR;
using Shop.Application.DTOs.SiteSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.Section;

namespace Shop.Application.Features.Section.Requests.Queries
{

    public record SectionAndDetailsReadsQueryReq(int? storeId) : IRequest<List<SectionAndDetailsDto>>;
    public record SectionAndDetailsReadQueryReq(int Olaviat) : IRequest<SectionAndDetailsDto>;
    public record SectionAndDetailsReadByIdQueryReq(int id) : IRequest<SectionAndDetailsDto>;
    public record SectionAndDetailsReadStoreQueryReq(string userName) : IRequest<SectionAndDetailsDto>;

}
