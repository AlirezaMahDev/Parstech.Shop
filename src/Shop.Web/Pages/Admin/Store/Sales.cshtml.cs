using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.OrderDetail;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.UserStore;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.OrderDetail.Requests.Queries;
using Shop.Application.Features.OrderStatus.Requests.Queries;
using Shop.Application.Features.User.Requests.Queries;
using Shop.Application.Features.UserStore.Requests.Commands;
using Shop.Application.Features.UserStore.Requests.Queries;

namespace Shop.Web.Pages.Admin.Store
{



    [Authorize(Roles = "SupperUser,Finanicial,Sale,Store")]
    public class SalesModel : PageModel
    {

        #region Constractor
        private readonly IMediator _mediator;
        public SalesModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Properties
        [BindProperty]
        public SalesParameterDto parameters { get; set; }
        public SalesPagingDto result { get; set; }
        public List<UserForSelectListDto> Users { get; set; }

        [BindProperty]
        public List<UserStoreDto> UserStores { get; set; } = new List<UserStoreDto>();


        [BindProperty]
        public ResponseDto Response { get; set; } = new ResponseDto();
        #endregion
        public async Task<IActionResult> OnGet()
        {

            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostGetData()
        {
            parameters.CurrentPage = 1;
            parameters.TakePage = 100;
            if (User.IsInRole("SupperUser"))
            {
                result = await _mediator.Send(new OrderDetailsForStoreReportQueryReq(parameters, true));
                UserStores = await _mediator.Send(new UserStoreReadsCommandReq());
            }
            else if (User.IsInRole("Store"))
            {

                var user = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
                var userStore = await _mediator.Send(new UserStoreOfUserReadQueryReq(user.Id));

                UserStores.Add(userStore);
                parameters.StoreId = userStore.Id;
                result = await _mediator.Send(new OrderDetailsForStoreReportQueryReq(parameters, false));

            }

            result.StoresSelect = UserStores.ToArray();
            return new JsonResult(result);
        }
        public async Task<IActionResult> OnPostSearch()
        {
            parameters.TakePage = 100;
            if (User.IsInRole("SupperUser"))
            {
                result = await _mediator.Send(new OrderDetailsForStoreReportQueryReq(parameters, true));
                UserStores = await _mediator.Send(new UserStoreReadsCommandReq());
            }
            else if (User.IsInRole("Store"))
            {

                var user = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
                var userStore = await _mediator.Send(new UserStoreOfUserReadQueryReq(user.Id));

                UserStores.Add(userStore);
                parameters.StoreId = userStore.Id;
                result = await _mediator.Send(new OrderDetailsForStoreReportQueryReq(parameters, false));

            }
            return new JsonResult(result);
        }
        #region Get Statuses

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostGetStatuses(int OrderId)
        {

            var list = await _mediator.Send(new GetOrderStatusByOrderIdQueryReq(OrderId));
            Response.Object = list;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        #endregion
        #region Contract
        public async Task<IActionResult> OnPostGetContract(int OrderId)
        {

            if (User.IsInRole("SupperUser") || User.IsInRole("Sale") || User.IsInRole("Finanicial"))
            {
                var res = await _mediator.Send(new ContractOrderQueryReq(OrderId, "All"));
                return new JsonResult(res);
            }
            else if (User.IsInRole("Store"))
            {

                var user = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
                var userStore = await _mediator.Send(new UserStoreOfUserReadQueryReq(user.Id));

                var res = await _mediator.Send(new ContractOrderQueryReq(OrderId, userStore.LatinStoreName));
                return new JsonResult(res);
            }
            else { return new JsonResult(Response); }

        }
        #endregion
    }
}
