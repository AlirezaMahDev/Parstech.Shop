var dataSet = [];
var tableMain;
var FilterInput = document.getElementById("FilterInput");
var Parameter_Filter = document.getElementById("Parameter_Filter");










$(window).on('resize', function () {
    $('#data-table').css("width", "100%");
});


$(document).ready(function () {
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
                "<h5 class=''>" + element.name + "</h5>",
                "<button onclick='GetCategurySubmit(" + element.id + ")' class='btn btn-sm btn-block btn-success curve m-1'>ویرایش </button>"
            ];
        dataSet.push(data);
    });
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
    var IdList = document.querySelectorAll('.categuryId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}

function GetCategurySubmit(id) {
    IdForInputs(id);
    $("#GetCateguryForm").submit();
}

//دریافت محصول
var PropertyCateguryDto_Id = document.getElementById("PropertyCateguryDto_Id");
var PropertyCateguryDto_Name = document.getElementById("PropertyCateguryDto_Name");


function CleanItem() {

    PropertyCateguryDto_Id.value = null;
    PropertyCateguryDto_Name.value = null;
   
}
function FillItem(element) {
    PropertyCateguryDto_Id.value = element.id;
    PropertyCateguryDto_Name.value = element.name;
   
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