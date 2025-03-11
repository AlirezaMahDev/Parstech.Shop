function CleanProduct() {
    $("#GetAllCatForm2").submit();
}

//Search Feuture
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
function OnCompleteFeuture(xhr) {
    // console.log(xhr);

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        var Data = xhr.responseJSON.object;
        console.log(Data);
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

var feutureDataSet = [];
var feuturetableMain = null;
var feuturecaption = document.getElementById("feuturecaption");
var AddFeutureInput_PropertyId = document.getElementById("AddFeutureInput_PropertyId");
var AddFeutureInput_Id = document.getElementById("AddFeutureInput_Id");
var DeleteAddFeutureInput_Id = document.getElementById("DeleteAddFeutureInput_Id");
function FillFeutureDataSet(Data) {
    console.log(Data);
    Data.forEach(function (element) {
        const data =
            [
                "<h5 class=''>" + element.caption + "</h5>",
                "<button onclick='OpenAddEditModal(" + element.id + ",0)' class='btn btn-sm btn-block btn-success curve m-1'>ثبت ویژگی برای این محصول </button>"
            ];
        feutureDataSet.push(data);
    });
}


//Search Feuture


//Add Feuture
function OpenAddEditModal(propertyId, id) {

    AddFeutureInput_PropertyId.value = propertyId;
    AddFeutureInput_Id.value = id;
    $("#AddEditFeutureModal").modal("show");

}

function OnCompleteAEFeuture(xhr) {
    console.log(xhr);
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        if (xhr.responseJSON.isSuccessed) {
            ToastSuccess("عملیات با موفقیت انجام شد.")
            $("#AddEditFeutureModal").modal('hide');
            ClearDataSet();
            tableMain
                .clear()
                .draw();
            dataSet = [];

            $("#GetDataForm").submit();
        }
        else {
            ToastError(xhr.responseJSON.object.message)
        }

    }

}

//Add Feuture


//Delete Feuture
function OpenDeleteFeuture(id) {
    DeleteAddFeutureInput_Id.value = id;
    $("#DeleteFeutureModal").modal('show');
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

//Delete Feuture

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