var dataSet = [];
var tableMain;
var FilterInput = document.getElementById("FilterInput");
var Parameter_Filter = document.getElementById("Parameter_Filter");
var FilterInput = document.getElementById("FilterInput");
var Parameter_CurrentPage = document.getElementById("Parameter_CurrentPage");


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
        const data =
            [
                "<h5 class=''> <img src='/Shared/Images/Products/" + element.brandImage + "'width='50'/></h5>",
                "<h5 class=''>" + element.brandTitle + "</h5>",
                "<h5 class=''>" + element.latinBrandTitle + "</h5>",
                "<button onclick='GetBrandSubmit(" + element.brandId + ")' class='btn btn-sm btn-block btn-success curve m-1'>ویرایش </button>"
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
    FillDataSet(Data);


    tableMain = $('#data-table').DataTable({
        "searching": false,
        "paging": false,
        data: dataSet,
        columns: [
            {title: 'تصویر برند'},
            {title: 'نام برند'},
            {title: 'نام برند'},
            {title: 'عملیات'},
        ],
        "columnDefs": [{
            "targets": 0,
            "className": 'w-25',
        }],
        "pageLength": 25
    });
    tableMain.destroy();


}

function OnErrorData() {

}


function IdForInputs(id) {
    var IdList = document.querySelectorAll('.Id');
    IdList.forEach(function (input) {
        input.value = id;
    });
}

function GetBrandSubmit(id) {
    IdForInputs(id);
    $("#GetBrandForm").submit();
}

//دریافت محصول
var BrandDto_BrandId = document.getElementById("BrandDto_BrandId");
var BrandDto_BrandTitle = document.getElementById("BrandDto_BrandTitle");
var BrandDto_BrandImage = document.getElementById("BrandDto_BrandImage");
var BrandImage = document.getElementById("BrandImage");
var BrandDto_LatinBrandTitle = document.getElementById("BrandDto_LatinBrandTitle");


function CleanItem() {

    BrandDto_BrandId.value = null;
    BrandDto_BrandTitle.value = null;
    BrandDto_BrandImage.innerHTML = null;
    BrandDto_LatinBrandTitle.innerHTML = null;

}

function FillItem(element) {
    BrandDto_BrandId.value = element.brandId;
    BrandDto_BrandTitle.value = element.brandTitle;
    BrandDto_LatinBrandTitle.value = element.latinBrandTitle;
    BrandImage.innerHTML = null;
    BrandImage.insertAdjacentHTML("afterbegin", "<img src='/Shared/Images/Products/" + element.brandImage + "' width='200'/>")
    BrandDto_BrandImage.value = element.brandImage;
}

function OnLoadingGetItem() {
    //CleanProduct();
}

function OnCompleteGetItem(xhr) {
    // console.log(xhr);
    FillItem(xhr.responseJSON.object);
    $("#AddOrEditModal").modal("show");
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

    } else {
        FillItem(xhr.responseJSON.object);
        $("#AddOrEditModal").modal("hide");

        tableMain
            .clear()
            .draw();
        dataSet = [];
        $("#GetDataForm").submit();

        ToastSuccess("عملیات با موفقیت انجام شد")
    }

}

function OnErrorAE() {

}