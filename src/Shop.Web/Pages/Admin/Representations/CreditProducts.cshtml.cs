using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.CreditProductStockPrice;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductLog;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Application.DTOs.ProductStockPrice;
using Shop.Application.DTOs.Representation;
using Shop.Application.DTOs.RepresentationType;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.CreditProductStockPrice.Requests.Commands;
using Shop.Application.Features.CreditProductStockPrice.Requests.Queries;
using Shop.Application.Features.ProductRepresentation.Requests.Queries;
using Shop.Application.Features.Representation.Requests.Commands;
using Shop.Application.Features.RepresentationType.Requests.Commands;
using Shop.Application.Features.User.Requests.Queries;
using Shop.Application.Features.UserStore.Requests.Queries;

namespace Shop.Web.Pages.Admin.Representations
{
    [Authorize(Roles = "SupperUser,Inventory,Store")]
    public class CreditProductsModel : PageModel
    {
        #region Constractor 
        private readonly IMediator _mediator;
        private readonly IProductRepresesntationRepository _productRepresesntationRep;

        public CreditProductsModel(IMediator mediator, IProductRepresesntationRepository productRepresesntationRep)
        {
            _mediator = mediator;
            _productRepresesntationRep = productRepresesntationRep;
        }
        #endregion




        #region Properties

        public ProductRepresentationPagingDto list { get; set; }

        [BindProperty]
        public ProductRepresenationParameterDto parameters { get; set; } = new ProductRepresenationParameterDto();


        [BindProperty]
        public List<RepresentationDto> Representations { get; set; } = new List<RepresentationDto>();


        public ResponseDto response { get; set; } = new ResponseDto();


        [BindProperty]
        public CreditProductStockPriceDto Credit { get; set; } 
        
        
        
        public int repId { get; set; } 

        #endregion

        #region Get

        public async Task<IActionResult> OnGet()
        {
            parameters.CurrentPage = 1;
            if (User.IsInRole("SupperUser"))
            {
                Representations = await _mediator.Send(new RepresentationReadsCommandReq());
                repId = 0;
            }
            else if (User.IsInRole("Store") || User.IsInRole("StoreBySend"))
            {
                var user = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
                var userStore = await _mediator.Send(new UserStoreOfUserReadQueryReq(user.Id));
                var rep = await _mediator.Send(new RepresentationReadCommandReq(userStore.RepId));
                repId = rep.Id;
                Representations.Add(rep);


            }
            else
            {
                Representations = await _mediator.Send(new RepresentationReadsCommandReq());
            }

            
            return Page();
        }



        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostGetData()
        {
            parameters.TakePage = 30;
            if (parameters.CurrentPage == 0)
            {
                parameters.CurrentPage = 1;
            }
            if (parameters.RepId == 0)
            {
                var user = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
                var userStore = await _mediator.Send(new UserStoreOfUserReadQueryReq(user.Id));

                if (userStore != null)
                {
                    var rep = await _mediator.Send(new RepresentationReadCommandReq(userStore.RepId));
                    parameters.RepId = rep.Id;
                }
                
            }
            
           
            list = await _mediator.Send(new CreditProductPagingQueryReq(parameters));
            response.Object = list;
            response.IsSuccessed = true;
            return new JsonResult(response);
        }



        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostGetOne(int id,int productStockPriceId)
        {
          var item= await  _mediator.Send(new GetCreditProductStockPriceQueryReq(id, productStockPriceId));
            response.Object = item;
            response.IsSuccessed = true;
            return new JsonResult(response);
        }
        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostCreateOrUpdate()
        {
            Credit.PrePay = long.Parse(Credit.TextPrePay.Replace(",", ""));
            Credit.PayMonth = long.Parse(Credit.TextPayMonth.Replace(",", ""));
            Credit.Total = long.Parse(Credit.TextTotal.Replace(",", ""));
            var res = await _mediator.Send(new CreateOrUpdateCreditCommandReq(Credit));
            return new JsonResult(res);

        }
        #endregion
    }
}
