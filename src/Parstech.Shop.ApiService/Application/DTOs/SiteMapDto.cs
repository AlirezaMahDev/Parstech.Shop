using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.SiteMap
{
    public class SitemapDto
    {
        public string loc { get; set; }
        public string lastmod { get; set; }
        public string priority { get; set; }
    }
    public class SiteMapCategury
    {
        public string LatinGroupTitle { get; set; }
    }
    public class SiteMapProducts
    {
        public string ShortLink { get; set; }
        public DateTime CreateDate { get; set; }
        public int Id { get; set; }
    }
    public class SiteMapStore
    {
        public string LatinStoreName { get; set; }
        
    }
}
