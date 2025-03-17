let currentPage = document.getElementById("currentPage");
let page;

//let userFilter = document.getElementById("userFilter");
//let walletType = document.getElementById("walletType");
//let transactionType = document.getElementById("transactionType");
let fromDate = document.getElementById("fromDate");
let toDate = document.getElementById("toDate");

let exuserFilter = document.getElementById("exuserFilter");

let exfromDate = document.getElementById("exfromDate");
let extoDate = document.getElementById("extoDate");
$(document).ready(function () {
    currentPage.value = 1;
    page = currentPage.value;
    fromDate.value = null;
    toDate.value = null;
    GetData();
});

function GetData() {
    $.ajax({
        type: "POST",
        url: "/Admin/Store/Sales?handler=GetData",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (response) {
            FillData(response);


        },
        failure: function (response) {

        },
        error: function (response) {

        }
    });

}


let DataSection = document.getElementById("DataSection");

function FillData(Data) {
    page = Data.currentPage;
    DataSection.innerHTML = null;
    console.log(Data.list);
    DataSection.insertAdjacentHTML("beforeend", "<table class='table  table-hover table-striped no-footer' id='data-table' role='grid' aria-describedby='data-table_info'><thead><tr role='row'><th class='small text-center'>شرح قلم</th><th class='small text-center'>تعداد</th><th class='small text-center'>مبلغ(تومان)</th><th class='small text-center'>جمع(تومان)</th><th class='small text-center'>مالیات(تومان)</th><th class='small text-center'>جمع کل(تومان)</th><th class='small text-center'>سفارش</th><th class='small text-center'>تاریخ</th><th class='small text-center'>تامین کننده</th><th class='small text-center'>حق و العمل کاری</th></tr></thead><tbody id='listSection'></tbody></table></div >");
    let listSection = document.getElementById("listSection");

    if (Data.list.length > 0) {
        Data.list.forEach(function (element) {

            listSection.insertAdjacentHTML("beforebegin", "<tr role='row'><td class='small text-center'>" + element.name + "</td><td class='small text-center'>" + element.count + "</td><td class='small text-center'>" + separate(element.price) + "</td><td class='small text-center'>" + separate(element.detailSum) + "</td><td class='small text-center font-weight-bold'>" + separate(element.tax) + "</td><td class='small text-center'>" + separate(element.total) + "</td><td class='small text-center'>" + element.orderCode + "</td><td class='small text-center font-weight-bold '>" + element.createDateShamsi + "</td><td class='small text-center'>" + element.storeName + " <button onclick='GetStatusOfOrder(" + element.orderId + ")' type='button' class='btn btn-default'><i class=' fas fa-info Yellow m-1'></i></button></td><td class='small text-center'><button onclick='GetContract(" + element.orderId + ")' type='button' class='btn btn-default btn-xs btn-primary'>استعلام</button></td></tr>");
        });
    } else {
        listSection.insertAdjacentHTML("beforebegin", "<h5 class='Red font-weight-bold'>اطلاعاتی جهت نمایش یافت نشد.</h5>")
    }

    Data.storesSelect.forEach(function (element) {
        userFilter.insertAdjacentHTML("beforeend", "<option value=" + element.id + ">" + element.storeName + "</option>");
    })
}

function OnComplete(xhr) {
    console.log(xhr);
    ToastSuccess("اطلاعات فیلتر شد");
    FillData(xhr.responseJSON);
}

function GenerateExcel() {
    exuserFilter.value = userFilter.value;
    exwalletType.value = walletType.value;
    extransactionType.value = transactionType.value;
    exfromDate.value = fromDate.value;
    extoDate.value = toDate.value;
    $("#ExcelForm").submit();
}

function RunPaging(type) {
    if (type == "next") {
        currentPage.value = page += 1;
    } else {
        if (currentPage.value == 1) {
            currentPage.value = 1;
        } else {
            currentPage.value = page -= 1;
        }
    }
    $("#SerachForm").submit();
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
        data: {"OrderId": orderId},
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
        StatusOfOrderSection.insertAdjacentHTML("beforeend", "<div class='col-lg-6' ><p class='text-center m-t-10 m-b-10 font-weight-bold'>" + element.statusName + "</p><hr /></div><div class='col-lg-3' ><p class='text-center m-t-10 m-b-10 font-weight-bold Orange'>" + element.createDateShamsi + "</p><hr /></div><div class='col-lg-3' ><p class='text-center m-t-10 m-b-10 font-weight-bold'>" + element.createBy + "</p><hr /></div>");
    });
    $("#StatusOfOrderModal").modal('show');
}


//حق العمل کاری
function GetContract(orderId) {
    $.ajax({
        type: "POST",
        url: "/Admin/Store/Sales?handler=GetContract",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {"OrderId": orderId},
        success: function (response) {
            fillContract(response);


        },
        failure: function (response) {

        },
        error: function (response) {

        }
    });

}

let contractData = document.getElementById("contractData");

function fillContract(response) {
    contractData.innerHTML = null;
    response.details.forEach(function (element) {
        contractData.insertAdjacentHTML("beforeend", "<tr><td>" + element.detailName + "</td><td>" + separate(element.total) + "</td><td>" + separate(element.store) + "</td><td>" + separate(element.we) + "</td></tr>");
    });
    contractData.insertAdjacentHTML("beforeend", "<tr><td colspan='2'>مجموع</td><td>" + separate(response.totalStore) + "</td><td>" + separate(response.totalWe) + "</td></tr>");

    $("#ContractOrderModal").modal('show');
}