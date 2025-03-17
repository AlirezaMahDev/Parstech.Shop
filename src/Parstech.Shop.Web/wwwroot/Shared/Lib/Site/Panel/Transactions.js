var dataSet = [];
var tableMain;

var paging = document.getElementById("paging");
var pagingHeader = document.getElementById("pagingHeader");
var Parameter_CurrentPage = document.getElementById("Parameter_CurrentPage");
var walletType = document.getElementById("walletType");


function GetTransaction() {
    walletType.value = "Amount";
    Parameter_CurrentPage.value = 1;
    $("#GetDataForm").submit();
}

function GetAghsat() {
    walletType.value = "Fecilities";
    Parameter_CurrentPage.value = 1;
    $("#GetDataForm").submit();
}

function GetCoins() {
    walletType.value = "Coin";
    Parameter_CurrentPage.value = 1;
    $("#GetDataForm").submit();
}

function GetOrgCredit() {
    walletType.value = "OrgCredit";
    Parameter_CurrentPage.value = 1;
    $("#GetDataForm").submit();
}

$(window).on('resize', function () {
    $('#data-table').css("width", "100%");
});


$(document).ready(function () {
    Parameter_CurrentPage.value = 1;
    walletType.value = "Amount";
    $("#GetDataForm").submit();
});


function FillDataSet(Data) {

    Data.list.forEach(function (element) {
        // console.log(element);
        const data =
            [
                "<h6>" + separate(element.price) + "</h6>",
                "" + element.typeName + "",
                "" + element.createDateShamsi + "",
                "" + element.expireDateShamsi + "",
                "<h6>" + element.trackingCode + "  </h6>",
                "<button onclick='TransactionDetail(" + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-circle-info Blue m-1'></i>جزئیات</button>"

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
    paging.insertAdjacentHTML("beforeend", "<button type='button' onclick='RunPaging(" + first + ")' class='btn  btn-xs font-weight-bold ml-2'>اولین صفحه<i class=' fas fa-circle-arrow-right p-1'></i></button><button type='button' onclick='RunPaging(" + privious + ")' class='btn  btn-xs font-weight-bold ml-2'><i class=' fas fa-arrow-right p-1'></i></button><button type='button' onclick='RunPaging(" + next + ")' class='btn  btn-xs font-weight-bold ml-2'><i class=' fas fa-arrow-left p-1'></i></button><button type='button' onclick='RunPaging(" + last + ")' class='btn  btn-xs font-weight-bold ml-2'><i class=' fas fa-circle-arrow-left p-1'></i>آخرین صفحه</button> <h6 class='m-2'>صفحه " + current + " از " + last + "</h5>");

}

function RunPaging(page) {
    Parameter_CurrentPage.value = page;
    $("#GetDataForm").submit();
}


function OnLoading() {
    if (dataSet.length > 0) {
        tableMain
            .clear()
            .draw();
        dataSet = [];
    }

}

function OnComplete(xhr) {
    // console.log(xhr);
    var Data = xhr.responseJSON.object;
    FillDataSet(Data);
    tableMain = $('#data-table').DataTable({
        "searching": false,
        "paging": false,
        data: dataSet,
        columns: [
            {title: 'مبلغ'},
            {title: 'نوع تراکنش'},
            {title: 'تاریخ ایجاد'},
            {title: 'مهلت پرداخت'},
            {title: 'کد پیگیری'},
            {title: 'عملیات'},
        ],
        "columnDefs": [{
            "targets": 5,
            "className": 'w-30',
        }],
        "pageLength": 25
    });
    tableMain.destroy();
}

function OnError() {

}


function IdForInputs(id) {
    var IdList = document.querySelectorAll('.OrderId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}

var TransactionDto = document.getElementById("TransactionDto");
var TransactionId = document.getElementById("transactionId");

function CleanItem() {

    TransactionDto.innerHTML = null;
    TransactionId.innerHTML = null;

}

function FillItem(Data) {
    TransactionDto.insertAdjacentHTML("afterbegin", "<div class='row'><div class='col-12  m-auto m-b-10'><div class='form-group round m-auto text-center'><label><i class=' fas fa-money-bill-wave Orange text-center'></i> مبلغ:</label><label class='control-label'> " + separate(Data.price) + " ربال</label></div></div><div class='col-12  m-auto m-b-10'><div class='form-group round m-auto text-center'><label><i class=' fas fa-stream Orange text-center'></i> نوع: </label><label class='control-label'>" + Data.typeName + "</label></div><br /><hr /></div><div class='col-6  m-auto m-b-10'><div class='form-group round m-auto'><label><i class='  fas fa-fingerprint Orange'></i> کد پیگیری: </label><label class='control-label'>" + Data.trackingCode + "</label></div></div><div class='col-6  m-auto m-b-10'><div class='form-group round m-auto'><label><i class=' far fa-file-lines Orange'></i> توضیحات(شناسه تسهیلات): </label><label class='control-label'>" + Data.description + "</label></div></div><div class='col-6  m-auto m-b-10'><div class='form-group round m-auto'><label><i class=' fas fa-calendar-plus Orange'></i> تاریخ ایجاد: </label><label class='control-label'>" + Data.createDateShamsi + "</label></div></div><div class='col-6  m-auto m-b-10'><div class='form-group round m-auto'><label><i class=' fas fa-calendar-xmark Orange'></i> مهلت پرداخت: </label><label class='control-label'>" + Data.expireDateShamsi + "</label></div></div><div class='col-12  m-auto m-b-10'><a class='btn btn-xs BgYellow font-weight-bold cart-Hover' href='" + Data.fileName + "'><i class='fas fa-download '></i> دریافت رسید </a></div></div>");
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
