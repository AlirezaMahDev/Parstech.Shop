
var Credit_Id = document.getElementById("Credit_Id");
var Credit_ProductStockPriceId = document.getElementById("Credit_ProductStockPriceId");


function AcceptCredit(id, productStockPriceId) {
    Credit_Id.value = id;
    Credit_ProductStockPriceId.value = productStockPriceId;
}


function OnLoadingCredit() {
    
}

function OnCompleteCredit(xhr) {
    console.log(xhr);

    var response = xhr.responseJSON;
    if (response.object != null) {
        ToastSuccess(response.message);
        setTimeout(RedirectToDargah(response.object), 3000);
    }
    else if (response.isSuccessed) {
        swal({
            title: response.message,
            type: 'question',
            showCancelButton: true,
            confirmButtonColor: '#1bd92f',
            cancelButtonColor: '#777',
            confirmButtonText: 'مشاهده سفارشات',
            cancelButtonText: 'یستن'
        }).then(function () {
            window.location.href = "/Panel/Orders";
        }, function (dismiss) {
            if (dismiss === 'cancel') {
                window.location.href = "/Checkout";
            }
        }).catch(swal.noop);
    }

    else {
        ToastError(response.message);
    }
}




function RedirectToDargah(object) {
    window.location.href = object;
}