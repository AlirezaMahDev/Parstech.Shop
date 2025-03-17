var dataSet = [];
var tableMain;
var FilterInput = document.getElementById("FilterInput");
var Parameter_Filter = document.getElementById("Parameter_Filter");
var paging = document.getElementById("paging");
var pagingHeader = document.getElementById("pagingHeader");

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

function FillDataSet(Data) {
    Data.list.forEach(function (element) {
        //console.log(element);

        const data =
            [
                "<h5 class='font-weight-bold'>" + element.code + "</h5>",
                "<h5 class=''>" + element.amount + "</h5>",
                "<h5 class=''>" + element.couponTypeName + "</h5>",
                "<h5 class=''>" + element.expireDateShamsi + "</h5>",
                "<button onclick='GetCouponSubmit(" + element.id + ")' class='btn btn-default'><i class=' fas fa-pencil Yellow m-1'></i>ویرایش </button><button onclick='DeleteCouponSubmit(" + element.id + "," + element.code + ")' class='btn btn-default'><i class='far fa-circle-xmark Red m-1'></i>حذف</button>",
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
            {title: 'کد کوپن'},
            {title: 'ارزش'},
            {title: 'تایپ'},
            {title: 'تاریخ انقضا'},
            {title: 'عملیات'},
        ],
        "columnDefs": [{
            "targets": 4,
            "className": 'w-30',
        }],
        "pageLength": 25
    });
    tableMain.destroy();


}

function OnErrorData() {

}

function IdForInputs(id) {
    var IdList = document.querySelectorAll('.CouponId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}

var couponDto_Id = document.getElementById("couponDto_Id");
var couponDto_Code = document.getElementById("couponDto_Code");
var couponDto_Amount = document.getElementById("couponDto_Amount");
var couponDto_Persent = document.getElementById("couponDto_Persent");
var couponDto_Type = document.getElementById("couponDto_Type");
var couponDto_ExpireDateShamsi = document.getElementById("couponDto_ExpireDateShamsi");
var couponDto_MinPrice = document.getElementById("couponDto_MinPrice");
var couponDto_MaxPrice = document.getElementById("couponDto_MaxPrice");
var couponDto_JustNewUser = document.getElementById("couponDto_JustNewUser");
var couponDto_TwoUseSameTime = document.getElementById("couponDto_TwoUseSameTime");
var couponDto_LimitUse = document.getElementById("couponDto_LimitUse");
var couponDto_LimitEachUser = document.getElementById("couponDto_LimitEachUser");
var couponDto_Categury = document.getElementById("couponDto_Categury");

var couponDto_Products = document.getElementById("couponDto_Products");
var couponDto_Users = document.getElementById("couponDto_Users");

function CleanItem() {
    console.log("ghhggh");
    couponDto_Id.value = null;
    couponDto_Code.value = null;
    couponDto_Amount.value = null;
    couponDto_Persent.value = null;
    couponDto_ExpireDateShamsi.value = null;
    couponDto_MinPrice.value = null;
    couponDto_MaxPrice.value = null;
    couponDto_JustNewUser.value = null;
    couponDto_TwoUseSameTime.value = null;
    couponDto_LimitUse.value = null;
    couponDto_LimitEachUser.value = null;
}

function FillItem(element) {
    //  console.log(element);
    couponDto_Id.value = element.id;
    couponDto_Code.value = element.code;
    couponDto_Amount.value = element.amount;
    couponDto_Persent.value = element.persent;
    couponDto_Type.value = element.couponTypeId;
    couponDto_ExpireDateShamsi.value = element.expireDateShamsi;
    couponDto_MinPrice.value = element.minPrice
    couponDto_MaxPrice.value = element.maxPrice;
    couponDto_JustNewUser.value = element.justNewUser;
    couponDto_TwoUseSameTime.value = element.twoUseSameTime;
    couponDto_LimitUse.value = element.limitUse;
    couponDto_LimitEachUser.value = element.limitEachUser;
    couponDto_Categury.value = element.categury;
    couponDto_Products.value = element.products;
    couponDto_Users.value = element.users;
}

function OnLoadingGetItem() {
    CleanItem();
}

function OnCompleteGetItem(xhr) {
    // console.log(xhr);
    CleanItem();
    FillItem(xhr.responseJSON.object);
    $("#EditAndDeleteModal").modal("show");
}

function OnErrorGetItem() {

}

function GetCouponSubmit(id) {
    IdForInputs(id);
    $("#GetCoupon").submit();
}

function DeleteCouponSubmit(id, code) {
    IdForInputs(id);


    swal({
        title: '  آیا از حذف کوپن ' + code + ' مطمئن هستید؟',
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: 'بله، حذف شود. '
    }).then(function () {
        $("#DeleteCoupon").submit();
    }, function (dismiss) {
        if (dismiss === 'cancel') {
            swal(
                'لغو گردید',
                'کوپن شما همچنان وجود دارد.',
                'error'
            ).catch(swal.noop);
        }
    }).catch(swal.noop);


}

function OnLoadingUpdate() {
    CleanItem();
}

function OnCompleteUpdate(xhr) {
    // console.log(xhr);

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    } else {
        FillItem(xhr.responseJSON.object);
        $("#EditAndDeleteModal").modal("hide");

        tableMain
            .clear()
            .draw();
        dataSet = [];
        $("#GetDataForm").submit();

        if (!xhr.responseJSON.isSuccessed) {
            xhr.responseJSON.errors.forEach(function (element) {
                ToastError(element.errorMessage)
            });
        } else {
            ToastSuccess("عملیات با موفقیت انجام شد")
        }
    }

}

function OnErrorUpdate() {

}

function OnLoadingDelete() {
    CleanItem();
}

function OnCompleteDelete(xhr) {
    // console.log(xhr);
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    } else {
        FillItem(xhr.responseJSON.object);

        tableMain
            .clear()
            .draw();
        dataSet = [];
        $("#GetDataForm").submit();


        if (!xhr.responseJSON.isSuccessed) {
            ToastError(xhr.responseJSON.message)
        } else {
            ToastSuccess(xhr.responseJSON.message)
        }

    }

}

function OnErrorDelete() {

}

function CouponDeleteConfirm() {

}