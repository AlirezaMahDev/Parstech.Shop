
var dataSet = [];
var tableMain = null;

var productId = document.getElementById("PId");
var GalleryData = document.getElementById("GalleryData");
var Content = document.getElementById("Content");





$(document).ready(function () {
   // console.log("-");
    IproductIdForInputs(productId.value);
    $("#GetDataForm").submit();

});

function FillDataSet(Data) {
    Data.galleryDtos.forEach(function (element) {
        if (element.isMain == true) {
            GalleryData.insertAdjacentHTML("beforeend", " <div class='col-md-2 col-sm-6'><div class='effect sides-effect'><a href='#'><img  style='height:120px;' src='/Shared/Images/Products/" + element.imageName + "' class='img-center img-responsive' alt=''><div class='absolute'><h2>" + element.alt + "</h2><p>تصویر اصلی محصول</p></div></a></div><button onclick='DeleteGalleryShowModal(" + element.id + ")' class='btn btn-red font-weight-bold text-white'>حذف تصویر</button></div>")

        }
        else {
            GalleryData.insertAdjacentHTML("beforeend", " <div class='col-md-2 col-sm-6'><div class='effect sides-effect'><a href='#'><img style='height:120px;' src='/Shared/Images/Products/" + element.imageName + "' class='img-center img-responsive' alt=''><div class='absolute'><h2>" + element.alt + "</h2></div></a></div><button onclick='DeleteGalleryShowModal(" + element.id + ")' class='btn btn-red font-weight-bold text-white'>حذف تصویر</button></div>")

        }
    });

    //Content.value = Data.productDto.description;

    Data.propertyDtos.forEach(function (element) {
        const data =
            [
                "<h5 class=''>" + element.propertyName + "</h5>",
                "<h5 class=''>" + element.value + "</h5>",
                "<button onclick='OpenAddEditModal(" + element.propertyId + "," + element.id + ")' class='btn btn-sm btn-block btn-success curve m-1'>ویرایش </button><button onclick='OpenDeleteFeuture(" + element.id + ")' class='btn btn-sm btn-block btn-danger curve m-1'>حذف</button>"
            ];
        dataSet.push(data);
    });
}
function ClearDataSet() {
    GalleryData.innerHTML = null;
    
    Gallery_File.value = null;
    Gallery_Alt.value = null;
    Gallery_IsMain.value = null;
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
            { title: 'مقدار ' },
            { title: 'عملیات' },
        ],
        "columnDefs": [{
            "targets": 1,
            "className": 'w-50',
        }],
        "pageLength": 25
    });
    tableMain.destroy();

    //tableMain = $('#data-table').DataTable({
    //    "searching": false,
    //    "paging": false,
    //    data: dataSet,
    //    columns: [
    //        { title: 'تصویر محصول' },
    //        { title: 'نام' },
    //        { title: 'نوع' },
    //        { title: 'قیمت(ریال)' },
    //        { title: 'عملیات' },
    //    ],
    //    "columnDefs": [{
    //        "targets": 1,
    //        "className": 'w-50',
    //    }],
    //    "pageLength": 25
    //});
    //tableMain.destroy();


}
function OnErrorData() {

}


//افزودن گالری
var Gallery_File = document.getElementById("Gallery_File");
var Gallery_Alt = document.getElementById("Gallery_Alt");
var Gallery_IsMain = document.getElementById("Gallery_IsMain");
var GalleryId = document.getElementById("GalleryId");
function DeleteGalleryShowModal(id) {
    GalleryId.value = id;
    $("#GalleryDeleteModal").modal("show");
}

function OnLoadingGallery() {


}
function OnCompleteGallery(xhr) {

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        ToastSuccess("عملیات با موفقیت انجام شد.")
        $("#DeleteFeutureModal").modal('hide');
        ClearDataSet();
        tableMain
            .clear()
            .draw();
        dataSet = [];

        $("#GetDataForm").submit();
    }

}
function OnErrorGallery() {

}







function OnLoadingData() {


    //FillFeutureDataSet = null;
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
            { title: 'مقدار ' },
            { title: 'عملیات' },
        ],
        "columnDefs": [{
            "targets": 1,
            "className": 'w-50',
        }],
        "pageLength": 25
    });
    tableMain.destroy();

    //tableMain = $('#data-table').DataTable({
    //    "searching": false,
    //    "paging": false,
    //    data: dataSet,
    //    columns: [
    //        { title: 'تصویر محصول' },
    //        { title: 'نام' },
    //        { title: 'نوع' },
    //        { title: 'قیمت(ریال)' },
    //        { title: 'عملیات' },
    //    ],
    //    "columnDefs": [{
    //        "targets": 1,
    //        "className": 'w-50',
    //    }],
    //    "pageLength": 25
    //});
    //tableMain.destroy();


}
function OnErrorData() {

}




//ویژگی ها
var parentId = document.getElementById("parentId");
var SubCategury = document.getElementById("SubCategury");
var categuryId = document.getElementById("categuryId");

document.getElementById('my-select').addEventListener('change', function () {
   // console.log('You selected: ', this.value);
    parentId.value = this.value;
    $("#GetSubsForm").submit();

});







function OnLoadingSub() {
    //CleanProduct();
}
function OnCompleteSub(xhr) {
   // console.log(xhr);

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        SubCategury.innerHTML = null;
        var Data = xhr.responseJSON.object;
        Data.forEach(function (element) {
            SubCategury.insertAdjacentHTML("beforeend", "<option value=" + element.groupId + ">" + element.groupTitle + "</option>")
        });
    }

}
function OnErrorSub() {

}



//جسنجوی ویژگی


var feutureDataSet = [];
var feuturetableMain = null;
var feuturecaption = document.getElementById("feuturecaption");
var AddFeutureInput_PropertyId = document.getElementById("AddFeutureInput_PropertyId");
var AddFeutureInput_Id = document.getElementById("AddFeutureInput_Id");
var DeleteAddFeutureInput_Id = document.getElementById("DeleteAddFeutureInput_Id");
function FillFeutureDataSet(Data) {

    Data.forEach(function (element) {
        const data =
            [
                "<h5 class=''>" + element.caption + "</h5>",
                "<button onclick='OpenAddEditModal(" + element.id + ",0)' class='btn btn-sm btn-block btn-success curve m-1'>ثبت ویژگی برای این محصول </button>"
            ];
        feutureDataSet.push(data);
    });
}

function Filter() {

    if (feuturetableMain != null) {
        feuturetableMain
            .clear()
            .draw();
        feutureDataSet = [];
    }
    //FillFeutureDataSet = null;

    $("#SearchFeutureForm").submit();
}
function OnLoadingFeuture() {


}
function OnCompleteFeuture(xhr) {
   // console.log(xhr);

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        var Data = xhr.responseJSON.object;
        FillFeutureDataSet(Data);

        feuturetableMain = $('#data-table2').DataTable({
            "searching": true,
            "paging": true,
            data: feutureDataSet,
            columns: [
                { title: 'نام ویژگی' },
                { title: 'عملیات' },
            ],
            "columnDefs": [{
                "targets": 1,
                "className": 'w-50',
            }],
            "pageLength": 50
        });
        feuturetableMain.destroy();
    }

}
function OnErrorFeuture() {

}



function OpenAddEditModal(propertyId, id) {
    //feuturecaption.value = caption;
    AddFeutureInput_PropertyId.value = propertyId;
    AddFeutureInput_Id.value = id;
    $("#AddEditFeutureModal").modal("show");

}
function OnLoadingAEFeuture() {


}
function OnCompleteAEFeuture(xhr) {

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        ToastSuccess("عملیات با موفقیت انجام شد.")
        $("#AddEditFeutureModal").modal('hide');
        ClearDataSet();
        tableMain
            .clear()
            .draw();
        dataSet = [];

        $("#GetDataForm").submit();
    }

}
function OnErrorAEFeuture() {

}


function OpenDeleteFeuture(id) {
    DeleteAddFeutureInput_Id.value = id;
    $("#DeleteFeutureModal").modal('show');
}
function OnLoadingDeleteFeuture() {


}
function OnCompleteDeleteFeuture(xhr) {

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        ToastSuccess("عملیات با موفقیت انجام شد.")
        $("#DeleteFeutureModal").modal('hide');
        ClearDataSet();
        tableMain
            .clear()
            .draw();
        dataSet = [];

        $("#GetDataForm").submit();
    }

}
function OnErrorDeleteFeuture() {

}

//دخیره

var productContent = document.getElementById("productContent");
//var Content = document.getElementById("Content");

function SaveContent() {
    //console.log(productContent.value);
    //console.log(Content.value);
   
    $("#SaveForm").submit();
}
function OnLoadingSave() {


}
function OnCompleteSave(xhr) {

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        ToastSuccess("عملیات با موفقیت انجام شد.")
        $("#DeleteFeutureModal").modal('hide');
        ClearDataSet();
        tableMain
            .clear()
            .draw();
        dataSet = [];

        $("#GetDataForm").submit();
    }

}
function OnErrorSave() {

}


function IproductIdForInputs(id) {
    var IdList = document.querySelectorAll('.productId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}