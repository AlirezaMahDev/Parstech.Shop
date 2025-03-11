var TdataSet = [];
var TtableMain;
var TFilterInput = document.getElementById("FilterInput");
var WTParameter_Filter = document.getElementById("WTParameter.Filter");
var walletType = document.getElementById("walletType");

var WTpaging = document.getElementById("WTpaging");
var WTpagingHeader = document.getElementById("WTpagingHeader");


function GetTransaction(id) {
    IdForInputs(id);
    walletType.value = "Amount";
    WTParameter_CurrentPage.value = 1;
    $("#DataTransactionsForm").submit();
}
function GetAghsat(id) {
    IdForInputs(id);
    walletType.value = "Fecilities";
    WTParameter_CurrentPage.value = 1;
    $("#DataTransactionsForm").submit();
}
function GetSazman(id) {
    IdForInputs(id);
    walletType.value = "OrgCredit";
    WTParameter_CurrentPage.value = 1;
    $("#DataTransactionsForm").submit();
}
function GetCoins(id) {
    IdForInputs(id);
    walletType.value = "Coin";
    WTParameter_CurrentPage.value = 1;
    $("#DataTransactionsForm").submit();
}




function TFilter() {

    TtableMain
        .clear()
        .draw();
    TdataSet = [];

    WTParameter_Filter.value = TFilterInput.value;
    WTParameter_CurrentPage.value = 1;
    $("#DataTransactionsForm").submit();
}


function TFillDataSet(Data) {
    Data.list.forEach(function (element) {
        // console.log(element);
        const data =
            [
                "<h5 class=''>" + separate(element.price) + "</h5>",
                "<h5 class='font-weight-bold'>" + element.typeName + "</h5>",
                "<h5 class=' font-weight-bold'>" + element.createDateShamsi + "</h5>",
                "<h5 class='Blue'>" + element.expireDateShamsi + "</h5>",
                "<h5 class='Green font-weight-bold'>" + element.trackingCode + "</h5>",
                "<div class='btn-group'><button onclick='TransactionDetail(" + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-circle-info Blue m-1'></i>جزئیات</button></div></div>"
            ];
        TdataSet.push(data);
    });
    var WTfirst = 1;
    var WTcurrent = Data.currentPage;
    var WTnext = Data.currentPage + 1;
    var WTprivious = Data.currentPage - 1;
    var WTlast = Data.pageCount + 1;

    if (WTnext > WTlast) {
        WTnext = Data.currentPage;
    }
    if (WTprivious < 1) {
        WTprivious = Data.currentPage;
    }

    // console.log("first" + WTfirst);
    // console.log("perivous" + WTprivious);
    // console.log("next" + WTnext);
    // console.log("last" + WTlast);
    WTpaging.innerHTML = null;
    WTpagingHeader.innerHTML = null;
    WTpagingHeader.insertAdjacentHTML("beforeend", "<h6 class='m-2'>صفحه " + WTcurrent + " از " + WTlast + "</h5>");
    WTpaging.insertAdjacentHTML("beforeend", "<button onclick='TRunPaging(" + WTfirst + ")' class='btn cart-Blue btn-xs font-weight-bold ml-2'>اولین صفحه<i class=' fas fa-circle-arrow-right p-1'></i></button><button onclick='TRunPaging(" + WTprivious + ")' class='btn cart-Green btn-xs font-weight-bold ml-2'><i class=' fas fa-arrow-right p-1'></i></button><button onclick='TRunPaging(" + WTnext + ")' class='btn cart-Green btn-xs font-weight-bold ml-2'><i class=' fas fa-arrow-left p-1'></i></button><button onclick='TRunPaging(" + WTlast + ")' class='btn cart-Blue btn-xs font-weight-bold ml-2'><i class=' fas fa-circle-arrow-left p-1'></i>آخرین صفحه</button> <h6 class='m-2'>صفحه " + WTcurrent + " از " + WTlast + "</h5>");

}

function TRunPaging(page) {
    TtableMain
        .clear()
        .draw();
    TdataSet = [];

    WTParameter_CurrentPage.value = page;
    $("#DataTransactionsForm").submit();
}

function OnLoadingDataTransactions() {

    if (TdataSet.length > 0) {
        TtableMain
            .clear()
            .draw();
        TdataSet = [];
    }
}

function OnCompleteDataTransactions(xhr) {

    // console.log(xhr);
    var Data = xhr.responseJSON.object;
    TFillDataSet(Data);
    TtableMain = $('#Tdata-table').DataTable({
        "searching": false,
        "paging": false,
        data: TdataSet,
        columns: [
            { title: 'مبلغ' },
            { title: 'نوع تراکنش' },
            { title: 'تاریخ ایجاد' },
            { title: 'مهلت پرداخت' },
            { title: 'کد پیگیری' },
            { title: 'عملیات' },
        ],
        "columnDefs": [{
            "targets": 5,
            "className": 'w-30',
        }],
        "pageLength": 25
    });
    TtableMain.destroy();
    $("#TransactionModal").modal('show');

}

function OnErrorDataTransactions() {

}

var TransactionDto = document.getElementById("TransactionDto");
var TransactionId = document.getElementById("transactionId");

function CleanItem() {

    TransactionDto.innerHTML = null;
    TransactionId.innerHTML = null;

}

function FillItem(Data) {
    console.log(Data);
    if (Data.typeId == 6) {
        TransactionDto.insertAdjacentHTML("afterbegin", "<div class='row'><div class='col-6  m-auto m-b-10'><label><i class=' fas fa-money-bill-wave Orange'></i> مبلغ: </label><div class='form-group round m-auto'><label class='control-label font-weight-bold'> " + Data.price + " </label></div></div> <div class='col-6  m-auto m-b-10'><label><i class=' fas fa-stream Orange'></i> نوع:</label><div class='form-group round m-auto'><label class='control-label font-weight-bold'>" + Data.typeName + "</label></div></div> <div class='col-6  m-auto m-b-10'><label><i class=' icon-magnifier Orange'></i> کد پیگیری: </label><div class='form-group round m-auto'><label class='control-label font-weight-bold'>" + Data.trackingCode + "</label></div></div><div class='col-6  m-auto m-b-10'><label><i class=' fas fa-calendar-plus Orange'></i> تاریخ ایجاد: </label><div class='form-group round m-auto'><label class='control-label font-weight-bold'>" + Data.createDateShamsi + "</label></div></div><div class='col-6  m-auto m-b-10'><label><i class=' fas fa-calendar-xmark Orange'></i> تاریخ پایان: </label><div class='form-group round m-auto'><label class='control-label font-weight-bold'>" + Data.expireDateShamsi + "</label></div></div><div class='col-6  m-auto m-b-10'><label><i class=' fas fa-calendar-xmark Orange'></i> درصد: </label><div class='form-group round m-auto'><label class='control-label font-weight-bold'>" + Data.persent + "</label></div></div><div class='col-6  m-auto m-b-10'><label><i class=' fas fa-calendar-xmark Orange'></i> تعداد ماه: </label><div class='form-group round m-auto'><label class='control-label font-weight-bold'>" + Data.month + "</label></div></div><div class='col-6  m-auto m-b-10'><label><i class=' fas fa-calendar-xmark Orange'></i> وضعیت شروع اقساط: </label><div class='form-group round m-auto'><label class='control-label font-weight-bold'>" + Data.startName + "</label></div></div><div class='col-12  m-auto m-b-10'><label><i class=' far fa-file-lines Orange'></i> توضیحات: </label><div class='form-group round m-auto'><label class='control-label font-weight-bold'>" + Data.description + "</label></div></div><div class='col-12  m-auto m-b-10'><a href='" + Data.fileName + "'><i class='fas fa-download Blue'></i> دانلود </a><a class='btn btn-blue' href='#' onclick='TasviyeGhest("+Data.id+")'>تسویه </a></div></div>");

    }
    else {
        TransactionDto.insertAdjacentHTML("afterbegin", "<div class='row'><div class='col-6  m-auto m-b-10'><label><i class=' fas fa-money-bill-wave Orange'></i> مبلغ: </label><div class='form-group round m-auto'><label class='control-label font-weight-bold'> " + Data.price + " </label></div></div> <div class='col-6  m-auto m-b-10'><label><i class=' fas fa-stream Orange'></i> نوع:</label><div class='form-group round m-auto'><label class='control-label font-weight-bold'>" + Data.typeName + "</label></div></div> <div class='col-6  m-auto m-b-10'><label><i class=' icon-magnifier Orange'></i> کد پیگیری: </label><div class='form-group round m-auto'><label class='control-label font-weight-bold'>" + Data.trackingCode + "</label></div></div><div class='col-6  m-auto m-b-10'><label><i class=' fas fa-calendar-plus Orange'></i> تاریخ ایجاد: </label><div class='form-group round m-auto'><label class='control-label font-weight-bold'>" + Data.createDateShamsi + "</label></div></div><div class='col-6  m-auto m-b-10'><label><i class=' fas fa-calendar-xmark Orange'></i> تاریخ پایان: </label><div class='form-group round m-auto'><label class='control-label font-weight-bold'>" + Data.expireDateShamsi + "</label></div></div><div class='col-6  m-auto m-b-10'><label><i class=' fas fa-calendar-xmark Orange'></i> درصد: </label><div class='form-group round m-auto'><label class='control-label font-weight-bold'>" + Data.persent + "</label></div></div><div class='col-6  m-auto m-b-10'><label><i class=' fas fa-calendar-xmark Orange'></i> تعداد ماه: </label><div class='form-group round m-auto'><label class='control-label font-weight-bold'>" + Data.month + "</label></div></div><div class='col-6  m-auto m-b-10'><label><i class=' fas fa-calendar-xmark Orange'></i> وضعیت شروع اقساط: </label><div class='form-group round m-auto'><label class='control-label font-weight-bold'>" + Data.startName + "</label></div></div><div class='col-12  m-auto m-b-10'><label><i class=' far fa-file-lines Orange'></i> توضیحات: </label><div class='form-group round m-auto'><label class='control-label font-weight-bold'>" + Data.description + "</label></div></div><div class='col-12  m-auto m-b-10'><a href='" + Data.fileName + "'><i class='fas fa-download Blue'></i> دانلود </a></div></div>");

    }
}

function OnLoadingTransactionDetail() {
    CleanItem();
}
function OnCompleteTransactionDetail(xhr) {
    // console.log(xhr);
    FillItem(xhr.responseJSON.object);
    $("#TransactionDetailModal").modal("show");
}

function OnErrorTransactionDetail() {

}

function TransactionDetail(id) {
    TransactionId.value = id;
    $("#TransactionDetail").submit();
}

let TasviyeGhestId = document.getElementById("TasviyeGhestId");
function TasviyeGhest(id) {
    swal({
        title: 'از ثبت این عملیات مطمئن هستید؟',
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        
        confirmButtonText: 'بله، تسویه شود. '
    }).then(function () {
        console.log("ghghgh");
        TasviyeGhestId.value = id;
        $("#TasviyeGhestForm").submit();
    }, function (dismiss) {
        if (dismiss === 'cancel') {
            swal(
                'لغو گردید',
                'قسط مورد نظر در انتظار تسویه است.',
                'error'
            ).catch(swal.noop);
        }
    }).catch(swal.noop);
}



function OnCompleteTasviyeGhest(xhr) {
    
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    else if (xhr.responseJSON.isSuccessed) {
        ToastSuccess("عملیات با موفقیت انجام شد");

        
    }
    else {
        ToastError(xhr.responseJSON.message);
        //$("#FecilitiesModal").modal("hide");
    }
}
