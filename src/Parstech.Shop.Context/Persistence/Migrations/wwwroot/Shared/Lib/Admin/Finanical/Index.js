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
        // console.log(element);
        if (element.isBlock) {
            const data =
                [
                    "<h5 style='width: 160px;'>" + element.userName + " <h5 class='Red font-weight-bold'>مسدود</h5><br /><small  class='Gray font-weight-bold'>" + element.userName + "</small></h5>",
                    "<h5  class='font-weight-bold'>" + element.userName + "</h5>",
                    "<h5 class='font-weight-bold'>" + element.coin + "</h5>",
                    "<h5 class=' font-weight-bold'>" + separate(element.amount) + "</h5>",
                    "<h5 class=' font-weight-bold'>" + separate(element.orgCredit) + "</h5>",
                    "<h5 class='Blue font-weight-bold'>" + separate(element.fecilities) + "</h5>",

                    "<div style='width: 260px;' class='btn-group'><button onclick='GetTransaction(" + element.walletId + ")' type='button' class='btn btn-default'><i class='fas fa-circle-info Green m-1'></i>کیف پول</button><button onclick='GetSazman(" + element.walletId + ")' type='button' class='btn btn-default'><i class='fas fa-circle-info Red m-1'></i>اعتبار سازمانی</button><button onclick='GetAghsat(" + element.walletId + ")' type='button' class='btn btn-default'><i class='fas fa-circle-info Blue m-1'></i>تسهیلات</button><div class='btn-group'><button type='button' class='btn btn-default dropdown-toggle' data-bs-toggle='dropdown' aria-expanded='true'><i class='icon-options'></i><i class='icon-arrow-down'></i></button><ul class='dropdown-menu' style=''><li><button class='btn' onclick='showFecilitiesModal(" + element.walletId + ")'> <i class=' far fa-credit-card Blue'></i> ثبت تسهیلات </button></li><li><button onclick='BlockWalletSubmit(" + element.walletId + ")' class='btn'><i class=' fas fa-square-minus Red'></i> انسداد حساب </button></li><li><button onclick='UnblockWalletSubmit(" + element.walletId + ")' class='btn'><i class=' fas fa-square-check Green'></i> رفع مسدودیت </button></li></ul></div></div>"
                ];
            dataSet.push(data);
        }
        else {
            const data =
                [
                    "<h5 style='width: 160px;'>" + element.fullName + " <small  class='Green font-weight-bold'>مجاز</small><br /><small  class='Gray font-weight-bold'>" + element.userName + "</small></h5>",
                    
                    "<h5  class='font-weight-bold'>" + element.coin + "</h5>",
                    "<h5 s class=' font-weight-bold'>" + separate(element.amount) + "</h5>",
                    "<h5  class=' font-weight-bold'>" + separate(element.orgCredit) + "</h5>",
                    "<h5  class='Blue font-weight-bold'>" + separate(element.fecilities) + "</h5>",

                    "<div  class='btn-group'><button onclick='GetTransaction(" + element.walletId + ")' type='button' class='btn btn-default'><i class='fas fa-circle-info Green m-1'></i>کیف پول</button><button onclick='GetSazman(" + element.walletId + ")' type='button' class='btn btn-default'><i class='fas fa-circle-info Red m-1'></i>اعتبار سازمانی</button><button onclick='GetAghsat(" + element.walletId + ")' type='button' class='btn btn-default'><i class='fas fa-circle-info Blue m-1'></i>تسهیلات</button><div class='btn-group'><button type='button' class='btn btn-default dropdown-toggle' data-bs-toggle='dropdown' aria-expanded='true'><i class='icon-options'></i><i class='icon-arrow-down'></i></button><ul class='dropdown-menu' style=''><li><button class='btn' onclick='showFecilitiesModal(" + element.walletId + ")'> <i class=' far fa-credit-card Blue'></i> ثبت تسهیلات </button></li><li><button onclick='BlockWalletSubmit(" + element.walletId + ")' class='btn'><i class=' fas fa-square-minus Red'></i> انسداد حساب </button></li><li><button onclick='UnblockWalletSubmit(" + element.walletId + ")' class='btn'><i class=' fas fa-square-check Green'></i> رفع مسدودیت </button></li></ul></div></div>"
                ];
            dataSet.push(data);
        }
        //<button onclick='GetCoins(" + element.walletId + ")' type='button' class='btn btn-default'><i class=' fas fa-coins Orange m-1'></i>امتیازات</button>
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
            { title: 'کاربر' },
            
            { title: 'امتیاز' },
            { title: 'مانده کیف پول(تومان)' },
            { title: 'اعتبار سازمانی(تومان)' },
            { title: 'تسهیلات(تومان)' },
            //{ title: 'وضعیت' },
            { title: 'عملیات' },
        ],
        cellAttributes: { alignment: 'center' },
        columnDefs: [
            {
                targets: 5,
                className: 'tdWidth'
            }
        ],

        "pageLength": 25
    });
    tableMain.destroy();


}

function OnErrorData() {

}


function IdForInputs(id) {
    var IdList = document.querySelectorAll('.walletId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}

function GetCategurySubmit(id) {
    IdForInputs(id);
    $("#GetWalletForm").submit();
}

//دریافت محصول

var Block = document.getElementById("Block");


function CleanItem() {


}
function FillItem(element) {

}

function OnLoadingGetItem() {
    //CleanProduct();
}
function OnCompleteGetItem(xhr) {
    // console.log(xhr);
    FillItem(xhr.responseJSON.object);
    $("#AddOrEditCateguryModal").modal("show");
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
        $("#AddOrEditCateguryModal").modal("hide");

        tableMain
            .clear()
            .draw();
        dataSet = [];
        $("#GetDataForm").submit();

        ToastSuccess("عملیات با موفقیت انجام شد")
    }

}


function OnCompleteFecilities(xhr) {

}




function OnLoadingBlockOrUnblock() {
    CleanItem()
}

function OnCompleteBlockOrUnblock(xhr) {


    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        if (dataSet.length > 0) {
            tableMain
                .clear()
                .draw();
            dataSet = [];
        }
        $("#GetDataForm").submit();

        ToastSuccess("عملیات با موفقیت انجام شد")
    }

}

function OnErrorBlockOrUnblock() {

}

function BlockWalletSubmit(id) {
    IdForInputs(id);
    Block.value = true;
    swal({
        title: 'آیا از مسدود کردن این کیف پول مطمئن هستید؟',
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: 'بله، مسدود شود. '
    }).then(function () {
        $("#BlockOrUnblockForm").submit();
    }, function (dismiss) {
        if (dismiss === 'cancel') {
            swal(
                'لغو گردید',
                'کیف پول همچنان مجاز به استفاده است.',
                'error'
            ).catch(swal.noop);
        }
    }).catch(swal.noop);

}

function UnblockWalletSubmit(id) {
    IdForInputs(id);
    Block.value = false;
    swal({
        title: 'آیا از رفع مسدودیت این کیف پول مطمئن هستید؟',
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: 'بله، رفع مسدودیت شود. '
    }).then(function () {
        $("#BlockOrUnblockForm").submit();
    }, function (dismiss) {
        if (dismiss === 'cancel') {
            swal(
                'لغو گردید',
                'کیف پول همچنان مسدود است.',
                'error'
            ).catch(swal.noop);
        }
    }).catch(swal.noop);

}



function OnCompleteRegister(xhr) {
    var ErrorRegistrationSection = document.getElementById("ErrorRegistrationSection");
    ErrorRegistrationSection.innerHTML = null;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        if (xhr.responseJSON.isSuccessed) {
            ToastSuccess("ثبت تسهیلات با موفقیت موفقیت انجام شد")
        }
        else {
            ToastError("اطلاعات وارد شده با خطا بارگذاری شده اند")
        }
        xhr.responseJSON.object.forEach(function (element) {
            ErrorRegistrationSection.insertAdjacentHTML("beforeend", "<span class='badge BgRed w-100 font-weight-bold'><h5>" + element.caption + "</h5><h5>" + element.errorMessage + "</h5></span>");
        });

    }
}

function OnCompletePayment(xhr) {
    var ErrorPaymentSection = document.getElementById("ErrorPaymentSection");
    ErrorPaymentSection.innerHTML = null;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        if (xhr.responseJSON.isSuccessed) {
            ToastSuccess("ثبت تسویه اقساط با موفقیت موفقیت انجام شد")
        }
        else {
            ToastError("اطلاعات وارد شده با خطا بارگذاری شده اند")
        }
        xhr.responseJSON.object.forEach(function (element) {
            ErrorPaymentSection.insertAdjacentHTML("beforeend", "<span class='badge BgRed w-100 font-weight-bold'><h5>" + element.caption + "</h5><h5>" + element.errorMessage + "</h5></span>");
        });

    }
}