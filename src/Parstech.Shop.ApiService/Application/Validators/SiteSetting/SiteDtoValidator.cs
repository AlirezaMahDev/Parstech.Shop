using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.SiteSetting;

namespace Shop.Application.Validators.SiteSetting
{
    public class SiteDtoValidator : AbstractValidator<SiteDto>
    {
        public SiteDtoValidator()
        {

            
        }
    }
}
