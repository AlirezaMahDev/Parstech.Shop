var compareSection = document.getElementById("compareSection");
$(document).ready(function () {
    //Parameter_CurrentPage.value = 1;
    $("#GetDataForm").submit();
});


function OnComplete(xhr) {
    compareSection.innerHTML = null;
    console.log(xhr);

    var Data = xhr.responseJSON;
    var row = 12;
    switch (Data.length) {
        case 0:
            row = 0
            break;
        case 1:
            row=12
            break;
        case 2:
            row=6
            break;
        case 3:
            row=4
            break;
        case 4:
            row=3
            break;
    }
    if (row != 0) {
        Data.forEach(function (element) {
            var idPC = "pc-" + element.productId;
            var idPA = "pa-" + element.productId;
            compareSection.insertAdjacentHTML("beforeend", "<div class='col-lg-" + row + "'><div class='panel-body rtl'><div class='portlet box border shadow round'><div class='portlet-heading'><div class='product-item__outer'><div class='product-item__inner px-xl-4 p-3'><div class='product-item__body pb-xl-2'><div class='mb-2'></div><div class='mb-2'><button class='btn BgRed btn-xs text-white mb-1' style='cursor:pointer'onclick='DeleteFromCompare(" + element.userProductId + ")'>حذف از مقایسه</button><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockId + "'' class='d-block text-center'><img class='img-fluid' src='/Shared/Images/Products/" + element.image + "' alt='3016'></a></div><h6 class='font-size-12 mb-1 product-item__title'><a href='' class=' font-weight-bold'>" + element.name + "</a></h6></div></div></div></div><div class='portlet-body'><h6 class='text-center'>مقایسه محصول</h6><div class='m-0' style='border-bottom: 1px solid #ddd;' id='" + idPC + "'></div><h6 class='text-center'>مشخصات دیگر محصول</h6><div class='m-0' style='border-bottom: 1px solid #ddd;' id='" + idPA + "'></div></div></div></div></div>")
            var idPCSection = document.getElementById(idPC);
            element.commonProperties.forEach(function (com) {
                idPCSection.insertAdjacentHTML("beforebegin", "<span class='font-weight-bold'>" + com.caption + "</span><p>" + com.value + "</p>")
            });
            var idPASection = document.getElementById(idPA);
            element.productProperties.forEach(function (all) {
                idPASection.insertAdjacentHTML("beforebegin", "<span class='font-weight-bold'>" + all.caption + "</span><p>" + all.value + "</p>")
            });
        });
    }
    else {
        
        compareSection.insertAdjacentHTML("beforeend", "<div class='alert alert-secondery border Gray font-weight-bold text-center font-size-17'><i class=' ec ec-compare'></i>هیچ محصولی در لیست مقایسه شما وجود ندارد</div>")

    }
    
}