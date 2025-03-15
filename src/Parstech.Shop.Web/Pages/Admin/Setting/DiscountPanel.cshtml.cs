using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parstech.Shop.Web.Services.GrpcClients;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductLog;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Application.DTOs.ProductStockPrice;
using Shop.Application.DTOs.Representation;
using Shop.Application.DTOs.RepresentationType;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.ProductRepresentation.Requests.Queries;
using Shop.Application.Features.ProductStockPrice.Requests.Commands;
using Shop.Application.Features.ProductStockPrice.Requests.Queries;
using Shop.Application.Features.ProductStockPriceSection.Requests.Commands;
using Shop.Application.Features.ProductStockPriceSection.Requests.Queries;
using Shop.Application.Features.Representation.Requests.Commands;
using Shop.Application.Features.RepresentationType.Requests.Commands;
using Shop.Application.Features.User.Requests.Queries;
using Shop.Application.Features.UserStore.Requests.Queries;
using Shop.Domain.Models;

namespace Shop.Web.Pages.Admin.Setting
{
    [Authorize(Roles = "SupperUser,Inventory,Store")]
    public class DiscountPanelModel : PageModel
    {
        #region Constractor

        private readonly IMediator _mediator;
        private readonly IRepresentationAdminGrpcClient _representationClient;
        private readonly IUserGrpcClient _userClient;

        public DiscountPanelModel(
            IMediator mediator,
            IRepresentationAdminGrpcClient representationClient,
            IUserGrpcClient userClient)
        {
            _mediator = mediator;
            _representationClient = representationClient;
            _userClient = userClient;
        }

        #endregion
        #region Properties

        //paging parameter
        [BindProperty]
        public ProductDiscountParameterDto Parameter { get; set; } = new ProductDiscountParameterDto();

        //categuries
        [BindProperty]
        public ProductDiscountPagingDto List { get; set; }


        //result
        [BindProperty]
        public ResponseDto Response { get; set; } = new ResponseDto();

        //Representations
        [BindProperty]
        public List<RepresentationDto> Representations { get; set; } = new List<RepresentationDto>();


        [BindProperty]
        public int RepId { get; set; }

        [BindProperty]
        public int productId { get; set; }

        [BindProperty]
        public ProductDto product { get; set; }

        [BindProperty]
        public ProductStockPriceDto productStock { get; set; }

        [BindProperty]
        public ProductRepresentationDto productRepresentation { get; set; }

        [BindProperty]
        public List<RepresentationTypeDto> repTypes { get; set; }


        [BindProperty]
        public ProductRepresentationDto ProductRepresentationDto { get; set; }

        //log
        [BindProperty]
        public LogDto LogDto { get; set; }

        [BindProperty]
        public PagingDto ProductLogPaging { get; set; }

        [BindProperty]
        public ParameterLogDto LogParameter { get; set; } = new ParameterLogDto();

        [BindProperty]
        public ProductRepresenationParameterDto PrParameter { get; set; } = new ProductRepresenationParameterDto();

        public class logClass
        {
            public LogDto LogDto { get; set; }
            public PagingDto ProductLogPaging { get; set; }
            public List<ProductRepresenationChartDto> ProductRepresntationChart { get; set; }
            public PagingDto ProductRepresntationPaging { get; set; }
        }
        [BindProperty]
        public logClass Log { get; set; } = new logClass();
        #endregion

        #region Get

        public async Task<IActionResult> OnGet()
        {
            Parameter.CurrentPage = 1;
            if (User.IsInRole("SupperUser"))
            {
                Representations = await _representationClient.GetRepresentationsAsync(new RepresentationParameterDto { CurrentPage = 1, TakePage = 100 });
            }
            else if (User.IsInRole("Store") || User.IsInRole("StoreBySend"))
            {
                var user = await _userClient.GetUserByUserNameAsync(User.Identity.Name);
                var userStore = await _userClient.GetUserStoreByUserIdAsync(user.Id);
                var rep = await _representationClient.GetRepresentationByIdAsync(userStore.RepId);
                Representations.Add(rep);
            }
            else
            {
                Representations = await _representationClient.GetRepresentationsAsync(new RepresentationParameterDto { CurrentPage = 1, TakePage = 100 });
            }

            repTypes = await _representationClient.GetRepresentationTypesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostData()
        {

            Parameter.TakePage = 30;
            List = await _mediator.Send(new DiscountProductListPagingQueryReq(Parameter));
            Response.Object = List;
            Response.IsSuccessed = true;
            
            return new JsonResult(Response);
        }

        
        #endregion
        public async Task<IActionResult> OnPostGetSetionsOfProductStockPrice(int productStockPriceId)
        {
          var response= await _mediator.Send(new GetSectionOfProductStockPriceQueryReq(productStockPriceId));
            return new JsonResult(response);
        }
        public async Task<IActionResult> OnPostDeleteSetionsOfProductStockPrice(int ProductStockPriceSectionId,int ProductStockPriceId)
        {
          await _mediator.Send(new DeleteProdcutStockPriceSectionCommandReq(ProductStockPriceSectionId));
            var response = await _mediator.Send(new GetSectionOfProductStockPriceQueryReq(ProductStockPriceId));
            return new JsonResult(response);
        }
        public async Task<IActionResult> OnPostChangeShowInDiscountPanel(int ProductStockPriceSectionId,int isShow)
        {
            var item =await _mediator.Send(new ProductStockPriceReadCommandReq(ProductStockPriceSectionId));
            switch (isShow)
            {
                case 0:
                    item.ShowInDiscountPanels = false; break;
                case 1:
                    item.ShowInDiscountPanels = true; break;
            }
            
          var response= await _mediator.Send(new ProductStockPriceUpdateCommandReq(item));
            return new JsonResult(response);
        }

        public async Task<IActionResult> OnPostAddProductStockPriceSection(int ProductStockPriceSectionId, int sectionId)
        {
            await _mediator.Send(new CreateProdcutStockPriceSectionCommandReq(ProductStockPriceSectionId,sectionId));
            var response = await _mediator.Send(new GetSectionOfProductStockPriceQueryReq(ProductStockPriceSectionId));
            return new JsonResult(response);
            
        }
    }
}
