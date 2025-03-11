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
        url: "/Admin/Reports/Finanical/ActiveCredit?handler=GetData",
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
    DataSection.insertAdjacentHTML("beforeend", "<table class='table  table-hover table-striped no-footer' id='data-table' role='grid' aria-describedby='data-table_info'><thead><tr role='row'><th class='small text-center'>کاربر</th><th class='small text-center'>روند تسهیلات</th><th class='small text-center'>وضعیت</th><th class='small text-center'>کد پیگیری تراکنش</th><th class='small text-center'>تاریخ</th><th class='small text-center'>مبلغ(تومان)</th><th class='small text-center'>نوع حساب</th><th class='small text-center'>دوره پرداخت</th><th class='small text-center'>درصد سود تسهیلات</th><th class='small text-center'>شروع دوره(اولین بازپرداخت)</th><th class='small text-center'>پایان دوره</th><th class='small text-center'>اقساط</th></tr></thead><tbody id='listSection'></tbody></table></div >");
    let listSection = document.getElementById("listSection");

    if (Data.list.length > 0) {
        Data.list.forEach(function (element) {
            var Type;
            var Start;
            var Active;
            switch (element.type) {
                case "OrgCredit":
                    Type = "اعتبار سازمانی";
                    break;
                case "Fecilities":
                    Type = "تسهیلات وام";
                    break;
                case "Amount":
                    Type = "کیف پول کاربر";
                    break;
            }
            if (element.start) {
                Start="شروع دوره"
            }
            else {
                Start = "شروع نشده"
            }
            if (element.active) {
                Active = "در حال استفاده"
            }
            else {
                Active = "خاتمه دوره"
            }
            listSection.insertAdjacentHTML("beforebegin", "<tr role='row'><td class='small text-center'>" + element.firstName + " " + element.lastName + "</td><td class='small text-center'>" + Start + "</td><td class='small text-center'>" + Active + "</td><td class='small text-center'>" + element.trackingCode + "</td><td class='small text-center'>" + element.createDateShamsi + "</td><td class='small text-center font-weight-bold'>" + separate(element.price) + "</td><td class='small text-center'>" + Type + "</td><td class='small text-center'>" + element.month + " ماهه</td><td class='small text-center'>" + element.persent + " %</td><td class='small text-center font-weight-bold'>" + element.firstDate + "</td><td class='small text-center font-weight-bold'>" + element.lastDate + "</td><td class='small text-center'><a onclick='GetAghsat(" + element.id + ")'>مشاهده</a></td></tr>");
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

function GetAghsat(id) {
    $.ajax({
        type: "POST",
        url: "/Admin/Reports/Finanical/ActiveCredit?handler=GetAghsat",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {"id":id},
        success: function (response) {
            fillAghsat(response);
        },
        failure: function (response) {

        },
        error: function (response) {

        }
    });

}

let aghsats = document.getElementById("aghsats");
function fillAghsat(data) {
    $("#AghsatModal").modal('show');
    aghsats.innerHTML = null;
    data.forEach(function (element) {
        aghsats.insertAdjacentHTML("beforeend", "<div class='row'><div class='col-4'><h5>تاریخ سر رسید: <strong>" + element.createDateShamsi + "</strong></h5></div><div class='col-4'><h5>مبلغ سررسید:<strong>" + separate(element.price) + " تومان</strong></h5></div><div class='col-4'><h5>توضیحات:<strong> " + element.description + " | " + element.trackingCode + "</strong></h5></div></div><hr />");
    });
}