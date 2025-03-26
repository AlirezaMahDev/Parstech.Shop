function GetCategury(id) {
    IproductIdForInputs(id);
    $("#GetCateguryForm").submit();
}



var Cats = document.getElementById("CatsData");

var categuriesSelect = document.getElementById("categuriesSelect");
var categuriesSelect2 = document.getElementById("categuriesSelect2");
var SearchCatText = document.getElementById("SearchCatText");
var SearchCatText2 = document.getElementById("SearchCatText2");
var FilterCatForm = document.getElementById("FilterCatForm");
var FilterCatForm2 = document.getElementById("FilterCatForm2");
function OnLoadingCateguries() {
    Cats.innerHTML = null;
}
function OnCompleteCateguries(xhr) {
    // console.log(xhr.responseJSON.object);
    $("#CateguriesModal").modal('show');


    var Data = xhr.responseJSON.object;

    Data.forEach(function (element) {
        if (element.isParnet) {
            Cats.insertAdjacentHTML("beforeend", " <tr><td colspan='2'>" + element.groupTitle + "</td><td><button onclick='DeleteCategury(" + element.id + ")' class='btn btn-danger'>حذف</button></td></tr>")
        }
        else {
            Cats.insertAdjacentHTML("beforeend", " <tr><td></td><td>" + element.groupTitle + "</td><td><button onclick='DeleteCategury(" + element.id + ")' class='btn btn-danger'>حذف</button></td></tr>")
        }
    });


}
function OnFailureCateguries() {

}


function DeleteCategury(id) {
    IproductIdForInputs(id);
    $("#DeleteCatForm").submit();
}
function OnCompleteDeleteCat(xhr) {
    IproductIdForInputs(xhr.responseJSON.object);

    ClearDataSet();
    tableMain
        .clear()
        .draw();
    dataSet = [];

    $("#GetDataForm").submit();
}


function showAddCatModal() {
    $("#GetAllCatForm").submit();
}
function OnCompleteGetAllCat(xhr) {
    var Data = xhr.responseJSON.object;
    // console.log(Data);
    categuriesSelect.innerHTML = null;
    Data.forEach(function (element) {
        if (element.isParnet) {
            categuriesSelect.insertAdjacentHTML("beforeend", "<option value='" + element.groupId + "'>دسته بندی اصلی (" + element.groupTitle + ")</option>")
        }
        else {
            categuriesSelect.insertAdjacentHTML("beforeend", "<option value='" + element.groupId + "'>" + element.groupTitle + "</option>")
        }

    });
    $("#AddCateguryModal").modal('show');
}

function SearchCat() {
    FilterCatForm.value = SearchCatText.value;
    $("#GetAllCatForm").submit();
}







function OnCompleteGetAllCat2(xhr) {
    var Data = xhr.responseJSON.object;
    // console.log(Data);
    categuriesSelect2.innerHTML = null;
    categuriesSelect2.insertAdjacentHTML("beforeend", "<option>انتخاب نشده</option>")
    Data.forEach(function (element) {
        if (element.isParnet) {
            categuriesSelect2.insertAdjacentHTML("beforeend", "<option value='" + element.groupId + "'>دسته بندی اصلی (" + element.groupTitle + ")</option>")
        }
        else {
            categuriesSelect2.insertAdjacentHTML("beforeend", "<option value='" + element.groupId + "'>" + element.groupTitle + "</option>")
        }

    });

}


function SearchCat2() {
    FilterCatForm2.value = SearchCatText2.value;
    $("#GetAllCatForm2").submit();
}

function OnCompleteAddCat(xhr) {
    ToastSuccess("عملیات با موفقیت انجام شد");
    //IproductIdForInputs(xhr.responseJSON.object);
    /*$("#GetCateguryForm").submit();*/
    $("#AddCateguryModal").modal('hide');
    ClearDataSet();
    tableMain
        .clear()
        .draw();
    dataSet = [];

    $("#GetDataForm").submit();
}