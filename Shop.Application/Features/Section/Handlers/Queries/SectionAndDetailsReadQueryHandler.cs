using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Section;
using Shop.Application.DTOs.SiteSetting;
using Shop.Application.Features.Section.Requests.Queries;
using Shop.Domain.Models;
using static System.Collections.Specialized.BitVector32;

namespace Shop.Application.Features.Section.Handlers.Queries
{
    public class SectionAndDetailsReadsQueryHandler : IRequestHandler<SectionAndDetailsReadsQueryReq, List<SectionAndDetailsDto>>
    {
        private readonly ISectionRepository _sectionRep;
        private readonly ISectionDetailRepository _sectionDetailRep;
        private readonly IMapper _mapper;

        public SectionAndDetailsReadsQueryHandler(ISectionRepository sectionRep, ISectionDetailRepository sectionDetailRep, IMapper mapper)
        {
            _sectionRep = sectionRep;
            _sectionDetailRep = sectionDetailRep;
            _mapper = mapper;
        }

        public async Task<List<SectionAndDetailsDto>> Handle(SectionAndDetailsReadsQueryReq request, CancellationToken cancellationToken)
        {
            var result = new List<SectionAndDetailsDto>();
            var sections = new List<Domain.Models.Section>();
            if (request.storeId != null)
            {
                sections = await _sectionRep.GetSectionsOfStore(request.storeId.Value);
            }
            else
            {
                sections = await _sectionRep.GetSectionsOfStore(null);
            }

            foreach (var section in sections.OrderBy(u => u.Sort))
            {

                var sectionMap = _mapper.Map<SectionAndDetailsDto>(section);

                var sectionDetails = await _sectionDetailRep.GetDetailsOfSection(section.Id);
                var sectionDetailMap = _mapper.Map<List<SectionDetailShowDto>>(sectionDetails);
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
            _categuryRep= categuryRep;
            _mapper = mapper;
        }

        public async Task<SectionAndDetailsDto> Handle(SectionAndDetailsReadQueryReq request, CancellationToken cancellationToken)
        {
            SectionAndDetailsDto result = new SectionAndDetailsDto();
            var section = await _sectionRep.GetByOlaviat(request.Olaviat);
            if (section != null)
            {
                result = _mapper.Map<SectionAndDetailsDto>(section);

                var sectionDetails = await _sectionDetailRep.GetDetailsOfSection(section.Id);
                var sectionDetailMap = new List<SectionDetailShowDto>();

                foreach (var sectionDetail in sectionDetails)
                {
                    {
                        var dto = _mapper.Map<SectionDetailShowDto>(sectionDetail);
                        if (dto.CateguryId != null)
                        {
                            var cat = await _categuryRep.GetAsync(dto.CateguryId.Value);
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
    
    public class SectionAndDetailsReadQueryByIdHandler : IRequestHandler<SectionAndDetailsReadByIdQueryReq, SectionAndDetailsDto>
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
            _categuryRep= categuryRep;
            _mapper = mapper;
        }

        public async Task<SectionAndDetailsDto> Handle(SectionAndDetailsReadByIdQueryReq request, CancellationToken cancellationToken)
        {
            SectionAndDetailsDto result = new SectionAndDetailsDto();
            var section = await _sectionRep.GetAsync(request.id);
            if (section != null)
            {
                result = _mapper.Map<SectionAndDetailsDto>(section);

                var sectionDetails = await _sectionDetailRep.GetDetailsOfSection(section.Id);
                var sectionDetailMap = new List<SectionDetailShowDto>();

                foreach (var sectionDetail in sectionDetails)
                {
                    {
                        var dto = _mapper.Map<SectionDetailShowDto>(sectionDetail);
                        if (dto.CateguryId != null)
                        {
                            var cat = await _categuryRep.GetAsync(dto.CateguryId.Value);
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
    public class SectionAndDetailsReadStoreQueryHandler : IRequestHandler<SectionAndDetailsReadStoreQueryReq, SectionAndDetailsDto>
    {
        private readonly ISectionRepository _sectionRep;
        private readonly IUserRepository _userRep;
        private readonly IUserStoreRepository _userStoreRep;
        private readonly ICateguryRepository _categuryRep;
        private readonly ISectionDetailRepository _sectionDetailRep;
        private readonly IMapper _mapper;

        public SectionAndDetailsReadStoreQueryHandler(ISectionRepository sectionRep,
            ISectionDetailRepository sectionDetailRep, ICateguryRepository categuryRep,
            IUserStoreRepository userStoreRep, IUserRepository userRep,
            IMapper mapper)
        {
            _sectionRep = sectionRep;
            _sectionDetailRep = sectionDetailRep;
            _userStoreRep = userStoreRep;
            _categuryRep = categuryRep;
            _userStoreRep = userStoreRep;
            _mapper = mapper;
        }

        public async Task<SectionAndDetailsDto> Handle(SectionAndDetailsReadStoreQueryReq request, CancellationToken cancellationToken)
        {
            SectionAndDetailsDto result = new SectionAndDetailsDto();
           
            var userstore =await _userStoreRep.GetStoreByLatinName(request.userName);
            var section = await _sectionRep.GetByStore(userstore.Id);
            if (section != null)
            {
                result = _mapper.Map<SectionAndDetailsDto>(section);

                var sectionDetails = await _sectionDetailRep.GetDetailsOfSection(section.Id);
                var sectionDetailMap = new List<SectionDetailShowDto>();

                foreach (var sectionDetail in sectionDetails)
                {
                    {
                        var dto = _mapper.Map<SectionDetailShowDto>(sectionDetail);
                        if (dto.CateguryId != null)
                        {
                            var cat = await _categuryRep.GetAsync(dto.CateguryId.Value);
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
}
