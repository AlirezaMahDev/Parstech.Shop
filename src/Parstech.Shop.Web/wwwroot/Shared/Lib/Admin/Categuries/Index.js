var dataSet = [];
var tableMain;
var FilterInput = document.getElementById("FilterInput");
var Parameter_Filter = document.getElementById("Parameter_Filter");
var paging = document.getElementById("paging");


$(window).on('resize', function () {
    $('#data-table').css("width", "100%");
});


$(document).ready(function () {
    Parameter_CurrentPage.value = 1;
    $("#GetDataForm").submit();

});


function Filter() {

    tableMain
        .clear()
        .draw();
    dataSet = [];

    Parameter_Filter.value = FilterInput.value;
    $("#GetDataForm").submit();
}


function FillDataSet(Data) {
    console.log(Data);
    Data.list.forEach(function (element) {
        // console.log(element);
        let show;
        let showColor;
        if (element.show) {
            show = "نمایش"
            showColor = "Green";
        } else {
            show = "عدم نمایش";
            showColor = "Red";
        }

        switch (element.sath) {
            case 1:
                const data =
                    [
                        "<h5 class='font-weight-bold w-100'>" + element.groupTitle + "</h5>",
                        "<h5 class='font-weight-bold'></h5>",
                        "<h5 class='font-weight-bold'></h5>",
                        "<h5 class='font-weight-bold'></h5>",
                        "<h5 class='" + showColor + " font-weight-bold'>" + show + "</h5>",
                        "<button onclick='GetCategurySubmit(" + element.groupId + ")' class='btn btn-sm btn-block btn-success curve m-1'>ویرایش </button><button onclick='DeleteCat(" + element.groupId + ")' class='btn btn-sm btn-block btn-danger curve m-1'>حذف</button>",
                        "<h5 class='Green'>اصلی(والد)</h5>",
                    ];
                dataSet.push(data);
                break;
            case 2:
                const data2 =
                    [
                        "<h5 class='font-weight-bold'></h5>",
                        "<h5 class='font-weight-bold w-100'>" + element.groupTitle + "</h5>",

                        "<h5 class='font-weight-bold'></h5>",
                        "<h5 class='font-weight-bold'></h5>",
                        "<h5 class='" + showColor + " font-weight-bold'>" + show + "</h5>",
                        "<button onclick='GetCategurySubmit(" + element.groupId + ")' class='btn btn-sm btn-block btn-success curve m-1'>ویرایش </button><button onclick='DeleteCat(" + element.groupId + ")' class='btn btn-sm btn-block btn-danger curve m-1'>حذف</button>",
                        "<h5 class=''>" + element.parentTitle + "</h5>",
                    ];
                dataSet.push(data2);
                break;
            case 3:
                const data3 =
                    [
                        "<h5 class='font-weight-bold'></h5>",
                        "<h5 class='font-weight-bold'></h5>",
                        "<h5 class='font-weight-bold w-100'>" + element.groupTitle + "</h5>",

                        "<h5 class='font-weight-bold'></h5>",
                        "<h5 class='" + showColor + " font-weight-bold'>" + show + "</h5>",
                        "<button onclick='GetCategurySubmit(" + element.groupId + ")' class='btn btn-sm btn-block btn-success curve m-1'>ویرایش </button><button onclick='DeleteCat(" + element.groupId + ")' class='btn btn-sm btn-block btn-danger curve m-1'>حذف</button>",
                        "<h5 class=''>" + element.parentTitle + "</h5>",
                    ];
                dataSet.push(data3);
                break;
            case 4:
                const data4 =
                    [
                        "<h5 class='font-weight-bold'></h5>",
                        "<h5 class='font-weight-bold'></h5>",
                        "<h5 class='font-weight-bold'></h5>",
                        "<h5 class='font-weight-bold w-100'>" + element.groupTitle + "</h5>",


                        "<h5 class='" + showColor + " font-weight-bold'>" + show + "</h5>",
                        "<button onclick='GetCategurySubmit(" + element.groupId + ")' class='btn btn-sm btn-block btn-success curve m-1'>ویرایش </button><button onclick='DeleteCat(" + element.groupId + ")' class='btn btn-sm btn-block btn-danger curve m-1'>حذف</button>",
                        "<h5 class=''>" + element.parentTitle + "</h5>",
                    ];
                dataSet.push(data4);
                break;
            case 5:
                break;
            case 6:
                break;
        }


    });
    var first = 1;
    var current = Data.currentPage;
    var next = Data.currentPage + 1;
    var privious = Data.currentPage - 1;
    var last = Data.pageCount + 1;

    if (next > last) {
        next = Data.currentPage;
    }
    if (privious < 1) {
        privious = Data.currentPage;
    }

    // console.log("first" + first);
    // console.log("perivous" + privious);
    // console.log("next" + next);
    // console.log("last" + last);
    paging.innerHTML = null;
    //pagingHeader.innerHTML = null;
    //pagingHeader.insertAdjacentHTML("beforeend", "<h6 class='m-2'>صفحه " + current + " از " + last + "</h5>");
    paging.insertAdjacentHTML("beforeend", "<button onclick='RunPaging(" + first + ")' class='btn cart-Blue btn-xs font-weight-bold ml-2'>اولین صفحه<i class=' fas fa-circle-arrow-right p-1'></i></button><button onclick='RunPaging(" + privious + ")' class='btn cart-Green btn-xs font-weight-bold ml-2'><i class=' fas fa-arrow-right p-1'></i></button><button onclick='RunPaging(" + next + ")' class='btn cart-Green btn-xs font-weight-bold ml-2'><i class=' fas fa-arrow-left p-1'></i></button><button onclick='RunPaging(" + last + ")' class='btn cart-Blue btn-xs font-weight-bold ml-2'><i class=' fas fa-circle-arrow-left p-1'></i>آخرین صفحه</button> <h6 class='m-2'>صفحه " + current + " از " + last + "</h5>");

}

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
    //console.log(xhr);
    var Data = xhr.responseJSON.object;
    FillDataSet(Data);


    tableMain = $('#data-table').DataTable({
        "searching": false,
        "paging": false,
        data: dataSet,
        columns: [
            {title: 'سطح یک'},
            {title: 'سطح دو'},
            {title: 'سطح سه'},
            {title: 'سطح چهار'},
            {title: 'منو'},
            {title: 'عملیات'},
            {title: 'ارث بری'},

        ],


        "pageLength": 25
    });
    tableMain.destroy();


}

function OnErrorData() {

}


function IdForInputs(id) {
    var IdList = document.querySelectorAll('.categuryId');
    IdList.forEach(function (input) {
        input.value = id;
    });
}

function GetCategurySubmit(id) {
    IdForInputs(id);
    $("#GetCateguryForm").submit();
}

//دریافت محصول
var CateguryDto_GroupId = document.getElementById("CateguryDto_GroupId");
var CateguryDto_GroupTitle = document.getElementById("CateguryDto_GroupTitle");
var CateguryDto_LatinGroupTitle = document.getElementById("CateguryDto_LatinGroupTitle");
var CateguryDto_ParentId = document.getElementById("CateguryDto_ParentId");
var CateguryDto_IsParnet = document.getElementById("CateguryDto_IsParnet");
var CateguryDto_Row = document.getElementById("CateguryDto_Row");


function CleanItem() {

    CateguryDto_GroupId.value = null;
    CateguryDto_GroupTitle.value = null;
    CateguryDto_LatinGroupTitle.value = null;
    CateguryDto_ParentId.value = null;
    CateguryDto_IsParnet.value = false;
    CateguryDto_Row.value = null;
    CateguryDto_ParentId.innerHTML = null;


    $("#GetAllCatForm").submit();

}

function FillItem(element) {
    $("#GetAllCatForm").submit();
    console.log(element);
    CateguryDto_GroupId.value = element.groupId;
    CateguryDto_GroupTitle.value = element.groupTitle;
    CateguryDto_LatinGroupTitle.value = element.latinGroupTitle;
    CateguryDto_ParentId.value = element.parentId;
    if (element.isParnet) {
        document.getElementById("base").checked = true;

    } else {
        document.getElementById("nobase").checked = true;
    }

    if (element.show) {
        document.getElementById("showMenu").checked = true;

    } else {
        document.getElementById("notShowMenu").checked = true;
    }
    CateguryDto_IsParnet.value = element.isParnet;
    CateguryDto_Row.value = element.row;

}

function OnLoadingGetItem() {
    //CleanProduct();
}

function OnCompleteGetItem(xhr) {
    // console.log(xhr);
    FillItem(xhr.responseJSON.object);
    $("#AddOrEditCateguryModal").modal("show");
}

function OnErrorGetItem() {

}


function OnLoadingAE() {
    var cpId = document.getElementById("cpId");

    //CleanProduct();
}

function OnCompleteAE(xhr) {
    //console.log(xhr);

    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    } else {
        FillItem(xhr.responseJSON.object);
        $("#AddOrEditCateguryModal").modal("hide");

        //tableMain
        //    .clear()
        //    .draw();
        //dataSet = [];
        //$("#GetDataForm").submit();

        ToastSuccess("عملیات با موفقیت انجام شد")
    }

}

function OnErrorAE() {

}


var cats = document.getElementById("cats");
var SearchCatText = document.getElementById("SearchCatText");

var FilterCatForm = document.getElementById("FilterCatForm");

function OnCompleteGetAllCat(xhr) {
    var Data = xhr.responseJSON.object;
    //console.log(Data);


    cats.innerHTML = null;

    cats.insertAdjacentHTML("beforeend", "<select class='form-control'  id='cpId'></select >");


    let active;
    cpId.insertAdjacentHTML("beforeend", "<option>پس از جسنجو انتخاب کنید</option>")
    Data.forEach(function (element) {
        if (CateguryDto_ParentId.value == element.groupId) {
            active = "selected";
        } else {
            active = "";
        }
        if (element.isParnet) {
            cpId.insertAdjacentHTML("beforeend", "<option  value='" + element.groupId + "' " + active + ">دسته بندی اصلی (" + element.groupTitle + ")</option>")
        } else {
            cpId.insertAdjacentHTML("beforeend", "<option value='" + element.groupId + "' " + active + ">" + element.groupTitle + "</option>")
        }

    });
    cats.insertAdjacentHTML("beforeend", "");
    cpId.addEventListener("change", (e) => {

        const targetValue = e.target.value;
        CateguryDto_ParentId.value = targetValue;

    });
}

function SearchCat() {
    FilterCatForm.value = SearchCatText.value;
    $("#GetAllCatForm").submit();
}


//Delete Ctaegury


function DeleteCat(id) {
    IdForInputs(id);
    $("#DeleteCateguryForm").submit();
}

function OnCompleteDeleteItem(xhr) {
    var response = xhr.responseJSON;
    if (xhr.status != 200) {
        ToastError("در خواست شما با شکست مواجه شده است")

    } else {
        if (response.isSuccessed) {
            ToastSuccess(response.message);
            ToastSuccess("صفحه را مجدد فراخوانی کنید");

        } else {
            ToastError(response.message);
        }
    }

}
