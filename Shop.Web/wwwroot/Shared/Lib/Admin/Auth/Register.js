$(document).ready(function () {
    //console.log("fgfg");
    GetCapcha();
});



function OnComplete(xhr) {
    Response = xhr.responseJSON;
    //console.log(xhr);
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
        
    }

    if (Response.isSuccessed) {
        ToastSuccess(Response.message)
        setTimeout(RouteLink("/Auth/LoginRegister"), 3000);
    }
    else {
        ToastError(Response.message);

    }
}
function RouteLink(link) {
    window.location.href = link;
}
