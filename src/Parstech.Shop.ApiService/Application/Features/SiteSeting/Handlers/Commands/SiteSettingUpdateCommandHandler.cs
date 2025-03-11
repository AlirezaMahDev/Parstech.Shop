using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.SiteSeting.Requests.Commands;
using Shop.Application.Generator;
using Shop.Domain.Models;

namespace Shop.Application.Features.SiteSeting.Handlers.Commands
{
    
    public class SiteSettingUpdateCommandHandler : IRequestHandler<SiteSettingUpdateCommandReq, Unit>
    {
        #region Constractor
        private ISiteSettingRepository _siteSettingRep;
        private readonly IMapper _mapper;

        public SiteSettingUpdateCommandHandler(ISiteSettingRepository siteSettingRep, IMapper mapper)
        {
            _siteSettingRep = siteSettingRep;
            _mapper = mapper;
        }

        #endregion

        public async Task<Unit> Handle(SiteSettingUpdateCommandReq request, CancellationToken cancellationToken)
        {
            var siteSetting = await _siteSettingRep.GetAsync(request.id);

            if (request.SiteDto.LogoFile!=null)
            {
                string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images", siteSetting.Logo);
                using (FileStream fs = new FileStream(tempFile, FileMode.Open)) {}
                try
                {
                    File.Delete(tempFile);
                    request.SiteDto.Logo = "logo" + Path.GetExtension(request.SiteDto.LogoFile.FileName);
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images", request.SiteDto.Logo);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        request.SiteDto.LogoFile.CopyTo(stream);
                    }
                    
                }
                catch (Exception e)
                {
                }
            }
            if (request.SiteDto.EnamadFile != null)
            {
                string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images", siteSetting.Enamad);
                using (FileStream fs = new FileStream(tempFile, FileMode.Open)) { }
                try
                {
                    request.SiteDto.Enamad = NameGenerator.GenerateUniqCode() + Path.GetExtension(request.SiteDto.EnamadFile.FileName);
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images", request.SiteDto.Enamad);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        request.SiteDto.EnamadFile.CopyTo(stream);
                    }
                    File.Delete(tempFile);
                }
                catch (Exception e)
                {
                }
            }
            if (request.SiteDto.EnamadFile != null)
            {
                string tempFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images", siteSetting.EtaemadElectronic);
                using (FileStream fs = new FileStream(tempFile, FileMode.Open)) { }
                try
                {
                    request.SiteDto.EtaemadElectronic = NameGenerator.GenerateUniqCode() + Path.GetExtension(request.SiteDto.EtaemadElectronicFile.FileName);
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Images", request.SiteDto.EtaemadElectronic);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        request.SiteDto.LogoFile.CopyTo(stream);
                    }
                    File.Delete(tempFile);
                }
                catch (Exception e)
                {
                }
            }

            _mapper.Map(request.SiteDto, siteSetting);
            await _siteSettingRep.UpdateAsync(siteSetting);

            return Unit.Value;
        }
    }

    public class SeoSettingUpdateCommandHandler : IRequestHandler<SeoSettingUpdateCommandReq, Unit>
    {
        #region Constractor
        private ISiteSettingRepository _siteSettingRep;
        private readonly IMapper _mapper;

        public SeoSettingUpdateCommandHandler(ISiteSettingRepository siteSettingRep, IMapper mapper)
        {
            _siteSettingRep = siteSettingRep;
            _mapper = mapper;
        }

        #endregion
        public async Task<Unit> Handle(SeoSettingUpdateCommandReq request, CancellationToken cancellationToken)
        {
            var seoeSetting = await _siteSettingRep.GetAsync(request.id);
            
            _mapper.Map(request.SeoDto, seoeSetting);
            await _siteSettingRep.UpdateAsync(seoeSetting);

            return Unit.Value;
        }
    }
}
