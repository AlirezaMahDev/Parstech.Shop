
let orderId = document.getElementById("orderId");
let RahakarnOrder = document.getElementById("RahakarnOrder");
let RahakarnUser = document.getElementById("RahakarnUser");
let RahakarnProduct = document.getElementById("RahakarnProduct");
let sendSection = document.getElementById("sendSection");
let sendLoad = document.getElementById("sendLoad");
let followSection = document.getElementById("followSection");
let followLoad = document.getElementById("followLoad");

$(window).on('resize', function () {
    $('#data-table').css("width", "100%");
});


$(document).ready(function () {
    GetData(orderId.value);
});


function GetData(id) {
    $.ajax({
        type: "POST",
        url: "/admin/orders/apihamkaran/" + orderId.value + "/?handler=Data",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { "orderId": id },
        success: function (response) {
            console.log(response);
            FillItem(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}



function FillItem(response) {
    let order = response.order;
    let customer = response.customer;
    let products = response.products;
    
    let sendButton = "<button onclick='SendOrder(" + order.orderId + ")' type='button' class='btn btn-block btn-default'><i class='fas fa-circle-nodes Green  m-1'></i> ارسال سفارش به سامانه راهکاران</button>";
    let followButton = "<button  type='button' class='btn btn-block btn-default'><i class='fas fa-circle-nodes Green  m-1'></i> فاکتور قبلا صادر شده است</button>";
    

    let op = order.rahkaranPishNumber;
    let ofn = order.rahakaranFactorNumber;
    let ofs = order.rahakaranFactorSerial;
    let oButton = "<button onclick='EditOrderRahkaran(" + order.orderId + ")' type='button' class='btn btn-default'><i class='  fas fa-pen Yellow m-1'></i> ویرایش شناسه راهکاران</button>"
    
    if (ofn == null) {
        ofn = "ثبت نشده";
        oButton = "<button onclick='AddOrderRahkaran(" + order.orderId + ")' type='button' class='btn btn-default'><i class='  fas fa-plus Yellow m-1'></i> ثبت شناسه راهکاران</button>"
        followButton = "<button onclick='FollowOrder(" + order.orderId + ")' type='button' class='btn btn-block btn-default'><i class='fas fa-circle-nodes Green  m-1'></i> استعلام فاکتور از سامانه راهکاران</button>";

    }
    if (ofs == null) {
        ofs = "ثبت نشده";
        oButton = "<button onclick='AddOrderRahkaran(" + order.orderId + ")' type='button' class='btn btn-default'><i class='  fas fa-plus Yellow m-1'></i> ثبت شناسه راهکاران</button>"

    }
    if (op == null) {
        op = "ثبت نشده";
        oButton = "<button onclick='AddOrderRahkaran(" + order.orderId + ")' type='button' class='btn btn-default'><i class='  fas fa-plus Yellow m-1'></i> ثبت شناسه راهکاران</button>"
        sendButton = "<button onclick='SendOrder(" + order.orderId + ")' type='button' class='btn btn-block btn-default'><i class='fas fa-circle-nodes Green  m-1'></i> ارسال سفارش به سامانه راهکاران</button>";
        followButton = "<button  type='button' class='btn btn-block btn-default'><i class='fas fa-circle-nodes Green  m-1'></i> شناسه پیش فاکتور ثبت نشده است</button>";

    }
    RahakarnOrder.insertAdjacentHTML("afterbegin", "<tr><td>" + order.orderCode + " </td><td>" + op + "</td><td>" + ofn + "</td><td>" + ofs + "</td><td>" + oButton + "</td></tr>");


    let un = customer.nationalCode;
    let ue = customer.economicCode;
    let ur = customer.rahkaranUserId;
    let uButton = "<button onclick='EditUserRahkaran(" + customer.id + ")' type='button' class='btn btn-default'><i class='  fas fa-pen Yellow m-1'></i> ویرایش شناسه راهکاران</button>"

    if (un == null) {
        un = "ثبت نشده";

    }
    if (ue == null) {
        ue = "ثبت نشده";
    }
    if (ur == null) {
        ur = "ثبت نشده";
        uButton = "<button onclick='AddUserRahkaran(" + customer.id + ")' type='button' class='btn btn-default'><i class='  fas fa-plus Yellow m-1'></i> ثبت شناسه راهکاران</button>"
         sendButton = "<button type='button' class='btn btn-block btn-default'><i class='fas fa-circle-nodes Green  m-1'></i> شناسه مشتری ثبت نشده است!</button>";

    }
    RahakarnUser.insertAdjacentHTML("afterbegin", "<tr><td>" + customer.firstName + " " + customer.lastName + "</td><td>" + customer.userName + "</td><td>" + un + "</td><td>" + ue + "</td><td>" + ur + "</td><td>" + uButton + "</td></tr>");


    

    products.forEach(function (element) {
        let pr = element.rahkaranProductId;
        let pUnit = element.rahkaranUnitId;
        let pButton = "<button onclick='EditProductRahkaran(" + element.productId + ")' type='button' class='btn btn-default'><i class='  fas fa-pen Yellow m-1'></i> ویرایش شناسه راهکاران</button>"

        if (pr == null) {
            pr = "ثبت نشده";
            pButton = "<button onclick='AddProductRahkaran(" + element.productId + ")' type='button' class='btn btn-default'><i class='  fas fa-plus Yellow m-1'></i> ثبت شناسه راهکاران</button>"
            sendButton = "<button type='button' class='btn btn-block btn-default'><i class='fas fa-circle-nodes Green  m-1'></i> شناسه قلم ثبت نشده است!</button>";

        }
        if (pUnit == null) {
            pUnit = "ثبت نشده";
           
        }
        RahakarnProduct.insertAdjacentHTML("afterbegin", "<tr><td>" + element.name + "</td><td>" + element.code + "</td><td>" + pr + "</td><td>" + pUnit + "</td><td>" + pButton + "</td></tr>");
    });


    sendSection.insertAdjacentHTML("beforeend", sendButton);
    followSection.insertAdjacentHTML("beforeend", followButton);

}

//RAHKARAN ORDER

let roOrderId = document.getElementById("roOrderId");
let roRahkaranPishNumber = document.getElementById("roRahkaranPishNumber");
let roRahakaranFactorNumber = document.getElementById("roRahakaranFactorNumber");
let roRahakaranFactorSerial = document.getElementById("roRahakaranFactorSerial");
let roType = document.getElementById("roType");
function GetRO(id) {
    $.ajax({
        type: "POST",
        url: "/admin/orders/apihamkaran/" + orderId.value + "/?handler=Order",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { "id": id },
        success: function (response) {
            console.log(response);
            fillRO(response)
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function fillRO(response) {
    roOrderId.value = response.orderId;
    roRahkaranPishNumber.value = response.rahkaranPishNumber;
    roRahakaranFactorNumber.value = response.rahakaranFactorNumber;
    roRahakaranFactorSerial.value = response.rahakaranFactorSerial;
}
function EditOrderRahkaran(id) {
    $("#OrderModal").modal('show');
    roType.value = 2;
    GetRO(id);
}
function AddOrderRahkaran(id) {
    $("#OrderModal").modal('show');
    roType.value = 1;
    roOrderId.value =id;
}



//RAHKARAN USER
let ruUserId = document.getElementById("ruUserId");
let ruRahkaranUserId = document.getElementById("ruRahkaranUserId");
let ruType = document.getElementById("ruType");
function GetRU(id) {
    $.ajax({
        type: "POST",
        url: "/admin/orders/apihamkaran/" + orderId.value + "/?handler=User",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { "id": id },
        success: function (response) {
            console.log(response);
            fillRU(response)
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function fillRU(response) {
    ruUserId.value = response.userId
    ruRahkaranUserId.value = response.rahkaranUserId
}

function EditUserRahkaran(id) {
    $("#UserModal").modal('show');
    ruType.value = 2;
    GetRU(id);
}
function AddUserRahkaran(id) {
    $("#UserModal").modal('show');
    ruType.value = 1;
    ruUserId.value = id;
}





//RAHKARAN PRODUCT
let rpProductId = document.getElementById("rpProductId");
let rpRahkaranProductId = document.getElementById("rpRahkaranProductId");
let rpRahkaranUnitId = document.getElementById("rpRahkaranUnitId");
let rpType = document.getElementById("rpType");
function GetRP(id) {
    $.ajax({
        type: "POST",
        url: "/admin/orders/apihamkaran/" + orderId.value + "/?handler=Product",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { "id": id },
        success: function (response) {
            console.log(response);
            fillRP(response)
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}
function fillRP(response) {
    rpProductId.value = response.productId
    rpRahkaranProductId.value = response.rahkaranProductId
    rpRahkaranUnitId.value = response.rahkaranUnitId
}
function EditProductRahkaran(id) {
    $("#ProductModal").modal('show');
    rpType.value = 2;
    GetRP(id);
}
function AddProductRahkaran(id) {
    $("#ProductModal").modal('show');
    rpType.value = 1;
    rpProductId.value = id;
}














function OnComplete(xhr) {
    // console.log(xhr);

    if (xhr.status != 200) {

        ToastError("در خواست شما با شکست مواجه شده است");
        Clean();
        GetData(orderId.value);

    }
    else {

        ToastSuccess("عملیات با موفقیت انجام شد");
        Clean();
        GetData(orderId.value);
    }

}

function Clean() {
    RahakarnOrder.innerHTML = null;
    
    RahakarnUser.innerHTML = null;
    RahakarnProduct.innerHTML = null;
    sendSection.innerHTML = null;
    followSection.innerHTML = null;
}


function IdForInputs(id) {
    var IdList = document.querySelectorAll('.orderId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}

function SendOrder(id) {
    IdForInputs(id);
    
    sendLoad.insertAdjacentHTML("beforeend", "<div class='badge w-100 badge-warning'>در حال انجام فرآیند <i class='fas fa-spinner fa-spin fa-2x fa-fw'></i></div>");
    $("#SendOrderform").submit();
}
function FollowOrder(id) {
    IdForInputs(id);
    followLoad.insertAdjacentHTML("beforeend", "<div class='badge w-100 badge-warning'>در حال انجام فرآیند <i class='fas fa-spinner fa-spin fa-2x fa-fw'></i></div>");

    $("#FollowOrderform").submit();
}
function OnCompleteApi(xhr) {
    console.log(xhr);
    var Response = xhr.responseJSON;
    sendLoad.innerHTML = null;
    followLoad.innerHTML = null;
    if (xhr.status != 200) {

        ToastError("در خواست شما با شکست مواجه شده است");
        Clean();
        GetData(orderId.value);

    }

    else if (Response.isSuccessed) {

        ToastSuccess(Response.message);
        Clean();
        GetData(orderId.value);
    }
    else if (Response.message != null) {

        ToastError(Response.message);
        Clean();
        GetData(orderId.value);
    }
    else {
        ToastError("خطا در اتصال وب سرویس");
        Clean();
        GetData(orderId.value);
    }
}