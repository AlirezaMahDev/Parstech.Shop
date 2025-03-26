var table = document.getElementById("table");
var OrdersSection = document.getElementById("OrdersSection");
var paging = document.getElementById("paging");
var pagingHeader = document.getElementById("pagingHeader");
var Parameter_CurrentPage = document.getElementById("Parameter_CurrentPage");


$(window).on('resize', function () {
    $('#data-table').css("width", "100%");
});


$(document).ready(function () {
    //Parameter_CurrentPage.value = 1;
    $("#GetDataForm").submit();
});


function FillDataSet(Data) {
    
    Data.list.forEach(function (element) {
       // console.log(element);

        OrdersSection.insertAdjacentHTML("beforebegin", "<tr><td><h6 class='Red'>" + element.orderCode + "</h6></td><td><i class='" + element.statusIcon + " Blue m-1'></i>" + element.status + "</td><td>" + element.costumer + "</td><td><h6 class='font-weight-bold'>" + separate(element.total) + " تومان </h6></td><td><button onclick='GetOrderSubmit(" + element.orderId + ")' class='btn btn-default'><i class='fas fa-circle-info Blue m-1'></i>نمایش جزییات</button></td></tr>")

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
   // console.log(OrdersSection.innerHTML);
    var rowCount = table.rows.length;
    for (var i = 1; i < rowCount; i++) {
        table.deleteRow(i);
    }
    Parameter_CurrentPage.value = page;
    $("#GetDataForm").submit();
}



function OnLoading() {
   
    CleanItem();
}

function OnComplete(xhr) {
   // console.log(xhr);
    var Data = xhr.responseJSON.object;
    FillDataSet(Data);
}

function OnError() {

}


function IdForInputs(id) {
    var IdList = document.querySelectorAll('.OrderId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}

function GetOrderSubmit(id) {
    IdForInputs(id);
    $("#GetOrderForm").submit();
}

//دریافت محصول

var OrderHeader = document.getElementById("OrderHeader");
var UserInfo = document.getElementById("UserInfo");
var OrderShipping = document.getElementById("OrderShipping");
var Products = document.getElementById("Products");
var UserShippingList = document.getElementById("UserShippingList");


function CleanItem() {

    OrderHeader.innerHTML = null;
    UserInfo.innerHTML = null;
    OrderShipping.innerHTML = null;
    Products.innerHTML = null;
   
}


function FillItem(Data) {
    OrderHeader.insertAdjacentHTML("afterbegin", "<label  class=' control-label'> شناسه سفارش : <strong>" + Data.order.orderCode + "</strong></label>        <label  class=' control-label'> تاریخ سفارش : <strong>" + Data.order.createDate + "</strong></label>");
    UserInfo.insertAdjacentHTML("afterbegin", "<label class=' control-label'>نام و نام خانوادگی : <strong>" + Data.costumer.firstName + " " + Data.costumer.lastName + "</strong></label><br /><label class=' control-label'>شماره تماس : <strong>" + Data.costumer.mobile + "</strong></label><br /><label class=' control-label'>نام تحویل گیرنده : <strong>" + Data.orderShipping.firstName + " " + Data.orderShipping.lastName + "</strong></label>")
    OrderShipping.insertAdjacentHTML("afterbegin", "<label  class=' control-label'>شماره تماس : <strong>" + Data.orderShipping.mobile + "</strong></label><br /><label  class=' control-label'>آدرس : <strong>" + Data.orderShipping.fullAddress + "</strong></label><br /><label  class=' control-label'>کد پستی : <strong>" + Data.orderShipping.postCode + "</strong></label>")
    var Radif = 1;
    Data.orderDetailDto.forEach(function (element) {

        Products.insertAdjacentHTML("afterbegin", "<tr><td> " + Radif + " </td><td> " + element.productName + "<br /><strong class='Orange'>" + element.productCode + "</strong> </td><td> " + element.count + " </td><td> " + separate(element.price) + " </td><td> " + separate(element.detailSum) + " </td><td> " + separate(element.tax) + " </td><td> " + separate(element.discount) + " </td><td> " + separate(element.total) + " </td></tr>");
        Radif++;
    });
    Products.insertAdjacentHTML("beforeend", "<tr><td colspan='7'> جمع </td><td colspan='7'> " + separate(Data.order.orderSum) + " </td></tr> <tr><td colspan='7'> مالیات </td><td colspan='7'> " + separate(Data.order.tax) + " </td></tr> <tr><td colspan='7'> تخفیف </td><td colspan='7'> " + separate(Data.order.discount) + " </td></tr> <tr><td colspan='7'> جمع کل </td><td colspan='7'> " + separate(Data.order.total) + " </td></tr>")
    
}


function OnLoadingGetItem() {
    CleanItem();
}
function OnCompleteGetItem(xhr) {
   // console.log(xhr);
    FillItem(xhr.responseJSON.object);
    $("#ShowOrderDetailModal").modal("show");
}
function OnErrorGetItem() {

}







function OnLoadingAE() {
    //CleanProduct();
}
function OnCompleteAE(xhr) {
    //console.log(xhr);

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        FillItem(xhr.responseJSON.object);
        $("#ShowOrderDetailModal").modal("hide");

        tableMain
            .clear()
            .draw();
        dataSet = [];
        $("#GetDataForm").submit();

        ToastSuccess("عملیات با موفقیت انجام شد")
    }

}

function OnErrorAE() {

}




function ShowStatusModal(OrderId) {
    IdForInputs(OrderId);
    $("#ShowStatusModal").modal("show");

}

function OnLoadingStatus() {
    //CleanProduct();
}
function OnCompleteStatus(xhr) {
    //console.log(xhr);

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {

        $("#ShowStatusModal").modal("hide");

        tableMain
            .clear()
            .draw();
        dataSet = [];
        $("#GetDataForm").submit();

        ToastSuccess("عملیات با موفقیت انجام شد")
    }

}

function OnErrorStatus() {

}

