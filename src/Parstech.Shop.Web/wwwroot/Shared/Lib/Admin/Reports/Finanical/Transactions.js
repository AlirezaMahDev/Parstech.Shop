let currentPage = document.getElementById("currentPage");
let page;

let userFilter = document.getElementById("userFilter");
let walletType = document.getElementById("walletType");
let transactionType = document.getElementById("transactionType");
let fromDate = document.getElementById("fromDate");
let toDate = document.getElementById("toDate");

let exuserFilter = document.getElementById("exuserFilter");
let exwalletType = document.getElementById("exwalletType");
let extransactionType = document.getElementById("extransactionType");
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
        url: "/Admin/Reports/Finanical/Transactions?handler=GetData",
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
    DataSection.insertAdjacentHTML("beforeend", "<table class='table  table-hover table-striped no-footer' id='data-table' role='grid' aria-describedby='data-table_info'><thead><tr role='row'><th class='small text-center'>کاربر</th><th class='small text-center'>شناسه تراکنش</th><th class='small text-center'>کد پیگیری تراکنش</th><th class='small text-center'>تاریخ</th><th class='small text-center'>مبلغ(تومان)</th><th class='small text-center'>نوع حساب</th><th class='small text-center'>موجودی حساب</th><th class='small text-center'>نوع تراکنش</th><th class='small text-center'>توضیحات</th></tr></thead><tbody id='listSection'></tbody></table></div >");
    let listSection = document.getElementById("listSection");

    if (Data.list.length > 0) {
        Data.list.forEach(function (element) {
            var Type;
            switch (element.type) {
                case "OrgCredit":
                    Type = "اعتبار سازمانی";
                    break;
                case "Fecilities":
                    Type = "تسهبلات وام";
                    break;
                case "Amount":
                    Type = "کیف پول کاربر";
                    break;
            }
            listSection.insertAdjacentHTML("beforebegin", "<tr role='row'><td class='small text-center'>" + element.firstName + " " + element.lastName + "</td><td class='small text-center'>" + element.id + "</td><td class='small text-center'>" + element.trackingCode + "</td><td class='small text-center'>" + element.createDateShamsi + "</td><td class='small text-center font-weight-bold'>" + separate(element.price) + "</td><td class='small text-center'>" + Type + "</td><td class='small text-center'>" + separate(element.orgCredit) + "</td><td class='small text-center font-weight-bold " + element.color + "'>" + element.typeTitle + "</td><td class='small text-center'>" + element.description + " </td></tr>");
        });
    }
    else {
        listSection.insertAdjacentHTML("beforebegin","<h5 class='Red font-weight-bold'>اطلاعاتی جهت نمایش یافت نشد.</h5>")
    }
    
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
    }
    else {
        if (currentPage.value == 1) {
            currentPage.value = 1;
        }
        else {
            currentPage.value = page -= 1;
        }
    }
    $("#SerachForm").submit();
}