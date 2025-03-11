using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.DTOs.Property;
using Shop.Application.DTOs.PropertyCategury;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Categury.Requests.Queries;
using Shop.Application.Features.Product.Requests.Commands;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Application.Features.ProductGallery.Requests.Commands;
using Shop.Application.Features.ProductGallery.Requests.Queries;
using Shop.Application.Features.ProductProperty.Requests.Commands;
using Shop.Application.Features.ProductProperty.Requests.Queries;
using Shop.Application.Features.Property.Requests.Queries;
using Shop.Application.Features.PropertyCategury.Requests.Commands;
using Shop.Application.Validators.ProductGallery;

namespace Shop.Web.Pages.Admin.Products
{
    [Authorize(Roles = "SupperUser,Sale,Store")]
    public class EditContentModel : PageModel
    {
        #region Constractor

        private readonly IMediator _mediator;

        public EditContentModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        #region Properties

        //id
        [BindProperty]
        public int productId { get; set; }


        //product
        [BindProperty]
        public ProductDto ProductDto { get; set; }

        //gallery
        [BindProperty]
        public ProductGalleryDto Gallery { get; set; }


        [BindProperty]
        public int GalleryId { get; set; }

        [BindProperty]
        public List<ProductGalleryDto> Galleries { get; set; }

        //property
        [BindProperty]
        public string? productContent { get; set; }




        //result
        [BindProperty]
        public ResponseDto Response { get; set; } = new ResponseDto();

        //result
        public class Object
        {
            public ProductDto ProductDto { get; set; }
            public List<ProductGalleryDto> GalleryDtos { get; set; }
            public List<ProductPropertyDto> PropertyDtos { get; set; }
        }

        [BindProperty]
        public Object ResultObject { get; set; } = new Object();


        //Categuries

        public class CateguryObject
        {
            public List<CateguryDto> categuries { get; set; }
            public List<CateguryDto> subCateguries { get; set; } = new List<CateguryDto>();
            //properties
            public List<PropertyCateguryDto> propertyCategury { get; set; }
        }

        [BindProperty]
        public CateguryObject categuryObject { get; set; } = new CateguryObject();


        [BindProperty]
        public int categuryId { get; set; }


        //feutures
        [BindProperty]
        public List<PropertyDto> Properties { get; set; } = new List<PropertyDto>();


        [BindProperty]
        public int CatId { get; set; }

        [BindProperty]
        public int propertyCatId { get; set; }



        [BindProperty]
        public ProductPropertyDto AddFeutureInput { get; set; }


        #endregion

        #region Get

        public async Task<IActionResult> OnGet(int Id)
        {
            productId = Id;
            categuryObject.categuries = await _mediator.Send(new CateguryByParentIdReadQueryReq(0));
            categuryObject.propertyCategury = await _mediator.Send(new PropertyCateguryReadsCommandReq());
            var product = await _mediator.Send(new ProductReadCommandReq(productId));
            productContent = product.Description;
            return Page();
        }

        public async Task<IActionResult> OnPostData()
        {
            ResultObject.ProductDto = await _mediator.Send(new ProductReadCommandReq(productId));
            ResultObject.GalleryDtos = await _mediator.Send(new GalleriesOfProductQueryReq(productId));
            ResultObject.PropertyDtos = await _mediator.Send(new PropertiesOfProductQueryReq(productId));

            Response.Object = ResultObject;
            Response.IsSuccessed = true;

            return new JsonResult(Response);
        }

        #endregion

        #region Gallery

        public async Task<IActionResult> OnPostAddGallery()
        {
            #region Validator
            var validator = new ProductGalleryDtoValidator();
            var valid = validator.Validate(Gallery);
            if (!valid.IsValid)
            {
                Response.IsSuccessed = false;
                Response.Errors = valid.Errors;
                Response.Object = Gallery;
                return new JsonResult(Response);
            }
            #endregion


            await _mediator.Send(new ProductGalleryCreateCommandReq(Gallery));
            Response.IsSuccessed = true;
            Response.Message = "تصویر محصول با موفقیت بارگذاری گردید.";
            Response.Object = Gallery;

            return new JsonResult(Response);
        }

        public async Task<IActionResult> OnPostDeleteGallery()
        {
            await _mediator.Send(new ProductGalleryDeleteCommandReq(GalleryId));
            Response.IsSuccessed = true;
            Response.Message = "تصویر محصول با موفقیت حذف گردید.";
            Response.Object = Gallery;

            return new JsonResult(Response);
        }

        #endregion

        #region Feuture

        public async Task<IActionResult> OnPostSubs(int parentId)
        {

            categuryObject.subCateguries = await _mediator.Send(new CateguryByParentIdReadQueryReq(parentId));

            Response.Object = categuryObject.subCateguries;
            Response.IsSuccessed = true;

            return new JsonResult(Response);
        }

        public async Task<IActionResult> OnPostSearchFeuture()
        {
            Properties = await _mediator.Send(new PropertiesSearchQueryReq(CatId, propertyCatId, null));

            Response.Object = Properties;
            Response.IsSuccessed = true;

            return new JsonResult(Response);
        }

        public async Task<IActionResult> OnPostAddEditFeuture()
        {
            if (AddFeutureInput.Id != 0)
            {
                var feuture = await _mediator.Send(new ProductPropertyUpdateCommandReq(AddFeutureInput));
                Response.Object = feuture;
            }
            else
            {
                var feuture = await _mediator.Send(new ProductPropertyCreateCommandReq(AddFeutureInput));
                Response.Object = feuture;
            }

            Response.IsSuccessed = true;

            return new JsonResult(Response);
        }
        public async Task<IActionResult> OnPostDeleteFeuture()
        {
            if (AddFeutureInput.Id != 0)
            {
                await _mediator.Send(new ProductPropertyDeleteCommandReq(AddFeutureInput.Id));

                Response.IsSuccessed = true;

                return new JsonResult(Response);
            }
            else
            {
                Response.IsSuccessed = false;

                return new JsonResult(Response);
            }

        }

        #endregion

        public async Task<IActionResult> OnPostSave()
        {

            var p = await _mediator.Send(new ProductEditContentQueryReq(productId, productContent));

            Response.Object = p;
            Response.IsSuccessed = true;

            return new JsonResult(Response);
        }
    }
}
