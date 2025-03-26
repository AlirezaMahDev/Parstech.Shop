var ShortLink = document.getElementById('ShortLink');
var SLink;
var ProductNameSecion = document.getElementById('ProductNameSecion');
var PriceSection = document.getElementById('PriceSection');
var ProductNav = document.getElementById('ProductNav');
var RelatedSection = document.getElementById('RelatedSection');
var DescriptionSection = document.getElementById('DescriptionSection');
var PropertySection = document.getElementById('PropertySection');

//Mobile
let ProductNameSecionMobile = document.getElementById("ProductNameSecionMobile");
let ProductNameSecionMobile2 = document.getElementById("ProductNameSecionMobile2");
let PriceSectionMobile = document.getElementById("PriceSectionMobile");
$(window).on('resize', function () {
    $('#data-table').css('width', '100%');
});


$(document).ready(function () {

    $('#GetDataForm').submit();
});


function FillNameSection(Data) {
    console.log(Data);
    ProductNav.innerText = Data.name;
    if (Data.latinName == null) {
        Data.latinName = " ";
    }
    SLink = Data.shortLink;
    console.log(SLink);

    if (Data.discountDate != null) {
        const myArray = Data.discountDate.split("-");
        let newDate = myArray[0] + "-" + myArray[1] + "-" + myArray[2];
        console.log(newDate);
        var targetDate = new Date(newDate).getTime();
        DisountCounterProductDetail(targetDate);
    }

    

    ProductNameSecion.insertAdjacentHTML("beforeend", "<div class='product-item__body col-12 col-md-12'><div class=''><div class='text-gray-9 font-size-14 pb-2 border-color-1 border-bottom mb-3'><i class='  fas fa-shop Red p-1'></i>فروشنده :<a href='/Products/Stores/" + Data.storeLatin + "'><span class='Black font-weight-bold'>" + Data.store + "</span><a/></div><h5 class='mb-2 '><a href='#' class='black font-weight-bold'>" + Data.name + " </a></h5><span class='mb-2 '><a href='#' class='black font-weight-bold'>" + Data.latinName + "</a></span></div></div>")
    ProductNameSecionMobile.insertAdjacentHTML("beforeend", "<div class='product-item__body col-12 col-md-12'><div class=''><div class='text-gray-9 font-size-14 pb-2 border-color-1 border-bottom mb-3'><i class='  fas fa-shop Red p-1'></i>فروشنده :<a href='/Products/Stores/" + Data.storeLatin + "'><span class='Black font-weight-bold'>" + Data.store + "</span><a/></div><h5 class='mb-2 '><a href='#' class='black font-weight-bold'>" + Data.name + " </a></h5><span class='mb-2 '><a href='#' class='black font-weight-bold'>" + Data.latinName + "</a></span></div></div>")

    if (Data.brand.brandImage != null) {
        ProductNameSecion.insertAdjacentHTML("beforeend", "<a href = '#' class='d-inline-block max-width-150 ml-n2 mb-2' ><img class='img-fluid' src='/Shared/Images/" + Data.brand.brandImage + "' alt='" + Data.brand.brandTitle + "'></a>");
        ProductNameSecionMobile2.insertAdjacentHTML("beforeend", "<a href = '#' class='d-inline-block max-width-150 ml-n2 mb-2' ><img class='img-fluid' src='/Shared/Images/" + Data.brand.brandImage + "' alt='" + Data.brand.brandTitle + "'></a>");

    } 
    ProductNameSecion.insertAdjacentHTML("beforeend", "<div class='mb-2'><strong>ویژگی محصول </strong><div class='row m-2' id='properties'></div><strong>توضیحات محصول </strong><div class='row m-2' id='shortdesc'></div><strong>شناسه محصول </strong><div class='row m-2' id='pid'></div></div>");
    ProductNameSecionMobile2.insertAdjacentHTML("beforeend", "<div class='mb-2'><strong>ویژگی محصول </strong><div class='row m-2' id='properties2'></div><strong>توضیحات محصول </strong><div class='row m-2' id='shortdesc2'></div><strong>شناسه محصول </strong><div class='row m-2' id='pid2'></div></div>");

    let properties = document.getElementById("properties");
    let properties2 = document.getElementById("properties2");
    let shortdesc = document.getElementById("shortdesc");
    let shortdesc2 = document.getElementById("shortdesc2");
    let pid = document.getElementById("pid");
    let pid2 = document.getElementById("pid2");


   
    
    var SomeProperties = Data.someProperties;
    SomeProperties.forEach(function (element) {
        properties.insertAdjacentHTML("beforeend", "<div class='col-lg-6 col-sm-12 pb-0 mb-0'><p class='mb-0'>" + element.propertyName + " : " + element.value + "</p></div>");
        properties2.insertAdjacentHTML("beforeend", "<div class='col-lg-6 col-sm-12 pb-0 mb-0'><p class='mb-0'>" + element.propertyName + " : " + element.value + "</p></div>");
    });

    if (Data.shortDescription!= null) {
        shortdesc.insertAdjacentHTML("beforeend", " <div class='col-12 pb-0 mb-0'><p class='mb-0'>" + Data.shortDescription + "</p></div>")
        shortdesc2.insertAdjacentHTML("beforeend", " <div class='col-12 pb-0 mb-0'><p class='mb-0'>" + Data.shortDescription + "</p></div>")
    }

    
    if (Data.code != null) {
        pid.insertAdjacentHTML("beforeend", "<div class='col-12 pb-0 mb-0'><p class='mb-0'>" + Data.code + "</p></div>")
        pid2.insertAdjacentHTML("beforeend", "<div class='col-12 pb-0 mb-0'><p class='mb-0'>" + Data.code + "</p></div>")
    }
    
    
}
function FillPriceSection(Data) {
    console.log(Data);
    
    if (Data.quantity > 0) {
        PriceSection.insertAdjacentHTML("beforeend", "<div class='text-gray-9 font-size-14 pb-2 border-color-1 border-bottom mb-3'>موجود در انبار<i class=' fas fa-check-to-slot Green p-1'></i></div>")
        PriceSectionMobile.insertAdjacentHTML("beforeend", "<div class='text-gray-9 font-size-14 pb-2 border-color-1 border-bottom mb-3'>موجود در انبار<i class=' fas fa-check-to-slot Green p-1'></i></div>")
    }
    else {
        PriceSection.insertAdjacentHTML("beforeend", "<div class='text-gray-9 font-size-14 pb-2 border-color-1 border-bottom mb-3'>ناموجود<i class=' fas fa-check-to-slot Gray p-1'></i></div>")
        PriceSectionMobile.insertAdjacentHTML("beforeend", "<div class='text-gray-9 font-size-14 pb-2 border-color-1 border-bottom mb-3'>ناموجود<i class=' fas fa-check-to-slot Gray p-1'></i></div>")
    }
    if (Data.quantity > 0 && Data.salePrice > 0) {
        if (Data.discountPrice > 0) {
            
            PriceSection.insertAdjacentHTML("beforeend", "<div class='flex-center-between-price rtl border-width-2 border-color-1'><div class='prodcut-price '><del class='font-size-1 tex-gray-6  '>" + separate(Data.salePrice) + "</del><ins class='font-size-19 font-weight-bold  text-decoration-none d-flex'>" + separate(Data.discountPrice) + "<img src='/shared/toman.svg'></ins></div></div>")
            PriceSectionMobile.insertAdjacentHTML("beforeend", "<div class='flex-center-between-price rtl border-width-2 border-color-1'><div class='prodcut-price '><del class='font-size-1 tex-gray-6  '>" + separate(Data.salePrice) + "</del><ins class='font-size-19 font-weight-bold  text-decoration-none d-flex'>" + separate(Data.discountPrice) + "<img src='/shared/toman.svg'></ins></div></div>")
        }
        else {
            PriceSection.insertAdjacentHTML("beforeend", "<div class='flex-center-between-price rtl border-width-2 border-color-1'><div class='prodcut-price '><ins class='font-size-19 font-weight-bold  text-decoration-none d-flex'>" + separate(Data.salePrice) + "<img src='/shared/toman.svg'></ins></div></div>")
            PriceSectionMobile.insertAdjacentHTML("beforeend", "<div class='flex-center-between-price rtl border-width-2 border-color-1'><div class='prodcut-price '><ins class='font-size-19 font-weight-bold  text-decoration-none d-flex'>" + separate(Data.salePrice) + "<img src='/shared/toman.svg'></ins></div></div>")
        }
    }


    PriceSection.insertAdjacentHTML("beforeend", "<div class='text-gray-9 font-size-14 pb-2 border-color-1 border-bottom mb-3'>امتیاز شما  : <span class='Orange font-weight-bold'>" + Data.score + " پارس کوین</span><i class='  fab fa-gg-circle Orange p-1'></i></div>");
    PriceSectionMobile.insertAdjacentHTML("beforeend", "<div class='text-gray-9 font-size-14 pb-2 border-color-1 border-bottom mb-3'>امتیاز شما  : <span class='Orange font-weight-bold'>" + Data.score + " پارس کوین</span><i class='  fab fa-gg-circle Orange p-1'></i></div>");
    
    if (Data.childs != null) {
        console.log(Data.childs);
        PriceSection.insertAdjacentHTML("beforeend", "<div class='mb-3'><h6 class='font-size-14'>مدل های دیگر این محصول</h6><select id='selectModel' class='form-control form-control-Custom'></select></div>");
        PriceSectionMobile.insertAdjacentHTML("beforeend", "<div class='mb-3'><h6 class='font-size-14'>مدل های دیگر این محصول</h6><select id='selectModel2' class='form-control form-control-Custom'></select></div>");

        var selectModel = document.getElementById("selectModel");
        var selectModel2 = document.getElementById("selectModel2");
        Data.childs.forEach(function (element) {
            
            if (Data.id == element.id) {
                selectModel.insertAdjacentHTML("beforeend", "<option  value='" + element.productStockPriceId + "' selected>" + element.variationName + "</option>");
                selectModel2.insertAdjacentHTML("beforeend", "<option  value='" + element.productStockPriceId + "' selected>" + element.variationName + "</option>");
            }
            else {
                selectModel.insertAdjacentHTML("beforeend", "<option  value='" + element.productStockPriceId + "'>" + element.variationName + "</option>");
                selectModel2.insertAdjacentHTML("beforeend", "<option  value='" + element.productStockPriceId + "'>" + element.variationName + "</option>");
            }

        });
        selectModel.addEventListener("change", (e) => {

            const targetValue = e.target.value;
            window.location.href = "/Products/Detail/" + SLink +"/" + targetValue;                ;
        });
        selectModel2.addEventListener("change", (e) => {

            const targetValue = e.target.value;
            window.location.href = "/Products/Detail/" + SLink +"/" + targetValue;                ;
        });

    }
    if (Data.quantity > 0 && Data.salePrice > 0) {
        PriceSection.insertAdjacentHTML("beforeend", "<div class='mb-2 pb-0dot5'><a onclick='CreateCheckout(" + Data.productStockPriceId + ")' href='#' class='btn btn-block  BgSormei text-white'><i class='ec ec-add-to-cart mr-2 font-size-20'></i> افزودن به سبد</a></div>");
        PriceSectionMobile.insertAdjacentHTML("beforeend", "<div class='mb-2 pb-0dot5'><a onclick='CreateCheckout(" + Data.productStockPriceId + ")' href='#' class='btn btn-block  BgSormei text-white'><i class='ec ec-add-to-cart mr-2 font-size-20'></i> افزودن به سبد</a></div>");

    }
    else {
        PriceSection.insertAdjacentHTML("beforeend", "<div class='mb-2 pb-0dot5'><a href='#' class='btn btn-block  BgToosi text-white'>محصول ناموجود</a></div>");
        PriceSectionMobile.insertAdjacentHTML("beforeend", "<div class='mb-2 pb-0dot5'><a href='#' class='btn btn-block  BgToosi text-white'>محصول ناموجود</a></div>");

    }

    PriceSection.insertAdjacentHTML("beforeend", "<a href='#' onclick='scrollToStores()' class='btn btn-Custom btn-block  bg-gray-17 Gray font-size-12 text-right mb-2'>خرید از فروشندگان دیگر <i class=' fas fa-shop-slash Yellow'></i></a>");
    PriceSectionMobile.insertAdjacentHTML("beforeend", "<a href='#' onclick='scrollToStores()' class='btn btn-Custom btn-block  bg-gray-17 Gray font-size-12 text-right mb-2'>خرید از فروشندگان دیگر <i class=' fas fa-shop-slash Yellow'></i></a>");


    let Actions = document.getElementById("Actions");
    Actions.insertAdjacentHTML("beforeend", " <li class='mb-2' data-bs-toggle='tooltip' data-bs-placement='left' ><a href='#'onclick='AddToCompare(" + Data.id + ")'  class='text-orange-90' data-toggle='tooltip' data-placement='top' aria-label='مقایسه محصول' data-bs-original-title='مقایسه محصول' aria-describedby='tooltip887359'><i class='font-size-22 ec ec-compare'></i></a></li><li data-bs-toggle='tooltip' data-bs-placement='left'><a href='#'onclick='AddToFavorite(" + Data.id + ")' class='text-orange-90' data-toggle='tooltip' data-placement='top' aria-label='علاقه مندی' data-bs-original-title='علاقه مندی'><i class='font-size-22 ec ec-favorites'></i></a></li>;")
    //PriceSection.insertAdjacentHTML("beforeend", "<div class='flex-content-center flex-wrap'><a href='#' onclick='AddToFavorite(" + Data.id + ")' class='text-gray-6 font-size-13 mr-2'><i class='ec ec-favorites mr-1 font-size-15'></i>افزودن به علاقه مندی ها</a><a href='#' onclick='AddToCompare(" + Data.id + ")' class='text-gray-6 font-size-13 ml-2'><i class='ec ec-compare mr-1 font-size-15'></i> مقایسه</a></div>");
}
function FillRelatedSection(Data) {

    if (Data.relatedStores.length > 0) {
        Data.relatedStores.forEach(function (element) {
            if (element.quantity > 0 && element.salePrice > 0) {
                if (element.discountPrice > 0) {
                    RelatedSection.insertAdjacentHTML("beforeend", "<li class='product-item remove-divider'><div class='product-item__outer w-100'><div class='product-item__inner remove-prodcut-hover py-4 row'><div class='product-item__header col-6 col-md-2'><div class='mb-2'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class='d-block text-center'><img class='w-50' class='img-fluid' src='/Shared/Images/Products/" + element.image + "' alt='" + element.id + "'></a></div></div><div class='product-item__body col-6 col-md-7'><div class='pr-lg-10'><div class='text-gray-9 font-size-14 pb-2 border-color-1 border-bottom mb-3'> <i class='  fas fa-shop Red p-1'></i>فروشنده :<span class='Black font-weight-bold'>" + element.storeName + "</span></div><h5 class='mb-2 product-item__title'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class='Gray font-weight-bold'>" + element.name + "</a></h5></div><div class='row'><div class='col-12'><p class='" + element.shortDescription + "'></p></div></div></div><div class='product-item__footer col-md-3 d-md-block'><div class='mb-2  flex-center-between'><div class='prodcut-price mt-3'><div class='prodcut-price d-flex align-items-center position-relative'><ins class='font-size-19 font-weight-bold text-red text-decoration-none d-flex'>" + element.discountPrice + " <img src='/shared/toman.svg'></ins><del class='font-size-1 tex-gray-6 position-absolute bottom-100'>" + element.salePrice + "</del></div></div></div></div></div></div></li>");

                }
                else {
                    RelatedSection.insertAdjacentHTML("beforeend", "<li class='product-item remove-divider'><div class='product-item__outer w-100'><div class='product-item__inner remove-prodcut-hover py-4 row'><div class='product-item__header col-6 col-md-2'><div class='mb-2'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class='d-block text-center'><img class='w-50' class='img-fluid' src='/Shared/Images/Products/" + element.image + "' alt='" + element.id + "'></a></div></div><div class='product-item__body col-6 col-md-7'><div class='pr-lg-10'><div class='text-gray-9 font-size-14 pb-2 border-color-1 border-bottom mb-3'> <i class='  fas fa-shop Red p-1'></i>فروشنده :<span class='Black font-weight-bold'>" + element.storeName + "</span></div><h5 class='mb-2 product-item__title'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class='Gray font-weight-bold'>" + element.name + "</a></h5></div><div class='row'><div class='col-12'><p class='" + element.shortDescription + "'></p></div></div></div><div class='product-item__footer col-md-3 d-md-block'><div class='mb-2  flex-center-between'><div class='prodcut-price mt-3'><div class='prodcut-price d-flex align-items-center position-relative'><ins class='font-size-19 font-weight-bold  text-decoration-none d-flex'>" + element.salePrice + " <img src='/shared/toman.svg'></ins></div></div></div></div></div></div></li>");

                }
                
            }
            else {
                
                RelatedSection.insertAdjacentHTML("beforeend", "<li class='product-item remove-divider'><div class='product-item__outer w-100'><div class='product-item__inner remove-prodcut-hover py-4 row'><div class='product-item__header col-6 col-md-2'><div class='mb-2'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class='d-block text-center'><img class='w-50' class='img-fluid' src='/Shared/Images/Products/" + element.image + "' alt='" + element.id + "'></a></div></div><div class='product-item__body col-6 col-md-7'><div class='pr-lg-10'><div class='text-gray-9 font-size-14 pb-2 border-color-1 border-bottom mb-3'> <i class='  fas fa-shop Red p-1'></i>فروشنده :<span class='Black font-weight-bold'>" + element.storeName + "</span></div><h5 class='mb-2 product-item__title'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class='Gray font-weight-bold'>" + element.name + "</a></h5></div><div class='row'><div class='col-12'><p class='" + element.shortDescription + "'></p></div></div></div><div class='product-item__footer col-md-3 d-md-block'><div class='mb-2  flex-center-between'><div class='prodcut-price mt-3'><div class='mb-2 pb-0dot5'><a href='#' class='btn btn-Custom btn-block defoult BgRed White'>محصول ناموجود</a></div></div></div></div></div></div></li>");

            }
            });
    }
    else
    {
        RelatedSection.insertAdjacentHTML("beforeend", "<span class='text-muted'>فروشنده دیگری برای این محصول وجود ندارد</span>");
    }
    

}
function FillDescriptionSection(Data) {
    DescriptionSection.innerHTML = Data.description;
}
function FillPropertySection(Data) {

    Data.properties.forEach(function (element) {
        //PropertySection.insertAdjacentHTML("beforeend", "<h3 class='font-size-18 mb-4'>" + element.propertyCategury + "</h3><div class='table-responsive'><table id='PropTable-" + element.id + "' class='table table-hover'><tbody></tbody></table></div>");
        PropertySection.insertAdjacentHTML("beforeend", "<h3 class='font-size-18 mb-4'></h3><div class='table-responsive'><table id='PropTable-" + element.id + "' class='table table-hover'><tbody></tbody></table></div>");
        var table = document.getElementById("PropTable-" + element.id);
        element.properties.forEach(function (prop) {

            table.insertAdjacentHTML("beforeend", "<tr><th class='px-4 px-xl-5 border-top-0 w-30'>" + prop.propertyName + "</th><td class='border-top-0 w-70'>" + prop.value + "</td></tr>")
        });
    });
   
}

function OnLoadingData() {


}

function OnCompleteData(xhr) {
    console.log(xhr);
    FillNameSection(xhr.responseJSON.object);
    FillPriceSection(xhr.responseJSON.object);
    FillRelatedSection(xhr.responseJSON.object);
    FillDescriptionSection(xhr.responseJSON.object);
    FillPropertySection(xhr.responseJSON.object);




}

function OnErrorData() {

}
function scrollToStores(){
    window.scrollTo(0, 800);
}



function DisountCounterProductDetail(targetDate) {


    function updateCountdown() {
        var now = new Date().getTime();
        var distance = targetDate - now;

        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);



        document.getElementById("DiscountGet").innerHTML = "<button class='btn transition-3d-hover font-weight-bold text-red h1' > <i class=' fas fa-clock-rotate-left text-red' > </i> <small class='text-black font-weight-bold'>تخفیف تا </small >" + seconds + " : " + minutes + " : " + hours + " : " + days + "  </button><div class='rounded-pill bg-gray-3 height-10 position-relative'><span class='position-absolute left-0 top-0 bottom-0 rounded-pill w-60 bg-Red'></span></div>";

        //days + " روز " + hours + " ساعت "
        //    + minutes + " دقیقه " + seconds + " ثانیه ";

        if (distance < 0) {
            clearInterval(interval);
            document.getElementById("DiscountGet").innerHTML = "<button class='btn transition-3d-hover font-weight-bold text-red' > <i class=' fas fa-clock-rotate-left text-red' > </i> <small class='text-black font-weight-bold'>تخفیف </small >منقضی شده</button><div class='rounded-pill bg-gray-3 height-10 position-relative'><span class='position-absolute left-0 top-0 bottom-0 rounded-pill w-60 bg-Red'></span></div>";
        }
    }

    // اجرای تابع بالا هر ثانیه
    var interval = setInterval(updateCountdown, 1000);
}



