function OnLoadingSectionCU() {

}

function OnCompleteSectionCU(xhr) {
    Response = xhr.responseJSON;
    // console.log(xhr);
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    if (Response.isSuccessed) {
        ToastSuccess(Response.message)
        $('#CreateSectionModal').modal('hide');
        setTimeout(Refresh, 2000);
    } else {
        ToastError(Response.message);
    }
}

function OnErrorSectionCU() {
    ToastError("خطا در ارسال درخواست");
}


function OnLoadingSectionDetailCU() {

}

function OnCompleteSectionDetailCU(xhr) {
    Response = xhr.responseJSON;
    // console.log(xhr);
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    if (Response.isSuccessed) {
        ToastSuccess(Response.message)
        $('#CreateSectionDetailModal').modal('hide');
        setTimeout(Refresh, 2000);
    } else {
        ToastError(Response.message);
    }
}

function OnErrorSectionDetailCU() {
    ToastError("خطا در ارسال درخواست");
}

function Refresh() {
    window.location.href = "/Admin/Setting/Sections/";
}
