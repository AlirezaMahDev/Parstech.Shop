
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


var PreShow = document.getElementById("PreShow");

function CleanProduct() {

    //ProductDto_Id.value = null;
    //ProductDto_SingleSale.value = null;
    //ProductDto_ProductId.value = null;
    //ProductDto_Visit.value = null;
    //ProductDto_CreateDate.value = null;
    //ProductDto_Name.value = null;
    //ProductDto_Code.value = null;
    //ProductDto_TypeId.value = null;
    //ProductDto_ParentId.value = null;
    //ParentText.innerText = null;
    //ProductDto_BrandId.value = null;
    //ProductDto_StoreId.value = null;
    //ProductDto_TaxId.value = null;
    //ProductDto_Score.value = null;
    //ProductDto_ShortLink.value = null;
    //ProductDto_ShortDescription.value = null;
    //ProductDto_LatinName.value = null;
    //ProductDto_VariationName.value = null;
    //ProductDto_QuantityPerBundle.value = null;
    //ProductDto_ParentId.value = null;
    $("#GetAllCatForm2").submit();
}
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
    ProductDto_ParentId.value = element.parentId;
    ProductDto_BrandId.value = element.brandId;
   

    //ProductDto_StoreId.value = element.storeId;
    ProductDto_TaxId.value = element.taxId;
    ProductDto_Score.value = element.score;
    ProductDto_Description.value = element.description;
    ProductDto_ShortDescription.value = element.shortDescription;
    ProductDto_LatinName.value = element.latinName;
    ProductDto_VariationName.value = element.variationName;
    ProductDto_Keywords.value = element.keywords;
    ProductDto_TaxCode.value = element.taxCode;
    //ProductDto_QuantityPerBundle.value = element.quantityPerBundle;
    ProductDto_ParentId.value = element.parentId;
    ProductDto_ShortLink.value = element.shortLink;


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

    if (element.variationName != null) {
        VariationSection.classList.remove('hidden');
        VariationSection.classList.add('show');
    }
    else {
        VariationSection.classList.add('hidden');
        VariationSection.classList.remove('show');
    }
    //var description = document.getElementById("description");
    //description.innerText = element.description;
}

$(document).ready(function () {
    console.log(productId.value);

    $("#GetDataForm").submit();
    ProductIdNumber = productId.value;
    IproductIdForInputs(ProductIdNumber);
    CheckProductId();
});

function FillDataSet(Data) {

    StockData.innerHTML = null;
    ChildData.innerHTML = null;
    GalleryData.innerHTML = null;
    CateguryData.innerHTML = null;
    dataSet = [];


    console.log(Data);
    FillProdcut(Data.productDto);


    Data.categuryDtos.forEach(function (element) {
        if (element.isParnet) {
            CateguryData.insertAdjacentHTML("beforeend", " <tr><td colspan='2'>" + element.groupTitle + "</td><td><button onclick='DeleteCategury(" + element.id + ")' class='btn btn-danger'>حذف</button></td></tr>")
        }
        else {
            CateguryData.insertAdjacentHTML("beforeend", " <tr><td></td><td>" + element.groupTitle + "</td><td><button onclick='DeleteCategury(" + element.id + ")' class='btn btn-danger'>حذف</button></td></tr>")
        }
    });
    Data.galleryDtos.forEach(function (element) {
        if (element.isMain == true) {
            GalleryData.insertAdjacentHTML("beforeend", " <div class='col-md-2 col-sm-6'><div class='effect sides-effect'><a href='#'><img  style='height:120px;' src='/Shared/Images/Products/" + element.imageName + "' class='img-center img-responsive' alt=''><div class='absolute'><h2>" + element.alt + "</h2><p>تصویر اصلی محصول</p></div></a></div><button onclick='DeleteGalleryShowModal(" + element.id + ")' class='btn btn-red font-weight-bold text-white'>حذف تصویر</button></div>")

        }
        else {
            GalleryData.insertAdjacentHTML("beforeend", " <div class='col-md-2 col-sm-6'><div class='effect sides-effect'><a href='#'><img style='height:120px;' src='/Shared/Images/Products/" + element.imageName + "' class='img-center img-responsive' alt=''><div class='absolute'><h2>" + element.alt + "</h2></div></a></div><button onclick='DeleteGalleryShowModal(" + element.id + ")' class='btn btn-red font-weight-bold text-white'>حذف تصویر</button></div>")

        }
    });

    //Content.value = Data.productDto.description;

    Data.propertyDtos.forEach(function (element) {

        const data =
            [
                "<h5 class=''>" + element.propertyName + "</h5>",
                "<h5 class=''>" + element.value + "</h5>",
                "<div class='btn-group'><button onclick='OpenAddEditModal(" + element.propertyId + "," + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-pen Orange m-1'></i>تغییر مقدار</button><button onclick='OpenDeleteFeuture(" + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div>"

            ];
        dataSet.push(data);
    });

    Data.childsAndStock.productDtos.forEach(function (element) {

        switch (element.typeId) {
            case 3:
                ChildData.insertAdjacentHTML("beforeend", " <tr><td >" + element.id + "</td><td >" + element.name + "</td><td >" + element.code + "</td><td >" + element.variationName + "</td><td><div class= 'btn-group'> <div class='btn-group'><button type='button' class='btn btn-default dropdown-toggle' data-bs-toggle='dropdown' aria-expanded='true'><i class='icon-options'></i><i class='icon-arrow-down'></i></button><ul class='dropdown-menu' style=''><li><a href='#'onclick='GetUpdateVariation(" + element.id + ")' class=' btn btn-default btn-block h6 text-center'><i class=' fas fa-file-signature Orange m-1'></i>ویرایش نام متغیر</a></li><li><a  onclick='SingleAdStock(" + element.id + ")' class=' btn btn-default btn-block h6 text-center'><i class=' fas fa-file-signature Orange m-1'></i> انبار محصول</a></li><li><button onclick='DeleteChild(" + element.id + ")' type='button' class='btn btn-default btn-block'><i class=' fas fa-trash Red m-1'></i>حذف محصول</button></li></ul></div></div></td></tr>")
                break;
            case 5:
                ChildData.insertAdjacentHTML("beforeend", " <tr><td >" + element.id + "</td><td >" + element.name + "</td><td >" + element.code + "</td><td >" + element.variationName + "</td><td><div class= 'btn-group'> <div class='btn-group'><button type='button' class='btn btn-default dropdown-toggle' data-bs-toggle='dropdown' aria-expanded='true'><i class='icon-options'></i><i class='icon-arrow-down'></i></button><ul class='dropdown-menu' style=''><li><a href='/Admin/Products/CreateOrUpdate/" + element.id + "' class=' btn btn-default btn-block h6 text-center'><i class=' fas fa-file-signature Orange m-1'></i>ویرایش</a></li><li><a onclick='SingleAdStock(" + element.id + ")' class=' btn btn-default btn-block h6 text-center'><i class=' fas fa-file-signature Orange m-1'></i> انبار محصول</a></li><li><button onclick='DeleteChild(" + element.id + ")' type='button' class='btn btn-default btn-block'><i class=' fas fa-trash Red m-1'></i>حذف محصول</button></li></ul></div></div></td></tr>")
                break;

            default:
                //ChildData.insertAdjacentHTML("beforeend", " <tr><td >" + element.id + "</td><td >" + element.name + "</td><td >" + element.code + "</td><td >" + element.variationName + "</td><td><div class= 'btn-group'> <div class='btn-group'><button type='button' class='btn btn-default dropdown-toggle' data-bs-toggle='dropdown' aria-expanded='true'><i class='icon-options'></i><i class='icon-arrow-down'></i></button><ul class='dropdown-menu' style=''><li><a href='/Admin/Products/CreateOrUpdate/" + element.id + "' class=' btn btn-default btn-block h6 text-center'><i class=' fas fa-file-signature Orange m-1'></i>ویرایش</a></li><li><button onclick='Delete(" + element.id + ")' type='button' class='btn btn-default btn-block'><i class=' fas fa-trash Red m-1'></i>حذف محصول</button></li></ul></div></div></td></tr>")
                break;
        }


    });

    console.log(Data.childsAndStock.productStockDtos);
    Data.childsAndStock.productStockDtos.forEach(function (element) {

        switch (element.typeId) {
            case 1:
                StockData.insertAdjacentHTML("beforeend", " <tr><td >" + element.id + " <br/><small class='Orange font-weight-bold'>محصول" + element.productId + " </small></td><td class='small'>" + element.productName + " <small class='Orange font-weight-bold'>محصول ساده </small></td ><td class='small'>" + element.storeName + "</td><td class='small'>" + element.repName + "</td><td >" + element.quantity + "</td><td >" + element.salePrice + "<br/><small class='Red font-weight-bold'>" + element.discountPrice + "</small></td><td><div class= 'btn-group'><button onclick='SubmitGetPrice(" + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-money-bill-transfer Green m-1'></i>قیمت</button><button onclick='ShowProductRepresentationModal(" + element.id + "," + element.repId + ")' type='button' class='btn btn-default'><i class='fas fa-file-circle-plus Orange m-1'></i>موجودی</button><button onclick='DeleteStock(" + element.repId + "," + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div></td></tr>")


                break;
            case 2:
                StockData.insertAdjacentHTML("beforeend", " <tr><td >" + element.id + " <br/><small class='Orange font-weight-bold'>محصول" + element.productId + " </small></td><td class='small'>" + element.productName + " <small class='Orange font-weight-bold'>متغیر </small></td><td class='small'>" + element.storeName + "</td><td class='small'>" + element.repName + "</td><td >" + element.quantity + "</td><td >" + element.salePrice + "<br/><small class='Red font-weight-bold'>" + element.discountPrice + "</small></td><td><div class= 'btn-group'><button onclick='DeleteStock(" + element.repId + "," + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div></td></tr>")
                break;

            case 3:
                StockData.insertAdjacentHTML("beforeend", " <tr><td >" + element.id + " <br/><small class='Orange font-weight-bold'>محصول" + element.productId + " </small></td><td class='small'>" + element.productName + " <small class='Orange font-weight-bold'>زیرمجموعه </small></td ><td class='small'>" + element.storeName + "</td><td class='small'>" + element.repName + "</td><td >" + element.quantity + "</td><td >" + element.salePrice + "<br/><small class='Red font-weight-bold'>" + element.discountPrice + "</small></td><td><div class= 'btn-group'><button onclick='SubmitGetPrice(" + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-money-bill-transfer Green m-1'></i>قیمت</button><button onclick='ShowProductRepresentationModal(" + element.id + "," + element.repId + ")' type='button' class='btn btn-default'><i class='fas fa-file-circle-plus Orange m-1'></i>موجودی</button><button onclick='DeleteStock(" + element.repId + "," + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div></td></tr>")

                break;
            case 4:
                StockData.insertAdjacentHTML("beforeend", " <tr><td >" + element.id + " <br/><small class='Orange font-weight-bold'>محصول" + element.productId + " </small></td><td class='small'>" + element.productName + " <small class='Orange font-weight-bold'>باندل</small></td ><td class='small'>" + element.storeName + "</td><td class='small'>" + element.repName + "</td><td >" + element.quantity + "</td><td >" + element.salePrice + "<br/><small class='Red font-weight-bold'>" + element.discountPrice + "</small></td><td><div class= 'btn-group'><button onclick='SubmitGetPrice(" + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-money-bill-transfer Green m-1'></i>قیمت</button><button onclick='DeleteStock(" + element.repId + "," + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div></td></tr>")
                break;
            case 5:
                StockData.insertAdjacentHTML("beforeend", " <tr><td >" + element.id + " <br/><small class='Orange font-weight-bold'>محصول" + element.productId + " </small></td><td class='small'>" + element.productName + " <small class='Orange font-weight-bold'>زیرمجموعه </small></td ><td class='small'>" + element.storeName + "</td><td class='small'>" + element.repName + "</td><td >" + element.quantity + "<br/><small>تعداد در هر پک: " + element.quantityPerBundle + "</small></td><td >" + element.salePrice + "<br/><small class='Red font-weight-bold'>" + element.discountPrice + "</small></td><td><div class= 'btn-group'><button onclick='SubmitGetPrice(" + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-money-bill-transfer Green m-1'></i>قیمت</button><button onclick='ShowProductRepresentationModal(" + element.id + "," + element.repId + ")' type='button' class='btn btn-default'><i class='fas fa-file-circle-plus Orange m-1'></i>موجودی</button><button onclick='ShowChangeQuantityPerBundleModal(" + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-file-circle-plus Orange m-1'></i>تغییر در موجودی هر پک</button><button onclick='DeleteStock(" + element.repId + "," + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div></td></tr>")

                break;

            default:
                break;
        }


    });
}
function ClearDataSet() {
    GalleryData.innerHTML = null;

    Gallery_File.value = null;
    Gallery_Alt.value = null;
    Gallery_IsMain.value = null;
}



//افزودن گالری
var Gallery_File = document.getElementById("Gallery_File");
var Gallery_Alt = document.getElementById("Gallery_Alt");
var Gallery_IsMain = document.getElementById("Gallery_IsMain");
var GalleryId = document.getElementById("GalleryId");
function DeleteGalleryShowModal(id) {
    GalleryId.value = id;
    $("#GalleryDeleteModal").modal("show");
}

function OnLoadingGallery() {


}
function OnCompleteGallery(xhr) {

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        ToastSuccess("عملیات با موفقیت انجام شد.")
        $("#DeleteFeutureModal").modal('hide');
        ClearDataSet();
        tableMain
            .clear()
            .draw();
        dataSet = [];

        $("#GetDataForm").submit();
    }

}
function OnErrorGallery() {

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
        CheckProductId();

        if (Data.object2 == "Store") {
            isActive.disabled = true;
        }
        else {
            isActive.disabled = false;
        }
    }

    ToastSuccess("اطلاعات با موفقیت دریافت شد");

}
function OnErrorData() {

}




//ویژگی ها
var parentId = document.getElementById("parentId");
var SubCategury = document.getElementById("SubCategury");
var categuryId = document.getElementById("categuryId");

//document.getElementById('my-select').addEventListener('change', function () {
//    // console.log('You selected: ', this.value);
//    parentId.value = this.value;
//    $("#GetSubsForm").submit();

//});







function OnLoadingSub() {
    //CleanProduct();
}
function OnCompleteSub(xhr) {
    // console.log(xhr);

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        SubCategury.innerHTML = null;
        var Data = xhr.responseJSON.object;
        Data.forEach(function (element) {
            SubCategury.insertAdjacentHTML("beforeend", "<option value=" + element.groupId + ">" + element.groupTitle + "</option>")
        });
    }

}
function OnErrorSub() {

}



//جسنجوی ویژگی


var feutureDataSet = [];
var feuturetableMain = null;
var feuturecaption = document.getElementById("feuturecaption");
var AddFeutureInput_PropertyId = document.getElementById("AddFeutureInput_PropertyId");
var AddFeutureInput_Id = document.getElementById("AddFeutureInput_Id");
var DeleteAddFeutureInput_Id = document.getElementById("DeleteAddFeutureInput_Id");
function FillFeutureDataSet(Data) {
    console.log(Data);
    Data.forEach(function (element) {
        const data =
            [
                "<h5 class=''>" + element.caption + "</h5>",
                "<button onclick='OpenAddEditModal(" + element.id + ",0)' class='btn btn-sm btn-block btn-success curve m-1'>ثبت ویژگی برای این محصول </button>"
            ];
        feutureDataSet.push(data);
    });
}

function Filter() {

    if (feuturetableMain != null) {
        feuturetableMain
            .clear()
            .draw();
        feutureDataSet = [];
    }
    //FillFeutureDataSet = null;

    $("#SearchFeutureForm").submit();
}
function OnLoadingFeuture() {


}
function OnCompleteFeuture(xhr) {
    // console.log(xhr);

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        var Data = xhr.responseJSON.object;
        console.log(Data);
        FillFeutureDataSet(Data);

        feuturetableMain = $('#data-table2').DataTable({
            "searching": true,
            "paging": true,
            data: feutureDataSet,
            columns: [
                { title: 'نام ویژگی' },
                { title: 'عملیات' },
            ],
            "columnDefs": [{
                "targets": 1,
                "className": 'w-50',
            }],
            "pageLength": 50
        });
        feuturetableMain.destroy();
    }

}
function OnErrorFeuture() {

}



function OpenAddEditModal(propertyId, id) {
    //feuturecaption.value = caption;
    AddFeutureInput_PropertyId.value = propertyId;
    AddFeutureInput_Id.value = id;
    $("#AddEditFeutureModal").modal("show");

}
function OnLoadingAEFeuture() {


}
function OnCompleteAEFeuture(xhr) {
    console.log(xhr);
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        if (xhr.responseJSON.isSuccessed) {
            ToastSuccess("عملیات با موفقیت انجام شد.")
            $("#AddEditFeutureModal").modal('hide');
            ClearDataSet();
            tableMain
                .clear()
                .draw();
            dataSet = [];

            $("#GetDataForm").submit();
        }
        else {
            ToastError(xhr.responseJSON.object.message)
        }

    }

}
function OnErrorAEFeuture() {

}


function OpenDeleteFeuture(id) {
    DeleteAddFeutureInput_Id.value = id;
    $("#DeleteFeutureModal").modal('show');
}
function OnLoadingDeleteFeuture() {


}
function OnCompleteDeleteFeuture(xhr) {

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        ToastSuccess("عملیات با موفقیت انجام شد.")
        $("#DeleteFeutureModal").modal('hide');
        ClearDataSet();
        tableMain
            .clear()
            .draw();
        dataSet = [];

        $("#GetDataForm").submit();
    }

}
function OnErrorDeleteFeuture() {

}

//دخیره

var productContent = document.getElementById("productContent");
//var Content = document.getElementById("Content");

function SaveContent() {
    //console.log(productContent.value);
    //console.log(Content.value);

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



function IproductIdForInputs(id) {
    var IdList = document.querySelectorAll('.productId');
    IdList.forEach(function (input) {
        input.value = id;
    });
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

function OnLoadingProductParents() {
    ProductDto_ParentId.innerHTML = null;
}

function OnCompleteProductParents(xhr) {
    //console.log(xhr);
    var Data = xhr.responseJSON.object;
    Data.forEach(function (element) {
        ProductDto_ParentId.insertAdjacentHTML("beforeend", "<option value='" + element.id + "'>" + element.productName + "/<strong class=text-warning>شناسه: " + element.code + "</strong></option>");
    });
}



function CheckProductId() {
    console.log(TypeId);
    let CateguryButton = document.getElementById("CateguryButton");
    let GalleryButton = document.getElementById("GalleryButton");
    let FeutureButton = document.getElementById("FeutureButton");
    let ChildButton = document.getElementById("ChildButton");
    let StockButton = document.getElementById("StockButton");
    CateguryButton.innerHTML = null;
    GalleryButton.innerHTML = null;
    FeutureButton.innerHTML = null;
    ChildButton.innerHTML = null;
    StockButton.innerHTML = null;
    console.log(ProductIdNumber);
    if (ProductIdNumber != 0) {

        CateguryButton.insertAdjacentHTML("afterbegin", "<button class='btn btn-sm btn-success btn-round hover-green' onclick='showAddCatModal()' data-bs-toggle='modal' data-bs-target='#AddCateguryModal'><i class='icon-plus'></i></button>");
        GalleryButton.insertAdjacentHTML("afterbegin", "<button class='btn btn-sm btn-success btn-round hover-green' onclick='CleanProduct()' data-bs-toggle='modal' data-bs-target='#GalleryModal'><i class='icon-plus'></i></button>");
        FeutureButton.insertAdjacentHTML("afterbegin", "<button class='btn btn-sm btn-success btn-round hover-green' onclick='CleanProduct()' data-bs-toggle='modal' data-bs-target='#SearchFeutureModal'><i class='icon-plus'></i></button>");
        if (TypeId == 2) {
            ChildButton.insertAdjacentHTML("afterbegin", "<button class='btn btn-sm btn-success btn-round hover-green' onclick='CleanProduct()' data-bs-toggle='modal' data-bs-target='#AddVariationModal'><i class='icon-plus'></i></button>");
        }
        else {
            ChildButton.insertAdjacentHTML("afterbegin", "<small class='Orange font-weight-bold'>تنها برای محصولات متغیر امکان پذیر است.در صورت نیاز به افزودن محصول زیر مجموعه باندل از قسمت افزودن محصول اقدام فرمایید</small>");
        }

        StockButton.insertAdjacentHTML("afterbegin", "<button class='btn btn-sm btn-success btn-round hover-green' onclick='GetDulicateProduct(" + ProductIdNumber + ")' ><i class='icon-plus'></i></button>");
    }
    else {
        CateguryButton.insertAdjacentHTML("afterbegin", "<small class='Orange font-weight-bold'>ذخیره محصول الزامیست</small>");
        GalleryButton.insertAdjacentHTML("afterbegin", "<small class='Orange font-weight-bold'>ذخیره محصول الزامیست</small>");
        FeutureButton.insertAdjacentHTML("afterbegin", "<small class='Orange font-weight-bold'>ذخیره محصول الزامیست</small>");
        ChildButton.insertAdjacentHTML("afterbegin", "<small class='Orange font-weight-bold'>ذخیره محصول الزامیست</small>");
        StockButton.insertAdjacentHTML("afterbegin", "<small class='Orange font-weight-bold'>ذخیره محصول الزامیست</small>");

    }
}




function IdForInputs(id) {

    var IdList = document.querySelectorAll('.RepId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}
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
}

function IdForProductInputs(id) {

    var IdList = document.querySelectorAll('.ProductId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}

function SubmitGetPrice(id) {
    IdForProductInputs(id);

    $("#GetPriceForm").submit();
}




function OnLoadingGetPrice() {


}
function OnCompleteGetPrice(xhr) {
    var Data = xhr.responseJSON.object;
    FillPriceItem(Data);
    $("#PriceModal").modal("show");
}

function OnErrorGetPrice() {

}

function OnLoadingEditPrice() {


}

function OnCompleteEditPrice(xhr) {


    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        if (xhr.responseJSON.isSuccessed) {
            var Data = xhr.responseJSON.object;
            FillPriceItem(Data);
            Filter();
            ToastSuccess("عملیات با موفقیت انجام شد")
            ClearDataSet();
            tableMain
                .clear()
                .draw();
            dataSet = [];

            $("#GetDataForm").submit();
        }
        else {
            ToastError("اطلاعات وارد شده با خطا بارگذاری شده اند")
            console.log(xhr.responseJSON);
            xhr.responseJSON.errors.forEach(function (element) {
                ToastError(element.errorMessage);
            });
        }


    }

}

function OnErrorEditPrice() {

}


//Mojusi
function ShowProductRepresentationModal(productId, repId) {
    IdForProductInputs(productId);
    IdForInputs(repId);
    $("#ProductRepresentationModal").modal("show");
}


function SingleAdStock(id) {
    var dublicateProductId = document.getElementById("dublicateProductId");
    dublicateProductId.value = id;
    $("#ProductDuplicateModal").modal('show');
}



function OnLoadingAddPr() {


}

function OnCompleteAddPr(xhr) {

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {

        Filter();
        ToastSuccess("عملیات با موفقیت انجام شد")
        ClearDataSet();
        tableMain
            .clear()
            .draw();
        dataSet = [];

        $("#GetDataForm").submit();
    }


}

function OnErrorAddPr() {

}


function ShowChangeQuantityPerBundleModal(id) {
    var productStockPriceId = document.getElementById("productStockPriceId");
    productStockPriceId.value = id;
    $("#ChangeQuantityPerBundleModal").modal("show");
}
function OnCompleteChangeQuantityPerBundle(xhr) {
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {

        Filter();
        ToastSuccess("عملیات با موفقیت انجام شد")
        ClearDataSet();
        tableMain
            .clear()
            .draw();
        dataSet = [];

        $("#GetDataForm").submit();
    }
}




function DeleteStock(rep, id) {
    var pspId = document.getElementById("pspId");
    var repId = document.getElementById("repId");
    pspId.value = id;
    repId.value = rep;

    swal({
        title: '  آیا از حذف کالا از انبار مطمئن هستید؟',
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: 'بله، حذف شود. '
    }).then(function () {
        $("#DeleteProductStockPrice").submit();
    }, function (dismiss) {
        if (dismiss === 'cancel') {
            swal(
                'لغو گردید',
                ' لغو عملیات حذف کالا از انبار .',
                'error'
            ).catch(swal.noop);
        }
    }).catch(swal.noop);



}

function OnCompleteDeleteProductStock(xhr) {
    console.log(xhr);
    var result = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {

        if (result.isSuccessed) {
            ToastSuccess("عملیات با موفقیت انجام شد");
            tableMain
                .clear()
                .draw();
            dataSet = [];

            $("#GetDataForm").submit();

        }
        else {
            ToastError("عملیات حذف به دلیل وجود موجودی زیرمجموعه و یا سفارش از این کالا امکان پذیر نیست")
        }


    }
}

// Variation


function DeleteChild(id) {
    var childId = document.getElementById("childId");
    childId.value = id;

    swal({
        title: '  آیا از حذف کالا مطمئن هستید؟',
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: 'بله، حذف شود. '
    }).then(function () {
        $("#DeleteChild").submit();
    }, function (dismiss) {
        if (dismiss === 'cancel') {
            swal(
                'لغو گردید',
                ' لغو عملیات حذف کالا از انبار .',
                'error'
            ).catch(swal.noop);
        }
    }).catch(swal.noop);



}

function OnCompleteDeleteChild(xhr) {
    console.log(xhr);
    var result = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {

        if (result.isSuccessed) {
            ToastSuccess("عملیات با موفقیت انجام شد");
            tableMain
                .clear()
                .draw();
            dataSet = [];

            $("#GetDataForm").submit();

        }
        else {
            ToastError("عملیات حذف به دلیل وجود موجودی انبار امکان پذیر نیست")
        }


    }
}



function OnLoadingAddVariation() {

}
function OnCompleteAddVariation(xhr) {
    var result = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {

        if (result.isSuccessed) {
            ToastSuccess("عملیات با موفقیت انجام شد");
            //BlankFilter();

            ClearDataSet();
            tableMain
                .clear()
                .draw();
            dataSet = [];

            $("#GetDataForm").submit();
        }
        else {
            ToastError("عملیات امکان پذیر نیست")
        }

    }
}



var variationId = document.getElementById("variationId");
function GetUpdateVariation(id) {
    variationId.value = id;
    $("#EditVariationModal").modal('show');
}
function OnCompleteEditVariation(xhr) {
    
    var result = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {

        if (result.isSuccessed) {
            ToastSuccess("عملیات با موفقیت انجام شد");
            //BlankFilter();

            ClearDataSet();
            tableMain
                .clear()
                .draw();
            dataSet = [];

            $("#GetDataForm").submit();
        }
        else {
            ToastError("عملیات امکان پذیر نیست")
        }

    }
}








function GetDulicateProduct(id) {
    //IproductIdForInputs(id);
    $("#ProductDuplicateModal").modal('show');
}
function OnCompleteDublicate(xhr) {
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {

        $("#ProductDuplicateModal").modal('hide');

        if (xhr.responseJSON.isSuccessed) {
            ToastSuccess("عملیات با موفقیت انجام شد");
            //BlankFilter();

            ClearDataSet();
            tableMain
                .clear()
                .draw();
            dataSet = [];

            $("#GetDataForm").submit();
        }
        else {
            ToastError(" انبار تکراری ! موجودی انبار از قبل وجود دارد ")
        }
    }
}