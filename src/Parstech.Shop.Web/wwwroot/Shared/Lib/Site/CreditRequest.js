let formCredit_Name = document.getElementById("formCredit_Name");
let formCredit_Family = document.getElementById("formCredit_Family");
let formCredit_PersonalCode = document.getElementById("formCredit_PersonalCode");
let formCredit_InternationalCode = document.getElementById("formCredit_InternationalCode");
let formCredit_Mobile = document.getElementById("formCredit_Mobile");
let formCredit_State = document.getElementById("formCredit_State");
let formCredit_TextRequestPrice = document.getElementById("formCredit_TextRequestPrice");

function CleanForm() {
    formCredit_Name.value = null;
    formCredit_Family.value = null;
    formCredit_PersonalCode.value = null;
    formCredit_InternationalCode.value = null;
    formCredit_Mobile.value = null;
    formCredit_State.value = null;
    formCredit_TextRequestPrice.value = null;
}

function OnCompleteCreditRequest(xhr) {
    // console.log(xhr);
    Response = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    if (Response.isSuccessed) {
        ToastSuccess(Response.message);
        CleanForm();
    } else {


        xhr.responseJSON.errors.forEach(function (element) {
            ToastError(element.errorMessage);
        });
        ToastError(Response.message);
    }
}