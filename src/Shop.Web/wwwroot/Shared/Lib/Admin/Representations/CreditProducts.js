var dataSet = [];
var tableMain;
var paging = document.getElementById("paging");
var pagingHeader = document.getElementById("pagingHeader");
var repId = document.getElementById("repId");
$(document).ready(function () {

    fetch('/Components/GetProductStockPricesSelect/' + repId.value)
        .then(response => response.text())
        .then(html => {
            document.getElementById("ProductsSelect").innerHTML = html;
            document.getElementById("addProduct").innerHTML = html;
        })
        .catch(error => console.error('Error:', error));

    GetList();
});


function GetList() {
    
    $.ajax({
        type: "POST",
        url: "/Admin/Representations/CreditProducts?handler=GetData",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        
        success: function (response) {
            console.log(response);
            ResponseData(response);
        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }
    }); 
}




function ResponseData(Data) {
    // console.log(xhr);
    dataSet = [];
    FillDataSet(Data)
    



    tableMain = $('#data-table').DataTable({
        "searching": false,
        "paging": false,
        data: dataSet,
        columns: [

            
            { title: 'نام محصول' },
           
            { title: 'تعداد اقساط ماهانه' },
            { title: 'درصد سود' },
            { title: 'پیش پرداخت' },
            { title: 'قسط ماهانه' },
            { title: 'مجموع قیمت' },
            { title: 'وضعیت' },
            { title: 'عملیات' },
        ],
        "columnDefs": [{

            "targets": [0,7],
            "className": 'w-20',

        }],
        "pageLength": 25
    });
    tableMain.destroy();
}


function FillDataSet(Data) {
    console.log(Data);
    Data.object.list.forEach(function (element) {

        
        var ActionHtml;
        var Status;
       
        if (element.active) {
            Status = "<h5 class='badge BgGreen w-100 font-weight-bold'>تایید شده</h5>";
        }
        else {
            Status = "<h5 class='badge BgRed w-100 font-weight-bold'>در انتظار تایید</h5>"
        }
        

        const data =
            [
                "" + element.name + "<h5 class='Orange font-weight-bold'>" + element.code + "</h5>",
               "<h5 class=' font-weight-bold'>" + element.month + "</h5>",
               "<h5 class=' font-weight-bold'>" + element.persent + "</h5>",
                "<h5 class=' font-weight-bold'>" + separate(element.prePay) + "</h5>",
                "<h5 class=' font-weight-bold'>" + separate(element.payMonth) + "</h5>",
                "<h5 class=' font-weight-bold'>" + separate(element.total) + " </h5> <h5 class=' font-weight-bold'>قیمت کالا: " + separate(element.salePrice) + " </h5> <h5 class='Red font-weight-bold'>با تخفیف: " + separate(element.discountPrice) + " </h5>",
                Status,
                "<button onclick='GetCredit(" + element.id + "," + element.productStockPriceId + ")' type='button' class='btn btn-sm btn-round BgOrange text-white font-weight-bold'><i class='fas fa-file-signature m-1'></i>ویرایش</button>",
            ];
        dataSet.push(data);


    });
    var first = 1;
    var current = Data.object.currentPage;
    var next = Data.object.currentPage + 1;
    var privious = Data.object.currentPage - 1;
    var last = Data.object.pageCount + 1;

    if (next > last) {
        next = Data.object.currentPage;
    }
    if (privious < 1) {
        privious = Data.object.currentPage;
    }

    paging.innerHTML = null;
    pagingHeader.innerHTML = null;
    pagingHeader.insertAdjacentHTML("beforeend", "<h6 class='m-2'>صفحه " + current + " از " + last + "</h5>");
    paging.insertAdjacentHTML("beforeend", "<button onclick='RunPaging(" + first + ")' class='btn cart-Blue btn-xs font-weight-bold ml-2'>اولین صفحه<i class=' fas fa-circle-arrow-right p-1'></i></button><button onclick='RunPaging(" + privious + ")' class='btn cart-Green btn-xs font-weight-bold ml-2'><i class=' fas fa-arrow-right p-1'></i></button><button onclick='RunPaging(" + next + ")' class='btn cart-Green btn-xs font-weight-bold ml-2'><i class=' fas fa-arrow-left p-1'></i></button><button onclick='RunPaging(" + last + ")' class='btn cart-Blue btn-xs font-weight-bold ml-2'><i class=' fas fa-circle-arrow-left p-1'></i>آخرین صفحه</button> <h6 class='m-2'>صفحه " + current + " از " + last + "</h5>");

}









//Paging
function RunPaging(page) {
    tableMain
        .clear()
        .draw();
    dataSet = [];

    //Parameter_Filter.value = null;
    Parameter_CurrentPage.value = page;
    $("#GetDataForm").submit();
}

function OnLoadingData() {
}
function OnCompleteData(xhr) {
  
    var Data = xhr.responseJSON;
    ResponseData(Data);
}
function OnErrorData() {
}








let Id = document.getElementById("Id");
let ProductStockPriceId = document.getElementById("ProductStockPriceId");
let ProductStockPriceText = document.getElementById("ProductStockPriceText");
let Month = document.getElementById("Month");
let Persent = document.getElementById("Persent");
let Total = document.getElementById("Total");
let PayMonth = document.getElementById("PayMonth");
let PrePay = document.getElementById("PrePay");
let Active = document.getElementById("Active");
let addProduct = document.getElementById("addProduct");
let formCredit = document.getElementById("formCredit");

function fillCreditForm(object)
{
    Id.value = object.id;
    ProductStockPriceText.innerText = object.name;
    ProductStockPriceId.value = object.productStockPriceId;
    Month.value = object.month;
    Persent.value = object.persent;
    Total.value = object.total;
    PayMonth.value = object.payMonth;
    PrePay.value = object.prePay;
    Active.value = object.active;
}
function clearCreditForm() {
    Id.value = null;
    ProductStockPriceId.value = null;
    ProductStockPriceText.innerText = null;
    Month.value = null;
    Persent.value = null;
    Total.value = null;
    PayMonth.value = null;
    PrePay.value = null;
    Active.value = null;
}

function GetCredit(id,productStockPrice) {
    $.ajax({
        type: "POST",
        url: "/Admin/Representations/CreditProducts?handler=GetOne",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { "id": id, "productStockPrice": productStockPrice },
        success: function (response) {
            console.log(response);
            fillCreditForm(response.object);
            $("#CreditModal").modal("show");
        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }
    });
}
function AddCredit() {
    clearCreditForm();
    ProductStockPriceText.innerText = addProduct.options[addProduct.selectedIndex].text;
    ProductStockPriceId.value = addProduct.options[addProduct.selectedIndex].value;
    $("#CreditModal").modal("show");
}

function OnLoadingCredit() {

}

function OnCompleteCredit(xhr) {
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    else if (xhr.responseJSON.isSuccessed) {
        ToastSuccess("عملیات با موفقیت انجام شد");
        clearCreditForm();
       
        $("#CreditModal").modal("hide");
        tableMain
            .clear()
            .draw();
        dataSet = [];
        GetList();
    }
    else {
        ToastError(xhr.responseJSON.message);
        //$("#FecilitiesModal").modal("hide");
    }
}





// تابع تبدیل رشته به عدد (حذف کاماها)
function toNumber(value) {
    return value ? parseInt(value.toString().replace(/,/g, '')) || 0 : 0;
}

// تابع فرمت عدد با جداکننده هزارگان
function formatNumber(num) {
    return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

// تابع محاسبه مبلغ ماهانه
function calculatePayMonth() {
    const principal = toNumber(Total.value) - toNumber(PrePay.value);
    const month = parseInt(Month.value);
    const persent = parseFloat(Persent.value);

    if (principal > 0 && month > 0 && persent > 0) {
        const monthlyRate = persent / 100 / 12;
        return principal * (monthlyRate * Math.pow(1 + monthlyRate, month)) / (Math.pow(1 + monthlyRate, month) - 1);
    }
    return 0;
}

// تابع محاسبه نرخ سود
function calculatePersent() {
    const principal = toNumber(Total.value) - toNumber(PrePay.value);
    const month = parseInt(Month.value);
    const payMonth = toNumber(PayMonth.value);

    if (principal > 0 && month > 0 && payMonth > 0) {
        return ((payMonth * month - principal) / principal) * 100 / (month / 12);
    }
    return 0;
}

// تابع محاسبه مبلغ کل
function calculateTotal() {
    const month = parseInt(Month.value);
    const persent = parseFloat(Persent.value);
    const payMonth = toNumber(PayMonth.value);
    const prePay = toNumber(PrePay.value);

    if (payMonth > 0 && month > 0 && persent > 0) {
        const monthlyRate = persent / 100 / 12;
        const principal = payMonth * (Math.pow(1 + monthlyRate, month) - 1) / (monthlyRate * Math.pow(1 + monthlyRate, month));
        return principal + prePay;
    }
    return 0;
}

// تابع اصلی محاسبات
function calculate() {
    const activeElement = document.activeElement;

    // اگر نرخ سود تغییر کرده باشد
    if (activeElement === Persent) {
        const calculatedPayMonth = calculatePayMonth();
        if (calculatedPayMonth > 0) {
            PayMonth.value = formatNumber(Math.round(calculatedPayMonth));
        }
    }
    // اگر مبلغ ماهانه تغییر کرده باشد
    else if (activeElement === PayMonth) {
        const calculatedPersent = calculatePersent();
        if (calculatedPersent > 0) {
            Persent.value = Math.max(1, Math.round(calculatedPersent));
        }
    }

    // همیشه مبلغ کل را محاسبه کن
    const calculatedTotal = calculateTotal();
    if (calculatedTotal > 0) {
        Total.value = formatNumber(Math.round(calculatedTotal));
    }
}

// رویدادهای تغییر برای تمام فیلدهای عددی
[Month, Persent, PayMonth, PrePay].forEach(input => {
    input.addEventListener('input', function () {
        // اعتبارسنجی و فرمت اعداد
        if (this === PayMonth || this === PrePay) {
            const num = toNumber(this.value);
            this.value = num > 0 ? formatNumber(num) : '';
        }

        // انجام محاسبات با تأخیر 500ms
        clearTimeout(this.timer);
        this.timer = setTimeout(calculate, 500);
    });
});

// برای فیلد Total محاسبات جداگانه
Total.addEventListener('input', function () {
    const num = toNumber(this.value);
    this.value = num > 0 ? formatNumber(num) : '';

    // اگر مبلغ کل تغییر کرد، مبلغ ماهانه را محاسبه کن
    if (document.activeElement === Total) {
        clearTimeout(this.timer);
        this.timer = setTimeout(() => {
            const calculatedPayMonth = calculatePayMonth();
            if (calculatedPayMonth > 0) {
                PayMonth.value = formatNumber(Math.round(calculatedPayMonth));
            }
        }, 500);
    }
});