
var dataSet = [];
var tableMain = null;

var productId = document.getElementById("PId");
var GalleryData = document.getElementById("GalleryData");
var Content = document.getElementById("Content");
var variableSelector = document.getElementById("ProductDto_TypeId");
let ProductIdNumber = 0;
let TypeId = 0;
var ProductDto_Id = document.getElementById("ProductDto_Id");
var ProductDto_SingleSale = document.getElementById("ProductDto_SingleSale");
var ProductDto_ProductId = document.getElementById("ProductDto_ProductId");
var ProductDto_CreateDate = document.getElementById("ProductDto_CreateDate");
var ProductDto_Visit = document.getElementById("ProductDto_Visit");
var ProductDto_Id = document.getElementById("ProductDto_Id");
var ProductDto_Name = document.getElementById("ProductDto_Name");
var ProductDto_Code = document.getElementById("ProductDto_Code");
var ProductDto_TypeId = document.getElementById("ProductDto_TypeId");
var ProductDto_ParentId = document.getElementById("ProductDto_ParentId");
var ParentText = document.getElementById("ParentText");
var ProductDto_BrandId = document.getElementById("ProductDto_BrandId");
var ProductDto_StoreId = document.getElementById("ProductDto_StoreId");
var ProductDto_TaxId = document.getElementById("ProductDto_TaxId");
var ProductDto_Score = document.getElementById("ProductDto_Score");
var ProductDto_ShortLink = document.getElementById("ProductDto_ShortLink");
var ProductDto_ShortDescription = document.getElementById("ProductDto_ShortDescription");
var variableSelector = document.getElementById("ProductDto_TypeId");
var ProductDto_LatinName = document.getElementById("ProductDto_LatinName");
var ProductDto_VariationName = document.getElementById("ProductDto_VariationName");
var ProductDto_QuantityPerBundle = document.getElementById("ProductDto_QuantityPerBundle");
var ProductDto_ParentId = document.getElementById("ProductDto_ParentId");
var ProductDto_Description = document.getElementById("ProductDto_Description");
var ProductDto_ShortLink = document.getElementById("ProductDto_ShortLink");
var ProductDto_TaxCode = document.getElementById("ProductDto_TaxCode");
var ProductDto_Keywords = document.getElementById("ProductDto_Keywords");
var isActive = document.getElementById("isActive");
let brandSelectedValue = document.getElementById("selectedValue");
let Role;

var PreShow = document.getElementById("PreShow");
var CountCkEditor = 0;

function IdForInputs(id) {

    var IdList = document.querySelectorAll('.RepId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}
function IdForProductIdInputs(id) {

    var IdList = document.querySelectorAll('.ProductId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}
function IdForProductStockIdInputs(id) {

    var IdList = document.querySelectorAll('.ProductStockId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}
function IdForGalleryIdInputs(id) {

    var IdList = document.querySelectorAll('.GalleryId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}

$(document).ready(function () {
    console.log(productId.value);

    $("#GetDataForm").submit();
    ProductIdNumber = productId.value;
    IdForProductIdInputs(ProductIdNumber);
    //CheckProductId();
    console.log(ProductDto_Description.value);
   

});


function FillProdcut(element) {
    console.log(element);
    ProductDto_Id.value = element.id;
    ProductDto_SingleSale.value = element.singleSale;
    //ProductDto_ProductId.value = element.productId;
    ProductDto_Visit.value = element.visit;
    ProductDto_CreateDate.value = element.createDate;

    ProductDto_Name.value = element.name;
    ProductDto_Code.value = element.code;
    ProductDto_TypeId.value = element.typeId;
    //ProductDto_ParentId.value = element.parentId;
    ProductDto_BrandId.value = element.brandId;
   

    //ProductDto_StoreId.value = element.storeId;
    ProductDto_TaxId.value = element.taxId;
    ProductDto_Score.value = element.score;
    
    ProductDto_ShortDescription.value = element.shortDescription;
    ProductDto_Description.value = element.description;
    ProductDto_LatinName.value = element.latinName;
    //ProductDto_VariationName.value = element.variationName;
    ProductDto_Keywords.value = element.keywords;
    ProductDto_TaxCode.value = element.taxCode;
    //ProductDto_QuantityPerBundle.value = element.quantityPerBundle;
    //ProductDto_ParentId.value = element.parentId;
    ProductDto_ShortLink.value = element.shortLink;
    if (CountCkEditor == 0) {
        CreateEditor(ProductDto_Description.value);
        CountCkEditor++;
    }
   

    if (element.isActive == true) {
        isActive.value = 1;
    }
    else {
        isActive.value = 2;
    }


    if (element.parentProductName != null) {
        ParentText.innerText = element.parentProductName
    }
    else {
        ParentText.innerText = null;
    }

    //if (element.variationName != null) {
    //    VariationSection.classList.remove('hidden');
    //    VariationSection.classList.add('show');
    //}
    //else {
    //    VariationSection.classList.add('hidden');
    //    VariationSection.classList.remove('show');
    //}
    //var description = document.getElementById("description");
    //description.innerText = element.description;
}




function FillDataSet(Data) {

    
    GalleryData.innerHTML = null;
    dataSet = [];
    Role = Data.role;

    console.log(Data);
    FillProdcut(Data.productDto);

    productId = Data.productDto.id;
    fetch('/admin/Products/CreateOrUpdateAjax/Categuries?id=' + Data.productDto.id) // دریافت اطلاعات محصول
        .then(response => response.text())
        .then(html => {
            document.getElementById("CateguriesSections").innerHTML = html;
        })
        .catch(error => console.error('Error:', error));

    fetch('/admin/Products/CreateOrUpdateAjax/Galleries?id=' + Data.productDto.id) // دریافت اطلاعات محصول
        .then(response => response.text())
        .then(html => {
            document.getElementById("GalleryData").innerHTML = html;
        })
        .catch(error => console.error('Error:', error));

    fetch('/admin/Products/CreateOrUpdateAjax/Feutures?id=' + Data.productDto.id) // دریافت اطلاعات محصول
        .then(response => response.text())
        .then(html => {
            document.getElementById("FeuturesData").innerHTML = html;
        })
        .catch(error => console.error('Error:', error));

    fetch('/admin/Products/CreateOrUpdateAjax/ChildsAndStock?id=' + Data.productDto.id) // دریافت اطلاعات محصول
        .then(response => response.text())
        .then(html => {
            document.getElementById("ChildAndStockData").innerHTML = html;
        })
        .catch(error => console.error('Error:', error));
}





function CreateEditor(content) {
    ClassicEditor
        .create(document.querySelector('#ProductDto_Description'),
            {
                ckfinder: {
                    // Upload the images to the server using the CKFinder QuickUpload command.
                    uploadUrl: '/UploadImage'
                },

                htmlSupport: {
                    allow: [
                        {
                            name: /.*/,
                            attributes: true,
                            classes: true,
                            styles: true
                        }
                    ]
                },
                initialData: content,
                language: {
                    // The UI will be Arabic.
                    ui: 'ar',

                    // And the content will be edited in Arabic.
                    content: 'ar'
                }

            })
        .catch(error => { console.error(error); });
}





function OnLoadingData() {

    ToastSuccess("در حال دریافت اطلاعات محصول");
    //FillFeutureDataSet = null;
}
function OnCompleteData(xhr) {
    console.log(xhr);

    if (xhr.responseJSON.isSuccessed) {
        var Data = xhr.responseJSON.object;
        ProductIdNumber = Data.productDto.Id;
        TypeId = Data.productDto.typeId;
        console.log(TypeId);
        FillDataSet(Data);
        console.log(Data);


        tableMain = $('#data-table').DataTable({
            "searching": false,
            "paging": false,
            data: dataSet,
            columns: [
                { title: 'ویژگی' },
                { title: 'مقدار ' },
                { title: 'عملیات' },
            ],
            "columnDefs": [{
                "targets": 1,
                "className": 'w-50',
            }],
            "pageLength": 25
        });
        tableMain.destroy();
        PreShow.innerHTML = null;
        PreShow.insertAdjacentHTML("beforeend", "<small>مشاهده <a class='Orange font-weight-bold' href='/Products/Detail/" + Data.productDto.shortLink + "/" + Data.productDto.productStockPriceId + "' target='_blank'>پیش نمایش محصول</a></small><br/><small class='Red font-weight-bold'>در صورت تغییر در اطلاعات محصول مجدد باید منتظر تائید راهبر سامانه جهت انتشار محصول بمانید!</small")

        if (Data.object2 == "Store") {
            isActive.disabled = true;
        }
        else {
            isActive.disabled = false;
        }
    }
    else {
        CreateEditor("");
        CountCkEditor++;
    }
    ToastSuccess("اطلاعات با موفقیت دریافت شد");

}
function OnErrorData() {

}




//ویژگی ها
var parentId = document.getElementById("parentId");
var SubCategury = document.getElementById("SubCategury");
var categuryId = document.getElementById("categuryId");












//جسنجوی ویژگی















//دخیره

var productContent = document.getElementById("productContent");
//var Content = document.getElementById("Content");

function SaveContent() {
    
    //if (Role == Store) {
    //    isActive = 2;
    //}
    $("#SaveForm").submit();
}
function OnLoadingSave() {


}
function OnCompleteSave(xhr) {
    console.log(xhr)
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        if (xhr.responseJSON.isSuccessed) {
            ToastSuccess("عملیات با موفقیت انجام شد.")
        }
        else {
            ToastError("نوع محصول به دلیل وجود زیرمجموعه قابل تغییر نیست ابتدا زیرمجموعه های این محصول را حذف و اصلاح نمایید")
        }

        $("#DeleteFeutureModal").modal('hide');
        if (xhr.responseJSON.object2 == "Create") {
            window.location.href = '/Admin/Products/createorupdate/' + xhr.responseJSON.object.id;
        }
        ClearDataSet();
        tableMain
            .clear()
            .draw();
        dataSet = [];

        $("#GetDataForm").submit();

    }

}
function OnErrorSave() {

}





function SearchProductParents(type) {
    var FilterParrents = document.getElementById("FilterParrents");
    console.log(SearchProduct.value);
    console.log(FilterParrents.value);
    FilterParrents.value = SearchProduct.value;
    console.log(FilterParrents.value);
    $("#ProductParentsForm").submit();
}



var VariationSection = document.getElementById("VariationSection");
var BundleSection = document.getElementById("BundleSection");
var ParrentSection = document.getElementById("ParrentSection");



var SelectParentSection = document.getElementById("SelectParentSection");
var SearchProduct = document.getElementById("SearchProduct");

var Type = document.getElementById("Type");
var ProductParentsForm = document.getElementById("ProductParentsForm");

variableSelector.addEventListener("change", (e) => {
    const targetValue = e.target.value;

    if (targetValue == 3) {
        VariationSection.classList.remove('hidden');
        VariationSection.classList.add('show');

        BundleSection.classList.remove('show');
        BundleSection.classList.add('hidden');

        ParrentSection.classList.remove('hidden');
        ParrentSection.classList.add('show');
        Type.value = 1;
    }
    else if (targetValue == 5) {

        BundleSection.classList.remove('hidden');
        BundleSection.classList.add('show');

        VariationSection.classList.remove('show');
        VariationSection.classList.add('hidden');

        ParrentSection.classList.remove('hidden');
        ParrentSection.classList.add('show');
        Type.value = 2;
    }
    else {
        VariationSection.classList.remove('show');
        VariationSection.classList.add('hidden');

        ParrentSection.classList.remove('show');
        ParrentSection.classList.add('hidden');

        BundleSection.classList.remove('show');
        BundleSection.classList.add('hidden');
    }
});

//function OnLoadingProductParents() {
//    ProductDto_ParentId.innerHTML = null;
//}

//function OnCompleteProductParents(xhr) {
//    //console.log(xhr);
//    var Data = xhr.responseJSON.object;
//    Data.forEach(function (element) {
//        ProductDto_ParentId.insertAdjacentHTML("beforeend", "<option value='" + element.id + "'>" + element.productName + "/<strong class=text-warning>شناسه: " + element.code + "</strong></option>");
//    });
//}



//function CheckProductId() {
//    console.log(TypeId);
    //let CateguryButton = document.getElementById("CateguryButton");
    //let GalleryButton = document.getElementById("GalleryButton");
    //let FeutureButton = document.getElementById("FeutureButton");
    //let ChildButton = document.getElementById("ChildButton");
    //let StockButton = document.getElementById("StockButton");
    //CateguryButton.innerHTML = null;
    //GalleryButton.innerHTML = null;
    //FeutureButton.innerHTML = null;
    //ChildButton.innerHTML = null;
    //StockButton.innerHTML = null;
    //console.log(ProductIdNumber);
    //if (ProductIdNumber != 0) {

    //    //CateguryButton.insertAdjacentHTML("afterbegin", "");
    //    //GalleryButton.insertAdjacentHTML("afterbegin", "");
    //    //FeutureButton.insertAdjacentHTML("afterbegin", "");
    //    if (TypeId == 2) {
    //        ChildButton.insertAdjacentHTML("afterbegin", "<button class='btn btn-sm btn-success btn-round hover-green' onclick='CleanProduct()' data-bs-toggle='modal' data-bs-target='#AddVariationModal'><i class='icon-plus'></i></button>");
    //    }
    //    else {
    //        ChildButton.insertAdjacentHTML("afterbegin", "<small class='Orange font-weight-bold'>تنها برای محصولات متغیر امکان پذیر است.در صورت نیاز به افزودن محصول زیر مجموعه باندل از قسمت افزودن محصول اقدام فرمایید</small>");
    //    }

    //    StockButton.insertAdjacentHTML("afterbegin", "<button class='btn btn-sm btn-success btn-round hover-green' onclick='GetDulicateProduct(" + ProductIdNumber + ")' ><i class='icon-plus'></i></button>");
    //}
    //else {
    //    CateguryButton.insertAdjacentHTML("afterbegin", "<small class='Orange font-weight-bold'>ذخیره محصول الزامیست</small>");
    //    //GalleryButton.insertAdjacentHTML("afterbegin", "");
    //    FeutureButton.insertAdjacentHTML("afterbegin", "<small class='Orange font-weight-bold'>ذخیره محصول الزامیست</small>");
    //    ChildButton.insertAdjacentHTML("afterbegin", "<small class='Orange font-weight-bold'>ذخیره محصول الزامیست</small>");
    //    StockButton.insertAdjacentHTML("afterbegin", "<small class='Orange font-weight-bold'>ذخیره محصول الزامیست</small>");

    //}
//}





//Price
var product_Id = document.getElementById("product_Id");
var product_RepId = document.getElementById("product_RepId");
var product_StoreId = document.getElementById("product_StoreId");
var product_StockStatus = document.getElementById("product_StockStatus");
var product_ProductId = document.getElementById("product_ProductId");
var product_BasePrice = document.getElementById("product_BasePrice");
var product_Price = document.getElementById("product_Price");
var product_SalePrice = document.getElementById("product_SalePrice");
var product_DiscountPrice = document.getElementById("product_DiscountPrice");
var product_DiscountDateShamsi = document.getElementById("product_DiscountDateShamsi");
var product_Quantity = document.getElementById("product_Quantity");
var product_CateguryOfUserId = document.getElementById("product_CateguryOfUserId");
var product_CateguryOfUserType = document.getElementById("product_CateguryOfUserType");

function CleanPriceItem() {

}
function FillPriceItem(Data) {
    console.log(Data);
    //product_Id.value = Data.id;
    product_ProductId.value = Data.productId;
    product_RepId.value = Data.repId;
    product_StoreId.value = Data.storeId;
    product_StockStatus.value = Data.stockStatus;
    product_BasePrice.value = Data.textBasePrice;
    product_Price.value = Data.textPrice;
    product_SalePrice.value = Data.textSalePrice;
    product_DiscountPrice.value = Data.textDiscountPrice;
    product_DiscountDateShamsi.value = Data.discountDateShamsi;
    product_Quantity.value = Data.quantity;
    product_CateguryOfUserId.value = Data.categuryOfUserId;
    product_CateguryOfUserType.value = Data.categuryOfUserType;
}



function ClearDataSet() {

}