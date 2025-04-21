var sale = [];
var base = [];
var price = [];
var discount = [];
var priceLabel = [];
var config2 = {};
var chartPrice = document.getElementById("chartPrice");

var PriceLogdataSet = [];
var PriceLogtableMain;
var PrdataSet = [];
var PrableMain;


var PrName = [];
var PrCount = [];
var PrColor = [];
var chartPr = document.getElementById("chartPr");



function FilterPriceLog() {
    PriceLogtableMain
        .clear()
        .draw();
    PriceLogdataSet = [];
}
function FilterPrLog() {
    PrableMain
        .clear()
        .draw();
    PrdataSet = [];
}


function FillPriceLogDataSet(Data) {
   // console.log(Data);
    Data.list.forEach(function (element) {
       // console.log(element);
        const data =
            [
                "<h5>" + element.createDateShamsi + "</h5>",
                "<h5>" + element.productLogTypeName + "</h5>",
                "<h5 class=' font-weight-bold Red'>" + separate(element.oldValue) + "</h5>",
                "<h5 class=' font-weight-bold Green'>" + separate(element.newValue) + "</h5>",
                "<h5 class=' font-weight-bold'>" + element.userName + "</h5>",
            ];
        PriceLogdataSet.push(data);
    });
}

function FillPrLogDataSet(Data) {
   console.log(Data);
    Data.list.forEach(function (element) {
       // console.log(element);
        const data =
            [
                "<h5>" + element.uniqeCode + "</h5>",
                "<h5>" + element.createDateShamsi + "</h5>",
                "<h5>" + element.type + "</h5>",
                "<h5 class=' font-weight-bold'>" + element.quantity + "</h5>",
                "<h5 class=' font-weight-bold'>" + element.userName + "</h5>",
            ];
        PrdataSet.push(data);
    });
}
function OnLoadingGetLogs() {
    sale = [];
    base = [];
    price = [];
    discount = [];
    saleLabel = [];
    baseLabel = [];
    priceLabel = [];
    discountLabel = [];
    chartPrice.innerHTML = null;

    PrName = [];
    PrCount = [];
    PrColor = [];
    chartPr.innerHTML = null;
    
    if (PriceLogdataSet.length > 0) {
        FilterPriceLog();
    }
    if (PrdataSet.length > 0) {
        FilterPrLog();
    }
}
function OnCompleteGetLogs(xhr) {
   // console.log(xhr);
    //chart Price
    var Data = xhr.responseJSON.object.logDto;
    Data.baseLogDtos.forEach(function (element) {
        base.push(element.newValue);
        baseLabel.push(element.createDate);
    });
    Data.discountLogDtos.forEach(function (element) {
        discount.push(element.newValue);
        discountLabel.push(element.createDate);
    });
    Data.priceLogDtos.forEach(function (element) {
        price.push(element.newValue);
        priceLabel.push(element.createDateShamsi);
    });
    Data.saleLogDtos.forEach(function (element) {
        sale.push(element.newValue);
        saleLabel.push(element.createDate);
    });
    chartPrice.insertAdjacentHTML("afterbegin", "<canvas id='line2' class='min-height-300'></canvas>")
    ConfigChart();
    


    //Table Price
    var DataPriceLog = xhr.responseJSON.object.productLogPaging;
    FillPriceLogDataSet(DataPriceLog);
    PriceLogtableMain = $('#data-table2').DataTable({
        "searching": false,
        "paging": false,
        data: PriceLogdataSet,
        columns: [

            { title: 'تاریخ' },
            { title: 'تغییر' },
            { title: 'قیمت قبلی' },
            { title: 'قیمت ویرایش شده' },
            { title: 'توسط' },
        ],
        "pageLength": 25
    });
    PriceLogtableMain.destroy();


    //Pr Chart
    var prData = xhr.responseJSON.object.productRepresntationChart;
    prData.forEach(function (element) {
        PrName.push(element.name);
        PrCount.push(element.count);

        var randomColorFactor = function () {
            return Math.round(Math.random() * 255);
        };
        var randomColor = function (opacity) {
            return 'rgba(' + randomColorFactor() + ',' + randomColorFactor() + ',' + randomColorFactor() + ',' + (opacity || '.3') + ')';
        };
        PrColor.push(randomColor);
        
    });
   // console.log(PrColor);
    chartPr.insertAdjacentHTML("afterbegin", " <canvas id='pie1'></canvas>")
    ConfigChart2();
    
    //pr Table
    var DataPrLog = xhr.responseJSON.object.productRepresntationPaging;
    FillPrLogDataSet(DataPrLog);
    PrableMain = $('#data-table3').DataTable({
        "searching": false,
        "paging": false,
        data: PrdataSet,
        columns: [

            { title: 'شناسه' },
            { title: 'تاریخ' },
            { title: 'درخواست' },
            { title: 'تعداد' },
            { title: 'توسط' },
           
        ],
        "pageLength": 25
    });
    PrableMain.destroy();


    $("#LogsModal").modal("show");
}
function OnErrorGetLogs() {

}







function ConfigChart() {
    Chart.defaults.global.defaultFontFamily = "IranSans";
    config2 = {
        type: "line",
        data: {
            labels: priceLabel,
            datasets: [{
                //backgroundColor: "rgba(151,187,205,0.5)",
                borderColor: "#40cd43",
                borderWidth: 2,
                label: "قیمت خرید",
                discription: "قیمت خرید",
                data: price,
            }
                , {

                borderColor: "#ff7800",
                borderWidth: 2,
                label: "قیمت فروش",
                data: sale,
            }, {

                borderColor: "#ff006f",
                borderWidth: 2,
                label: "قیمت تخفیف",
                data: discount,
            }, {

                borderColor: "#40e5e9",
                borderWidth: 2,
                label: "قیمت خام",
                data: base,
            }]
        },
        options: {
            maintainAspectRatio: false,
            responsive: true,
            title: {
                display: true
            },
            hover: {
                mode: "nearest",
                intersect: true
            },
            scales: {
                xAxes: [{
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: "تاریخ"
                    }
                }],
                yAxes: [{
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: "مبلغ"
                    },
                    ticks: {
                        suggestedMin: 0,
                        suggestedMax: 900000
                    },
                }]
            }
        }
    };
    var ctx = document.getElementById("line2").getContext("2d");
    window.line2 = new Chart(ctx, config2);
}
function ConfigChart2() {
   // console.log(PrCount);
    Chart.defaults.global.defaultFontFamily = "IranSans";
    var config1 = {
        type: 'pie',
        data: {
            labels: PrName,
            datasets: [{
                backgroundColor: [
                    
                    
                    "#31e100",
                    "#ff003b",
                    "#ff9014",
                    
                    "#206bff",
                ],
                borderWidth: 3,
                label: "شهر تهران",
                data: PrCount,
            }]
        },
        options: {
            responsive: true
        }
    };

    /*window.onload = function () {*/
        var ctx1 = document.getElementById("pie1").getContext("2d");
        window.pie1 = new Chart(ctx1, config1);
    //};

}



function ShowLogModal(id) {
    IdForProductInputs(id);
    $("#GetLogsForm").submit();

}
    //window.onload = function () {

    //};

