function ShowAddStockProduct(id) {
    
    $("#AddStockModal").modal('show');
}

//price
function SubmitGetPrice(id) {
    IdForProductStockIdInputs(id);

    $("#GetPriceForm").submit();
}


function OnCompleteGetPrice(xhr) {
    var Data = xhr.responseJSON.object;
    FillPriceItem(Data);
    $("#PriceModal").modal("show");
}
//price

//Change Price
function OnCompleteEditPrice(xhr) {


    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {
        if (xhr.responseJSON.isSuccessed) {
            var Data = xhr.responseJSON.object;
            FillPriceItem(Data);
            Filter();
            ToastSuccess("عملیات با موفقیت انجام شد")
            ClearDataSet();
            tableMain
                .clear()
                .draw();
            dataSet = [];

            $("#GetDataForm").submit();
        }
        else {
            ToastError("اطلاعات وارد شده با خطا بارگذاری شده اند")
            console.log(xhr.responseJSON);
            xhr.responseJSON.errors.forEach(function (element) {
                ToastError(element.errorMessage);
            });
        }


    }

}

//Change Price

//Change Stock
function ShowProductRepresentationModal(productId, repId) {
    IdForProductStockIdInputs(productId);
    IdForInputs(repId);
    $("#ProductRepresentationModal").modal("show");
}


function OnCompleteAddPr(xhr) {

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {

        Filter();
        ToastSuccess("عملیات با موفقیت انجام شد")
        ClearDataSet();
        tableMain
            .clear()
            .draw();
        dataSet = [];

        $("#GetDataForm").submit();
    }


}

//Change Stock

//DeleteStocke
function DeleteStock(rep, id) {
    var pspId = document.getElementById("pspId");
    var repId = document.getElementById("repId");
    pspId.value = id;
    repId.value = rep;

    swal({
        title: '  آیا از حذف کالا از انبار مطمئن هستید؟',
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: 'بله، حذف شود. '
    }).then(function () {
        $("#DeleteProductStockPrice").submit();
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
function OnCompleteDeleteProductStock(xhr) {
    console.log(xhr);
    var result = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {

        if (result.isSuccessed) {
            ToastSuccess("عملیات با موفقیت انجام شد");
            tableMain
                .clear()
                .draw();
            dataSet = [];

            $("#GetDataForm").submit();

        }
        else {
            ToastError("عملیات حذف به دلیل وجود موجودی زیرمجموعه و یا سفارش از این کالا امکان پذیر نیست")
        }


    }
}
//DeleteStocke

//Quick Change Stock
function ShowQuickProductRepresentationModal(productId, repId) {
    IdForProductStockIdInputs(productId);
    IdForInputs(repId);
    $("#QuickProductRepresentationModal").modal("show");
}

function OnCompleteQuickAddPr(xhr) {

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {

        Filter();
        ToastSuccess("عملیات با موفقیت انجام شد")
        ClearDataSet();
        tableMain
            .clear()
            .draw();
        dataSet = [];

        $("#GetDataForm").submit();
    }


}


//Quick Change Stock

//Per Bundle
function ShowChangeQuantityPerBundleModal(id) {
    var productStockPriceId = document.getElementById("productStockPriceId");
    productStockPriceId.value = id;
    $("#ChangeQuantityPerBundleModal").modal("show");
}
function OnCompleteChangeQuantityPerBundle(xhr) {
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {

        Filter();
        ToastSuccess("عملیات با موفقیت انجام شد")
        ClearDataSet();
        tableMain
            .clear()
            .draw();
        dataSet = [];

        $("#GetDataForm").submit();
    }
}

//Per Bundle

