var dataSet = [];
var tableMain;
var paging = document.getElementById("paging");
var pagingHeader = document.getElementById("pagingHeader");
$(document).ready(function () {
    GetList();
});


function GetList() {
    $.ajax({
        type: "POST",
        url: "/Admin/Representations/testquick?handler=GetData",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        
        success: function (response) {
            console.log(response);
            ResponseData(response);
        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response);
        }
    }); 
}




function ResponseData(Data) {
    // console.log(xhr);

    FillDataSet(Data)
    



    tableMain = $('#data-table').DataTable({
        "searching": false,
        "paging": false,
        data: dataSet,
        columns: [

            { title: 'نوع' },
            { title: 'نام محصول' },
           
            { title: 'قیمت فروش(تومان)' },
            { title: 'موجودی' },
        ],
        "columnDefs": [{

            "targets": [2,3],
            "className": 'w-10',

        }],
        "pageLength": 25
    });
    tableMain.destroy();
}


function FillDataSet(Data) {
    console.log(Data);
    Data.object.list.forEach(function (element) {

        
        var ActionHtml;
        var type;
        switch (element.typeId) {
            case 1:
                type = "<h5 class='badge badge-success'>ساده</h5>";
                break;
            case 2:
                type = "<h5 class='badge badge-danger'>متغیر</h5>"

                break;
            case 3:
                type = "<h5 class='badge badge-warning'>زیرمجموعه</h5>";

                break;
            case 4:
                type = "<h5 class='badge badge-info'>باندل</h5>";

                break;
            case 5:
                type = "<h5 class='badge badge-info'>زیرمجموعه باندل</h5>";

                break;
        }

        let nameh5 = "<h5 id='n" + element.id + "'>" + element.name + "</h5>";


        const found = productItems.some(el => el.id === element.id);

        if (found) {
            
            const changedItem = productItems.find(u => u.id === element.id);

            nameh5 = "<h5 class='Red font-weight-bold' id='n" + element.id + "'>" + element.name + " <small class='Gray p-2'>قیمت قبلی:" + element.salePrice + "</small><small class='Gray'>موجودی قبلی:" + element.quantity + "</small></h5>";
            element.salePrice = changedItem.price;
            element.quantity = changedItem.quantity;
        }

        const data =
            [
                "<small>" + element.id + "</small>" + type + "<a target='_blank' href='/Admin/Products/CreateOrUpdate/" + element.productId + "' class='btn btn-xs btn-green m-1 font-weight-bold hover-green'>ویرایش </a>",
                "" + nameh5 + "<h5 class='Orange font-weight-bold'>" + element.code + "</h5>",
                "<input class='form-control' id='p" + element.id + "' onchange='changeTextboxValue(" + element.id + ")' value='" + element.salePrice + "'>",
                "<input class='form-control ' id='q" + element.id + "' onchange='changeTextboxValue(" + element.id + ")'  type='number' value='" + element.quantity + "'>",

            ];
        dataSet.push(data);


    });
    var first = 1;
    var current = Data.object.currentPage;
    var next = Data.object.currentPage + 1;
    var privious = Data.object.currentPage - 1;
    var last = Data.object.pageCount + 1;

    if (next > last) {
        next = Data.object.currentPage;
    }
    if (privious < 1) {
        privious = Data.object.currentPage;
    }

    paging.innerHTML = null;
    pagingHeader.innerHTML = null;
    pagingHeader.insertAdjacentHTML("beforeend", "<h6 class='m-2'>صفحه " + current + " از " + last + "</h5>");
    paging.insertAdjacentHTML("beforeend", "<button onclick='RunPaging(" + first + ")' class='btn cart-Blue btn-xs font-weight-bold ml-2'>اولین صفحه<i class=' fas fa-circle-arrow-right p-1'></i></button><button onclick='RunPaging(" + privious + ")' class='btn cart-Green btn-xs font-weight-bold ml-2'><i class=' fas fa-arrow-right p-1'></i></button><button onclick='RunPaging(" + next + ")' class='btn cart-Green btn-xs font-weight-bold ml-2'><i class=' fas fa-arrow-left p-1'></i></button><button onclick='RunPaging(" + last + ")' class='btn cart-Blue btn-xs font-weight-bold ml-2'><i class=' fas fa-circle-arrow-left p-1'></i>آخرین صفحه</button> <h6 class='m-2'>صفحه " + current + " از " + last + "</h5>");

}








//change items

let productItems = [];

function changeTextboxValue(id) {

    const quantityInput = document.getElementById("q"+id);
    const priceInput = document.getElementById("p"+id);
    const name = document.getElementById("n" + id);

    //var oldValue = quantityInput.attr("data-initial-value");
    //name.insertAdjacentHTML("beforeend", "<small class='Gray p-2'>قیمت قبلی:" + priceInput.value + "</small><small class='Gray'>موجودی قبلی:" + oldValue + "</small>");

    const productExist = productItems.find(u => u.id === id);
    if (productExist) {

        productExist.price = priceInput.value;
        productExist.quantity = quantityInput.value;


    }
    else {
        const newProductItem = { id: id, price: priceInput.value, quantity: quantityInput.value };
        productItems.push(newProductItem);
    }
    

    name.classList.add("Red");
    name.classList.add("font-weight-bold");
    // Log the array to the console
    console.log(productItems);
}








//Paging
function RunPaging(page) {
    tableMain
        .clear()
        .draw();
    dataSet = [];

    Parameter_Filter.value = null;
    Parameter_CurrentPage.value = page;
    $("#GetDataForm").submit();
}

function OnLoadingData() {
}
function OnCompleteData(xhr) {
  
    var Data = xhr.responseJSON;
    ResponseData(Data);
}
function OnErrorData() {
}




//SaveChange
function SaveChanges() {
    $.ajax({
        type: "POST",
        url: "/Admin/Representations/testquick?handler=SaveChanges",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: JSON.stringify(productItems), // Send the list of people as JSON
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.isSuccessed) {
                ToastSuccess("عملیات با موفقیت انجام شد")
                
                tableMain
                    .clear()
                    .draw();
                dataSet = [];
                productItems = [];
                GetList();
            }
            
        },
        failure: function (response) {
            ToastError("در خواست شما با شکست مواجه شده است")
        },
        error: function (response) {
            ToastError("در خواست شما با شکست مواجه شده است")
        }
    });
}

