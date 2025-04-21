
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.Section;
using Shop.Application.DTOs.User;
using Shop.Application.Features.Brand.Requests.Commands;
using Shop.Application.Features.Order.Requests.Queries;
using Shop.Application.Features.Product.Requests.Commands;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Application.Features.Section.Requests.Queries;
using Shop.Application.Features.Security.Requests.Queries;
using Shop.Application.Features.User.Requests.Queries;
using Shop.Application.Features.UserProduct.Requests.Command;


namespace Shop.Web.Pages
{
    public class IndexModel : PageModel
    {
        #region Constractor
        private readonly IMediator _mediator;
        private readonly IProductRepository _productRep;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(
            IMediator mediator,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IProductRepository productRep)
        {
            _mediator = mediator;
            _userManager = userManager;
            _signInManager = signInManager;
            _productRep = productRep;
        }
        #endregion

        #region Properties
        //result
        [BindProperty]
        public ResponseDto Response { get; set; } = new ResponseDto();

        [BindProperty]
        public List<SectionAndDetailsDto> Sections { get; set; }
        #endregion

        #region Get
        #endregion
        public async Task<IActionResult> OnGet()
        {
           
            Sections = await _mediator.Send(new SectionAndDetailsReadsQueryReq(null));
            
            return Page();
        }
        //public async Task<IActionResult> OnPostData()
        //{
        //var Item = await _mediator.Send(new SectionAndDetailsReadQueryReq(1));
        //Response.IsSuccessed = true;
        //Response.Object = Item;
        // return new JsonResult(Response);
        //}

        //public async Task<IActionResult> OnPostTest()
        //{
        //    //Parameter.CurrentPage = 1;


        //}
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostSearch(string Filter)
        {
            ProductSearchParameterDto parameter = new ProductSearchParameterDto();
            
            parameter.Take = 4;
            parameter.Filter = Filter;
            #region Get User If Authenticated
            var userName = "";
            if (User.Identity.IsAuthenticated)
            {
                userName= User.Identity.Name;
            }
            else
            {
                userName = null;
            }
            #endregion'
            var pagingItem = await _mediator.Send(new IntegratedProductsPagingQueryReq(parameter,userName));
            var list = pagingItem.ProductList;
            //var list = await _mediator.Send(new SearchProductQueryReq(Filter, 4));
            Response.Object = list;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }


        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostCompare(int ProductId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var product = await _mediator.Send(new ProductReadCommandReq(ProductId));
                var res = await _mediator.Send(new CreateUserProductCommandReq(User.Identity.Name, ProductId, "Compare"));

                if (res)
                {
                    Response.Object = product;
                    Response.IsSuccessed = true;
                    Response.Message = $"محصول {product.Name} به بخش مقایسه افزوده شد";
                }
                else
                {

                    Response.IsSuccessed = false;
                    Response.Message = $"لیست مقایسه شما تکمیل است";
                }
            }
            else
            {

                Response.IsSuccessed = false;
                Response.Message = "ابتدا وارد حساب خود شوید";
            }
            return new JsonResult(Response);
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostCompareDelete(int userProductId)
        {

            await _mediator.Send(new DeleteUserProductCommandReq(userProductId));

            Response.IsSuccessed = true;
            Response.Message = "محصول از لیست مقایسه شما حذف شد";

            return new JsonResult(Response);
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostFavorite(int ProductId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var product = await _mediator.Send(new ProductReadCommandReq(ProductId));
                var res = await _mediator.Send(new CreateUserProductCommandReq(User.Identity.Name, ProductId, "Favorite"));


                Response.Object = product;
                Response.IsSuccessed = true;
                Response.Message = $"محصول {product.Name} به علاقه مندی های شما افزوده شد";

            }
            else
            {

                Response.IsSuccessed = false;
                Response.Message = "ابتدا وارد حساب خود شوید";
            }
            return new JsonResult(Response);




        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostAddToOrder(int ProductId)
        {
            var product = await _mediator.Send(new ProductReadCommandReq(ProductId));
            Response.Object = product;
            Response.IsSuccessed = true;
            Response.Message = $"محصول {product.Name} به سبد خرید افزوده شد";
            return new JsonResult(Response);
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostCreateCheckout(int productId)
        {
            var userName = "-";
            if (User.Identity.IsAuthenticated)
            {
                userName = User.Identity.Name;
            }
            var result = await _mediator.Send(new CreateCheckoutQueryReq(userName, productId));

            Response = result;
            return new JsonResult(Response);
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostLogoutUser()
        {


            var result = await _mediator.Send(new LogoutUserQueryReq());
            Response = result;
            return new JsonResult(Response);
        }


        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostLoginRequest(string loginmobile)
        {

            var result = await _mediator.Send(new LoginRequestByMobileQueryReq(loginmobile));
            Response = result;
            return new JsonResult(Response);
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostLogin(string loginmobile, string loginactiveCode)
        {
            var result = await _mediator.Send(new LoginByActiveCodeQueryReq(loginmobile, loginactiveCode));
            Response = result;
            return new JsonResult(Response);
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostPasswordLogin(string loginmobile, string loginPassword)
        {
            var result = await _signInManager.PasswordSignInAsync(loginmobile, loginPassword, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                //var user=await _userRep.GetUserByUserName(Input.UserName);
                //user.LastLoginDate = DateTime.Now;
                //await _mediator.Send(new UserUpdateCommandReq(_mapper.Map<UserDto>(user)));
                Response.IsSuccessed = true;
                Response.Message = "ورود با موفقیت انجام شد . در حال انتقال به پنل";
                if (User.IsInRole("Customer"))
                {
                    Response.Object = "/Panel";
                }
                else
                {
                    Response.Object = "/Admin";
                }
                Response.Object2 = await _mediator.Send(new DataProtectQueryReq(loginmobile, "protect"));
            }
            else
            {
                Response.IsSuccessed = false;
                Response.Message = "کاربری با مشخصات وارد شده یافت نشد";
            }
            if (result.IsLockedOut)
            {
                Response.IsSuccessed = false;
                Response.Message = "حساب شما تا تاریخ فلان مسدود شده است.";

            }




            return new JsonResult(Response);
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostRegister(string registerMobile, string registerName, string registerFamily, string registerMeliCode, string registerState, string registerCity, string registerAddress)
        {
            UserRegisterDto input = new UserRegisterDto();
            input.UserName = registerMobile;
            input.FirstName = registerName;
            input.LastName = registerFamily;
            input.NationalCode = registerMeliCode;
            input.Country = "ایران";
            input.State = registerState;
            input.City = registerCity;
            input.Address = registerAddress;
            input.Mobile = registerMobile;

            var result = await _mediator.Send(new UserRegisterQueryReq(input));

            return new JsonResult(result);
        }


        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostCheckIsAuthenticated()
        {
            ResponseDto Response = new ResponseDto();
            if (User.Identity.IsAuthenticated)
            {
                Response.IsSuccessed = true;
            }
            else
            {
                Response.IsSuccessed = false;
                Response.Message = "لطفا جهت ثبت سفارش ابتدا وارد شوید";
            }


            return new JsonResult(Response);
        }

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostCountOpenOrder()
        {
            ResponseDto Response = new ResponseDto();
            if (User.Identity.IsAuthenticated)
            {
                Response.IsSuccessed = true;
                Response.Object = await _mediator.Send(new CountOfOpenOrderFromUserQueryReq(User.Identity.Name));
            }
            else
            {
                Response.IsSuccessed = false;

            }


            return new JsonResult(Response);
        }

        #region Relogin
        public async Task<IActionResult> OnPostReLogin(string activeSite)
        {
            ResponseDto response = new ResponseDto();
            if (User.Identity.IsAuthenticated)
            {
                response.IsSuccessed = false;
                response.Message = "isActive";
            }
            else
            {
                var Res = await _mediator.Send(new DataProtectQueryReq(activeSite, "unProtect"));
                if (Res.IsSuccessed)
                {
                    var iuser = await _userManager.FindByNameAsync(Res.Object.ToString());
                    if (iuser != null)
                    {
                        await _signInManager.SignInAsync(iuser, true);
                    }
                    response.IsSuccessed = true;
                    response.Message = "actived";
                }
                else
                {
                    response.IsSuccessed = false;
                    response.Message = "expired";
                }

            }
            return new JsonResult(response);
        }
        #endregion

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostGetBrands()
        {
            var Brands = await _mediator.Send(new BrandReadsCommandReq());
            return new JsonResult(Brands);
        }
    }
}
