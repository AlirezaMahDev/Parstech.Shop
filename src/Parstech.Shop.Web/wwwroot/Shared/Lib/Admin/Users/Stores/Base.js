var Id = document.getElementById("Id");
var Input_Id = document.getElementById("Input_Id");
var Input_UserId = document.getElementById("Input_UserId");
var Input_FirstName = document.getElementById("Input_FirstName");
var Input_LastName = document.getElementById("Input_LastName");
var Input_StoreName = document.getElementById("Input_StoreName");
var Input_Phone = document.getElementById("Input_Phone");
var Input_Mobile = document.getElementById("Input_Mobile");
var Input_Country = document.getElementById("Input_Country");
var Input_State = document.getElementById("Input_State");
var Input_City = document.getElementById("Input_City");
var Input_Address = document.getElementById("Input_Address");
var Input_PostCode = document.getElementById("Input_PostCode");
var Input_LatinStoreName = document.getElementById("Input_LatinStoreName");
var Input_PersentOfSale = document.getElementById("Input_PersentOfSale");
var Input_RepId = document.getElementById("Input_RepId");

function GetData(id) {
    Id.value = id;
    $("#GetDataForm").submit();
}


function CleanData() {

    Input_Id.value = null;
    Input_UserId.value = null;
    Input_FirstName.value = null;
    Input_LastName.value = null;
    Input_StoreName.value = null;
    Input_Phone.value = null;
    Input_Mobile.value = null;
    Input_Country.value = null;
    Input_State.value = null;
    Input_City.value = null;
    Input_Address.value = null;
    Input_PostCode.value = null;
    Input_LatinStoreName.value = null;
    Input_PersentOfSale.value = null
    Input_RepId.value = null


}

function FillData(element) {
    console.log(element);
    Input_Id.value = element.id;
    Input_UserId.value = element.userId;
    Input_FirstName.value = element.firstName;
    Input_LastName.value = element.lastName;
    Input_StoreName.value = element.storeName;

    Input_Phone.value = element.phone;
    Input_Mobile.value = element.mobile;
    Input_Country.value = element.country;
    Input_State.value = element.state;
    Input_City.value = element.city;
    Input_Address.value = element.address;
    Input_PostCode.value = element.postCode;
    Input_LatinStoreName.value = element.latinStoreName;
    Input_PersentOfSale.value = element.persentOfSale;
    Input_RepId.value = element.repId

}


function OnLoading() {
    CleanData();
}

function OnComplete(xhr) {
    if (xhr.responseJSON.object != null) {
        FillData(xhr.responseJSON.object);
        $("#StoreEdit").modal('show');
    } else {
        Input_UserId.value = userId.value;
        $("#StoreEdit").modal('show');
    }

}

function OnError() {

}


function OnLoadingUpdate() {

}

function OnCompleteUpdate(xhr) {
    // console.log(xhr);
    Response = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    if (Response.isSuccessed) {
        FillData(xhr.responseJSON.object);
        $('#StoreEdit').modal('show');
        ToastSuccess(Response.message);

    } else {

        ToastError(Response.message);

    }
}

function OnErrorUpdate() {

}




