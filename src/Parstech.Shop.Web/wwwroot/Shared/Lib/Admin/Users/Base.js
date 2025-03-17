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
    Data.userDtos.forEach(function (element) {
        const data =
            [
                element.userName,
                element.firstName,
                element.lastName,
                element.economicCode,

                "<button onclick='GetShippingListSubmit(" + element.id + ")' class='btn btn-success curve m-1'>حمل و نقل</button><button onclick='GetBillingSubmit(" + element.id + ")' class='btn btn-primary curve m-1'>اطلاعات کاربری</button><button onclick='GetPersmissoinDataListSubmit(" + element.id + ")' class='btn curve btn-dark m-1'>دسترسی ها</button><button onclick='loginByUser(" + element.id + ")' class='btn curve btn-red m-1 font-weight-bold text-white'>ورود به پنل کاربر</button>"
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


}

function OnCompleteData(xhr) {
    // console.log(xhr);
    var Data = xhr.responseJSON.object;
    // console.log(Data.userDtos);
    FillDataSet(Data);


    tableMain = $('#data-table').DataTable({
        "searching": false,
        "paging": false,
        data: dataSet,
        columns: [
            {title: 'نام کاربری'},
            {title: 'نام'},
            {title: 'نام خانوادگی'},
            {title: 'کد پرسنلی'},

            {title: 'عملیات'},
        ],
        "columnDefs": [{
            "targets": 4,
            "className": 'w-50',
            "targets": 4,
            "orderable": true
        }],
        "pageLength": 25
    });
    tableMain.destroy();


}

function OnErrorData() {

}


function IuserIdForInputs(id) {
    var IdList = document.querySelectorAll('.IUserId');
    IdList.forEach(function (input) {
        input.value = id;
    });


}

function SuserIdForInputs(id) {
    var IdList = document.querySelectorAll('.SUserId');
    IdList.forEach(function (input) {
        input.value = id;
    });


}

function ShippingIdForInputs(id) {
    var IdList = document.querySelectorAll('.ShippingId');
    IdList.forEach(function (input) {
        input.value = id;
    });


}


function GetBillingSubmit(id) {
    IuserIdForInputs(id);
    $("#GetBillingForm").submit();
}


function GetShippingListSubmit(id) {
    IuserIdForInputs(id);
    $("#GetShipppingListForm").submit();
}

function AddShipping() {

    $('#ShippingCreateUpdateModal').modal('show');
}

function GetShippingSubmit(id, userId) {
    IuserIdForInputs(userId);
    ShippingIdForInputs(id)
    $("#GetShippingForm").submit();
}

function DeleteShippingSubmit(id) {
    ShippingIdForInputs(id)
    $("#DeleteShippingForm").submit();
}


function GetPersmissoinDataListSubmit(id) {
    IuserIdForInputs(id);
    UserRole_NumberuserId.value = id;
    // console.log(UserRole_NumberuserId.value);
    $("#GetPersmissoinDataListForm").submit();
}


function loginByUser(id) {
    console.log(id);
    var loginUserId = document.getElementById("loginUserId");
    loginUserId.value = id;

    console.log(loginUserId.value);
    $("#LoginByUserForm").submit();
}
