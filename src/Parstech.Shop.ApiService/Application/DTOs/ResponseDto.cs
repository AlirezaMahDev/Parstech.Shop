using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Shop.Application.DTOs.Response
{
    public class ResponseDto
    {
        public bool IsSuccessed { get; set; }
        public Object Object { get; set; } = null!;
        public Object Object2 { get; set; } = null!;
        public Object role { get; set; } = null!;
        public Object CurrentParameter { get; set; } = null!;
        public List<ValidationFailure> Errors { get; set; } = null!;
        public string? Message { get; set; }
        
    }

    public class ErrorList
    {
        public string Caption { get; set; }
        public string ErrorMessage { get; set; }
    }
}
