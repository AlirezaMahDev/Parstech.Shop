﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Property;
using Shop.Application.DTOs.PropertyCategury;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Categury.Requests.Commands;
using Shop.Application.Features.Property.Requests.Commands;
using Shop.Application.Features.Property.Requests.Queries;
using Shop.Application.Features.PropertyCategury.Requests.Commands;

namespace Shop.Web.Pages.Admin.Feutures
{
    [Authorize(Roles = "SupperUser,Sale")]
    public class IndexModel : PageModel
    {
        #region Constractor

        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Properties

        //paging parameter
        [BindProperty]
        public PropertyParameterDto Parameter { get; set; } = new PropertyParameterDto();

        //categuries
        [BindProperty]
        public PagingDto List { get; set; }

        //categury
        [BindProperty]
        public PropertyDto PropertyDto { get; set; }

        [BindProperty]
        public int PropertyId { get; set; }

        //result
        [BindProperty]
        public ResponseDto Response { get; set; } = new ResponseDto();


        [BindProperty]
        public List<CateguryDto> CateguryDtos { get; set; }

        [BindProperty]
        public List<PropertyCateguryDto> PropertyCateguryDtos { get; set; }

        [BindProperty] public string FilterCat { get; set; }

        #endregion

        #region Get

        public async Task<IActionResult> OnGet()
        {
            CateguryDtos = await _mediator.Send(new CateguryReadCommandReq(null));
            PropertyCateguryDtos = await _mediator.Send(new PropertyCateguryReadsCommandReq());
            return Page();
        }

        public async Task<IActionResult> OnPostData()
        {
            //Parameter.CurrentPage = 1;
            Parameter.TakePage = 30;
            List = await _mediator.Send(new PropertyPagingQueryReq(Parameter));
            Response.Object = List;
            Response.IsSuccessed = true;

            return new JsonResult(Response);
        }

        #endregion

        #region Search Paging

        public async Task<IActionResult> OnPostSearch()
        {
            Parameter.CurrentPage = 1;
            Parameter.TakePage = 30;
            List = await _mediator.Send(new PropertyPagingQueryReq(Parameter));
            Response.Object = List;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        public async Task<IActionResult> OnPostPaging()
        {
            Parameter.TakePage = 30;
            List = await _mediator.Send(new PropertyPagingQueryReq(Parameter));
            Response.Object = List;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        #endregion
        #region Add Or EditCategury

        public async Task<IActionResult> OnPostItem()
        {
            PropertyDto = await _mediator.Send(new PropertyReadCommandReq(PropertyId));
            Response.Object = PropertyDto;
            return new JsonResult(Response);
        }

        public async Task<IActionResult> OnPostEditOrCreate()
        {
            if (PropertyDto.Id != 0)
            {
                await _mediator.Send(new PropertyUpdateCommandReq(PropertyDto));
                Response.Object = PropertyDto;
                Response.IsSuccessed = true;
                Response.Message = "دسته بندی با موفقیت ویرایش شد";
                return new JsonResult(Response);
            }
            else
            {
                await _mediator.Send(new PropertyCreateCommandReq(PropertyDto));
                Response.Object = PropertyDto;
                Response.IsSuccessed = true;
                Response.Message = "دسته بندی با موفقیت ثبت شد";
                return new JsonResult(Response);
            }
        }

        public async Task<IActionResult> OnPostGetAllCateguries()
        {
            var categuries = await _mediator.Send(new CateguryReadCommandReq(FilterCat));
            Response.Object = categuries;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }
        #endregion
    }
}
