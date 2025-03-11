var dataSet = [];
var tableMain;
var FilterInput = document.getElementById("FilterInput");
var Parameter_Filter = document.getElementById("Parameter_Filter");

var paging = document.getElementById("paging");
var pagingHeader = document.getElementById("pagingHeader");
var fromDate = document.getElementById("fromDate");
var toDate = document.getElementById("toDate");
var Pfilter = document.getElementById("Pfilter");
var Pstore = document.getElementById("Pstore");
var Pstatus = document.getElementById("Pstatus");
var PpayType = document.getElementById("PpayType");
var PuserId = document.getElementById("PuserId");









$(window).on('resize', function () {
    $('#data-table').css("width", "100%");
});


$(document).ready(function () {
    Parameter_CurrentPage.value = 1;
    fromDate.value = null;
    toDate.value = null;
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


function FillDataSet(Data) {
    dataSet = [];
    Data.list.forEach(function (element) {
       
        if (element.status == null) {
            element.status = "سبد خرید";
            element.statusIcon = "text-danger font-weight-bold";
        }
        
            const data =
                [
                    "<h5 class='font-weight-bold'>" + element.orderCode + "</h5>",
                    "<h5>" + element.statusName + "</h5>",
                    "<h5 class=''>" + element.firstName + " " + element.lastName + "</h5>",
                    "<h5 class=''>" + separate(element.total) + " تومان </h5>",
                    "<h5 class=''>" + element.typeName + "</h5>",
                    "<h5 class=''>" + element.createDateShamsi + "</h5>",
                    "<div class='btn-group'><button onclick='GetOrderSubmit(" + element.orderId + ")' class='btn btn-default'><i class='fas fa-circle-info Blue m-1'></i>نمایش جزییات</button><button onclick='ShowStatusModal(" + element.orderId + ")' type='button' class='btn btn-default'><i class=' fas fa-pencil Yellow m-1'></i>تغییر وضعیت</button><button onclick='GetStatusOfOrder(" + element.orderId + ")' type='button' class='btn btn-default'><i class=' fas fa-info Yellow m-1'></i> گزارش وضعیت</button><a href='/admin/orders/apihamkaran/" + element.orderId + "' class='btn btn-default mt-1'><i class=' fas fa-circle-nodes Green m-1'></i>وب سرویس</button></div>"
                ];
        dataSet.push(data);
    });

    var first = 1;
    var current = Data.currentPage;
    var next = Data.currentPage + 1;
    var privious = Data.currentPage - 1;
    var last = Data.pageCount ;

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

    CleanItem();
}

function OnCompleteData(xhr) {
   // console.log(xhr);
    var Data = xhr.responseJSON.object;
    FillDataSet(Data);

    tableMain = $('#data-table').DataTable({
        "searching": false,
        "paging": false,
        data: dataSet,
        columns: [
            { title: 'کد سفارش' },
            { title: 'وضعیت سفارش' },
            { title: 'نام مشتری' },
            { title: 'مبلغ نهایی' },
            { title: ' تسویه سفارش' },
            { title: 'تاریخ' },
            { title: 'عملیات         ' },
        ],
        
        "pageLength": 25
    });
    tableMain.destroy();


}

function OnErrorData() {

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
    UserShippingList.innerHTML = null;
}


function FillItem(Data) {
    OrderHeader.insertAdjacentHTML("afterbegin", "<label  class=' control-label'> شناسه سفارش : <strong>" + Data.order.orderCode + "</strong></label>        <label  class=' control-label'> تاریخ سفارش : <strong>" + Data.order.createDateShamsi + "</strong></label>");
    UserInfo.insertAdjacentHTML("afterbegin", "<label class=' control-label'>نام و نام خانوادگی : <strong>" + Data.costumer.firstName + " " + Data.costumer.lastName + "</strong></label><br /><label class=' control-label'>شماره تماس : <strong>" + Data.costumer.mobile + "</strong></label><br /><label class=' control-label'>نام تحویل گیرنده : <strong>" + Data.orderShipping.firstName + " " + Data.orderShipping.lastName + "</strong></label>")
    OrderShipping.insertAdjacentHTML("afterbegin", "<label  class=' control-label'>شماره تماس : <strong>" + Data.orderShipping.mobile + "</strong></label><br /><label  class=' control-label'>آدرس : <strong>" + Data.orderShipping.fullAddress + "</strong></label><br /><label  class=' control-label'>کد پستی : <strong>" + Data.orderShipping.postCode + "</strong></label>")
    var Radif = 1;
    Data.orderDetailDto.forEach(function (element) {
        console.log(element);
        Products.insertAdjacentHTML("afterbegin", "<tr><td> " + Radif + " </td><td> " + element.productName + "<br /><strong class='Orange'>" + element.productCode + "</strong> </td><td> " + element.storeName + "</td><td> " + element.count + " </td><td> " + separate(element.price) + " </td><td> " + separate(element.detailSum) + " </td><td> " + separate(element.tax) + " </td><td> " + separate(element.discount) + " </td><td> " + separate(element.total)+" </td></tr>");
       Radif++;
    });
    Products.insertAdjacentHTML("beforeend", "<tr><td colspan='4'> جمع </td><td colspan='4'> " + separate(Data.order.orderSum) + " </td></tr> <tr><td colspan='4'> مالیات </td><td colspan='4'> " + separate(Data.order.tax) + " </td></tr> <tr><td colspan='4'> تخفیف </td><td colspan='4'> " + separate(Data.order.discount) + " </td></tr> <tr><td colspan='4'> جمع کل </td><td colspan='4'> " + separate(Data.order.total) + " </td></tr>")
    Data.userShippingList.forEach(function (element) {

        UserShippingList.insertAdjacentHTML("beforeend", " <option value="+element.id+">"+element.address+"</option>");
      
        
    });
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




function ShowStatusModal(OrderId){
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

        if(xhr.responseJSON.isSuccessed){

            $("#ShowStatusModal").modal("hide");

            tableMain
                .clear()
                .draw();
            dataSet = [];
            $("#GetDataForm").submit();

        ToastSuccess(xhr.responseJSON.message)
        }
        else{
            $("#ShowStatusModal").modal("hide");
            ToastError(xhr.responseJSON.message)
        }
    }

}

function OnErrorStatus() {

}


//Statuses Of Order
function GetStatusOfOrder(orderId) {
    $.ajax({
        type: "POST",
        url: "/Admin/Orders?handler=GetStatuses",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { "OrderId": orderId },
        success: function (response) {
            FillStatusOfOrder(response.object);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

let StatusOfOrderSection = document.getElementById("StatusOfOrderSection");
function FillStatusOfOrder(Data) {
    console.log(Data);
    StatusOfOrderSection.innerHTML = null;
    Data.forEach(function (element) {
        let file;
        if (element.fileName != null) {
            file = "<div class='col-lg-2' ><a href='/Shared/Files/" + element.fileName + "' class='text-success m-t-20  font-weight-bold'>پیوست</a><hr /></div>";
        }
        else {
            file=""
        }
        StatusOfOrderSection.insertAdjacentHTML("beforeend", "<div class='col-lg-5' ><p class='text-center m-t-10 m-b-10 font-weight-bold'>" + element.statusName + "</p><hr /></div><div class='col-lg-2' ><p class='text-center m-t-10 m-b-10 font-weight-bold Orange'>" + element.createDateShamsi + "</p><hr /></div><div class='col-lg-3' ><p class='text-center m-t-10 m-b-10 font-weight-bold'>" + element.createBy + "</p><hr /></div>" + file + "");
    });
    $("#StatusOfOrderModal").modal('show');
}



function OpenOrderSetting() {

    let Oid = document.getElementById("Oid");
    GetOrderPays(Oid.value);
    $("#orderSettingModal").modal("show");

}

function GetOrderPays(orderId) {
    $.ajax({
        type: "POST",
        url: "/Admin/Orders?handler=OrderPays",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { "orderId": orderId },
        success: function (response) {
            FillOrderPays(response.object);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

let orderPays = document.getElementById("orderPays");
function FillOrderPays(pays) {
    orderPays.innerHTML = null;
    pays.forEach(function (element) {
        
        orderPays.insertAdjacentHTML("beforeend", "<div class='d-flex'><h5>مبلغ  <strong>" + separate(element.price) + "</strong> با روش پرداخت <strong>" + element.typeName + "</strong> و توضیحات <strong>" + element.description + "</strong>  <button type='button' onclick='DeleteOrderPays(" + element.id + ")' class='btn btn-danger btn-round'>حذف</button></h5><hr /></div>")
    })
}


function DeleteOrderPays(id) {
    $.ajax({
        type: "POST",
        url: "/Admin/Orders?handler=DeleteOrderPay",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { "id": id },
        success: function (response) {
            if (response.isSuccessed) {

               
                ToastSuccess(response.message);
                GetOrderPays(Oid.value);
            }
            else {

                ToastError(response.message)
            }
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function OnCompleteAddPay(xhr) {
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {

        if (xhr.responseJSON.isSuccessed) {

            
            ToastSuccess("روش بپرداخت با موفقیت ثبت گردید");
            GetOrderPays(Oid.value);
            $("#AddOrderPayModal").modal('hide');
        }
        else {
            
            ToastError("امکان ایجاد روش پرداخت وجود ندارد")
        }
    }
}
function AddOrderPayForm() {
    $("#AddOrderPayModal").modal('show');
}

function OnCompleteOrderComplete(xhr) {
    console.log(xhr.responseJSON);
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {

        if (xhr.responseJSON.isSuccessed) {


            ToastSuccess(xhr.responseJSON.message);
            GetOrderPays(Oid.value);
            $("#AddOrderPayModal").modal('hide');
        }
        else {

            ToastError(xhr.responseJSON.message)
        }
    }
}


//function OnCompleteSearch(xhr) {
//    console.log(xhr.responseJSON);
//    if (xhr.status != 200) {
//        ToastError("در خواست شما با شکست مواجه شده است")

//    }
//    else {

//        if (xhr.responseJSON.isSuccessed) {


//            ToastSuccess(xhr.responseJSON.message);
//            GetOrderPays(Oid.value);
//            $("#AddOrderPayModal").modal('hide');
//        }
//        else {

//            ToastError(xhr.responseJSON.message)
//        }
//    }
//}
function Clean() {


    Pfilter.value = null;
    PuserId.value = null;
    Pstore.value = null;
    Pstatus.value = null;
    PpayType.value = null;
    Parameter_CurrentPage.value = 1;
    fromDate.value = null;
    toDate.value = null;
    $("#GetDataForm").submit();
    
}
