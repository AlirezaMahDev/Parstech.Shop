function OnLoadingGallery() {

}

function OnCompleteGallery(xhr) {
    console.log(xhr);
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    } else if (!xhr.responseJSON.isSuccessed) {
        ToastError(xhr.responseJSON.message);
    } else {
        ToastSuccess("عملیات با موفقیت انجام شد.")
        $("#GalleryModal").modal('hide');
        $("#GalleryDeleteModal").modal('hide');
        $("#GalleryMultipleModal").modal('hide');
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

//var GalleryId = document.getElementById("GalleryId");

function DeleteGalleryShowModal(id) {
    IdForGalleryIdInputs(id);
    $("#GalleryDeleteModal").modal("show");
}

function ChangeMain(id) {
    IdForGalleryIdInputs(id)
    $("#ChangeMainGallery").submit();

}

function OnCompleteChangeMain(xhr) {

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    } else {
        ToastSuccess("عملیات با موفقیت انجام شد.")

        ClearDataSet();
        tableMain
            .clear()
            .draw();
        dataSet = [];

        $("#GetDataForm").submit();
    }

}