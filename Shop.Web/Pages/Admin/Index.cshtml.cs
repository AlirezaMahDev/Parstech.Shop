using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.Reports;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Counts.Requests.Queries;

namespace Shop.Web.Pages.Admin
{
    [Authorize(Roles = "SupperUser,Inventory,Sale,Finanicial,Store")]
    public class IndexModel : PageModel
    {
        #region Constractor

        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Property

        public IndexCountsDto IndexReport { get; set; }
        [BindProperty] public ResponseDto Response { get; set; } = new ResponseDto();
        #endregion

        #region Get
        public void OnGet()
        {

        }


        #endregion

        #region Data
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> OnPostGetData()
        {
            IndexReport = await _mediator.Send(new IndexCountsQueryReq());
            Response.IsSuccessed = true;
            Response.Object = IndexReport;
            return new JsonResult(Response);
        }

        #endregion


    }
}