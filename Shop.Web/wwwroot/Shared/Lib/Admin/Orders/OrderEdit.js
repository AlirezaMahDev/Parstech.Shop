var dataSet = [];
var tableMain;
var FilterInput = document.getElementById("FilterInput");
var Parameter_Filter = document.getElementById("Parameter_Filter");



function IdForInputs(id) {
    var IdList = document.querySelectorAll('.OrderId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}

function OrderShippingChange() {
    $("#OrderShippingChangeModal").modal("show");

}


function OnLoadingOrderShipping() {
    CleanItem();
    
}
function OnCompleteOrderShipping(xhr) {
   // console.log(xhr);

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    }
    else {

        $("#OrderShippingChange").modal("hide");
        $("ShowOrderDetailModal").modal("hide");
        GetOrderSubmit(xhr.responseJSON.object);
       

        ToastSuccess("عملیات با موفقیت انجام شد")
    }

}

function OnErrorOrderShipping() {

}












