var ErrorData = document.getElementById("Error");
var Response = null;


function OnLoading() {
   // console.log("Loading");
}

function OnSuccess() {

}

function OnComplete(xhr) {
    Response = xhr.responseJSON;
    //console.log(xhr);
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
        
    }

    if (Response.isSuccessed) {
        ToastSuccess(Response.message)
        setTimeout(RouteLink(Response.object), 2000);
        localStorage.setItem('ActiveSite', response.object2.object);
    }
    else {
        if (Response.message != null) {
            ErrorData.insertAdjacentHTML("afterbegin", "<div class='alert alert-danger  text-center'><i class='icon-ban'></i><h5 class='font-weight-bold'>" + Response.message + "</h5></div>")
        }
        if (Response.errors != null) {
            Response.errors.forEach(function (element) {
                ErrorData.insertAdjacentHTML("beforeend", "<div class='badge badge-danger w-100 mb-2 '> <h5 class='text-right'><i class='fa fa-warning p-2'></i>" + element.errorMessage + "</h5></div>");
            });
        }

    }
}
function OnError() {

}

function RouteLink(link) {
    window.location.href = link;
}