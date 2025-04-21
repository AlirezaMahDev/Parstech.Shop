using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.Api
{
    public class SearchReqDto
    {
        public string Filter { get; set; } = null!;
        public int Top_k { get; set; }
    }
    public class SearchResDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ShortLink { get; set; }
        public int Score { get; set; }
    }
}
