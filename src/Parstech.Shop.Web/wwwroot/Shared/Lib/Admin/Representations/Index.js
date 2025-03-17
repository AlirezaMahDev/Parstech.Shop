var dataSet = [];
var tableMain;
var FilterInput = document.getElementById("FilterInput");
var Parameter_Filter = document.getElementById("Parameter_Filter");
var Rep = document.getElementById("RepId");
var paging = document.getElementById("paging");
var pagingHeader = document.getElementById("pagingHeader");
var currentPage = 1;

$(document).ready(function () {
    Parameter_CurrentPage.value = 1;

    $("#GetDataForm").submit();

    fetch('/Components/GetProductsSelect')
        .then(response => response.text())
        .then(html => {
            document.getElementById("ProductsSelect").innerHTML = html;
        })
        .catch(error => console.error('Error:', error));

    fetch('/Components/GetCategurySelect')
        .then(response => response.text())
        .then(html => {
            document.getElementById("CategurySelect").innerHTML = html;
        })
        .catch(error => console.error('Error:', error));
});

function IdForInputs(id) {

    var IdList = document.querySelectorAll('.RepId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}


function Filter() {
    tableMain
        .clear()
        .draw();
    dataSet = [];

    //Parameter_Filter.value = FilterInput.value;
    Parameter_CurrentPage.value = 1;
    /*$("#GetDataForm").submit();*/
}

function BlankFilter() {
    tableMain
        .clear()
        .draw();
    dataSet = [];

    Parameter_Filter.value = null;
    Parameter_CurrentPage.value = 1;
    $("#GetDataForm").submit();
}

function FillDataSet(Data) {
    console.log(Data);
    Data.object.list.forEach(function (element) {

        var typeHtml;
        var ActionHtml;


        if (Data.role == "Store") {
            switch (element.typeId) {
                case 1:
                    typeHtml = "<small>" + element.id + "</small><h5 class='badge badge-success'>ساده</h5><a target='_blank' href='/Admin/Products/CreateOrUpdate/" + element.productId + "' class='btn btn-xs btn-green btn-block font-weight-bold hover-green'>ویرایش </a>"
                    ActionHtml = "<div class='btn-group'><button onclick='SubmitGetPrice(" + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-money-bill-transfer Green m-1'></i>قیمت</button><button onclick='ShowLogModal(" + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-circle-info Blue m-1'></i>جزئیات</button><button onclick='ShowQuickProductRepresentationModal(" + element.productStockPriceId + "," + element.repId + ")' type='button' class='btn btn-default'><i class='fas fa-file-circle-plus Orange m-1'></i>موجودی</button><button onclick='Delete(" + element.repId + "," + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div>"
                    break;
                case 2:
                    typeHtml = "<small>" + element.id + "</small><h5 class='badge badge-danger'>متغیر</h5><a target='_blank' href='/Admin/Products/CreateOrUpdate/" + element.productId + "' class='btn btn-xs btn-green btn-block font-weight-bold hover-green'>ویرایش </a>"
                    ActionHtml = "<div class='btn-group'><button onclick='ShowLogModal(" + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-circle-info Blue m-1'></i>جزئیات</button><button onclick='Delete(" + element.repId + "," + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div>"

                    break;
                case 3:
                    typeHtml = "<small>" + element.id + "</small><h5 class='badge badge-warning'>زیرمجموعه</h5>"
                    ActionHtml = "<div class='btn-group'><button onclick='SubmitGetPrice(" + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-money-bill-transfer Green m-1'></i>قیمت</button><button onclick='ShowLogModal(" + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-circle-info Blue m-1'></i>جزئیات</button><button onclick='ShowQuickProductRepresentationModal(" + element.productStockPriceId + "," + element.repId + ")' type='button' class='btn btn-default'><i class='fas fa-file-circle-plus Orange m-1'></i>موجودی</button><button onclick='Delete(" + element.repId + "," + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div>"

                    break;
                case 4:
                    typeHtml = "<small>" + element.id + "</small><h5 class='badge badge-info'>باندل</h5><a target='_blank' href='/Admin/Products/CreateOrUpdate/" + element.productId + "' class='btn btn-xs btn-green btn-block font-weight-bold hover-green'>ویرایش </a>"
                    ActionHtml = "<div class='btn-group'><button onclick='SubmitGetPrice(" + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-money-bill-transfer Green m-1'></i>قیمت</button><button onclick='ShowLogModal(" + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-circle-info Blue m-1'></i>جزئیات</button><button onclick='Delete(" + element.repId + "," + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div>"

                    break;
                case 5:
                    typeHtml = "<small>" + element.id + "</small><h5 class='badge badge-info'>زیرمجموعه باندل</h5><a target='_blank' href='/Admin/Products/CreateOrUpdate/" + element.productId + "' class='btn btn-xs btn-green btn-block font-weight-bold hover-green'>ویرایش </a>"
                    ActionHtml = "<div class='btn-group'><button onclick='SubmitGetPrice(" + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-money-bill-transfer Green m-1'></i>قیمت</button><button onclick='ShowLogModal(" + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-circle-info Blue m-1'></i>جزئیات</button><button onclick='ShowQuickProductRepresentationModal(" + element.productStockPriceId + "," + element.repId + ")' type='button' class='btn btn-default'><i class='fas fa-file-circle-plus Orange m-1'></i>موجودی</button><button onclick='Delete(" + element.repId + "," + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div>"

                    break;
            }
        } else {
            switch (element.typeId) {
                case 1:
                    typeHtml = "<small>" + element.id + "</small><h5 class='badge badge-success'>ساده</h5><a target='_blank' href='/Admin/Products/CreateOrUpdate/" + element.productId + "' class='btn btn-xs btn-green btn-block font-weight-bold hover-green'>ویرایش </a>"
                    ActionHtml = "<div class='btn-group'><button onclick='SubmitGetPrice(" + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-money-bill-transfer Green m-1'></i>قیمت</button><button onclick='ShowLogModal(" + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-circle-info Blue m-1'></i>جزئیات</button><button onclick='ShowProductRepresentationModal(" + element.productStockPriceId + "," + element.repId + ")' type='button' class='btn btn-default'><i class='fas fa-file-circle-plus Orange m-1'></i>موجودی</button><button onclick='Delete(" + element.repId + "," + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div>"
                    break;
                case 2:
                    typeHtml = "<small>" + element.id + "</small><h5 class='badge badge-danger'>متغیر</h5><a target='_blank' href='/Admin/Products/CreateOrUpdate/" + element.productId + "' class='btn btn-xs btn-green btn-block font-weight-bold hover-green'>ویرایش </a>"
                    ActionHtml = "<div class='btn-group'><button onclick='ShowLogModal(" + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-circle-info Blue m-1'></i>جزئیات</button><button onclick='Delete(" + element.repId + "," + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div>"

                    break;
                case 3:
                    typeHtml = "<small>" + element.id + "</small><h5 class='badge badge-warning'>زیرمجموعه</h5>"
                    ActionHtml = "<div class='btn-group'><button onclick='SubmitGetPrice(" + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-money-bill-transfer Green m-1'></i>قیمت</button><button onclick='ShowLogModal(" + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-circle-info Blue m-1'></i>جزئیات</button><button onclick='ShowProductRepresentationModal(" + element.productStockPriceId + "," + element.repId + ")' type='button' class='btn btn-default'><i class='fas fa-file-circle-plus Orange m-1'></i>موجودی</button><button onclick='Delete(" + element.repId + "," + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div>"

                    break;
                case 4:
                    typeHtml = "<small>" + element.id + "</small><h5 class='badge badge-info'>باندل</h5><a target='_blank' href='/Admin/Products/CreateOrUpdate/" + element.productId + "' class='btn btn-xs btn-green btn-block font-weight-bold hover-green'>ویرایش </a>"
                    ActionHtml = "<div class='btn-group'><button onclick='SubmitGetPrice(" + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-money-bill-transfer Green m-1'></i>قیمت</button><button onclick='ShowLogModal(" + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-circle-info Blue m-1'></i>جزئیات</button><button onclick='Delete(" + element.repId + "," + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div>"

                    break;
                case 5:
                    typeHtml = "<small>" + element.id + "</small><h5 class='badge badge-info'>زیرمجموعه باندل</h5><a target='_blank' href='/Admin/Products/CreateOrUpdate/" + element.productId + "' class='btn btn-xs btn-green btn-block font-weight-bold hover-green'>ویرایش </a>"
                    ActionHtml = "<div class='btn-group'><button onclick='SubmitGetPrice(" + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-money-bill-transfer Green m-1'></i>قیمت</button><button onclick='ShowLogModal(" + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-circle-info Blue m-1'></i>جزئیات</button><button onclick='ShowProductRepresentationModal(" + element.productStockPriceId + "," + element.repId + ")' type='button' class='btn btn-default'><i class='fas fa-file-circle-plus Orange m-1'></i>موجودی</button><button onclick='Delete(" + element.repId + "," + element.productStockPriceId + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div>"

                    break;
            }
        }
        console.log(element);
        if (element.discountPrice != 0) {
            const data =
                [
                    typeHtml,
                    "<h5>" + element.name + "</h5><h5 class='Orange font-weight-bold'>" + element.code + "</h5>",
                    "<h5 class=' font-weight-bold'>" + element.quantity + " عدد</h5>",
                    "<h5 class=' font-weight-bold'>" + separate(element.salePrice) + " تومان</h5><h5 class='Red font-weight-bold'>" + separate(element.discountPrice) + " تومان <small>(قیمت شگفت انگیز)</small></h5>",
                    ActionHtml
                ];
            dataSet.push(data);
        } else {
            const data =
                [
                    typeHtml,
                    "<h5>" + element.name + "</h5><h5 class='Orange font-weight-bold'>" + element.code + "</h5>",
                    "<h5 class=' font-weight-bold'>" + element.quantity + " عدد</h5>",
                    "<h5 class=' font-weight-bold'>" + separate(element.salePrice) + " تومان</h5>",
                    ActionHtml
                ];
            dataSet.push(data);
        }


        //switch (element.typeId) {
        //    case 1:
        //        StockData.insertAdjacentHTML("beforeend", " <tr><td >" + element.id + " <br/><small class='Orange font-weight-bold'>محصول" + element.productId + " </small></td><td class='small'>" + element.productName + " <small class='Orange font-weight-bold'>محصول ساده </small></td ><td class='small'>" + element.storeName + "</td><td class='small'>" + element.repName + "</td><td >" + element.quantity + "</td><td >" + element.salePrice + "<br/><small class='Red font-weight-bold'>" + element.discountPrice + "</small></td><td><div class= 'btn-group'><button onclick='SubmitGetPrice(" + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-money-bill-transfer Green m-1'></i>قیمت</button><button onclick='ShowProductRepresentationModal(" + element.id + "," + element.repId + ")' type='button' class='btn btn-default'><i class='fas fa-file-circle-plus Orange m-1'></i>موجودی</button><button onclick='DeleteStock(" + element.repId + "," + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div></td></tr>")


        //        break;
        //    case 2:
        //        StockData.insertAdjacentHTML("beforeend", " <tr><td >" + element.id + " <br/><small class='Orange font-weight-bold'>محصول" + element.productId + " </small></td><td class='small'>" + element.productName + " <small class='Orange font-weight-bold'>متغیر </small></td><td class='small'>" + element.storeName + "</td><td class='small'>" + element.repName + "</td><td >" + element.quantity + "</td><td >" + element.salePrice + "<br/><small class='Red font-weight-bold'>" + element.discountPrice + "</small></td><td><div class= 'btn-group'><button onclick='DeleteStock(" + element.repId + "," + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div></td></tr>")
        //        break;

        //    case 3:
        //        StockData.insertAdjacentHTML("beforeend", " <tr><td >" + element.id + " <br/><small class='Orange font-weight-bold'>محصول" + element.productId + " </small></td><td class='small'>" + element.productName + " <small class='Orange font-weight-bold'>زیرمجموعه </small></td ><td class='small'>" + element.storeName + "</td><td class='small'>" + element.repName + "</td><td >" + element.quantity + "</td><td >" + element.salePrice + "<br/><small class='Red font-weight-bold'>" + element.discountPrice + "</small></td><td><div class= 'btn-group'><button onclick='SubmitGetPrice(" + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-money-bill-transfer Green m-1'></i>قیمت</button><button onclick='ShowProductRepresentationModal(" + element.id + "," + element.repId + ")' type='button' class='btn btn-default'><i class='fas fa-file-circle-plus Orange m-1'></i>موجودی</button><button onclick='DeleteStock(" + element.repId + "," + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div></td></tr>")

        //        break;
        //    case 4:
        //        StockData.insertAdjacentHTML("beforeend", " <tr><td >" + element.id + " <br/><small class='Orange font-weight-bold'>محصول" + element.productId + " </small></td><td class='small'>" + element.productName + " <small class='Orange font-weight-bold'>باندل</small></td ><td class='small'>" + element.storeName + "</td><td class='small'>" + element.repName + "</td><td >" + element.quantity + "</td><td >" + element.salePrice + "<br/><small class='Red font-weight-bold'>" + element.discountPrice + "</small></td><td><div class= 'btn-group'><button onclick='SubmitGetPrice(" + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-money-bill-transfer Green m-1'></i>قیمت</button><button onclick='DeleteStock(" + element.repId + "," + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div></td></tr>")
        //        break;
        //    case 5:
        //        StockData.insertAdjacentHTML("beforeend", " <tr><td >" + element.id + " <br/><small class='Orange font-weight-bold'>محصول" + element.productId + " </small></td><td class='small'>" + element.productName + " <small class='Orange font-weight-bold'>زیرمجموعه </small></td ><td class='small'>" + element.storeName + "</td><td class='small'>" + element.repName + "</td><td >" + element.quantity + "<br/><small>تعداد در هر پک: " + element.quantityPerBundle + "</small></td><td >" + element.salePrice + "<br/><small class='Red font-weight-bold'>" + element.discountPrice + "</small></td><td><div class= 'btn-group'><button onclick='SubmitGetPrice(" + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-money-bill-transfer Green m-1'></i>قیمت</button><button onclick='ShowProductRepresentationModal(" + element.id + "," + element.repId + ")' type='button' class='btn btn-default'><i class='fas fa-file-circle-plus Orange m-1'></i>موجودی</button><button onclick='ShowChangeQuantityPerBundleModal(" + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-file-circle-plus Orange m-1'></i>تغییر در موجودی هر پک</button><button onclick='DeleteStock(" + element.repId + "," + element.id + ")' type='button' class='btn btn-default'><i class='fas fa-trash Red m-1'></i>حذف</button></div></td></tr>")

        //        break;

        //    default:
        //        break;
        //}

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

    // console.log("first" + first);
    // console.log("perivous" + privious);
    // console.log("next" + next);
    // console.log("last" + last);
    paging.innerHTML = null;
    pagingHeader.innerHTML = null;
    pagingHeader.insertAdjacentHTML("beforeend", "<h6 class='m-2'>صفحه " + current + " از " + last + "</h5>");
    paging.insertAdjacentHTML("beforeend", "<button onclick='RunPaging(" + first + ")' class='btn cart-Blue btn-xs font-weight-bold ml-2'>اولین صفحه<i class=' fas fa-circle-arrow-right p-1'></i></button><button onclick='RunPaging(" + privious + ")' class='btn cart-Green btn-xs font-weight-bold ml-2'><i class=' fas fa-arrow-right p-1'></i></button><button onclick='RunPaging(" + next + ")' class='btn cart-Green btn-xs font-weight-bold ml-2'><i class=' fas fa-arrow-left p-1'></i></button><button onclick='RunPaging(" + last + ")' class='btn cart-Blue btn-xs font-weight-bold ml-2'><i class=' fas fa-circle-arrow-left p-1'></i>آخرین صفحه</button> <h6 class='m-2'>صفحه " + current + " از " + last + "</h5>");

}

function RunPaging(page) {
    tableMain
        .clear()
        .draw();
    dataSet = [];

    Parameter_Filter.value = null;
    Parameter_CurrentPage.value = page;
    currentPage = page;
    $("#GetDataForm").submit();
}

var Rep = document.getElementById("RepId");
var Parameter_Filter = document.getElementById("Parameter_Filter");
var Parameter_Categury = document.getElementById("Parameter_Categury");
var Parameter_Type = document.getElementById("Parameter_Type");
var Parameter_Exist = document.getElementById("Parameter_Exist");
var ProductsSelect = document.getElementById("ProductsSelect");
var CategurySelect = document.getElementById("CategurySelect");
var TypeSelect = document.getElementById("TypeSelect");

function DataSubmit() {
    // console.log(Rep.value);
    IdForInputs(Rep.value);
    Parameter_Filter.value = ProductsSelect.value;
    Parameter_Categury.value = CategurySelect.value;
    Parameter_Type.value = TypeSelect.value;
    Parameter_Exist.value = ExistSelect.value;
    /*Parameter_CurrentPage.value = page;*/
    if (dataSet.length > 0) {
        Filter();
    }

    console.log(Parameter_Filter.value);
    console.log(ProductsSelect.value);
    $("#GetDataForm").submit();
}

function OnLoadingData() {


}

function OnCompleteData(xhr) {
    // console.log(xhr);
    var Data = xhr.responseJSON;
    FillDataSet(Data);


    tableMain = $('#data-table').DataTable({
        "searching": false,
        "paging": false,
        data: dataSet,
        columns: [

            {title: 'نوع'},
            {title: 'نام محصول'},
            {title: 'موجودی'},
            {title: 'قیمت فروش(تومان)'},
            {title: 'عملیات'},
        ],
        "columnDefs": [{
            "targets": 0,
            "className": 'w-30',
            "targets": 3,
            "className": 'w-30',

        }],
        "pageLength": 25
    });
    tableMain.destroy();


}

function OnErrorData() {

}


//Price
var product_Id = document.getElementById("product_Id");
var product_RepId = document.getElementById("product_RepId");
var product_StoreId = document.getElementById("product_StoreId");
var product_StockStatus = document.getElementById("product_StockStatus");
var product_ProductId = document.getElementById("product_ProductId");
var product_BasePrice = document.getElementById("product_BasePrice");
var product_Price = document.getElementById("product_Price");
var product_SalePrice = document.getElementById("product_SalePrice");
var product_DiscountPrice = document.getElementById("product_DiscountPrice");


function CleanPriceItem() {

}

function FillPriceItem(Data) {
    console.log(Data);
    //product_Id.value = Data.id;
    product_ProductId.value = Data.productId;
    product_RepId.value = Data.repId;
    product_StoreId.value = Data.storeId;
    product_StockStatus.value = Data.stockStatus;
    product_BasePrice.value = Data.textBasePrice;
    product_Price.value = Data.textPrice;
    product_SalePrice.value = Data.textSalePrice;
    product_DiscountPrice.value = Data.textDiscountPrice;
}

function IdForProductInputs(id) {

    var IdList = document.querySelectorAll('.ProductId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}

function SubmitGetPrice(id) {
    IdForProductInputs(id);

    $("#GetPriceForm").submit();
}

function OnLoadingGetPrice() {


}

function OnCompleteGetPrice(xhr) {
    var Data = xhr.responseJSON.object;
    FillPriceItem(Data);
    $("#PriceModal").modal("show");
}

function OnErrorGetPrice() {

}

function OnLoadingEditPrice() {


}

function OnCompleteEditPrice(xhr) {

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    } else {
        if (xhr.responseJSON.isSuccessed) {
            var Data = xhr.responseJSON.object;
            FillPriceItem(Data);
            /*Filter();*/
            ToastSuccess("عملیات با موفقیت انجام شد جهت مشاهده تغییرات جدید لیست را مجدد بازنشانی فرمایید")
            //ClearDataSet();
            //tableMain
            //    .clear()
            //    .draw();
            //dataSet = [];

            //$("#GetDataForm").submit();
        } else {
            ToastError("اطلاعات وارد شده با خطا بارگذاری شده اند")
            console.log(xhr.responseJSON);
            xhr.responseJSON.errors.forEach(function (element) {
                ToastError(element.errorMessage);
            });
        }


    }

}

function OnErrorEditPrice() {

}


//Mojusi
function ShowProductRepresentationModal(productId, repId) {
    IdForProductInputs(productId);
    IdForInputs(repId);
    $("#ProductRepresentationModal").modal("show");
}

function ShowQuickProductRepresentationModal(productId, repId) {
    IdForProductInputs(productId);
    IdForInputs(repId);
    $("#QuickProductRepresentationModal").modal("show");
}

function OnLoadingAddPr() {


}

function OnCompleteAddPr(xhr) {
    console.log(xhr);
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    } else {
        if (xhr.responseJSON.isSuccessed) {
            //Filter();
            ToastSuccess("عملیات با موفقیت انجام شد جهت مشاهده تغییرات جدید لیست را مجدد بازنشانی فرمایید")
        } else {
            ToastError(xhr.responseJSON.message);
        }

    }


}

function OnErrorAddPr() {

}

function OnLoadingQuickAddPr() {


}

function OnCompleteQuickAddPr(xhr) {

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    } else {

        Filter();
        ToastSuccess("عملیات با موفقیت انجام شد")
        ClearDataSet();
        tableMain
            .clear()
            .draw();
        dataSet = [];
        Parameter_CurrentPage = currentPage;
        $("#GetDataForm").submit();
    }


}

function OnErrorQuickAddPr() {

}


function Delete(rep, id) {
    var productStockPriceId = document.getElementById("productStockPriceId");
    var repId = document.getElementById("repId");
    productStockPriceId.value = id;
    repId.value = rep;

    swal({
        title: '  آیا از حذف کالا از انبار مطمئن هستید؟',
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#f44336',
        cancelButtonColor: '#777',
        confirmButtonText: 'بله، حذف شود. '
    }).then(function () {
        $("#DeleteProductStockPrice").submit();
    }, function (dismiss) {
        if (dismiss === 'cancel') {
            swal(
                'لغو گردید',
                ' لغو عملیات حذف کالا از انبار .',
                'error'
            ).catch(swal.noop);
        }
    }).catch(swal.noop);


}

function OnCompleteDelete(xhr) {
    console.log(xhr);
    var result = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    } else {

        if (result.isSuccessed) {
            ToastSuccess("عملیات با موفقیت انجام شد جهت مشاهده تغییرات جدید لیست را مجدد بازنشانی فرمایید");
            //BlankFilter();
        } else {
            ToastError("عملیات حذف به دلیل وجود سفارش از این کالا امکان پذیر نیست")
        }

    }
}