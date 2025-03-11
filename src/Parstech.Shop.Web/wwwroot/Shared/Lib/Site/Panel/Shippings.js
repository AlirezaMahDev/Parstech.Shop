var AddressData = document.getElementById("AddressData");

function IdForInputs(id) {
    var IdList = document.querySelectorAll('.Id');
    IdList.forEach(function (input) {
        input.value = id;
    });
}
$(document).ready(function () {
    $("#GetDataForm").submit();
})

function OnLoading() {
    AddressData.innerHTML = null;
}

function OnComplete(xhr) {
   // console.log(xhr);
    var list = xhr.responseJSON.object;
    list.forEach(function (element) {



        AddressData.insertAdjacentHTML("afterbegin", "<div class='portlet box border shadow round'><div class='portlet-heading'><div class='portlet-title'><h5 class='title'><i class=' fas fa-house-user p-1'></i>" + element.firstName + " " + element.lastName + "</h5></div><div class='buttons-box'><button type='button' class='btn BgOrange' onclick='GetShippingSubmit(" + element.id + ")'><i class=' fas fa-pen-to-square White'></i></button><button type='button'class='btn BgRed' onclick='DeleteSubmit(" + element.id + ")'><i class=' fas fa-trash White'></i></button></div></div><div class='portlet-body'><h6><i class=' fas fa-mobile-button p-1'></i>" + element.mobile + "</h6><h6><i class=' fas fa-magnifying-glass-location p-1'></i> " + element.postCode + "</h6><h6><i class=' fas fa-map-location-dot p-1'></i>" + element.country + " " + element.state + " " + element.city + " " + element.address + "</h6></div></div >")

    });
}

function OnError() {

}



var UserShippingId = document.getElementById("UserShippingId");
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

    UserShippingDto_Id.value = 0;
    UserShippingDto_UserId.value = 0;
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


function GetShippingSubmit(id) {
    UserShippingId.value = id;
    $("#GetItemForm").submit();
}

function OnLoadingGetItem() {
    CleanShipping();
}

function OnCompleteGetItem(xhr) {
   // console.log(xhr);
    FillShipping(xhr.responseJSON.object);
    $('#ShippingCreateUpdateModal').modal('show');
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
        $('#ShippingCreateUpdateModal').modal('hide');
        $("#GetDataForm").submit();
    }
    else {

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

    }
    else {

        ToastError(Response.message);

    }
}

function OnErrorShippingDelete() {

}

function openModal() {
    CleanShipping();
    $('#ShippingCreateUpdateModal').modal('show');
}
function closeModal() {
    $('#ShippingCreateUpdateModal').modal('hide');
}

function DeleteSubmit(id, code) {
    IdForInputs(id);
    swal({
        title: 'آیا از حذف آدرس خود مطمئن هستید؟',
        type: 'error',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        cancelButtonText: 'انصراف ',
        confirmButtonText: 'بله، حذف شود. '
    }).then(function () {
        $("#DeleteItemForm").submit();
    }, function (dismiss) {
        if (dismiss === 'cancel') {
            swal(
                'لغو گردید',
                'از حذف آدرس خود منصرف شدید',
                'error',
               
            ).catch(swal.noop);
        }
    }).catch(swal.noop);



}



function OnLoadingDelete() {
   
}
function OnCompleteDelete(xhr) {
   // console.log(xhr);
    Response = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    if (Response.isSuccessed) {
        ToastSuccess(Response.message);
        $("#GetDataForm").submit();
    }
    else {

        ToastError(Response.message);

    }
}
