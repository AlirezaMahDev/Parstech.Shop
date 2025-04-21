var dataSet = [];
var tableMain;
var FilterInput = document.getElementById("FilterInput");
var Parameter_Filter = document.getElementById("Parameter_Filter");
var paging = document.getElementById("paging");








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
                "<h5 class=''>" + element.caption + "</h5>",
                "<h5 class=''>" + element.propertyCateguryTitle + "</h5>",
                "<h5 class=''>" + element.categuryTitle + "</h5>",
                "<button onclick='GetCategurySubmit(" + element.id + ")' class='btn btn-sm btn-block btn-success curve m-1'>ویرایش </button>"
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
    //pagingHeader.innerHTML = null;
    //pagingHeader.insertAdjacentHTML("beforeend", "<h6 class='m-2'>صفحه " + current + " از " + last + "</h5>");
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
            { title: 'ویژگی' },
            { title: 'دسته بندی ویژگی' },
            { title: 'دسته بندی' },
            { title: 'عملیات' },
        ],
        "columnDefs": [{
            "targets": 0,
            "className": 'w-50',
        }],
        "pageLength": 25
    });
    tableMain.destroy();


}

function OnErrorData() {

}


function IdForInputs(id) {
    var IdList = document.querySelectorAll('.PropertyId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}

function GetCategurySubmit(id) {
    IdForInputs(id);
    $("#GetCateguryForm").submit();
    SearchCat();
}

//دریافت محصول
var PropertyDto_Id = document.getElementById("PropertyDto_Id");
var PropertyDto_Caption = document.getElementById("PropertyDto_Caption");
var PropertyDto_CateguryId = document.getElementById("PropertyDto_CateguryId");
var PropertyDto_PropertyCateguryId = document.getElementById("PropertyDto_PropertyCateguryId");


function CleanItem() {

    PropertyDto_Id.value = null;
    PropertyDto_Caption.value = null;
    PropertyDto_CateguryId.value = null;
    PropertyDto_PropertyCateguryId.value = null;
    SearchCat();
}
function FillItem(element) {
   // console.log(element);
    PropertyDto_Id.value = element.id;
    PropertyDto_Caption.value = element.caption;
    PropertyDto_CateguryId.value = element.categuryId;
    PropertyDto_PropertyCateguryId.value = element.propertyCateguryId;
   
   
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

function OnErrorAE() {

}




var SearchCatText = document.getElementById("SearchCatText");
var FilterCatForm = document.getElementById("FilterCatForm");
function SearchCat() {
    FilterCatForm.value = SearchCatText.value;
    $("#GetAllCatForm").submit();
}

function OnCompleteGetAllCat(xhr) {
    var Data = xhr.responseJSON.object;
    // console.log(Data);
    PropertyDto_CateguryId.innerHTML = null;
    Data.forEach(function (element) {
        if (element.isParnet) {
            PropertyDto_CateguryId.insertAdjacentHTML("beforeend", "<option value='" + element.groupId + "'>دسته بندی اصلی (" + element.groupTitle + ")</option>")
        }
        else {
            PropertyDto_CateguryId.insertAdjacentHTML("beforeend", "<option value='" + element.groupId + "'>" + element.groupTitle + "</option>")
        }

    });
    
}