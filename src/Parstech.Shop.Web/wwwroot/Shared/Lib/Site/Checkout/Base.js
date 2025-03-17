var Response;

let WalletPrice = 0;

$(document).ready(function () {
    GetData();

});

function GetData() {
    $.ajax({
        type: "POST",
        url: "/checkout?handler=Data",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());

        },
        data: {"Filter": SearchFilter.value},
        success: function (response) {
            console.log(response);
            DataSection.innerHTML = null;
            if (response.object.order != null) {
                FillData(response);
            } else {
                EmptyData();
            }
            GetCountOfOpenOrder();
        },
        failure: function (response) {
            EmptyData();
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}


var DataSection = document.getElementById("DataSection");

function FillData(response) {

    Response = response;
    var Order = response.object.order;
    var OrderCoupon = response.object.orderCoupon;
    var Details = response.object.orderDetailDto;
    var Shipping = response.object.orderShipping;


    DataSection.insertAdjacentHTML("beforeend", "<div class='col-lg-4 order-lg-2 mb-7 mb-lg-0'><div class='pl-lg-3 '><div class='bg-gray-1 rounded-lg'><div class='p-4 mb-4 checkout-table'><div id='OrderSection' class='border-bottom border-color-1 mb-5'><h3 class='section-title mb-0 pb-2 font-size-25'><i class='font-size-22 ec ec-shopping-bag'></i>جزئیات سفارش</h3><small class='Gray'>تمامی مبالغ به تومان می باشد</small></div><button onclick='showComplete()' class='pointer btn btn-Custom BgOrange btn-block  font-size-15 mb-3 py-2 White'>ثبت و پرداخت سفارش</button></div></div></div></div>");
    var OrderSection = document.getElementById("OrderSection");
    OrderSection.insertAdjacentHTML("beforeend", "<table class='table'><thead></thead><tbody></tbody><tfoot><tr><th><i class='  fab fa-first-order p-2 Blue'></i>مجموع محصولات</th><td>" + separate(Order.orderSum) + "</td></tr></tr><tr><th><i class='  fab fa-first-order p-2 Green'></i>مالیات بر ارزش افزوده 9%</th><td>" + separate(Order.tax) + "</td></tr><tr><th><i class='  fab fa-first-order p-2 Red'></i>تخفیف سبد خرید</th><td>" + separate(Order.discount) + "</td></tr><tr><th><i class='  fab fa-first-order p-2 Red'></i>تخفیف کل</th><td>" + separate(OrderCoupon.discountPrice) + "<br><small class='Red font-size-19 font-weight-bold'>" + OrderCoupon.couponType + "</small></td></tr><tr class='shipping'><th><i class='ec ec-transport p-2 Orange'></i>هزینه ارسال  <br /> <small class='Orange'>" + Shipping.shippingType + "</small></th><td data-title='Shipping rtl'><span class='amount'> " + separate(Order.shipping) + "<strong></strong></span><div class='mt-1'><a href='#' onclick='ShowShippingModal()' class='font-size-12 text-gray-90  underline-on-hover font-weight-bold mb-3 d-inline-block' role='button'><i class='ec ec-transport mr-1 Orange'></i> تغییر آدرس حمل و نقل</a></div></td><tr><th><i class='  fab fa-first-order p-2 Blue'></i>مجموع نهایی</th><td><strong>" + separate(Order.total) + "</strong></td></tr></tfoot></table>");
    DataSection.insertAdjacentHTML("beforeend", "<div class='col-lg-8 order-lg-1 rtl'><div class='container bg-gray-7  mb-md-0 border-width-2 border-color-1 productImageBackRadius'><h5 class='p-2'>محصولات سبد</h5><div id='DetailSection'></div></div></div>");
    var DetailSection = document.getElementById("DetailSection");
    Details.forEach(function (element) {
        var count = element.count;


        DetailSection.insertAdjacentHTML("beforeend", "<div class= 'row m-2  cart-table productImage' style = 'flex-direction: row !important;' ><div class='col-lg-4 order-lg-1 rtl' ><a onclick='DeleteDetail(" + element.id + ")' href='#' class='text-gray-32 font-size-26'><i class='fa-classic fa-solid fa-square-xmark fa-fw Red'></i></a><a href='#'><img class='img-fluid max-width-19 p-1 border border-color-1 productImageBackRadius' src='/Shared/Images/Products/" + element.image + "' alt='ALT'></a></div><div class='col-lg-6 order-lg-1 rtl'><a href='#' class='text-gray-90  font-size-17 font-weight-bold'>" + element.productName + "</a><br /><span class='sr-only mt-2'>Quantity</span><div class='mt-3 border rounded-pill py-1 width-122 w-xl-30 px-3 border-color-1'><div class='js-quantity row align-items-center'><div class='col-auto pr-1'><small class='font-weight-bold pr-3  pl-3 border-0'>" + element.count + "</small><a onclick='ChangeCount(" + element.id + ",1000)' class='js-minus btn btn-icon btn-xs btn-outline-secondary rounded-circle border-0' href='javascript:;'><small class='fas fa-minus btn-icon__inner'></small></a><a onclick='ChangeCount(" + element.id + ",1001)' class='js-plus btn btn-icon btn-xs btn-outline-secondary rounded-circle border-0' href='javascript:;'><small class='fas fa-plus btn-icon__inner'></small></a></div></div></div></div><div class='col-lg-2 order-lg-1 rtl'><span class='font-weight-bold text-red'>" + separate(element.discount) + "</span><br /><span class='font-weight-bold font-size-17'>" + separate(element.total) + " تومان</span></div></div > ");
    });
    DetailSection.insertAdjacentHTML("beforeend", "<tr><td colspan='6' class='border-top space-top-2 justify-content-center'><div class='pt-md-3'><div class='d-block d-md-flex flex-center-between'><div class='mb-3 mb-md-0 w-xl-50'><div class='input-group'><input type='text' class='form-control rounded-pill' id='couponCode' placeholder='کد تخفیف خود را وارد نمایید' aria-label='Coupon code' aria-describedby='subscribeButtonExample2' required><div><button  onclick='UseCoupon(" + Order.orderId + ")' class='btn btn-Custom m-2 btn-block BgOrange White px-4' type='button' ><i class='fas fa-tags d-md-none'></i><span class='d-none d-md-inline'>اعمال تخفیف</span></button></div></div><label class='sr-only' >Coupon code</label></div></div></div></td></tr>");
    var PayTypes = response.object.payTypes;
    DetailSection.insertAdjacentHTML("beforeend", "<h5 class=' mt-3'><i class=' fas fa-cash-register p-2 Blue'></i>روش های پرداخت</h5>");
    PayTypes.forEach(function (element) {
        if (response.object.orderPay != null) {
            var pay = response.object.orderPay;
            if (element.id == pay.id) {
                DetailSection.insertAdjacentHTML("beforeend", "<div onclick='SetOrderPay(" + element.id + ")'  class='pointer border-top border-width-3 border-color-1 pt-3 mb-5'><div'><div class='border-bottom border-color-1 border-dotted-bottom'><div class='p-3'><div ' class='custom-control custom-radio'><label class='pointer form-label font-size-16' for='" + element.id + "' data-toggle='collapse' data-target='#basicsCollapse" + element.id + "' aria-expanded='false' aria-controls='basicsCollapse" + element.id + "'> <i class=' fas fa-circle-check p-2 font-size-17 Green'></i>" + element.typeName + " <span class='Gray'>(" + element.description + ")</span><br /> <small class='Green font-size-19 font-weight-bold'>انتخاب شده</small></label></div></div>");
            } else {
                DetailSection.insertAdjacentHTML("beforeend", "<div onclick='SetOrderPay(" + element.id + ")'  class='pointer border-top border-width-3 border-color-1 pt-3 mb-3'><div><div class='border-bottom border-color-1 border-dotted-bottom'><div class='p-3' id='basicsHeading" + element.id + "'><div onclick='SetOrderPay(" + element.id + ")' class='custom-control custom-radio'><label class='pointer form-label font-size-16' for='" + element.id + "' data-toggle='collapse' data-target='#basicsCollapse" + element.id + "' aria-expanded='false' aria-controls='basicsCollapse" + element.id + "'> <i class='ml-2 fa-classic fa-regular fa-circle fa-fw'></i>" + element.typeName + "  <span class='Gray'>(" + element.description + ")</span></label></div></div>");

            }
        } else {
            DetailSection.insertAdjacentHTML("beforeend", "<div onclick='SetOrderPay(" + element.id + ")'  class='pointer border-top border-width-3 border-color-1 pt-3 mb-3'><div><div class='border-bottom border-color-1 border-dotted-bottom'><div class='p-3' id='basicsHeading" + element.id + "'><div onclick='SetOrderPay(" + element.id + ")' class='custom-control custom-radio'><label class='pointer form-label font-size-16' for='" + element.id + "' data-toggle='collapse' data-target='#basicsCollapse" + element.id + "' aria-expanded='false' aria-controls='basicsCollapse" + element.id + "'> <i class='ml-2 fa-classic fa-regular fa-circle fa-fw'></i>" + element.typeName + "  <span class='Gray'>(" + element.description + ")</span></label></div></div>");

        }
    });

}

function EmptyData() {
    DataSection.insertAdjacentHTML("beforeend", "<div class='alert alert-secondery border Gray font-weight-bold text-center font-size-17'><i class=' fas fa-basket-shopping'></i>هیچ محصولی در سبد خرید وجود ندارد</div>");
}


//Order Detail
function ChangeCount(detailId, count) {
    $.ajax({
        type: "POST",
        url: "/checkout?handler=ChangeDetail",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {"detailId": detailId, "count": count},
        success: function (response) {
            console.log(response);
            ToastSuccess(response.object.message);
            GetData()
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function DeleteDetail(detailId) {
    $.ajax({
        type: "POST",
        url: "/checkout?handler=Delete",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {"id": detailId},
        success: function (response) {
            console.log(response);
            ToastSuccess(response.message);

            GetData()
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}


//use Coupon
function UseCoupon(orderId) {
    //var couponCode = document.getElementById("couponCode");
    //use Cfunction ChangeCount(detailId,count) {
    $.ajax({
        type: "POST",
        url: "/checkout?handler=UseCoupon",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {"orderId": orderId, "code": $("#couponCode").val()},
        success: function (response) {
            console.log(response);
            ToastSuccess(response.object.message);
            GetData()
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function SetOrderPay(id) {
    console.log(id);
    Response.object.orderPay = Response.object.payTypes.find(obj => obj.id === id);
    //Response.object.orderPay = Response.object.payTypes[id -= 1];
    console.log(Response);

    DataSection.innerHTML = null;
    FillData(Response);
}

var CompleteInfo = document.getElementById("CompleteInfo");

var Data;
var ActiveTransaction;

function showComplete() {
    //var test = $("input[name=Pay" + 3 + "]").val();
    console.log(Response);
    //console.log(Data);
    CompleteInfo.innerHTML = null;
    $("#OrderComplete").modal('show');
    Data = Response.object;


    if (Data.orderPay.id == 1 || Data.orderPay.id == 5 || Data.orderPay.id == 6) {
        ShowComplete2(null);
    } else {
        $.ajax({
            type: "POST",
            url: "/checkout?handler=GetWallet",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());

            },
            data: {"userId": Data.order.userId, "type": Data.orderPay.id},
            success: function (response) {
                console.log(Data.orderPay.id);
                console.log(response);
                switch (Data.orderPay.id) {
                    case 2:
                        WalletPrice = response.object.amount;
                        ShowComplete2(WalletPrice);
                        break;
                    case 3:
                        WalletPrice = response.object.fecilities;
                        ActiveTransaction = response.object2;
                        ShowComplete2(WalletPrice);
                        break;
                    case 4:
                        console.log(response.object.orgCredit);
                        WalletPrice = response.object.orgCredit;
                        ActiveTransaction = response.object2;
                        ShowComplete2(WalletPrice);
                        break;
                    default:

                        break;
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


}

function ShowComplete2(WalletPrice) {
    CompleteInfo.insertAdjacentHTML("beforeend", "<h5 class='title'><i class='fab fa-first-order p-2 Blue'></i> <b>اطلاعات سفارش</b> </h5><div class='row'><div class='col-6'><div class='form-group' id='UserInfo'><label class=' control-label'>شناسه سفارش : <strong>" + Data.order.orderCode + "</strong></label></div></div><div class='col-6'><div class='form-group' id='UserInfo'><label class=' control-label'>تاریخ ثبت سفارش : <strong>" + Data.order.createDate + "</strong></label></div></div><div class='col-4'><div class='form-group' id='OrderShipping'><label class=' control-label'>مبلغ قابل پرداخت : <strong>" + separate(Data.order.total) + " تومان</strong></label></div></div></div><hr /><h5 class='title'><i class=' far fa-address-card Orange m-1'></i> <b>اطلاعات خریدار</b> </h5>")


    if (Data.orderShipping.firstName != null && Data.orderShipping.lastName != null) {
        CompleteInfo.insertAdjacentHTML("beforeend", "<div class='row'><div class='col-6'><label class=' control-label'>نام خریدار : <strong>" + Data.costumer.firstName + "" + Data.costumer.lastName + "</strong></label></div><div class='col-6'><label class=' control-label'>شماره موبایل : <strong>" + Data.costumer.mobile + "</strong></label></div>")
        CompleteInfo.insertAdjacentHTML("beforeend", "<div class='row'><div class='col-6'><label class=' control-label'>تحویل گیرنده : <strong>" + Data.orderShipping.firstName + "" + Data.orderShipping.lastName + "</strong></label><br><label class=' control-label'>کد پستی : <strong>" + Data.orderShipping.postCode + "</strong></label></div><div class='col-6'><label class=' control-label'>شماره موبایل : <strong>" + Data.orderShipping.mobile + "</strong></label><br><label class=' control-label'>شماره تماس : <strong>" + Data.orderShipping.phone + "</strong></label></div><div class='col-12'><label class=' control-label'>آدرس : <strong>" + Data.orderShipping.fullAddress + "</strong></label></div></div>")
    } else {
        CompleteInfo.insertAdjacentHTML("beforeend", "<div class='col-6'><label class=' control-label'><small class='Red font-weight-bold'> انتخاب نشده است</small><strong></strong></label></dv>")

    }

    console.log(Data.orderPay);
    if (Data.orderPay != null) {

        CompleteInfo.insertAdjacentHTML("beforeend", "<hr /><h5 class='title'> <i class='fas fa-credit-card p-2 Green'></i> <b>روش پرداخت</b> </h5></div><div class='row'><div class='col-12'><div class='form-group'><label class=' control-label'>نحوه پرداخت صورتحساب : <strong>" + Data.orderPay.typeName + "(<small>" + Data.orderPay.description + "</small>)</strong></label><br/></div></div>")
        console.log(Data.orderPay);
        if (Data.orderPay.id != 1 && Data.orderPay.id != 5 && Data.orderPay.id != 6) {
            CompleteInfo.insertAdjacentHTML("beforeend", "<span>موجودی حساب شما: <strong class='Orange'>" + separate(WalletPrice) + "تومان</strong></span>")
        }


        if (ActiveTransaction != null) {
            switch (ActiveTransaction.type) {
                case "Fecilities":
                    CompleteInfo.insertAdjacentHTML("beforeend", "<hr /><span class='mt-2  font-weight-bold'>مدت زمان بازپرداخت</span><select id='month' class='form-control'><option value=" + ActiveTransaction.month + ">" + ActiveTransaction.month + " ماهه</option></select>")
                    CompleteInfo.insertAdjacentHTML("beforeend", "<hr /><span class='badge bg-red m-2 font-weight-bold'>تنها یکبار امکان استفاده از تسهیلات  وجود دارد.پس از ثبت سفارش موجودی تسهیلات شما صفر خواهد شد</span>")

                    break;

                case "OrgCredit":
                    CompleteInfo.insertAdjacentHTML("beforeend", "<hr /><span class='mt-2 font-weight-bold'>مدت زمان بازپرداخت</span><select id='month' class='form-control'><option value=6>6 ماهه</option><option value=12>12 ماهه</option><option value=18>18 ماهه</option><option value=24>24 ماهه</option><option value=30>30 ماهه</option><option value=36>36 ماهه</option><option value=42>42 ماهه</option><option value=48>48 ماهه</option><option value=54>54 ماهه</option><option value=60>60 ماهه</option></select>")
                    CompleteInfo.insertAdjacentHTML("beforeend", "<hr /><span class='badge bg-warning m-2 font-weight-bold'>در صورتی که تا دهم ماه جاری سفارش  ثبت گردد.کسر از حقوق همکاران از 25 ام ماه جاری فعال خواهد شد</span>")

                    break;
            }
        }
    } else {
        CompleteInfo.insertAdjacentHTML("beforeend", "<hr /><h5 class='title'> <i class='fas fa-credit-card p-2 Green'></i> <b>روش پرداخت</b> </h5></div><div class='row'><div class='col-12'><div class='form-group'><label class=' control-label'>نحوه پرداخت صورتحساب : <small class='Red font-weight-bold'> انتخاب نشده است</small></strong></label></div></div>")

    }

    if (Data.orderCoupon.couponCode != null) {
        CompleteInfo.insertAdjacentHTML("beforeend", "<div class='col-12'><label class=' control-label'>کد تخفیف استفاده شده : <strong>" + Data.orderCoupon.couponCode + "</strong></label></div><div class='col-12'><label class=' control-label'>نحوه اعمال تخفیف : <strong>" + Data.orderCoupon.couponType + "</strong></label></div><div class='col-12'><label class=' control-label'>میزان مبلغ تخفیف : <strong>" + separate(Data.orderCoupon.discountPrice) + " تومان</strong></label></div>")
    }


    switch (Data.orderPay.id) {
        case 1:
            CompleteInfo.insertAdjacentHTML("beforeend", "<button type='button' class='btn BgGreen White btn-block btn-round' onclick='SubmitOrder()'>تائید اطلاعات و پرداخت</button>")

            break;
        case 2:
            CompleteInfo.insertAdjacentHTML("beforeend", "<button type='button' class='btn BgGreen White btn-block btn-round' onclick='SubmitOrder()'>تائید اطلاعات و پرداخت</button>")

            break;
        case 3:
            if (ActiveTransaction != null) {
                CompleteInfo.insertAdjacentHTML("beforeend", "<button type='button' class='pointer btn BgGreen White btn-block btn-round' onclick='CalculateAghsat()'>ثبت درخواست استفاده از تسهیلات</button>")
            } else {
                CompleteInfo.insertAdjacentHTML("beforeend", "<button type='button' class='btn BgRed White btn-block btn-round'>تسهیلات فعالی برای شما در سیستم ثبت نشده است</button>")

            }
            break;
        case 4:
            if (ActiveTransaction != null) {
                CompleteInfo.insertAdjacentHTML("beforeend", "<button type='button' class='pointer btn BgGreen White btn-block btn-round' onclick='CalculateAghsat()'>ثبت درخواست استفاده از تسهیلات</button>")
            } else {
                CompleteInfo.insertAdjacentHTML("beforeend", "<button type='button' class='pointer btn BgRed White btn-block btn-round'>تسهیلات فعالی برای شما در سیستم ثبت نشده است</button>")

            }
            break;
        case 5:
            CompleteInfo.insertAdjacentHTML("beforeend", "<button type='button' class='btn BgGreen White btn-block btn-round' onclick='SubmitOrder()'>تائید اطلاعات و پرداخت</button>")

            break;
        case 6:
            CompleteInfo.insertAdjacentHTML("beforeend", "<button type='button' class='btn BgGreen White btn-block btn-round' onclick='SubmitOrder()'>تائید اطلاعات و پرداخت</button>")

            break;
    }

    CompleteInfo.insertAdjacentHTML("beforeend", "</div>")

}


function closeModal() {
    $("#OrderComplete").modal('hide');
    $("#Shippings").modal('hide');

}

function closeAghsatModal() {
    $("#AghsatComplete").modal('hide');

}

function ShowShippingModal() {

    var Data = Response.object.userShippingList;
    var ShippingList = document.getElementById("ShippingList");

    ShippingList.innerHTML = null;
    Data.forEach(function (element) {
        var index = Data.indexOf(element);
        ShippingList.insertAdjacentHTML("beforeend", "<div class='card'><div class='card-body'><h6> <i class='ec ec-transport p-2 Orange'></i>تحویل گیرنده : " + element.firstName + "" + element.lastName + "</h6><p>آدرس: " + element.country + " " + element.state + " " + element.city + " " + element.address + "</p><button onclick='SetShipping(" + element.id + ")' type='button' class='btn btn-xs  BgGreen White font-weight-bold'>انتخاب این آدرس</button></div></div>");
    });
    $("#Shippings").modal('show');
}

function SetShipping(id) {
    //var orderShipping = Response.object.orderShipping;
    //var userShipping = Response.object.userShippingList[id];
    //orderShipping.firstName = userShipping.firstName;
    //orderShipping.lastName = userShipping.lastName;
    //orderShipping.fullAddress = userShipping.country + " " + userShipping.state + " " + userShipping.city + " " + userShipping.address;
    //orderShipping.id = userShipping.id;
    //orderShipping.mobile = userShipping.mobile;
    //orderShipping.orderId = 0;
    //orderShipping.phone = userShipping.phone;
    //orderShipping.postCode = userShipping.postCode;
    //DataSection.innerHTML = null;
    $.ajax({
        type: "POST",
        url: "/checkout?handler=ChangeShipping",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());

        },
        data: {"userShippingId": id, "orderId": Response.object.order.orderId},
        success: function (response) {
            console.log(response);
            ToastSuccess(response.message);
            $("#Shippings").modal('hide');
            DataSection.innerHTML = null;
            GetData();
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}


function CalculateAghsat() {
    let month = document.getElementById("month");


    let walletAmount = 0;
    let price = 0;
    console.log(ActiveTransaction);

    if (WalletPrice < Response.object.order.total) {
        price = WalletPrice;
    } else {
        price = Response.object.order.total;
    }

    $.ajax({
        type: "POST",
        url: "/checkout?handler=CalculateAghsat",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());

        },
        data: {
            "price": price,
            "transactionId": ActiveTransaction.id,
            "month": month.value,
        },
        success: function (response) {
            ShowAghsat(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function ShowAghsat(response) {
    var AghsatSection = document.getElementById("AghsatSection");
    AghsatSection.innerHTML = null;
    console.log(response);
    if (!response.isSuccessed) {
        ToastError(response.message);
    } else {

        ToastSuccess(response.message);
        var object = response.object;

        AghsatSection.insertAdjacentHTML("beforeend", "<h5 class='title'><i class='fab fa-first-order p-2 Blue'></i> <b>اطلاعات سفارش</b> </h5><div class='row'><div class='col-12'><span class='badge bg-red m-2 font-weight-bold w-100 p-1'>اقساط قابل پرداخت سفارش شما محاسبه گردید.<br/>لطفا با دقت آن را بررسی کرده و در صورت اطمینان سفارش خود را تائید فرمایید</span></div><div class='col-6'><div class='form-group' ><label class=' control-label'>شناسه سفارش : <strong>" + Data.order.orderCode + "</strong></label></div></div><div class='col-6'><div class='form-group' ><label class=' control-label'>تاریخ ثبت سفارش : <strong>" + Data.order.createDate + "</strong></label></div></div><div class='col-6'><div class='form-group' ><label class=' control-label'>مبلغ قابل پرداخت : <strong>" + separate(Data.order.total) + " تومان</strong></label></div></div><div class='col-6'><div class='form-group' ><label class=' control-label'>میزان تسهیلات شما : <strong>" + separate(WalletPrice) + " تومان</strong></label></div></div><div class='col-6'><div class='form-group' ><label class=' control-label'>مدت زمان بازپرداخت : <strong>" + object.ghestCount + " ماهه</strong></label></div></div><div class='col-6'><div class='form-group' ><label class=' control-label'>مبلغ هر قسط : <strong>" + separate(object.ghest) + " تومان</strong></label></div></div><div class='col-6'><div class='form-group' ><label class=' control-label'>مبلغ کارمزد : <strong>" + separate(object.karmozd) + " تومان</strong></label></div></div><div class='col-6'><div class='form-group' ><label class=' control-label'>مجموع اقساط : <strong>" + separate(object.totalPrice) + " تومان</strong></label></div></div><div class='col-12'><button type='button' class='btn bgGreen text-white btn-block' onclick='SubmitOrder()'>تائید و پرداخت سفارش</button></div></div>")
        $("#AghsatComplete").modal('show');
    }
}


function SubmitOrder() {
    //window.alert(ActiveTransaction.id);
    let month = document.getElementById("month");
    let tId = 0;
    let mon;
    if (ActiveTransaction != null) {
        tId = ActiveTransaction.id;
    }
    if (month != undefined) {
        mon = month.value;
    }


    $.ajax({
        type: "POST",
        url: "/checkout?handler=Complete",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());

        },
        data: {
            "orderId": Response.object.order.orderId,
            "orderShippingId": Response.object.orderShipping.id,
            "payTypeId": Response.object.orderPay.id,
            "transactionId": tId,
            "month": mon,
        },
        success: function (response) {
            CheckComplete(response);
        },
        failure: function (response) {
            ToastError("در حال حاضر امکان انتقال به درگاه وجود ندارد");
        },
        error: function (response) {
            ToastError("انتقال به درگاه پرداخت با خطا روبه رو شده است");
        }
    });
}

function CheckComplete(response) {
    console.log(response);
    if (response.object != null) {
        ToastSuccess(response.message);
        setTimeout(RedirectToDargah(response.object), 3000);
    } else if (response.isSuccessed) {
        swal({
            title: response.message,
            type: 'question',
            showCancelButton: true,
            confirmButtonColor: '#1bd92f',
            cancelButtonColor: '#777',
            confirmButtonText: 'مشاهده سفارشات',
            cancelButtonText: 'یستن'
        }).then(function () {
            window.location.href = "/Panel/Orders";
        }, function (dismiss) {
            if (dismiss === 'cancel') {
                window.location.href = "/Checkout";
            }
        }).catch(swal.noop);
    } else {
        ToastError(response.message);
    }
}

function RedirectComplete() {
    window.location.href = "/Panel/Orders";
}

function RedirectToDargah(object) {
    window.location.href = object;
}

