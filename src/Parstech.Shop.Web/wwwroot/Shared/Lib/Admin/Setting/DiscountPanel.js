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

    fetch('/Components/GetDiscountProductsSelect')
        .then(response => response.text())
        .then(html => {
            document.getElementById("ProductsSelect").innerHTML = html;
        })
        .catch(error => console.error('Error:', error));

    fetch('/Components/GetDiscountSectionSelect')
        .then(response => response.text())
        .then(html => {
            document.getElementById("SectionSelect").innerHTML = html;
            document.getElementById("SectionSelect2").innerHTML = html;
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
    //$("#GetDataForm").submit();
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
        var ShowInPanel;
        var CateguryOfUserId="";


        
        
            switch (element.typeId) {
                case 1:
                    typeHtml = "<small>" + element.id + "</small><h5 class='badge badge-success'>ساده</h5>"
                    ActionHtml = "<div class='btn-group'><button onclick='GetModal(" + element.productStockPriceId + ")' type='button' class='btn btn-default'>ویرایش</button></div>"
                    break;
                case 2:
                    typeHtml = "<small>" + element.id + "</small><h5 class='badge badge-danger'>متغیر</h5>"
                    ActionHtml = "";
                    break;
                case 3:
                    typeHtml = "<small>" + element.id + "</small><h5 class='badge badge-warning'>زیرمجموعه</h5>"
                    ActionHtml = "<div class='btn-group'><button onclick='GetModal(" + element.productStockPriceId + ")' type='button' class='btn btn-default'>ویرایش</button></div>"

                    break;
                case 4:
                    typeHtml = "<small>" + element.id + "</small><h5 class='badge badge-info'>باندل</h5>"
                    ActionHtml = "<div class='btn-group'><button onclick='GetModal(" + element.productStockPriceId + ")' type='button' class='btn btn-default'>ویرایش</button></div>"

                    break;
                case 5:
                    typeHtml = "<small>" + element.id + "</small><h5 class='badge badge-info'>زیرمجموعه باندل</h5>"
                    ActionHtml = "<div class='btn-group'><button onclick='GetModal(" + element.productStockPriceId + ")' type='button' class='btn btn-default'>ویرایش</button></div>"

                    break;
        }
        if (element.showInDiscountPanels == true) {
           ShowInPanel= "<h5 class='badge badge-success BgGreen'>نمایش در پنل تخفیف</h5>"
        }
        else {
           ShowInPanel= "<h5 class='badge badge-danger BgRed'>عدم نمایش در پنل تخفیف</h5>"
        }

        if (element.categuryOfUserId != null) {
            CateguryOfUserId = "<h5 class='badge badge-success BgBlue'>ویژه همکاران بانک ملی ایران</h5>"
        }
        
        
        console.log(element);

        const data =
            [
                typeHtml,
                "<h5>" + element.name + " " + CateguryOfUserId +"</h5><h5 class='Orange font-weight-bold'>" + element.code + "</h5>",
                "<h5 class=' font-weight-bold'>" + element.quantity + " عدد</h5>",
                "<h5 class=' font-weight-bold'>" + separate(element.salePrice) + " تومان</h5><h5 class='Red font-weight-bold'>" + separate(element.discountPrice) + " تومان <small>(قیمت شگفت انگیز)</small></h5>",
                ShowInPanel,
                ActionHtml
            ];
        dataSet.push(data);


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

var Rep = document.getElementById("RepId");
var Parameter_Filter = document.getElementById("Parameter_Filter");
var Parameter_Categury = document.getElementById("Parameter_Categury");
//var Parameter_Type = document.getElementById("Parameter_Type");
var Parameter_Exist = document.getElementById("Parameter_Exist");
var ProductsSelect = document.getElementById("ProductsSelect");
var CategurySelect = document.getElementById("CategurySelect");
//var TypeSelect = document.getElementById("TypeSelect");
var Parameter_SectionId = document.getElementById("Parameter_SectionId");
var SectionSelect = document.getElementById("SectionSelect");
function DataSubmit() {
    // console.log(Rep.value);
    IdForInputs(Rep.value);
    Parameter_Filter.value = ProductsSelect.value;
    Parameter_Categury.value = CategurySelect.value;
    //Parameter_Type.value = TypeSelect.value;
    Parameter_Exist.value = ExistSelect.value;
    Parameter_SectionId.value = SectionSelect.value;
    /*Parameter_CurrentPage.value = page;*/
    if (dataSet.length > 0) {
        Filter();
    }

    console.log(Parameter_Filter.value);
    console.log(ProductsSelect.value);
    $("#GetDataForm").submit();
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

            { title: 'نوع' },
            { title: 'نام محصول' },
            { title: 'موجودی' },
            { title: 'قیمت فروش(تومان)' },
            { title: 'پنل تخفیف' },
            { title: 'عملیات' },
        ],
        "columnDefs": [{
            "targets": [1,3],
            "className": 'w-30',
           

        }],
        "pageLength": 25
    });
    tableMain.destroy();


}

function OnErrorData() {

}





function IdForProductInputs(id) {

    var IdList = document.querySelectorAll('.ProductId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}
function IdForProductStockPriceInputs(id) {

    var IdList = document.querySelectorAll('.productStockPriceId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}



//section
var showInPanel = document.getElementById("showInPanel");
var addSection = document.getElementById("addSection");
var sections = document.getElementById("sections");





function GetModal(id) {
    IdForProductStockPriceInputs(id);

    $("#GetSetionsOfProductStockPrice").submit();
}

function OnLoadingGet() {


}
function OnCompleteGet(xhr) {
    var Data = xhr.responseJSON;
    console.log(Data);
    showInPanel.innerHTML = null;
    sections.innerHTML = null;
    FillSectionItem(Data);
    $("#Modal").modal("show");
}

function FillSectionItem(Data) {
    IdForProductStockPriceInputs(Data.produtSrockPriceId);
    if (Data.showInDiscountPanels == true) {

        showInPanel.insertAdjacentHTML("beforeend", "<button class='btn round BgYellow font-weight-bold'>نمایش در پنل تخفیف</button><button class='btn round BgGray font-weight-bold' OnClick='ChangeShowInDiscountPanel(" + Data.produtSrockPriceId +",0)'>عدم نمایش در پنل تخفیف</button>");
    }
    else {
        showInPanel.insertAdjacentHTML("beforeend", "        <button class='btn round font-weight-bold' OnClick='ChangeShowInDiscountPanel(" + Data.produtSrockPriceId +",1)'>نمایش در پنل تخفیف</button><button class='btn round BgYellow BgGray font-weight-bold' >عدم نمایش در پنل تخفیف</button>");
    }
    Data.sections.forEach(function (element) {
        sections.insertAdjacentHTML("beforeend", "<tr role='row' ><td class=''><h5 class=''>" + element.sectionName + "</h5></td><td class=' w-50'><button onclick='DeleteSection(" + element.id + "," + Data.produtSrockPriceId +")' class='btn btn-sm btn-block BgRed text-white curve m-1'>حذف </button></td></tr>");
    });

}

function OnErrorGet() {

}

var SectionId = document.getElementById("SectionId");
function DeleteSection(id,productStockPriceId) {
    IdForProductStockPriceInputs(productStockPriceId);
    SectionId.value = id;
    $("#DeleteSetionsOfProductStockPrice").submit();
}



var isShow = document.getElementById("isShow");
function ChangeShowInDiscountPanel(id,show) {
    IdForProductStockPriceInputs(id);
    isShow.value = show;
    $("#ChangeShowInDiscountPanelForm").submit();
}
function OnCompleteChange(xhr) {
    var Data = xhr.responseJSON;
    console.log(xhr);
    if (xhr.status==200) {
        ToastSuccess("عملیات با موفقیت انجام شد");
        showInPanel.innerHTML = null;
        if (Data.showInDiscountPanels == true) {

            showInPanel.insertAdjacentHTML("beforeend", "<button class='btn round BgYellow font-weight-bold'>نمایش در پنل تخفیف</button><button class='btn round BgGray font-weight-bold' OnClick='ChangeShowInDiscountPanel(" + Data.id + ",0)'>عدم نمایش در پنل تخفیف</button>");
        }
        else {
            showInPanel.insertAdjacentHTML("beforeend", "        <button class='btn round font-weight-bold' OnClick='ChangeShowInDiscountPanel(" + Data.id + ",1)'>نمایش در پنل تخفیف</button><button class='btn round BgYellow BgGray font-weight-bold' >عدم نمایش در پنل تخفیف</button>");
        }
    }
    else {
        ToastError("عملیات با خطا روبه رو شد");
    }
    

}
function OnCompleteChangeSection(xhr) {
    var Data = xhr.responseJSON;
    console.log(xhr);
    if (xhr.status==200) {
        ToastSuccess("عملیات با موفقیت انجام شد");
        sections.innerHTML = null;
        Data.sections.forEach(function (element) {
            sections.insertAdjacentHTML("beforeend", "<tr role='row' ><td class=''><h5 class=''>" + element.sectionName + "</h5></td><td class=' w-50'><button onclick='DeleteSection(" + element.id + "," + Data.produtSrockPriceId + ")' class='btn btn-sm btn-block BgRed text-white curve m-1'>حذف </button></td></tr>");
        });
    }
    else {
        ToastError("عملیات با خطا روبه رو شد");
    }
    

}



