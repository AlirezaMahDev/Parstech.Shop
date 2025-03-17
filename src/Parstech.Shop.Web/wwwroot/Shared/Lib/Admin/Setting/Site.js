var ErrorData = document.getElementById("Error");
var Response = null;


function OnLoading() {
    //  console.log("Loading");
}

function OnSuccess() {

}

function OnComplete(xhr) {
    Response = xhr.responseJSON;
    // console.log(xhr);
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    if (Response.isSuccessed) {
        ToastSuccess(Response.message)

    } else {

        ToastError(Response.message);
        if (Response.message != null) {
            ErrorData.insertAdjacentHTML("afterbegin", "<div class='alert alert-danger  text-center'><i class='icon-ban'></i><h5 class='font-weight-bold'>" + Response.message + "</h5></div>")
        }

    }
}

function OnError() {

}

function RouteLink() {
    window.location.href = "/admin";
}