using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Section.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Section.Handlers.Queries;

public class
    SectionAndDetailsReadsQueryHandler : IRequestHandler<SectionAndDetailsReadsQueryReq, List<SectionAndDetailsDto>>
{
    private readonly ISectionRepository _sectionRep;
    private readonly ISectionDetailRepository _sectionDetailRep;
    private readonly IMapper _mapper;

    public SectionAndDetailsReadsQueryHandler(ISectionRepository sectionRep,
        ISectionDetailRepository sectionDetailRep,
        IMapper mapper)
    {
        _sectionRep = sectionRep;
        _sectionDetailRep = sectionDetailRep;
        _mapper = mapper;
    }

    public async Task<List<SectionAndDetailsDto>> Handle(SectionAndDetailsReadsQueryReq request,
        CancellationToken cancellationToken)
    {
        List<SectionAndDetailsDto>? result = new();
        List<Domain.Models.Section> sections = new();
        if (request.storeId != null)
        {
            sections = await _sectionRep.GetSectionsOfStore(request.storeId.Value);
        }
        else
        {
            sections = await _sectionRep.GetSectionsOfStore(null);
        }

        foreach (Domain.Models.Section section in sections.OrderBy(u => u.Sort))
        {
            SectionAndDetailsDto? sectionMap = _mapper.Map<SectionAndDetailsDto>(section);

            List<Domain.Models.SectionDetail> sectionDetails = await _sectionDetailRep.GetDetailsOfSection(section.Id);
            List<SectionDetailShowDto>? sectionDetailMap = _mapper.Map<List<SectionDetailShowDto>>(sectionDetails);
            sectionMap.SectionDetails = sectionDetailMap;
            //sectionMap.SectionDetails = _mapper.Map<SectionAndDetailsDto>(sectionDetails).SectionDetails;
            result.Add(sectionMap);
        }

        return result;
    }
}

public class SectionAndDetailsReadQueryHandler : IRequestHandler<SectionAndDetailsReadQueryReq, SectionAndDetailsDto>
{
    private readonly ISectionRepository _sectionRep;
    private readonly ICateguryRepository _categuryRep;
    private readonly ISectionDetailRepository _sectionDetailRep;
    private readonly IMapper _mapper;

    public SectionAndDetailsReadQueryHandler(ISectionRepository sectionRep,
        ISectionDetailRepository sectionDetailRep,
        ICateguryRepository categuryRep,
        IMapper mapper)
    {
        _sectionRep = sectionRep;
        _sectionDetailRep = sectionDetailRep;
        _categuryRep = categuryRep;
        _mapper = mapper;
    }

    public async Task<SectionAndDetailsDto> Handle(SectionAndDetailsReadQueryReq request,
        CancellationToken cancellationToken)
    {
        SectionAndDetailsDto result = new();
        Domain.Models.Section section = await _sectionRep.GetByOlaviat(request.Olaviat);
        if (section != null)
        {
            result = _mapper.Map<SectionAndDetailsDto>(section);

            List<Domain.Models.SectionDetail> sectionDetails = await _sectionDetailRep.GetDetailsOfSection(section.Id);
            List<SectionDetailShowDto> sectionDetailMap = new();

            foreach (Domain.Models.SectionDetail sectionDetail in sectionDetails)
            {
                {
                    SectionDetailShowDto? dto = _mapper.Map<SectionDetailShowDto>(sectionDetail);
                    if (dto.CateguryId != null)
                    {
                        Domain.Models.Categury? cat = await _categuryRep.GetAsync(dto.CateguryId.Value);
                        dto.LatingCateguryName = cat.LatinGroupTitle;
                    }

                    sectionDetailMap.Add(dto);
                }
            }

            result.SectionDetails = sectionDetailMap;
        }

        return result;
    }
}

public class
    SectionAndDetailsReadQueryByIdHandler : IRequestHandler<SectionAndDetailsReadByIdQueryReq, SectionAndDetailsDto>
{
    private readonly ISectionRepository _sectionRep;
    private readonly ICateguryRepository _categuryRep;
    private readonly ISectionDetailRepository _sectionDetailRep;
    private readonly IMapper _mapper;

    public SectionAndDetailsReadQueryByIdHandler(ISectionRepository sectionRep,
        ISectionDetailRepository sectionDetailRep,
        ICateguryRepository categuryRep,
        IMapper mapper)
    {
        _sectionRep = sectionRep;
        _sectionDetailRep = sectionDetailRep;
        _categuryRep = categuryRep;
        _mapper = mapper;
    }

    public async Task<SectionAndDetailsDto> Handle(SectionAndDetailsReadByIdQueryReq request,
        CancellationToken cancellationToken)
    {
        SectionAndDetailsDto result = new();
        Domain.Models.Section? section = await _sectionRep.GetAsync(request.id);
        if (section != null)
        {
            result = _mapper.Map<SectionAndDetailsDto>(section);

            List<Domain.Models.SectionDetail> sectionDetails = await _sectionDetailRep.GetDetailsOfSection(section.Id);
            List<SectionDetailShowDto> sectionDetailMap = new();

            foreach (Domain.Models.SectionDetail sectionDetail in sectionDetails)
            {
                {
                    SectionDetailShowDto? dto = _mapper.Map<SectionDetailShowDto>(sectionDetail);
                    if (dto.CateguryId != null)
                    {
                        Domain.Models.Categury? cat = await _categuryRep.GetAsync(dto.CateguryId.Value);
                        dto.LatingCateguryName = cat.LatinGroupTitle;
                    }

                    sectionDetailMap.Add(dto);
                }
            }

            result.SectionDetails = sectionDetailMap;
        }

        return result;
    }
}

public class
    SectionAndDetailsReadStoreQueryHandler : IRequestHandler<SectionAndDetailsReadStoreQueryReq, SectionAndDetailsDto>
{
    private readonly ISectionRepository _sectionRep;
    private readonly IUserRepository _userRep;
    private readonly IUserStoreRepository _userStoreRep;
    private readonly ICateguryRepository _categuryRep;
    private readonly ISectionDetailRepository _sectionDetailRep;
    private readonly IMapper _mapper;

    public SectionAndDetailsReadStoreQueryHandler(ISectionRepository sectionRep,
        ISectionDetailRepository sectionDetailRep,
        ICateguryRepository categuryRep,
        IUserStoreRepository userStoreRep,
        IUserRepository userRep,
        IMapper mapper)
    {
        _sectionRep = sectionRep;
        _sectionDetailRep = sectionDetailRep;
        _userStoreRep = userStoreRep;
        _categuryRep = categuryRep;
        _userStoreRep = userStoreRep;
        _mapper = mapper;
    }

    public async Task<SectionAndDetailsDto> Handle(SectionAndDetailsReadStoreQueryReq request,
        CancellationToken cancellationToken)
    {
        SectionAndDetailsDto result = new();

        Domain.Models.UserStore userstore = await _userStoreRep.GetStoreByLatinName(request.userName);
        Domain.Models.Section section = await _sectionRep.GetByStore(userstore.Id);
        if (section != null)
        {
            result = _mapper.Map<SectionAndDetailsDto>(section);

            List<Domain.Models.SectionDetail> sectionDetails = await _sectionDetailRep.GetDetailsOfSection(section.Id);
            List<SectionDetailShowDto> sectionDetailMap = new();

            foreach (Domain.Models.SectionDetail sectionDetail in sectionDetails)
            {
                {
                    SectionDetailShowDto? dto = _mapper.Map<SectionDetailShowDto>(sectionDetail);
                    if (dto.CateguryId != null)
                    {
                        Domain.Models.Categury? cat = await _categuryRep.GetAsync(dto.CateguryId.Value);
                        dto.LatingCateguryName = cat.LatinGroupTitle;
                    }

                    sectionDetailMap.Add(dto);
                }
            }

            result.SectionDetails = sectionDetailMap;
        }

        return result;
    }
}