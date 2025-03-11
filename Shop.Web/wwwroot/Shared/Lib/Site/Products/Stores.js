
var FilterInput = document.getElementById("FilterInput");
var Parameter_Filter = document.getElementById("Parameter_Filter");
//var Parameter_Type = document.getElementById("Parameter_Type");
//var Parameter_Categury = document.getElementById("Parameter_Categury");
var Parameter_Store = document.getElementById("Parameter_Store");
var store = document.getElementById("store");
//var Parameter_Brand = document.getElementById("Parameter_Brand");
var ProdcutsSection = document.getElementById("ProdcutsSection");
var productListSection = document.getElementById("productListSection");
var TypeSection = document.getElementById("TypeSection");
var paging = document.getElementById("paging");
var pagingHeader = document.getElementById("pagingHeader");
var Categury1 = document.getElementById("Categury1");
var Categury2 = document.getElementById("Categury2");
var Brand = document.getElementById("Brand");
var Store = document.getElementById("Store");
var StoreName = document.getElementById("StoreName");


let take = 25;
let skip = 0;
let List = [];
let scrolled = false;
let element = document.getElementById('PagingScroll');





$(window).on('resize', function () {
    $('#data-table').css("width", "100%");
});


$(document).ready(function () {
    //Parameter_CurrentPage.value = 1;
    //Parameter_Type.value = "Top";
    //Parameter_Brand.value = "";
    //Parameter_Store.value = store.value;
    GetStoreList();
});
console.log(Store.value);
let url = "/products/stores/" + Store.value + "/?handler=Data";

function GetStoreList() {
    $.ajax({
        type: "POST",
        url: url,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { "skip": skip, "store": Store.value },
        success: function (response) {
            console.log(response);
            StoreName.innerText = response.object2.storeName;
            FillDataSet(response.object);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function Filter() {

    Parameter_Filter.value = FilterInput.value;
    $("#GetDataForm").submit();
}


function FillDataSet(Data) {
    
    //console.log(skip);
    //console.log(Data);
    //scrolled = false;
    //let newArray = Data.productDtos.slice(skip, skip+take);
    //console.log(newArray);
    
    Data.productDtos.forEach(function (element) {
        console.log(element);
        var shortDescription = element.shortDescription;
        if (shortDescription == null) {
            shortDescription = "";
        }
        if (element.salePrice > 0) {
            if (element.discountPrice > 0) {
                ProdcutsSection.insertAdjacentHTML("beforeend", "<li class='col-6 col-md-2 col-wd-1gdot4 product-item'><div class='product-item__outer h-100'><div class='product-item__inner px-xl-4 p-3'><div class='product-item__body pb-xl-2'><div class='mb-2'></div><div class='mb-2 productHeight'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class='d-block text-center'><img class='img-fluid' src='/Shared/Images/Products/" + element.image + "' alt='" + element.id + "'></a></div><h6 class='font-size-12 mb-1 product-item__title'><a href='' class=' font-weight-bold'>" + element.name + "</a></h6><div class='flex-center-between mt-4 mb-1'><div class='prodcut-price d-flex align-items-center position-relative'><ins class='font-size-15 font-weight-bold text-red text-decoration-none d-flex'>" + separate(element.discountPrice) + "<img src='/shared/toman.svg' /></ins><del class='font-size-1 tex-gray-6 position-absolute bottom-100'>" + separate(element.salePrice) + "</del></div><div class='d-none d-xl-block prodcut-add-cart'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class='btn-add-cart btn-primary transition-3d-hover'><i class='ec ec-add-to-cart'></i></a></div></div></div><div class='product-item__footer'><div class='border-top pt-2 flex-center-between flex-wrap'><a href='#' onclick='AddToCompare(" + element.productId + ")' class='text-gray-6 font-size-13'><i class='ec ec-compare mr-1 font-size-15'></i> مقایسه</a><a href='#' onclick='AddToFavorite(" + element.productId + ")' class='text-gray-6 font-size-13'><i class='ec ec-favorites mr-1 font-size-15'></i> افزودن به علاقه مندی</a></div></div></div></div></li>")
                productListSection.insertAdjacentHTML("beforeend", "<li class='product-item remove-divider'><div class='product-item__outer w-100'><div class='product-item__inner remove-prodcut-hover py-4 row'><div class='product-item__header col-6 col-md-2'><div class='mb-2'><a href='' class='d-block text-center'><img class='img-fluid' src='/Shared/Images/Products/" + element.image + "' alt='" + element.id + "'></a></div></div><div class='product-item__body col-6 col-md-7'><div class='pr-lg-10'><div class='mb-2'></div><h5 class='mb-2 product-item__title'><a href='Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class='font-weight-bold'>" + element.name + "</a></h5></div><div class='row'><div class='col-12'><p class='small w-70'>" + shortDescription + "</p></div></div></div><div class='product-item__footer col-md-3 d-md-block'><div class='mb-2  flex-center-between'><div class='prodcut-price mt-3'><div class='prodcut-price d-flex align-items-center position-relative'><ins class='font-size-19 font-weight-bold text-red text-decoration-none d-flex'>" + separate(element.discountPrice) + " <img src='/shared/toman.svg'></ins><del class='font-size-1 tex-gray-6 position-absolute bottom-100'>" + separate(element.salePrice) + "</del></div></div><div class='prodcut-add-cart'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class='btn-add-cart btn-primary transition-3d-hover'><i class='ec ec-add-to-cart'></i></a></div></div><div class='flex-horizontal-center justify-content-between justify-content-wd-center flex-wrap border-top pt-3'><a href='#' onclick='AddToCompare(" + element.productId + ")' class='text-gray-6 font-size-13 mx-wd-3'><i class='ec ec-compare mr-1 font-size-15'></i> مقایسه محصول</a><a href='#' onclick='AddToFavorite(" + element.productId + ")' class='text-gray-6 font-size-13 mx-wd-3'><i class='ec ec-favorites mr-1 font-size-15'></i>علاقه مندم</a></div></div></div></div></li>")

            }
            else {
                ProdcutsSection.insertAdjacentHTML("beforeend", "<li class='col-6 col-md-2 col-wd-1gdot4 product-item'><div class='product-item__outer h-100'><div class='product-item__inner px-xl-4 p-3'><div class='product-item__body pb-xl-2'><div class='mb-2'></div><div class='mb-2 productHeight'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class='d-block text-center'><img class='img-fluid' src='/Shared/Images/Products/" + element.image + "' alt='" + element.id + "'></a></div><h6 class='font-size-12 mb-1 product-item__title'><a href='' class=' font-weight-bold'>" + element.name + "</a></h6><div class='flex-center-between mt-4 mb-1'><div class='prodcut-price d-flex align-items-center position-relative'><ins class='font-size-15 font-weight-bold  text-decoration-none d-flex'>" + separate(element.salePrice) + "<img src='/shared/toman.svg' /></ins></div><div class='d-none d-xl-block prodcut-add-cart'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class='btn-add-cart btn-primary transition-3d-hover'><i class='ec ec-add-to-cart'></i></a></div></div></div><div class='product-item__footer'><div class='border-top pt-2 flex-center-between flex-wrap'><a href='#' onclick='AddToCompare(" + element.productId + ")' class='text-gray-6 font-size-13'><i class='ec ec-compare mr-1 font-size-15'></i> مقایسه</a><a href='#' onclick='AddToFavorite(" + element.productId + ")' class='text-gray-6 font-size-13'><i class='ec ec-favorites mr-1 font-size-15'></i> افزودن به علاقه مندی</a></div></div></div></div></li>")
                productListSection.insertAdjacentHTML("beforeend", "<li class='product-item remove-divider'><div class='product-item__outer w-100'><div class='product-item__inner remove-prodcut-hover py-4 row'><div class='product-item__header col-6 col-md-2'><div class='mb-2'><a href='' class='d-block text-center'><img class='img-fluid' src='/Shared/Images/Products/" + element.image + "' alt='" + element.id + "'></a></div></div><div class='product-item__body col-6 col-md-7'><div class='pr-lg-10'><div class='mb-2'></div><h5 class='mb-2 product-item__title'><a href='Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class=' font-weight-bold'>" + element.name + "</a></h5></div><div class='row'><div class='col-12'><p class='small w-70'>" + shortDescription + "</p></div></div></div><div class='product-item__footer col-md-3 d-md-block'><div class='mb-2  flex-center-between'><div class='prodcut-price mt-3'><div class='prodcut-price d-flex align-items-center position-relative'><ins class='font-size-19 font-weight-bold  text-decoration-none d-flex'>" + separate(element.salePrice) + " <img src='/shared/toman.svg'></ins></div></div><div class='prodcut-add-cart'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class='btn-add-cart btn-primary transition-3d-hover'><i class='ec ec-add-to-cart'></i></a></div></div><div class='flex-horizontal-center justify-content-between justify-content-wd-center flex-wrap border-top pt-3'><a href='#' onclick='AddToCompare(" + element.productId + ")'class='text-gray-6 font-size-13 mx-wd-3'><i class='ec ec-compare mr-1 font-size-15'></i> مقایسه محصول</a><a href='#' onclick='AddToFavorite(" + element.productId + ")' class='text-gray-6 font-size-13 mx-wd-3'><i class='ec ec-favorites mr-1 font-size-15'></i> علاقه مندم</a></div></div></div></div></li>")

            }
        }
        else {

            ProdcutsSection.insertAdjacentHTML("beforeend", "<li class='col-6 col-md-2 col-wd-1gdot4 product-item'><div class='product-item__outer h-100'><div class='product-item__inner px-xl-4 p-3'><div class='product-item__body pb-xl-2'><div class='mb-2'></div><div class='mb-2 productHeight'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class='d-block text-center'><img class='img-fluid' src='/Shared/Images/Products/" + element.image + "' alt='" + element.id + "'></a></div><h6 class='font-size-12 mb-1 product-item__title'><a href='' class=' font-weight-bold'>" + element.name + "</a></h6><div class='flex-center-between mt-4 mb-1'><div class='prodcut-price d-flex align-items-center position-relative'><ins class='font-size-15 font-weight-bold text-decoration-none d-flex'>به اتمام رسیده </ins></div><div class='d-none d-xl-block prodcut-add-cart'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class='btn-add-cart btn-primary transition-3d-hover'><i class='ec ec-add-to-cart'></i></a></div></div></div><div class='product-item__footer'><div class='border-top pt-2 flex-center-between flex-wrap'><a href='#' onclick='AddToCompare(" + element.productId + ")' class='text-gray-6 font-size-13'><i class='ec ec-compare mr-1 font-size-15'></i> مقایسه</a><a href='#' onclick='AddToFavorite(" + element.productId + ")' class='text-gray-6 font-size-13'><i class='ec ec-favorites mr-1 font-size-15'></i> افزودن به علاقه مندی</a></div></div></div></div></li>")
            productListSection.insertAdjacentHTML("beforeend", "<li class='product-item remove-divider'><div class='product-item__outer w-100'><div class='product-item__inner remove-prodcut-hover py-4 row'><div class='product-item__header col-6 col-md-2'><div class='mb-2'><a href='' class='d-block text-center'><img class='img-fluid' src='/Shared/Images/Products/" + element.image + "' alt='" + element.id + "'></a></div></div><div class='product-item__body col-6 col-md-7'><div class='pr-lg-10'><div class='mb-2'></div><h5 class='mb-2 product-item__title'><a href='Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class=' font-weight-bold'>" + element.name + "</a></h5></div><div class='row'><div class='col-12'><p class='small w-70'>" + shortDescription + "</p></div></div></div><div class='product-item__footer col-md-3 d-md-block'><div class='mb-2  flex-center-between'><div class='prodcut-price mt-3'><div class='prodcut-price d-flex align-items-center position-relative'><ins class='font-size-15 font-weight-bold text-decoration-none d-flex'>به اتمام رسیده</ins></div></div><div class='prodcut-add-cart'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class='btn-add-cart btn-primary transition-3d-hover'><i class='ec ec-add-to-cart'></i></a></div></div><div class='flex-horizontal-center justify-content-between justify-content-wd-center flex-wrap border-top pt-3'><a href='#' onclick='AddToCompare(" + element.productId + ")' class='text-gray-6 font-size-13 mx-wd-3'><i class='ec ec-compare mr-1 font-size-15'></i> مقایسه محصول</a><a href='#' onclick='AddToFavorite(" + element.productId + ")' class='text-gray-6 font-size-13 mx-wd-3'><i class='ec ec-favorites mr-1 font-size-15'></i>علاقه مندم</a></div></div></div></div></li>")

        }


    });
    //let scrollTopPos = element.scrollTop;
    //console.log(scrolled);
    //console.log(scrollTopPos);
}




//window.addEventListener('scroll', function () {
    
//    let elementBottom = element.getBoundingClientRect().bottom;
//    let windowBottom = window.innerHeight;


    
//    if (!scrolled && elementBottom <= windowBottom) {
//        skip += 25;
//        FillDataSet(List);
//        scrolled = true;
//    }
//});









function OnLoadingData() {

    ProdcutsSection.innerHTML = null;
    productListSection.innerHTML = null;
}

function OnCompleteData(xhr) {
    console.log(xhr);
    var Data = xhr.responseJSON.object;
    List = Data;
    FillDataSet(List);
    
}

function OnErrorData() {

}



// تابعی برای بارگیری موارد جدید
function loadMoreItems() {
   
        skip += 25;
        GetStoreList();
   
}





