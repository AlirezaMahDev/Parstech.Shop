var AddressData = document.getElementById("AddressData");


function OnLoadingShippingListGet() {
    AddressData.innerHTML = null;
}

function OnCompleteShippingListGet(xhr) {
    // console.log(xhr);
    var list = xhr.responseJSON.object;
    list.forEach(function (element) {
        AddressData.insertAdjacentHTML("afterbegin", "<div class='col-lg-12'><div class='col-lg-12'><div class='portlet box border shadow round'><div class='portlet-heading'><div class='portlet-title'><h3 class='title'><i class='icon-location-pin'></i>" + element.firstName + " " + element.lastName + "</h3></div><div class='buttons-box'><button class='btn btn-warning' onclick='GetShippingSubmit(" + element.id + "," + element.userId + ")'><i class='icon-pencil'></i></button><button class='btn btn-warning' onclick='DeleteShippingSubmit(" + element.id + ")'><i class='icon-trash'></i></button></div></div><div class='portlet-body'><h4>" + element.mobile + "</h4><h5>" + element.postCode + "</h5><h5>" + element.country + " " + element.state + " " + element.city + " " + element.address + "</h5></div></div></div></div>")

    });
    $('#ShippingDataModal').modal('show');
}

function OnErrorShippingListGet() {

}


var UserShippingDto_Id = document.getElementById("UserShippingDto_Id");
var UserShippingDto_UserId = document.getElementById("UserShippingDto_UserId");
var UserShippingDto_FirstName = document.getElementById("UserShippingDto_FirstName");
var UserShippingDto_LastName = document.getElementById("UserShippingDto_LastName");

var UserShippingDto_Phone = document.getElementById("UserShippingDto_Phone");
var UserShippingDto_Mobile = document.getElementById("UserShippingDto_Mobile");
var UserShippingDto_Country = document.getElementById("UserShippingDto_Country");
var UserShippingDto_State = document.getElementById("UserShippingDto_State");
var UserShippingDto_City = document.getElementById("UserShippingDto_City");
var UserShippingDto_Address = document.getElementById("UserShippingDto_Address");
var UserShippingDto_PostCode = document.getElementById("UserShippingDto_PostCode");


function CleanShipping() {

    UserShippingDto_Id.value = null;
    UserShippingDto_UserId.value = null;
    UserShippingDto_FirstName.value = null;
    UserShippingDto_LastName.value = null;


    UserShippingDto_Phone.value = null;
    UserShippingDto_Mobile.value = null;
    UserShippingDto_Country.value = null;
    UserShippingDto_State.value = null;
    UserShippingDto_City.value = null;
    UserShippingDto_Address.value = null;
    UserShippingDto_PostCode.value = null;


}

function FillShipping(element) {
    UserShippingDto_Id.value = element.id;
    UserShippingDto_UserId.value = element.userId;
    UserShippingDto_FirstName.value = element.firstName;
    UserShippingDto_LastName.value = element.lastName;


    UserShippingDto_Phone.value = element.phone;
    UserShippingDto_Mobile.value = element.mobile;
    UserShippingDto_Country.value = element.country;
    UserShippingDto_State.value = element.state;
    UserShippingDto_City.value = element.city;
    UserShippingDto_Address.value = element.address;
    UserShippingDto_PostCode.value = element.postCode;

}


function OnLoadingShippingGet() {
    CleanShipping();
}

function OnCompleteShippingGet(xhr) {
    // console.log(xhr);
    FillShipping(xhr.responseJSON.object);
    $('#ShippingCreateUpdateModal').modal('show');
}

function OnErrorShippingGet() {

}


function OnLoadingShippingEC() {
    CleanShipping();
}

function OnCompleteShippingEC(xhr) {
    // console.log(xhr);
    Response = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    if (Response.isSuccessed) {
        ToastSuccess(Response.message);
        IuserIdForInputs(Response.object.userId);

        ShippingIdForInputs(Response.object.id)
        $("#GetShippingForm").submit();

    } else {

        ToastError(Response.message);

    }
}

function OnErrorShippingEC() {

}


function OnLoadingShippingDelete() {
    CleanShipping();
}

function OnCompleteShippingDelete(xhr) {
    // console.log(xhr);
    Response = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    if (Response.isSuccessed) {
        ToastSuccess(Response.message);
        $('#ShippingDataModal').modal('hide');

    } else {

        ToastError(Response.message);

    }
}

function OnErrorShippingDelete() {

}