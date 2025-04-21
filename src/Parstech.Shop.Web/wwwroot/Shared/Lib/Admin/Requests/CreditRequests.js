
let take = 25;
let skip = 0;
let List = [];






$(window).on('resize', function () {
    $('#data-table').css("width", "100%");
});


$(document).ready(function () {
    //Parameter_CurrentPage.value = 1;
    //Parameter_Type.value = "Top";
    //Parameter_Brand.value = "";
    //Parameter_Store.value = store.value;
    GetList();
});


function GetList() {
    $.ajax({
        type: "POST",
        url: "/Admin/Requests/CreditRequests?handler=Data",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { "skip": skip },
        success: function (response) {
            console.log(response);
            FillDataSet(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}




function FillDataSet(Data) {


    DataSection.innerHTML = null;
    
    DataSection.insertAdjacentHTML("beforeend", "<table class='table  table-hover table-striped no-footer' id='data-table' role='grid' aria-describedby='data-table_info'><thead><tr role='row'><th class='small text-center'>درخواست کننده</th><th class='small text-center'>کدپرسنلی</th><th class='small text-center'>کد ملی</th><th class='small text-center'>موبایل</th><th class='small text-center'>استان</th><th class='small text-center'>مبلغ(تومان)</th><th class='small text-center'>تاریخ</th><th class='small text-center'>وضعیت</th><th class='small text-center'>عملیات</th></tr></thead><tbody id='listSection'></tbody></table></div >");
    let listSection = document.getElementById("listSection");

    if (Data.length > 0) {
        Data.forEach(function (element) {

            listSection.insertAdjacentHTML("beforebegin", "<tr role='row'><td class='small text-center'>" + element.name + " " + element.family + "</td><td class='small text-center'>" + element.personalCode + "</td><td class='small text-center'>" + element.internationalCode + "</td><td class='small text-center'>" + element.mobile + "</td><td class='small text-center font-weight-bold'>" + element.state + "</td><td class='small text-center font-weight-bold'>" + separate(element.requestPrice) + "</td><td class='small text-center'>" + element.createDateShmai + "</td><td class='small text-center font-weight-bold'>" + element.status + "</td><td class='small text-center'><button class='btn btn-warning' onclick='ChangeStatus("+element.id+")'>تغییر وضعیت</button></td></tr>");
        });
    }
    else {
        listSection.insertAdjacentHTML("beforebegin", "<h5 class='Red font-weight-bold'>اطلاعاتی جهت نمایش یافت نشد.</h5>")
    }
}




// تابعی برای بارگیری موارد جدید
function loadMoreItems() {

    skip += 25;
    GetStoreList();

}


let FCid = document.getElementById("FCid");
let type = document.getElementById("type");

function ChangeStatus(id) {
    FCid.value = id;
    $("#ChangeModal").modal('show');
}

function OnCompleteChangeSubmit(xhr){
    Response = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    if (Response.isSuccessed) {
        ToastSuccess(Response.message);
        GetList();
        
    }
    else {
        ToastError(Response.message);
    }
}





