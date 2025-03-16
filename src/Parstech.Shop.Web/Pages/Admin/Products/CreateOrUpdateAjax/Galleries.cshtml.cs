using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parstech.Shop.Shared.Protos.ProductComponentsAdmin;
using Parstech.Shop.Web.GrpcClients;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.Response;
using Shop.Application.Validators.ProductGallery;
using Shop.Domain.Models;

namespace Shop.Web.Pages.Admin.Products.Detail
{
    public class EditContentModel : PageModel
    {
        #region Constructor

        private readonly IProductComponentsAdminGrpcClient _productComponentsClient;
        private readonly IProductAdminGrpcClient _productAdminClient;
        private readonly IMapper _mapper;
        private readonly IProductStockPriceRepository _productStockRep;

        public EditContentModel(
            IProductComponentsAdminGrpcClient productComponentsClient,
            IProductAdminGrpcClient productAdminClient,
            IMapper mapper,
            IProductStockPriceRepository productStockRep)
        {
            _productComponentsClient = productComponentsClient;
            _productAdminClient = productAdminClient;
            _mapper = mapper;
            _productStockRep = productStockRep;
        }

        #endregion
        
        //id
        [BindProperty] public int? productId { get; set; }

        //product
        [BindProperty] public List<ProductGalleryDto> GalleryDtos { get; set; } = new List<ProductGalleryDto>();

        //gallery
        [BindProperty] public ProductGalleryDto Gallery { get; set; }

        [BindProperty]
        public UploadViewModel UploadData { get; set; }

        [BindProperty] public int GalleryId { get; set; }

        //result
        [BindProperty] public ResponseDto Response { get; set; } = new ResponseDto();

        public async Task OnGet(int? id)
        {
            productId = id;
            if (productId != null)
            {
                var response = await _productComponentsClient.GetGalleriesOfProductAsync(productId.Value);
                if (response.IsSuccess)
                {
                    GalleryDtos = response.Galleries.ToList();
                }
            }
        }

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

            var res = await _productComponentsClient.CreateProductGalleryAsync(Gallery);

            return new JsonResult(res);
        }

        public async Task<IActionResult> OnPostDeleteGallery()
        {
            var res = await _productComponentsClient.DeleteProductGalleryAsync(GalleryId, Gallery.ProductId);

            return new JsonResult(res);
        }

        public async Task<IActionResult> OnPostChangeMainGallery()
        {
            var response = await _productComponentsClient.ChangeMainGalleryAsync(GalleryId, productId.Value);

            return new JsonResult(response);
        }
    }
}
