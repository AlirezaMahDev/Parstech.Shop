var ErrorData = document.getElementById("Error");
var Input_Id = document.getElementById("Input_Id");

var Input_SocialName = document.getElementById("Input_SocialName");
var Input_Title = document.getElementById("Input_Title");
var Input_Site = document.getElementById("Input_Site");
var Input_Description = document.getElementById("Input_Description");
var Input_Image = document.getElementById("Input_Image");
var image = document.getElementById("image");


var Response = null;

function GetEditData(id) {
    IdForIllInput(id);
    $("#GetEditForm").submit();

}

//Get
function OnLoadingGet() {
    CleanModal();
}

function OnCompleteGet(xhr) {
    Response = xhr.responseJSON;

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    if (Response.isSuccessed) {
        // console.log(Response);

        IdForIllInput(Response.object.id);
        Input_SocialName.value = Response.object.socialName;
        Input_Title.value = Response.object.title;
        Input_Site.value = Response.object.site;
        Input_Description.value = Response.object.description;
        Input_Image.value = Response.object.image
        image.insertAdjacentHTML("afterbegin", "<img width='170' src='/Shared/Images/" + Response.object.image + "'/>")
        $('#modal').modal('show');
        // console.log(Input_SocialName.value);
    } else {

        ToastError(Response.message);
        if (Response.message != null) {
            ErrorData.insertAdjacentHTML("afterbegin", "<div class='alert alert-danger  text-center'><i class='icon-ban'></i><h5 class='font-weight-bold'>" + Response.message + "</h5></div>")
        }

    }
}

function OnErrorGet() {

}


//Update
function OnLoadingUpdate() {
    // console.log("Loading");
}

function OnCompleteUpdate(xhr) {
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

function OnErrorUpdate() {

}


function OnLoading() {
    // console.log("Loading");
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


function IdForIllInput(id) {
    var IdList = document.querySelectorAll('.Input_Id');
    IdList.forEach(function (input) {
        input.value = id;
    });
}

function CleanModal() {
    IdForIllInput(0);
    Input_SocialName.value = null;
    Input_Title.value = null;
    Input_Site.value = null;
    Input_Description.value = null;
    Input_Image.value = null
    image.innerHTML = null;
}

function ShowCreateModal() {
    CleanModal();
    $('#modal').modal('show');

}