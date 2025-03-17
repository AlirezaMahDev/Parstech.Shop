function OnLoadingSectionDelete() {

}

function OnCompleteSectionDelete(xhr) {
    Response = xhr.responseJSON;
    // console.log(xhr);
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    if (Response.isSuccessed) {
        ToastSuccess(Response.message)
        $('#DeleteSectionModal').modal('hide');

        setTimeout(Refresh, 2000);
    } else {
        ToastError(Response.message);
    }
}

function OnErrorSectionDelete() {
    ToastError("خطا در ارسال درخواست");
}


function OnLoadingSectionDetailDelete() {

}

function OnCompleteSectionDetailDelete(xhr) {
    Response = xhr.responseJSON;
    // console.log(xhr);
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    if (Response.isSuccessed) {
        ToastSuccess(Response.message)
        $('#DeleteSectionDetailModal').modal('hide');
        setTimeout(Refresh, 2000);
    } else {
        ToastError(Response.message);
    }
}

function OnErrorSectionDetailDelete() {
    ToastError("خطا در ارسال درخواست");
}