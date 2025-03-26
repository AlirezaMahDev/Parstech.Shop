var dataSet = [];
var tableMain;
var FilterInput = document.getElementById("FilterInput");
var Parameter_Filter = document.getElementById("Parameter_Filter");


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

var paging = document.getElementById("paging");
var pagingHeader = document.getElementById("pagingHeader");
let serached = document.getElementById("serached");

function CleanProduct() {

    ProductDto_Id.value = null;
    ProductDto_SingleSale.value = null;
    ProductDto_ProductId.value = null;
    ProductDto_Visit.value = null;
    ProductDto_CreateDate.value = null;
    ProductDto_Name.value = null;
    ProductDto_Code.value = null;
    ProductDto_TypeId.value = null;
    ProductDto_ParentId.value = null;
    ParentText.innerText = null;
    ProductDto_BrandId.value = null;
    ProductDto_StoreId.value = null;
    ProductDto_TaxId.value = null;
    ProductDto_Score.value = null;
    ProductDto_ShortLink.value = null;
    ProductDto_ShortDescription.value = null;
    ProductDto_LatinName.value = null;
    ProductDto_VariationName.value = null;
    ProductDto_QuantityPerBundle.value = null;
    ProductDto_ParentId.value = null;
    serached.innerHTML = null;

}
function FillProdcut(element) {
    console.log(element);
    ProductDto_Id.value = element.id;
    ProductDto_SingleSale.value = element.singleSale;
    ProductDto_ProductId.value = element.productId;
    ProductDto_Visit.value = element.visit;
    ProductDto_CreateDate.value = element.createDate;
    ProductDto_Name.value = element.name;
    ProductDto_Code.value = element.code;
    ProductDto_TypeId.value = element.typeId;
    ProductDto_ParentId.value = element.parentId;
    ProductDto_BrandId.value = element.brandId;
    ProductDto_StoreId.value = element.storeId;
    ProductDto_TaxId.value = element.taxId;
    ProductDto_Score.value = element.score;
    ProductDto_ShortLink.value = element.shortLink;
    ProductDto_ShortDescription.value = element.shortDescription;
    ProductDto_LatinName.value = element.latinName;
    ProductDto_VariationName.value = element.variationName;
    ProductDto_QuantityPerBundle.value = element.quantityPerBundle;
    ProductDto_ParentId.value = element.parentId;

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
}










$(window).on('resize', function () {
    $('#data-table').css("width", "100%");
});


$(document).ready(function () {
    Parameter_CurrentPage.value = 1;
    $("#GetDataForm").submit();
});



function Filter() {

    tableMain
        .clear()
        .draw();
    dataSet = [];

    Parameter_Filter.value = FilterInput.value;
    $("#GetDataForm").submit();
}
function BlankFilter() {

    tableMain
        .clear()
        .draw();
    dataSet = [];

    Parameter_Filter.value = null;
    $("#GetDataForm").submit();
}


function FillDataSet(data) {
    var Data = data.object;
    var object2 = data.object2;
    console.log(Data.searched);

    serached.innerText = null;
    if (Data.searched != 0) {
        serached.innerText = "موارد پیدا شده:" + Data.searched;
    }
    let buttons;
    
    Data.productDtos.forEach(function (element) {

        if (object2 == "Store") {
            buttons = "<div class='btn-group'><div class='btn-group'><button type='button' class='btn btn-default dropdown-toggle' data-bs-toggle='dropdown' aria-expanded='true'><i class='icon-options'></i><i class='icon-arrow-down'></i></button><ul class='dropdown-menu' style=''><li><a href='/Admin/Products/CreateOrUpdate/" + element.productId + "'class=' btn hover-green btn-block h6 text-center'>میخواهم فروشنده این محصول باشم</a></li></ul></div> </div >"
        }
        else {
            buttons = "<div class='btn-group'><div class='btn-group'><button type='button' class='btn btn-default dropdown-toggle' data-bs-toggle='dropdown' aria-expanded='true'><i class='icon-options'></i><i class='icon-arrow-down'></i></button><ul class='dropdown-menu' style=''><li><a href='/Admin/Products/CreateOrUpdate/" + element.productId + "'class=' btn btn-default btn-block h6 text-center'><i class=' fas fa-file-signature Orange m-1'></i>ویرایش</a></li><li><button onclick='Delete(" + element.productId + ")' type='button' class='btn btn-default btn-block'><i class='  fas fa-trash Red m-1'></i>حذف محصول</button></li></ul></div> </div >"

        }
        let activeStatus;
        if (element.isActive) {
            activeStatus = "<small class='Green font-weight-bold'>منتشر شده</small>";
        }
        else {
            activeStatus = "<small class='Red font-weight-bold'>در انتظار انتشار</small>";
        }
        const data =
            [
                
                "<h6 class='font-weight-bold'><img width='50' src='/shared/images/Products/" + element.image + "'/>" + element.name + " <br/> </h6>",
                "<small><strong class='font-weight-bold'>کد محصول:</strong><strong class='text-warning'> " + element.code + "</strong></small>",

                "<small>" + element.typeName + "</small>",
                
                "<small>" + element.createDateShamsi + "</small>",
                "<a class='pointer' onclick='GetProductStores("+element.productId+")'>مشاهده</a>",
                activeStatus,
                buttons
                //"<div class='btn-group'><div class='btn-group'><button type='button' class='btn btn-default dropdown-toggle' data-bs-toggle='dropdown' aria-expanded='true'><i class='icon-options'></i><i class='icon-arrow-down'></i></button><ul class='dropdown-menu' style=''><li><button onclick='GetProductSubmit(" + element.id + ")' type='button' class='btn btn-default btn-block'><i class=' fas fa-pen-to-square Green m-1'></i>ویرایش سریع</button></li><li><a href='/Admin/Products/CreateOrUpdate/" + element.id + "'class=' btn btn-default btn-block h6 text-center'><i class=' fas fa-file-signature Orange m-1'></i>ویرایش</a></li><li><button onclick='GetCategury(" + element.id + ")' type='button' class='btn btn-default btn-block' > <i class=' fas fa-folder-closed Blue m-1'></i>دسته بندی</button ></li><li><button onclick='GetDulicateProduct(" + element.id + ")' type='button' class='btn btn-default btn-block'><i class=' fas  fas fa-file-export Cyen m-1'></i>کپی محصول</button></li><li><button onclick='GetCopyProduct(" + element.id + ")' type='button' class='btn btn-default btn-block'><i class='  fas fa-file-circle-plus Pink m-1'></i>دوبل کردن</button></li><li><button onclick='Delete(" + element.id + ")' type='button' class='btn btn-default btn-block'><i class='  fas fa-trash Red m-1'></i>حذف محصول</button></li></ul></div> </div >"
                ];
        dataSet.push(data);
    });

    var first = 1;
    var current = Data.currentPage;
    var next = Data.currentPage + 1;
    var privious = Data.currentPage - 1;
    var last = Data.pageCount + 1;

    if (next > last) {
        next = Data.currentPage;
    }
    if (privious < 1) {
        privious = Data.currentPage;
    }

   // console.log("first" + first);
   // console.log("perivous" + privious);
   // console.log("next" + next);
   // console.log("last" + last);
    paging.innerHTML = null;
    pagingHeader.innerHTML = null;
    pagingHeader.insertAdjacentHTML("beforeend", "<h6 class='m-2'>صفحه " + current + " از " + last + "</h5>");
    paging.insertAdjacentHTML("beforeend", "<button onclick='RunPaging(" + first + ")' class='btn cart-Blue btn-xs font-weight-bold ml-2'>اولین صفحه<i class=' fas fa-circle-arrow-right p-1'></i></button><button onclick='RunPaging(" + privious + ")' class='btn cart-Green btn-xs font-weight-bold ml-2'><i class=' fas fa-arrow-right p-1'></i></button><button onclick='RunPaging(" + next + ")' class='btn cart-Green btn-xs font-weight-bold ml-2'><i class=' fas fa-arrow-left p-1'></i></button><button onclick='RunPaging(" + last + ")' class='btn cart-Blue btn-xs font-weight-bold ml-2'><i class=' fas fa-circle-arrow-left p-1'></i>آخرین صفحه</button> <h6 class='m-2'>صفحه " + current + " از " + last + "</h5>");

}
function RunPaging(page) {
    tableMain
        .clear()
        .draw();
    dataSet = [];

    Parameter_Filter.value = null;
    Parameter_CurrentPage.value = page;
    $("#GetDataForm").submit();
}

function OnLoadingData() {


}

function OnCompleteData(xhr) {
    
    var Data = xhr.responseJSON;
   
    console.log(Data);
    FillDataSet(Data);



    tableMain = $('#data-table').DataTable({
        "searching": false,
        "paging": false,
        data: dataSet,
        columns: [
            { title: 'عنوان محصول' },
            
            
            { title: 'شناسه' },
            { title: 'نوع' },
            { title: 'تاریخ ایجاد' },
            { title: 'فروشندگان' },
            { title: 'انتشار' },
            
            
            { title: 'عملیات' },
        ],
        
        "pageLength": 25
    });
    tableMain.destroy();


}

function OnErrorData() {

}


function IproductIdForInputs(id) {
    var IdList = document.querySelectorAll('.productId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}

function GetProductSubmit(id) {
    IproductIdForInputs(id);
    $("#GetProductForm").submit();
}

//دریافت محصول


function OnLoadingProdcut() {
    //CleanProduct();
}
function OnCompleteProdcut(xhr) {
   // console.log(xhr);
    FillProdcut(xhr.responseJSON.object);
    SelectParentSection.classList.remove('show');
    SelectParentSection.classList.add('hidden');
    $("#EditProductModal").modal("show");
}

function OnErrorProdcut() {

}



function OnLoadingProductEC() {
    //CleanProduct();
}
function OnCompleteProductEC(xhr) {
    //console.log(xhr);

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        FillProdcut(xhr.responseJSON.object);
        $("#EditProductModal").modal("show");
        ToastSuccess("عملیات با موفقیت انجام شد")
    }

}


function OnErrorProductEC() {

}
//...................................

var SelectParentSection = document.getElementById("SelectParentSection");
var SearchProduct = document.getElementById("SearchProduct");

var Type = document.getElementById("Type");
var ProductParentsForm = document.getElementById("ProductParentsForm");

variableSelector.addEventListener("change", (e) => {
    const targetValue = e.target.value;

    if (targetValue == 3) {
        SelectParentSection.classList.remove('hidden');
        SelectParentSection.classList.add('show');

        VariationSection.classList.remove('hidden');
        VariationSection.classList.add('show');
        Type.value = 1;
    }
    else if (targetValue == 5) {
        SelectParentSection.classList.remove('hidden');
        SelectParentSection.classList.add('show');
        Type.value = 2;
    }
    else {
        SelectParentSection.classList.remove('show');
        SelectParentSection.classList.add('hidden');

        VariationSection.classList.add('hidden');
        VariationSection.classList.remove('show');
    }
});
function SearchProductParents(type) {
    var FilterParrents = document.getElementById("FilterParrents");
    console.log(SearchProduct.value);
    console.log(FilterParrents.value);
    FilterParrents.value = SearchProduct.value;
    console.log(FilterParrents.value);
    $("#ProductParentsForm").submit();
}
function OnLoadingProductParents() {

}

function OnCompleteProductParents(xhr) {
    //console.log(xhr);
    var Data = xhr.responseJSON.object;
    Data.forEach(function (element) {
        ProductDto_ParentId.insertAdjacentHTML("beforeend", "<option value='" + element.id + "'>" + element.productName + "/<strong class=text-warning>شناسه: " + element.code + "</strong></option>");
    });
}

function GetDulicateProduct(id) {
    IproductIdForInputs(id);
    $("#ProductDuplicateModal").modal('show');
}
function OnCompleteDublicate(xhr) {
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
       
        $("#ProductDuplicateModal").modal('hide');
        ToastSuccess("عملیات با موفقیت انجام شد");
        Filter();
    }
}


function GetCopyProduct(id) {
    IproductIdForInputs(id);
    $("#ProductCopyModal").modal('show');
}
function OnCompleteCopy(xhr) {
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {

        $("#ProductCopyModal").modal('hide');
        ToastSuccess("عملیات با موفقیت انجام شد");
        Filter();
    }
}


function Delete(id) {
    //var productId = document.getElementById("productId");
    //productId.value = id;
    IproductIdForInputs(id);
    swal({
        title: '  آیا از حذف کامل این کالا مطمئن هستید؟',
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: 'بله، حذف شود. '
    }).then(function () {
        $("#DeleteProduct").submit();
    }, function (dismiss) {
        if (dismiss === 'cancel') {
            swal(
                'لغو گردید',
                ' لغو عملیات حذف کالا  .',
                'error'
            ).catch(swal.noop);
        }
    }).catch(swal.noop);



}

function OnCompleteDelete(xhr) {
    console.log(xhr);
    var result = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {

        if (result.isSuccessed) {
            ToastSuccess("عملیات با موفقیت انجام شد");
            BlankFilter();
            Parameter_CurrentPage.value = 1;
            $("#GetDataForm").submit();
        }
        else {
            ToastError("عملیات حذف به دلیل وجود کالا در انبارها امکان پذیر نیست")
        }

    }
}

//Search Product For Store
let SroreSearchProduct=document.getElementById("SroreSearchProduct");
function SearchProducts() {
    $.ajax({
        type: "POST",
        url: "/Admin/Products?handler=SearchProduct",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { "Filter": SroreSearchProduct.value },
        success: function (response) {
            FillSearchedProoductForStore(response.object);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

let SearchedProductSection = document.getElementById("SearchedProductSection");
function FillSearchedProoductForStore(Data) {
    SearchedProductSection.innerHTML = null;
    Data.forEach(function (element) {
        SearchedProductSection.insertAdjacentHTML("beforeend", "<div class='card'><div class='card-body'><div class='row'><div class='col-8'><h5>" + element.name + "</h5></div><div class='col-4'><a href='/Admin/Products/CreateOrUpdate/" + element.id + "' class='btn hover-green'>میخواهم فروشنده این محصول باشم</a></div></div></div></div>")
    });
}


//Product Stores
function GetProductStores(productId) {
    $.ajax({
        type: "POST",
        url: "/Admin/Products?handler=ProductStores",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { "ProductId": productId },
        success: function (response) {
            FillProductStores(response.object);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

let ProductStoresSection = document.getElementById("ProductStoresSection");
function FillProductStores(Data) {
    ProductStoresSection.innerHTML = null;
    Data.forEach(function (element) {
        ProductStoresSection.insertAdjacentHTML("beforeend", "<p class='text-center m-t-30 m-b-40 font-weight-bold'>" + element.storeName + "</p><hr />");
    });
    $("#ProductStoresModal").modal('show');
}
