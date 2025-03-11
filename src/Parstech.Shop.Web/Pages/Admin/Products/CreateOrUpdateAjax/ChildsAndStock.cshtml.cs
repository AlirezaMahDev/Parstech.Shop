using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.DTOs.ProductStockPrice;
using Shop.Application.Features.Product.Requests.Commands;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Application.Features.ProductProperty.Requests.Queries;
using Shop.Application.Features.User.Requests.Queries;
using Shop.Application.Features.UserStore.Requests.Queries;

namespace Shop.Web.Pages.Admin.Products.Detail
{
    public class ChildsAndStockModel : PageModel
    {
        #region Constractor

        private readonly IMediator _mediator;

        private readonly IProductStockPriceRepository _productStockRep;


        public ChildsAndStockModel(IMediator mediator,

            IProductStockPriceRepository productStockRep)
        {
            _mediator = mediator;

            _productStockRep = productStockRep;
        }

        #endregion
        //id
        [BindProperty] public int? productId { get; set; }
        [BindProperty] public int? TypeId { get; set; } = 0;

        public ChildsAndStock ChildsAndStock { get; set; }
        public ProductDto product { get; set; }

        public async Task OnGet(int? id)
        {
            productId = id;
            if (productId != 0)
            {
                product = await _mediator.Send(new ProductReadCommandReq(id.Value));
                TypeId = product.TypeId;
            }
            if (User.IsInRole("Store") || User.IsInRole("StoreBySend"))
            {
                var user = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
                var userStore = await _mediator.Send(new UserStoreOfUserReadQueryReq(user.Id));
                ChildsAndStock =
                    await _mediator.Send(new GetChildsAndProductStocksQueryReq(productId.Value, userStore.Id));

                
            }
            else
            {
                ChildsAndStock =
                    await _mediator.Send(new GetChildsAndProductStocksQueryReq(productId.Value, 0));
            }
        }
    }
}
