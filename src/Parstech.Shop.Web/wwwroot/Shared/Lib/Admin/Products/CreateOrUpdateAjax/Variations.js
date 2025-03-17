var variationId = document.getElementById("variationId");

//Add
function OnCompleteAddVariation(xhr) {
    var result = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    } else {

        if (result.isSuccessed) {
            ToastSuccess("عملیات با موفقیت انجام شد");
            //BlankFilter();

            ClearDataSet();
            tableMain
                .clear()
                .draw();
            dataSet = [];

            $("#GetDataForm").submit();
        } else {
            ToastError("عملیات امکان پذیر نیست")
        }

    }
}

//Add

//Update
function GetUpdateVariation(id) {
    variationId.value = id;
    $("#EditVariationModal").modal('show');
}

function OnCompleteEditVariation(xhr) {

    var result = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    } else {

        if (result.isSuccessed) {
            ToastSuccess("عملیات با موفقیت انجام شد");
            //BlankFilter();

            ClearDataSet();
            tableMain
                .clear()
                .draw();
            dataSet = [];

            $("#GetDataForm").submit();
        } else {
            ToastError("عملیات امکان پذیر نیست")
        }

    }
}

//Update

//Add Single Stock
function SingleAdStock(id) {
    var dublicateProductId = document.getElementById("dublicateProductId");
    dublicateProductId.value = id;
    $("#AddStockModal").modal('show');
}

function OnCompleteAddSingleStock(xhr) {
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    } else {

        $("#ProductDuplicateModal").modal('hide');

        if (xhr.responseJSON.isSuccessed) {
            ToastSuccess("عملیات با موفقیت انجام شد");
            //BlankFilter();

            ClearDataSet();
            tableMain
                .clear()
                .draw();
            dataSet = [];

            $("#GetDataForm").submit();
        } else {
            ToastError(" انبار تکراری ! موجودی انبار از قبل وجود دارد ")
        }
    }
}

//Add Single Stock


//Delete
function DeleteChild(id) {
    var childId = document.getElementById("childId");
    childId.value = id;

    swal({
        title: '  آیا از حذف کالا مطمئن هستید؟',
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: 'بله، حذف شود. '
    }).then(function () {
        $("#DeleteChild").submit();
    }, function (dismiss) {
        if (dismiss === 'cancel') {
            swal(
                'لغو گردید',
                ' لغو عملیات حذف کالا از انبار .',
                'error'
            ).catch(swal.noop);
        }
    }).catch(swal.noop);


}

function OnCompleteDeleteChild(xhr) {
    console.log(xhr);
    var result = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    } else {

        if (result.isSuccessed) {
            ToastSuccess("عملیات با موفقیت انجام شد");
            tableMain
                .clear()
                .draw();
            dataSet = [];

            $("#GetDataForm").submit();

        } else {
            ToastError("عملیات حذف به دلیل وجود انبار امکان پذیر نیست")
        }


    }
}

//Delete




