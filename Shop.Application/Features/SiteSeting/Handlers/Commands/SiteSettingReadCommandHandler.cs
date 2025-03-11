using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.SiteSetting;
using Shop.Application.Features.SiteSeting.Requests.Commands;
using Shop.Domain.Models;

namespace Shop.Application.Features.SiteSeting.Handlers.Commands
{
    
    public class SiteSettingReadCommandHandler : IRequestHandler<SiteSettingReadCommandReq, SiteDto>
    {
        #region Constractor
        private ISiteSettingRepository _siteSettingRep;
        private readonly IMapper _mapper;

        public SiteSettingReadCommandHandler(ISiteSettingRepository siteSettingRep, IMapper mapper)
        {
            _siteSettingRep = siteSettingRep;
            _mapper = mapper;
        }



        #endregion
        

        public async Task<SiteDto> Handle(SiteSettingReadCommandReq request, CancellationToken cancellationToken)
        {
            var siteSetting = await _siteSettingRep.GetAsync(request.id);
            return _mapper.Map<SiteDto>(siteSetting);
        }
    }

    public class SeoSettingReadCommandHandler : IRequestHandler<SeoSettingReadCommandReq, SeoDto>
    {
        #region Constractor
        private ISiteSettingRepository _siteSettingRep;
        private readonly IMapper _mapper;

        public SeoSettingReadCommandHandler(ISiteSettingRepository siteSettingRep, IMapper mapper)
        {
            _siteSettingRep = siteSettingRep;
            _mapper = mapper;
        }

        #endregion
        public async Task<SeoDto> Handle(SeoSettingReadCommandReq request, CancellationToken cancellationToken)
        {
            var siteSetting = await _siteSettingRep.GetAsync(request.id);
            return _mapper.Map<SeoDto>(siteSetting);
        }
    }
}
