﻿
var FilterInput = document.getElementById("FilterInput");
var Parameter_Filter = document.getElementById("Parameter_Filter");
var Parameter_Type = document.getElementById("Parameter_Type");
var Parameter_Categury = document.getElementById("Parameter_Categury");
var Parameter_Store = document.getElementById("Parameter_Store");
var brand = document.getElementById("brand");
var Parameter_Brand = document.getElementById("Parameter_Brand");
var ProdcutsSection = document.getElementById("ProdcutsSection");
var productListSection = document.getElementById("productListSection");
var TypeSection = document.getElementById("TypeSection");
var paging = document.getElementById("paging");
var pagingHeader = document.getElementById("pagingHeader");
var Categury1 = document.getElementById("Categury1");
var Categury2 = document.getElementById("Categury2");
var Brand = document.getElementById("Brand");
var Store = document.getElementById("Store");











$(window).on('resize', function () {
    $('#data-table').css("width", "100%");
});


$(document).ready(function () {
    Parameter_CurrentPage.value = 1;
    Parameter_Type.value = "Top";
    Parameter_Brand.value = brand.value;
   
    $("#GetDataForm").submit();
});



function Filter() {

    Parameter_Filter.value = FilterInput.value;
    $("#GetDataForm").submit();
}

function FillType(typeId) {
    switch (typeId) {
        case 1:
            TypeSection.innerHTML = "<a onclick='ChangeType(1)'class='Orange p-2' href='#'>پربازدید ترین</a><a onclick='ChangeType(2)' class='Gray p-2' href='#'>جدید ترین</a><a onclick='ChangeType(3)' class='Gray p-2' href='#'>ارزان ترین ترین</a><a onclick='ChangeType(4)' class='Gray p-2' href='#'>گران ترین </a>"
            break;
        case 2:
            TypeSection.innerHTML = "<a onclick='ChangeType(1)'class='Gray p-2' href='#'>پربازدید ترین</a><a onclick='ChangeType(2)' class='Orange p-2' href='#'>جدید ترین</a><a onclick='ChangeType(3)' class='Gray p-2' href='#'>ارزان ترین ترین</a><a onclick='ChangeType(4)' class='Gray p-2' href='#'>گران ترین </a>"
            break;
        case 3:
            TypeSection.innerHTML = "<a onclick='ChangeType(1)'class='Gray p-2' href='#'>پربازدید ترین</a><a onclick='ChangeType(2)' class='Gray p-2' href='#'>جدید ترین</a><a onclick='ChangeType(3)' class='Orange p-2' href='#'>ارزان ترین ترین</a><a onclick='ChangeType(4)' class='Gray p-2' href='#'>گران ترین </a>"
            break;
        case 4:
            TypeSection.innerHTML = "<a onclick='ChangeType(1)'class='Gray p-2' href='#'>پربازدید ترین</a><a onclick='ChangeType(2)' class='Gray p-2' href='#'>جدید ترین</a><a onclick='ChangeType(3)' class='Gray p-2' href='#'>ارزان ترین ترین</a><a onclick='ChangeType(4)' class='Orange p-2' href='#'>گران ترین </a>"
            break;
    }
}
function FillDataSet(Data) {
    console.log(Data);

    Data.productDtos.forEach(function (element) {
        if (element.discountPrice > 0) {
            ProdcutsSection.insertAdjacentHTML("beforeend", "<li class='col-6 col-md-3 col-wd-3gdot4 product-item'><div class='product-item__outer h-100'><div class='product-item__inner px-xl-4 p-3'><div class='product-item__body pb-xl-2'><div class='mb-2'><a href='/Products/Detail/" + element.shortLink + "' class='font-size-12 text-gray-5'>" + element.categuryName + "</a></div><div class='mb-2'><a href='/Products/Detail/" + element.shortLink + "' class='d-block text-center'><img class='img-fluid' src='/Shared/Images/Products/" + element.image + "' alt='" + element.id + "'></a></div><h6 class='font-size-12 mb-1 product-item__title'><a href='' class=' font-weight-bold'>" + element.name + "</a></h6><div class='flex-center-between mt-4 mb-1'><div class='prodcut-price d-flex align-items-center position-relative'><ins class='font-size-15 font-weight-bold text-red text-decoration-none d-flex'>" + separate(element.discountPrice) + "<img src='/Shared/Toman.svg' /></ins><del class='font-size-1 tex-gray-6 position-absolute bottom-100'>" + separate(element.salePrice) + "</del></div><div class='d-none d-xl-block prodcut-add-cart'><a href='/Products/Detail/" + element.shortLink + "' class='btn-add-cart btn-primary transition-3d-hover'><i class='ec ec-add-to-cart'></i></a></div></div></div><div class='product-item__footer'><div class='border-top pt-2 flex-center-between flex-wrap'><a href='#' onclick='AddToCompare(" + element.id + ")' class='text-gray-6 font-size-13'><i class='ec ec-compare mr-1 font-size-15'></i> مقایسه</a><a href='#' onclick='AddToFavorite(" + element.id + ")' class='text-gray-6 font-size-13'><i class='ec ec-favorites mr-1 font-size-15'></i> افزودن به غلافه مندی</a></div></div></div></div></li>")
            productListSection.insertAdjacentHTML("beforeend", "<li class='product-item remove-divider'><div class='product-item__outer w-100'><div class='product-item__inner remove-prodcut-hover py-4 row'><div class='product-item__header col-6 col-md-2'><div class='mb-2'><a href='' class='d-block text-center'><img class='img-fluid' src='/Shared/Images/Products/" + element.image + "' alt='" + element.id + "'></a></div></div><div class='product-item__body col-6 col-md-7'><div class='pr-lg-10'><div class='mb-2'><a href='Products/Detail/" + element.shortLink + "' class='font-size-12 text-gray-5'>" + element.categuryName + "</a></div><h5 class='mb-2 product-item__title'><a href='Products/Detail/" + element.shortLink + "' class='text-blue font-weight-bold'>" + element.name + "</a></h5></div><div class='row'><div class='col-12'><p class='" + element.shortDescription + "'></p></div></div></div><div class='product-item__footer col-md-3 d-md-block'><div class='mb-2  flex-center-between'><div class='prodcut-price mt-3'><div class='prodcut-price d-flex align-items-center position-relative'><ins class='font-size-19 font-weight-bold text-red text-decoration-none d-flex'>" + separate(element.discountPrice) + " <img src='/Shared/Toman.svg'></ins><del class='font-size-1 tex-gray-6 position-absolute bottom-100'>" + separate(element.salePrice) + "</del></div></div><div class='prodcut-add-cart'><a href='../shop/single-product-fullwidth.html' class='btn-add-cart btn-primary transition-3d-hover'><i class='ec ec-add-to-cart'></i></a></div></div><div class='flex-horizontal-center justify-content-between justify-content-wd-center flex-wrap border-top pt-3'><a href='../shop/compare.html' class='text-gray-6 font-size-13 mx-wd-3'><i class='ec ec-compare mr-1 font-size-15'></i> Compare</a><a href='../shop/wishlist.html' class='text-gray-6 font-size-13 mx-wd-3'><i class='ec ec-favorites mr-1 font-size-15'></i> Wishlist</a></div></div></div></div></li>")

        }
        else {
            ProdcutsSection.insertAdjacentHTML("beforeend", "<li class='col-6 col-md-3 col-wd-3gdot4 product-item'><div class='product-item__outer h-100'><div class='product-item__inner px-xl-4 p-3'><div class='product-item__body pb-xl-2'><div class='mb-2'><a href='/Products/Detail/" + element.shortLink + "' class='font-size-12 text-gray-5'>" + element.categuryName + "</a></div><div class='mb-2'><a href='/Products/Detail/" + element.shortLink + "' class='d-block text-center'><img class='img-fluid' src='/Shared/Images/Products/" + element.image + "' alt='" + element.id + "'></a></div><h6 class='font-size-12 mb-1 product-item__title'><a href='' class=' font-weight-bold'>" + element.name + "</a></h6><div class='flex-center-between mt-4 mb-1'><div class='prodcut-price d-flex align-items-center position-relative'><ins class='font-size-15 font-weight-bold  text-decoration-none d-flex'>" + separate(element.salePrice) + "<img src='/Shared/Toman.svg' /></ins></div><div class='d-none d-xl-block prodcut-add-cart'><a href='/Products/Detail/" + element.shortLink + "' class='btn-add-cart btn-primary transition-3d-hover'><i class='ec ec-add-to-cart'></i></a></div></div></div><div class='product-item__footer'><div class='border-top pt-2 flex-center-between flex-wrap'><a href='#' onclick='AddToCompare(" + element.id + ")' class='text-gray-6 font-size-13'><i class='ec ec-compare mr-1 font-size-15'></i> مقایسه</a><a href='#' onclick='AddToFavorite(" + element.id + ")' class='text-gray-6 font-size-13'><i class='ec ec-favorites mr-1 font-size-15'></i> افزودن به غلافه مندی</a></div></div></div></div></li>")
            productListSection.insertAdjacentHTML("beforeend", "<li class='product-item remove-divider'><div class='product-item__outer w-100'><div class='product-item__inner remove-prodcut-hover py-4 row'><div class='product-item__header col-6 col-md-2'><div class='mb-2'><a href='' class='d-block text-center'><img class='img-fluid' src='/Shared/Images/Products/" + element.image + "' alt='" + element.id + "'></a></div></div><div class='product-item__body col-6 col-md-7'><div class='pr-lg-10'><div class='mb-2'><a href='Products/Detail/" + element.shortLink + "' class='font-size-12 text-gray-5'>" + element.categuryName + "</a></div><h5 class='mb-2 product-item__title'><a href='Products/Detail/" + element.shortLink + "' class='text-blue font-weight-bold'>" + element.name + "</a></h5></div><div class='row'><div class='col-12'><p class='" + element.shortDescription + "'></p></div></div></div><div class='product-item__footer col-md-3 d-md-block'><div class='mb-2  flex-center-between'><div class='prodcut-price mt-3'><div class='prodcut-price d-flex align-items-center position-relative'><ins class='font-size-19 font-weight-bold text-red text-decoration-none d-flex'>" + separate(element.discountPrice) + " <img src='/Shared/Toman.svg'></ins><del class='font-size-1 tex-gray-6 position-absolute bottom-100'>" + separate(element.salePrice) + "</del></div></div><div class='prodcut-add-cart'><a href='../shop/single-product-fullwidth.html' class='btn-add-cart btn-primary transition-3d-hover'><i class='ec ec-add-to-cart'></i></a></div></div><div class='flex-horizontal-center justify-content-between justify-content-wd-center flex-wrap border-top pt-3'><a href='../shop/compare.html' class='text-gray-6 font-size-13 mx-wd-3'><i class='ec ec-compare mr-1 font-size-15'></i> Compare</a><a href='../shop/wishlist.html' class='text-gray-6 font-size-13 mx-wd-3'><i class='ec ec-favorites mr-1 font-size-15'></i> Wishlist</a></div></div></div></div></li>")

        }
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


    paging.innerHTML = null;
    pagingHeader.innerHTML = null;
    pagingHeader.insertAdjacentHTML("beforeend", "<h6 class='m-2'>صفحه " + current + " از " + last + "</h5>");
    paging.insertAdjacentHTML("beforeend", " <li class='page-item'><a onclick='RunPaging(" + first + ")' class='page-link current' href='#'><i class='p-2 White fas fa-circle-arrow-right p-1'></i></a></li><li class='page-item'><a onclick='RunPaging(" + privious + ")' class='page-link' href='#'><i class='p-2 Orange fas fa-arrow-right p-1'></i></a></li><li class='page-item'><a onclick='RunPaging(" + next + ")' class='page-link' href='#'><i class='p-2 Orange fas fa-arrow-left p-1'></i></a></li><li class='page-item'><a onclick='RunPaging(" + last + ")' class='page-link current' href='#'><i class='p-2 White fas fa-circle-arrow-left p-1'></i></a></li>");

}
function RunPaging(page) {

    Parameter_Filter.value = null;
    Parameter_CurrentPage.value = page;
    $("#GetDataForm").submit();
}
function ChangeType(typeId) {
    switch (typeId) {
        case 1:
            Parameter_Type.value = "Top";
            break;
        case 2:
            Parameter_Type.value = "New";
            break;
        case 3:
            Parameter_Type.value = "HighPrice";
            break;
        case 4:
            Parameter_Type.value = "LowPrice";
            break;
    }

    $("#GetDataForm").submit();
}

function ChangeCategury(id) {
    var g = "g-" + id;
    var cat = document.getElementById(g);
    Parameter_Categury.value = cat.innerText;
    $("#GetDataForm").submit();
}
function ChangeBrand(id) {
    var b = "b-" + id;
    var brand = document.getElementById(b);
    Parameter_Brand.value = brand.innerText;
    $("#GetDataForm").submit();
}
function ChangeStore(id) {
    var s = "s-" + id;
    var store = document.getElementById(s);
    Parameter_Store.value = store.innerText;
    $("#GetDataForm").submit();
}




function OnLoadingData() {

    ProdcutsSection.innerHTML = null;
    productListSection.innerHTML = null;
}

function OnCompleteData(xhr) {
    console.log(xhr);
    var Data = xhr.responseJSON.object;
    switch (Parameter_Type.value) {
        case "Top":
            FillType(1);
            break;
        case "New":
            FillType(2);
            break;
        case "HighPrice":
            FillType(3);
            break;
        case "LowPrice":
            FillType(4);
            break;
        default:
            break;
    }
    FillDataSet(Data);
    Categury1.innerText = xhr.responseJSON.object2.groupTitle;
    Categury12.innerText = xhr.responseJSON.object2.groupTitle;

    Brand.innerText = Parameter_Brand.value;
    Store.innerText = Parameter_Store.value;
}

function OnErrorData() {

}



