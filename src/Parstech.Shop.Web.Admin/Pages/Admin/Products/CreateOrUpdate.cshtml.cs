using System.Globalization;

using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Convertor;
using Parstech.Shop.Context.Application.DTOs.Brand;
using Parstech.Shop.Context.Application.DTOs.Categury;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.DTOs.ProductCategury;
using Parstech.Shop.Context.Application.DTOs.ProductGallery;
using Parstech.Shop.Context.Application.DTOs.ProductProperty;
using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;
using Parstech.Shop.Context.Application.DTOs.ProductStockPrice;
using Parstech.Shop.Context.Application.DTOs.ProductType;
using Parstech.Shop.Context.Application.DTOs.Property;
using Parstech.Shop.Context.Application.DTOs.PropertyCategury;
using Parstech.Shop.Context.Application.DTOs.RepresentationType;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.DTOs.Tax;
using Parstech.Shop.Context.Application.DTOs.UserStore;
using Parstech.Shop.Context.Application.Features.Brand.Requests.Commands;
using Parstech.Shop.Context.Application.Features.Categury.Requests.Commands;
using Parstech.Shop.Context.Application.Features.Categury.Requests.Queries;
using Parstech.Shop.Context.Application.Features.Product.Requests.Commands;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;
using Parstech.Shop.Context.Application.Features.ProductCategury.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductLog.Requests.Queries;
using Parstech.Shop.Context.Application.Features.ProductProperty.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Queries;
using Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Queries;
using Parstech.Shop.Context.Application.Features.ProductType.Requests.Commands;
using Parstech.Shop.Context.Application.Features.Property.Requests.Queries;
using Parstech.Shop.Context.Application.Features.PropertyCategury.Requests.Commands;
using Parstech.Shop.Context.Application.Features.RepresentationType.Requests.Commands;
using Parstech.Shop.Context.Application.Features.Tax.Requests.Commands;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;
using Parstech.Shop.Context.Application.Features.UserStore.Requests.Commands;
using Parstech.Shop.Context.Application.Features.UserStore.Requests.Queries;
using Parstech.Shop.Context.Application.Validators.Product;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Products;

[Authorize(Roles = "SupperUser,Sale,Store")]
public class CreateOrUpdateModel : PageModel
{
    #region Constractor

    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IProductStockPriceRepository _productStockRep;


    public CreateOrUpdateModel(IMediator mediator,
        IMapper mapper,
        IProductStockPriceRepository productStockRep)
    {
        _mediator = mediator;
        _mapper = mapper;
        _productStockRep = productStockRep;
    }

    #endregion

    #region Properties

    //id
    [BindProperty] public int? productId { get; set; }


    //product
    [BindProperty] public ProductDto ProductDto { get; set; }

    //gallery
    [BindProperty] public ProductGalleryDto Gallery { get; set; }




    //[BindProperty] public List<ProductGalleryDto> Galleries { get; set; }

    //property
    [BindProperty] public string? productContent { get; set; }


    //result
    [BindProperty] public ResponseDto Response { get; set; } = new();

    //result
    public class Object
    {
        public ProductDto ProductDto { get; set; }
        //public List<ProductGalleryDto> GalleryDtos { get; set; }
        //public List<ProductPropertyDto> PropertyDtos { get; set; }
        //public List<ProductCateguryDto> CateguryDtos { get; set; }
        //public ChildsAndStock ChildsAndStock { get; set; }
        public string Role { get; set; } = "";
    }

    [BindProperty] public Object ResultObject { get; set; } = new();


    //Categuries

    public class CateguryObject
    {
        public List<CateguryDto> categuries { get; set; }

        public List<CateguryDto> subCateguries { get; set; } = new();

        //properties
        public List<PropertyCateguryDto> propertyCategury { get; set; }
    }

    [BindProperty] public CateguryObject categuryObject { get; set; } = new();


    [BindProperty] public int categuryId { get; set; }


    //feutures
    [BindProperty] public List<PropertyDto> Properties { get; set; } = new();


    [BindProperty] public int CatId { get; set; }

    [BindProperty] public int propertyCatId { get; set; }

    [BindProperty] public string propertyFilter { get; set; }


    [BindProperty] public ProductPropertyDto AddFeutureInput { get; set; }
    public List<ProductTypeDto> ProducyTypes { get; set; }
    public List<TaxDto> TaxList { get; set; }
    public List<BrandDto> Brands { get; set; }

    [BindProperty] public int Type { get; set; }

    [BindProperty] public string Filter { get; set; }


    //categury
    [BindProperty] public ProductCateguryDto categury { get; set; }

    [BindProperty] public List<ProductCateguryDto> categuries { get; set; }

    [BindProperty] public string FilterCat { get; set; }

    [BindProperty] public ProductStockPriceDto productStock { get; set; }

    [BindProperty] public ProductRepresentationDto productRepresentation { get; set; }


    [BindProperty] public ProductRepresentationDto ProductRepresentationDto { get; set; }

    [BindProperty] public List<RepresentationTypeDto> repTypes { get; set; }


    [BindProperty] public ProductDto variationDto { get; set; }

    public List<UserStoreDto> UserStores { get; set; } = new();

    [BindProperty] public int SelectedBrand { get; set; }

    #endregion

    #region Get

    public async Task<IActionResult> OnGet(int? Id)
    {
        productId = Id;
        categuryObject.categuries = await _mediator.Send(new CateguryByParentIdReadQueryReq(0));
        categuryObject.propertyCategury = await _mediator.Send(new PropertyCateguryReadsCommandReq());

        ProducyTypes = await _mediator.Send(new ProductTypeReadsCommandReq());
        TaxList = await _mediator.Send(new TaxReadsCommandReq());
        Brands = await _mediator.Send(new BrandReadsCommandReq());


        if (productId != null)
        {
            var product = await _mediator.Send(new ProductReadCommandReq(productId.Value));
            productContent = product.Description;
            SelectedBrand = product.BrandId;
        }

        repTypes = await _mediator.Send(new RepresentationTypeReadsCommandReq());

        if (User.IsInRole("SupperUser"))
        {
            UserStores = await _mediator.Send(new UserStoreReadsCommandReq());
        }
        else if (User.IsInRole("Store") || User.IsInRole("StoreBySend"))
        {
            var user = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
            var userStore = await _mediator.Send(new UserStoreOfUserReadQueryReq(user.Id));

            UserStores.Add(userStore);


        }

        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {
        if (productId != null)
        {
            ResultObject.ProductDto = await _mediator.Send(new ProductReadCommandReq(productId.Value));
            productContent = ResultObject.ProductDto.Description;
            //ResultObject.GalleryDtos = await _mediator.Send(new GalleriesOfProductQueryReq(productId.Value));
            //ResultObject.PropertyDtos = await _mediator.Send(new PropertiesOfProductQueryReq(productId.Value));
            //ResultObject.CateguryDtos = await _mediator.Send(new CateguriesOfProductQueryReq(productId.Value));
            //if (User.IsInRole("Store") || User.IsInRole("StoreBySend"))
            //{
            //    var user = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
            //    var userStore = await _mediator.Send(new UserStoreOfUserReadQueryReq(user.Id));
            //    ResultObject.ChildsAndStock =
            //        await _mediator.Send(new GetChildsAndProductStocksQueryReq(productId.Value, userStore.Id));

            //    ResultObject.Role = "Store";
            //}
            //else
            //{
            //    ResultObject.ChildsAndStock =
            //        await _mediator.Send(new GetChildsAndProductStocksQueryReq(productId.Value, 0));
            //}

            Response.Object = ResultObject;
            Response.IsSuccessed = true;
        }
        if (User.IsInRole("Store") || User.IsInRole("StoreBySend"))
        {
            Response.Object2 = "Store";
        }
        else
        {
            Response.Object2 = "All";
        }

        return new JsonResult(Response);
    }

    #endregion

    #region Save
    public async Task<IActionResult> OnPostSave(int isActive)
    {
        if (User.IsInRole("Store"))
        {
            ProductDto.IsActive = false;
        }
        else
        {
            if (isActive == 1)
            {
                ProductDto.IsActive = true;
            }
            else
            {
                ProductDto.IsActive = false;
            }
        }


        if (ProductDto.Id != 0)
        {
            var p = await _mediator.Send(new ProductUpdateCommandReq(ProductDto));
            Response.Object = p;
            Response.Object2 = "Update";
            if (p.Id != 0)
            {
                Response.IsSuccessed = true;
            }
            else
            {
                Response.IsSuccessed = false;
            }
        }
        else
        {
            var p = await _mediator.Send(new ProductCreateCommandReq(ProductDto));
            Response.Object = p;
            Response.Object2 = "Create";
            Response.IsSuccessed = true;
        }


        return new JsonResult(Response);
    }

    #endregion

    #region Categury

    public async Task<IActionResult> OnPostGetAllCateguries()
    {
        var categuries = await _mediator.Send(new CateguryReadCommandReq(FilterCat));
        Response.Object = categuries;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostEditOrCreateCategury()
    {
        await _mediator.Send(new ProductCateguryCreateCommandReq(categury));
        Response.Object = categury;
        Response.IsSuccessed = true;
        Response.Message = "دسته بندی محصول با موفقیت ثبت شد";
        #region NotActive Product When Role Is Store
        if (User.IsInRole("Store"))
        {
            var currtentProduct = await _mediator.Send(new ProductReadCommandReq(categury.ProductId));
            currtentProduct.IsActive = false;
            var p = await _mediator.Send(new ProductUpdateCommandReq(currtentProduct));

        }
        #endregion
        return new JsonResult(Response);
    }


    public async Task<IActionResult> OnPostDeleteCategury()
    {
        var ProductId = await _mediator.Send(new ProductCateguryDeleteCommandReq(productId.Value));
        Response.Object = ProductId;
        Response.IsSuccessed = true;
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
        Properties = await _mediator.Send(new PropertiesSearchQueryReq(CatId, propertyCatId, propertyFilter));

        Response.Object = Properties;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostAddEditFeuture()
    {
        if (AddFeutureInput.Id != 0)
        {
            var feuture = await _mediator.Send(new ProductPropertyUpdateCommandReq(AddFeutureInput));
            Response.IsSuccessed = true;
            Response.Object = feuture;
        }
        else
        {
            var feuture = await _mediator.Send(new ProductPropertyCreateCommandReq(AddFeutureInput));
            Response.IsSuccessed = true;
            Response.Object = feuture;
        }

        Response.IsSuccessed = true;
        #region NotActive Product When Role Is Store
        if (User.IsInRole("Store"))
        {
            var currtentProduct = await _mediator.Send(new ProductReadCommandReq(Gallery.ProductId));
            currtentProduct.IsActive = false;
            var p = await _mediator.Send(new ProductUpdateCommandReq(currtentProduct));

        }
        #endregion
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostDeleteFeuture()
    {
        if (AddFeutureInput.Id != 0)
        {
            await _mediator.Send(new ProductPropertyDeleteCommandReq(AddFeutureInput.Id));

            Response.IsSuccessed = true;
            #region NotActive Product When Role Is Store
            if (User.IsInRole("Store"))
            {
                var currtentProduct = await _mediator.Send(new ProductReadCommandReq(Gallery.ProductId));
                currtentProduct.IsActive = false;
                var p = await _mediator.Send(new ProductUpdateCommandReq(currtentProduct));

            }
            #endregion
            return new JsonResult(Response);
        }
        else
        {
            Response.IsSuccessed = false;

            return new JsonResult(Response);
        }
    }

    #endregion

    #region Variation

    public async Task<IActionResult> OnPostAddVariation()
    {
        var result =
            await _mediator.Send(new AddVariationForProductQueryReq(productId.Value, variationDto.VariationName));
        Response.IsSuccessed = result;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostEditVariation(int variationId)
    {
        var result =
            await _mediator.Send(new UpdateVariationNameOfProductQueryReq(variationId, variationDto.VariationName));
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostAddSingleStock(int storeId)
    {
        var res = await _mediator.Send(new ProductDuplicateForStoreQueryReq(productId.Value, storeId));
        Response.IsSuccessed = res;
        return new JsonResult(Response);
    }


    public async Task<IActionResult> OnPostDeleteChild(int childId)
    {
        var result = await _mediator.Send(new ProductDeleteQueryReq(childId));

        Response.IsSuccessed = result;
        return new JsonResult(Response);
    }

    #endregion

    #region  Stock

    #region Price

    public async Task<IActionResult> OnPostPriceItem()
    {
        productStock = await _mediator.Send(new ProductStockPriceReadCommandReq(productId.Value));

        Response.Object = productStock;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostEditPriceItem()
    {
        var currentproductStock = await _mediator.Send(new ProductStockPriceReadCommandReq(productStock.Id));
        productStock.Price = long.Parse(productStock.TextPrice.Replace(",", ""));
        productStock.SalePrice = long.Parse(productStock.TextSalePrice.Replace(",", ""));
        productStock.DiscountPrice = long.Parse(productStock.TextDiscountPrice.Replace(",", ""));
        productStock.BasePrice = long.Parse(productStock.TextBasePrice.Replace(",", ""));
        productStock.Quantity = currentproductStock.Quantity;
        productStock.MaximumSaleInOrder = currentproductStock.MaximumSaleInOrder;
        productStock.StoreId = currentproductStock.StoreId;
        productStock.RepId = currentproductStock.RepId;
        productStock.TaxId = currentproductStock.TaxId;
        productStock.QuantityPerBundle = currentproductStock.QuantityPerBundle;
        productStock.DiscountDate = currentproductStock.DiscountDate;
            

        #region Validator

        var validator = new ProductPriceValidator();
        var valid = validator.Validate(productStock);
        if (!valid.IsValid)
        {
            Response.IsSuccessed = false;
            Response.Errors = valid.Errors;

            return new JsonResult(Response);
        }

        #endregion

        if (productStock.DiscountPrice > 0)
        {
            if (productStock.DiscountDateShamsi != null)
            {
                var Date = ConvertPersianNumbersToEnglish.ToEnglishNumber(productStock.DiscountDateShamsi);

                string[] std = Date.Split('/');
                var az = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]),
                    int.Parse(std[2]),
                    new PersianCalendar()
                );
                productStock.DiscountDate = az;
            }
        }
        else
        {
            productStock.DiscountDate = null;
        }


        var current = _productStockRep.DapperGetProductStockPriceById(productStock.Id);
        var currentDto = _mapper.Map<ProductStockPriceDto>(current.Result);
        var edit = await _mediator.Send(new ProductStockPriceUpdateCommandReq(productStock));
        await _mediator.Send(new PriceConflictsCreateLogQueryReq(User.Identity.Name, currentDto, edit));
        Response.Object = edit;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion

    #region Mojudi

    public async Task<IActionResult> OnPostAddProductRepresentation()
    {
        var user = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
        ProductRepresentationDto.UserId = user.Id;
        var res = await _mediator.Send(new ProductRepresesntationCreateCommandReq(ProductRepresentationDto));
        Response.Object = res;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostQuickAddProductRepresentation()
    {
        var user = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
        ProductRepresentationDto.UserId = user.Id;
        var res = await _mediator.Send(new ProductRepresesntationQuickCreateCommandReq(ProductRepresentationDto));
        Response.Object = res;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostChangeQuantityPerBundle(int productStockPriceId, int QuantityPerBundle)
    {
        var res = await _mediator.Send(new ChangeQuantityPerBundleQueryReq(productStockPriceId, QuantityPerBundle));
        Response.Object = res;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }
    public async Task<IActionResult> OnPostDeleteProductStock(int rep, int id)
    {
        var pstock = await _mediator.Send(new ProductStockPriceReadCommandReq(id));
        int productId = pstock.ProductId;
        int storeId = pstock.StoreId;
        var result = await _mediator.Send(new ProductStockPriceDeleteQueryReq(rep, id));
        await _mediator.Send(new RefreshParentQuantityQueryReq(productId, storeId));
        Response.IsSuccessed = result;
        return new JsonResult(Response);
    }

    #endregion

    #endregion
    //public async Task<IActionResult> OnPostProductParents()
    //{
    //    if (Type == 1)
    //    {
    //        var result = await _mediator.Send(new GetAllParentVariableProductQueryReq(Filter));
    //        Response.Object = result;
    //    }
    //    else if (Type == 2)
    //    {
    //        var result = await _mediator.Send(new GetAllParentBundleProductQueryReq(Filter));
    //        Response.Object = result;
    //    }

    //    return new JsonResult(Response);
    //}

}