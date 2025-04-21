using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DTOs.Paging
{
    public class PagingDto
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public Array List { get; set; }
    }

    public class ParameterDto
    {
        public int CurrentPage { get; set; }
        public int TakePage { get; set; }
        public int Skip { get; set; }
        public int PageCount { get; set; }
        public string Filter { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string store { get; set; }
        public int Status { get; set; }
        public int PayType { get; set; }
        public int UserId { get; set; }
        public int Removed { get; set; }
    }

    public class CountDto
    {
        public int Count { get; set; }
        
    }


}
