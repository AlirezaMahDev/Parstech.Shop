using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.PayType;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Coupon.Requests.Queries;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.OrderDetail.Requests.Commands;
using Shop.Application.Features.OrderDetail.Requests.Queries;
using Shop.Application.Features.OrderShipping.Request.Queries;
using Shop.Application.Features.UserShipping.Requests.Queries;
using Shop.Application.Features.Wallet.Requests.Queries;
using Shop.Application.Features.WalletTransaction.Requests.Queries;

namespace Shop.Web.Pages.Checkout
{
    [Authorize]
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
        //result
        [BindProperty]
        public ResponseDto Response { get; set; } = new ResponseDto();
        public List<PayTypeDto> payTypes { get; set; }
        #endregion

        #region Get
        public IActionResult OnGet()
        {

            return Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostData()
        {
            var order = await _mediator.Send(new GetOpenOrderOfUserQueryReq(User.Identity.Name));
            await _mediator.Send(new RefreshOrderQueryReq(order.OrderId));
            if (order.OrderId != 0)
            {

                var orderShipping = await _mediator.Send(new OrderShippingGetByOrderIdQueryReq(order.OrderId));
                if (orderShipping.Id == 0)
                {
                    var ShippingId = await _mediator.Send(new GetFirstUserShippingQueryReq(order.UserId));
                    await _mediator.Send(new OrderShippingChangeQueryReq("Change", ShippingId, order.OrderId, 0));
                }

            }


            var Result = await _mediator.Send(new OrderDetailShowQueryReq(order.OrderId));
            Response.IsSuccessed = true;
            Response.Object = Result;


            return new JsonResult(Response);
        }


        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostGetWallet(int userId, int type)
        {
            ResponseDto Response = new ResponseDto();
            string typeName = null;


            if (User.Identity.IsAuthenticated)
            {
                var wallet = await _mediator.Send(new GetWalletByUserIdQueryReq(userId));
                Response.Object = wallet;
                Response.IsSuccessed = true;
                switch (type)
                {
                    case 3:
                        typeName = "Fecilities";
                        var activeTransaction = await _mediator.Send(new GetActiveAghsatTransactionQueryReq(wallet.WalletId, typeName));
                        if (activeTransaction != null)
                        {

                            Response.Object2 = activeTransaction;
                        }


                        break;
                    case 4:
                        typeName = "OrgCredit";
                        var activeTransaction2 = await _mediator.Send(new GetActiveAghsatTransactionQueryReq(wallet.WalletId, typeName));
                        if (activeTransaction2 != null)
                        {

                            Response.Object2 = activeTransaction2;
                        }


                        break;
                    default:
                        break;
                }


            }

            return new JsonResult(Response);
        }
        #endregion

        #region OrderDetail
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostChangeDetail(int detailId, int count)
        {
            //var orderDetail =new Application.DTOs.OrderDetail.OrderDetailDto();
            var orderDetail = await _mediator.Send(new OrderDetailReadCommandReq(detailId));
            orderDetail.Count = count;
            var result = await _mediator.Send(new RefreshOrderDetailQueryReq(orderDetail));
            Response.IsSuccessed = result.Status;
            Response.Object = result;

            return new JsonResult(Response);
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _mediator.Send(new OrderDetailDeleteCommandReq(id));
            Response.IsSuccessed = true;
            Response.Message = "عملیات یا موفقیت انجام شد";
            return new JsonResult(Response);
        }
        #endregion

        #region Coupon
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostUseCoupon(int orderId, string code)
        {
            var result = await _mediator.Send(new UseCouponQueryReq(orderId, code));
            Response.IsSuccessed = result.Status;
            Response.Object = result;
            return new JsonResult(Response);
        }
        #endregion

        #region Shipping
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostChangeShipping(int userShippingId, int orderId)
        {
            var item = await _mediator.Send(new OrderShippingChangeQueryReq("Change", userShippingId, orderId, 0));
            Response.IsSuccessed = true;
            Response.Message = "عملیات با موفقیت انجام شد";
            return new JsonResult(Response);
        }
        #endregion
        #region Complete

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostCalculateAghsat(long price, int transactionId, int month)
        {
            Response = await _mediator.Send(new CalculateAghsatQueryReq(price, transactionId, month));
            return new JsonResult(Response);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostComplete(int orderId, int orderShippingId, int payTypeId, int? transactionId, int month)
        {
            Response = await _mediator.Send(new CompleteOrderQueryReq(orderId, orderShippingId, payTypeId, transactionId, month));
            return new JsonResult(Response);
        }
        #endregion


    }
}
