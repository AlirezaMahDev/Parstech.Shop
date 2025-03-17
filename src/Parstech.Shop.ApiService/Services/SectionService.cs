using Grpc.Core;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Section.Requests.Queries;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Services;

public class SectionService : SectionServiceBase
{
    private readonly IMediator _mediator;
    private readonly ISectionRepository _sectionRepository;

    public SectionService(IMediator mediator, ISectionRepository sectionRepository)
    {
        _mediator = mediator;
        _sectionRepository = sectionRepository;
    }

    public override async Task<SectionResponse> GetSections(SectionRequest request, ServerCallContext context)
    {
        try
        {
            var query = new SectionAndDetailsReadsQueryReq(request.ParentId);
            var sections = await _mediator.Send(query);

            var response = new SectionResponse();
            response.Sections.AddRange(sections.Select(s => new Section
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Image = s.Image,
                ParentId = s.ParentId,
                IsActive = s.IsActive,
                Details =
                {
                    s.Details.Select(d => new SectionDetail
                    {
                        Id = d.Id,
                        SectionId = d.SectionId,
                        Title = d.Title,
                        Description = d.Description,
                        Image = d.Image,
                        Link = d.Link,
                        IsActive = d.IsActive,
                        Order = d.Order
                    })
                }
            }));

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<SectionDetailsResponse> GetSectionDetails(SectionDetailsRequest request,
        ServerCallContext context)
    {
        try
        {
            var details = await _sectionRepository.GetSectionDetails(request.SectionId);

            var response = new SectionDetailsResponse();
            response.Details.AddRange(details.Select(d => new SectionDetail
            {
                Id = d.Id,
                SectionId = d.SectionId,
                Title = d.Title,
                Description = d.Description,
                Image = d.Image,
                Link = d.Link,
                IsActive = d.IsActive,
                Order = d.Order
            }));

            return response;
        }
        catch (Exception ex)
        {
            throw new RpcException(new(StatusCode.Internal, ex.Message));
        }
    }
}