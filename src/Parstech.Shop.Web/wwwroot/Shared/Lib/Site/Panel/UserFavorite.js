var favoriteSection = document.getElementById("favoriteSection");
$(document).ready(function () {
    //Parameter_CurrentPage.value = 1;
    $("#GetDataForm").submit();
});


function OnComplete(xhr) {
    favoriteSection.innerHTML = null;
    console.log(xhr);

    var Data = xhr.responseJSON;
    var row = Data.length;

    if (row != 0) {
        Data.forEach(function (element) {

            favoriteSection.insertAdjacentHTML("beforeend", "<div class='col-lg-3'><div class='panel-body rtl'><div class='portlet box border shadow round'><div class='portlet-heading'><div class='product-item__outer'><div class='product-item__inner px-xl-4 p-3'><div class='product-item__body pb-xl-2'><div class='mb-2'></div><div class='mb-2'><button class='btn BgRed btn-xs text-white mb-1' style='cursor:pointer'onclick='DeleteFromCompare(" + element.userProductId + ")'>حذف از علاقه مندی</button><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockId + "' class='d-block text-center'><img class='img-fluid' src='/Shared/Images/Products/" + element.image + "' alt='3016'></a></div><h6 class='font-size-12 mb-1 product-item__title'><a href='' class=' font-weight-bold'>" + element.name + "</a></h6></div></div></div></div></div></div></div>")

        });
    } else {

        favoriteSection.insertAdjacentHTML("beforeend", "<div class='alert alert-secondery border Gray font-weight-bold text-center font-size-17'><i class=' ec ec-favorites'></i>هیچ محصولی در لیست علاقه مندی شما وجود ندارد</div>")

    }

}