var old = document.getElementById("old");
var New = document.getElementById("New");
var reNew = document.getElementById("reNew");


function Clean() {

    old.value = null;
    New.value = null;
    reNew.value = null;


}


function OnLoading() {

}

function OnComplete(xhr) {
    Response = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    if (Response.isSuccessed) {
        ToastSuccess(Response.message);
        Clean();
    } else {

        if (Response.errors != null) {
            console.log(Response);
            Response.errors.forEach(function (element) {
                ToastError(element.errorMessage);
            });
        } else {
            ToastError(Response.message);
        }

    }
}

function OnError() {

}


function OnLoadingUpdate() {
    CleanBilling();
}

function OnCompleteUpdate(xhr) {
    // console.log(xhr);
    Response = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    if (Response.isSuccessed) {
        FillBilling(xhr.responseJSON.object);
        ToastSuccess(Response.message);

    } else {

        ToastError(Response.message);

    }
}

function OnErrorUpdate() {

}