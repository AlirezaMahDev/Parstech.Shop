
$(window).on('resize', function () {
    $('#data-table').css("width", "100%");
});


$(document).ready(function () {
    $("#GetDataForm").submit();
});

var UserBillingDto_Id = document.getElementById("UserBillingDto_Id");
var UserBillingDto_UserId = document.getElementById("UserBillingDto_UserId");
var UserBillingDto_FirstName = document.getElementById("UserBillingDto_FirstName");
var UserBillingDto_LastName = document.getElementById("UserBillingDto_LastName");
var UserBillingDto_Company = document.getElementById("UserBillingDto_Company");
var UserBillingDto_Email = document.getElementById("UserBillingDto_Email");
var UserBillingDto_Phone = document.getElementById("UserBillingDto_Phone");
var UserBillingDto_Mobile = document.getElementById("UserBillingDto_Mobile");
var UserBillingDto_Mobile2 = document.getElementById("UserBillingDto_Mobile2");
var UserBillingDto_Country = document.getElementById("UserBillingDto_Country");
var UserBillingDto_State = document.getElementById("UserBillingDto_State");
var UserBillingDto_City = document.getElementById("UserBillingDto_City");
var UserBillingDto_Address = document.getElementById("UserBillingDto_Address");
var UserBillingDto_PostCode = document.getElementById("UserBillingDto_PostCode");
var UserBillingDto_EconomicCode = document.getElementById("UserBillingDto_EconomicCode");
var UserBillingDto_NationalCode = document.getElementById("UserBillingDto_NationalCode");


function CleanBilling() {

    UserBillingDto_Id.value = null;
    UserBillingDto_UserId.value = null;
    UserBillingDto_FirstName.value = null;
    UserBillingDto_LastName.value = null;
    UserBillingDto_Company.value = null;
    UserBillingDto_Email.value = null;
    UserBillingDto_Phone.value = null;
    UserBillingDto_Mobile.value = null;
    UserBillingDto_Mobile2.value = null;
    UserBillingDto_Country.value = null;
    UserBillingDto_State.value = null;
    UserBillingDto_City.value = null;
    UserBillingDto_Address.value = null;
    UserBillingDto_PostCode.value = null;
    UserBillingDto_EconomicCode.value = null;
    UserBillingDto_NationalCode.value = null;

}
function FillBilling(element) {
    UserBillingDto_Id.value = element.id;
    UserBillingDto_UserId.value = element.userId;
    UserBillingDto_FirstName.value = element.firstName;
    UserBillingDto_LastName.value = element.lastName;
    UserBillingDto_Company.value = element.company;
    UserBillingDto_Email.value = element.email;
    UserBillingDto_Phone.value = element.phone;
    UserBillingDto_Mobile.value = element.mobile;
    UserBillingDto_Mobile2.value = element.mobile;
    UserBillingDto_Country.value = element.country;
    UserBillingDto_State.value = element.state;
    UserBillingDto_City.value = element.city;
    UserBillingDto_Address.value = element.address;
    UserBillingDto_PostCode.value = element.postCode;
    UserBillingDto_EconomicCode.value = element.economicCode;
    UserBillingDto_NationalCode.value = element.nationalCode;
}




function OnLoadingBillingGet() {
    CleanBilling();
}

function OnCompleteBillingGet(xhr) {
   // console.log(xhr);
    FillBilling(xhr.responseJSON.object);
}

function OnErrorBillingGet() {

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

    }
    else {

        ToastError(Response.message);

    }
}

function OnErrorUpdate() {

}