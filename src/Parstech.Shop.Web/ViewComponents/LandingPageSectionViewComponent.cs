using Microsoft.AspNetCore.Mvc;
using Parstech.Shop.Shared.Protos.Section;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.ViewComponents
{
    [ViewComponent(Name = "LandingPageSection")]
    public class LandingPageSectionViewComponent : ViewComponent
    {
        private readonly SectionGrpcClient _sectionGrpcClient;
        
        public LandingPageSectionViewComponent(SectionGrpcClient sectionGrpcClient)
        {
            _sectionGrpcClient = sectionGrpcClient;
        }
        
        public async Task<IViewComponentResult> InvokeAsync(int? sectionId)
        {
            if (!sectionId.HasValue)
            {
                return View("Default");
            }
            
            var section = await _sectionGrpcClient.GetSectionAndDetailsByIdAsync(sectionId.Value);
            
            switch (section.SectionTypeId)
            {
                case 1: // Slider
                    var sliderModel = new SliderViewModel
                    {
                        Desktop = section.SectionDetails.Where(d => d.Title != "Mobile").ToList(),
                        Mobile = section.SectionDetails.Where(d => d.Title == "Mobile").ToList()
                    };
                    return View("SlideShow", sliderModel);
                
                case 2: // List Products
                    return View("ListProducts", section);
                
                case 3: // Discount
                    return View("Discount", section);
                
                case 4: // Two Banner
                    return View("TwoBanner", section);
                
                case 7: // Icons
                    return View("Icons", section);
                
                case 8: // Category Icons
                    return View("CateguryIcons", section);
                
                case 9: // Brand Slider
                    return View("BrandSlider", section);
                
                default:
                    return View("Default");
            }
        }
    }
    
    public class SliderViewModel
    {
        public List<SectionDetail> Desktop { get; set; }
        public List<SectionDetail> Mobile { get; set; }
    }
}
