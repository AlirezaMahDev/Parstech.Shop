function OnLoadingCreateUser() {


}

function OnCompleteCreateUser(xhr) {
    // console.log(xhr);
    Response = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")
    }

    if (Response.isSuccessed) {
        ToastSuccess(Response.message);

        Parameter_Filter.value = FilterInput.value;
        tableMain
            .clear()
            .draw();
        dataSet = [];
        $("#GetDataForm").submit();
        $("#CreateUser").modal('hide');
    } else {

        ToastError(Response.message);

    }


}

function OnErrorCreateUser() {

}


