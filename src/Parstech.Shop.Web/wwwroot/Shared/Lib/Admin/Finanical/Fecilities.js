function showFecilitiesModal(walletId) {
    IdForInputs(walletId);
    $("#FecilitiesModal").modal("show");
}

function OnLoadingFecilities() {

}

function OnCompleteFecilities(xhr) {
    console.log(xhr);
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    } else if (xhr.responseJSON.isSuccessed) {
        ToastSuccess("عملیات با موفقیت انجام شد");

        $("#FecilitiesModal").modal("hide");

        tableMain
            .clear()
            .draw();
        dataSet = [];
        $("#GetDataForm").submit();


    } else {
        console.log(xhr.responseJSON.errors);
        ToastError(xhr.responseJSON.message);
        if (xhr.responseJSON.errors != null) {
            xhr.responseJSON.errors.forEach(function (element) {
                ToastError(element.errorMessage);
            });
        }

        //$("#FecilitiesModal").modal("hide");
    }
}

function OnErrorFecilities() {

}