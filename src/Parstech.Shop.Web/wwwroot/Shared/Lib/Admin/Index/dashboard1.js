var MapData = [];
$(document).ready(function () {
    $("#GetDataForm").submit();
});

function refresh() {
    Clean();
    $("#GetDataForm").submit();
}

var ROW = document.getElementById("ROW");
var Users = document.getElementById("Users");
var Products = document.getElementById("Products");
var Orders = document.getElementById("Orders");
var allTransaction = document.getElementById("allTransaction");
var Coin = document.getElementById("Coin");
var transaction = document.getElementById("transaction");
var Fecilities = document.getElementById("Fecilities");
var PreOrders = document.getElementById("PreOrders");
var Factors = document.getElementById("Factors");
var Time = document.getElementById("Time");


function Clean() {
    /* Time.insertAdjacentHTML = null;*/
    MapData = [];
    RepCount = [];
}

function OnLoading() {

    ToastSuccess("در حال به روزرسانی اطلاعات سامانه");

}

function OnComplete(xhr) {
    ROW.classList.replace("hidden", "show");
    ToastSuccess("به روزرسانی اطلاعات با موفقیت انجام شد");
    // console.log(xhr);
    var Data = xhr.responseJSON.object;

    Users.setAttribute("data-value", Data.userCount);
    Products.setAttribute("data-value", Data.productCount);
    Orders.setAttribute("data-value", Data.isLoadOrderCount);
    allTransaction.setAttribute("data-value", Data.allTransactionsCount);

    allTransaction.setAttribute("value", Data.coinTransactionsCount);
    allTransaction.setAttribute("data-max", Data.allTransactionsCount);

    transaction.setAttribute("value", Data.walletTransactionsCount);
    transaction.setAttribute("data-max", Data.allTransactionsCount);

    Fecilities.setAttribute("value", Data.facilitiesTransactionsCount);
    Fecilities.setAttribute("data-max", Data.allTransactionsCount);

    PreOrders.setAttribute("value", Data.pishFactorCount);
    PreOrders.setAttribute("data-max", Data.isLoadOrderCount);

    Factors.setAttribute("value", Data.souratHesabCount);
    Factors.setAttribute("data-max", Data.isLoadOrderCount);

    var Amtime = Data.time.replace("AM", "");
    var Finaltime = Amtime.replace("PM", "");
    // <i onclick='refresh()' title='بازخوانی' class='icon-refresh Orange' style='cursor:pointer'></i>
    Time.insertAdjacentHTML("beforeend", " <div class='breadcrumb-left '> آخرین بروزرسانی " + Finaltime + " <i class='icon-calendar'></i></div>");

    //chart
    console.log(Data.representationsProductsForChart);
    Data.representationsProductsForChart.forEach(function (element) {

        var Formatted = element.representationProducts + 'محصول';
        const data = {
            value: element.representationProducts, label: element.representationName, formatted: Formatted
        }
        RepCount.push(data);
    });


    console.log(RepCount);
    CreateChartRep(RepCount);
    //map

    console.log(Data.representationsProductsForMap);
    var longitude = 0;
    Data.representationsProductsForMap.forEach(function (element) {

        var item = element.representationName + ' : ' + element.representationSells + ' ریال ';

        var longitudePlus = element.longitude + longitude;
        const data = {
            svgPath: targetSVG,
            zoomLevel: 5,
            scale: 0.5,
            title: item,
            latitude: element.latitude,
            longitude: longitudePlus
        };
        MapData.push(data);
        longitude += 0.1500;
    });

    //var r = 35.7561;
    //var m=r + 0.0200;
    //const data = { svgPath: targetSVG, zoomLevel: 5, scale: 0.5, title: "انبار پرووان", latitude: "35.7561" , longitude: "51.4558" };
    //MapData.push(data);

    //const data2 = { svgPath: targetSVG, zoomLevel: 5, scale: 0.5, title: "انبار پرووان2", latitude: m, longitude: "51.3347" };
    //MapData.push(data2);

    CreateMap(MapData);

    $(".counter-down").incrementalCounter({digits: 'auto'});
    $('.knob-animate').each(function () {
        var $this = $(this);
        var val = $this.val();

        $this.knob();
        $({
            value: 0
        }).animate({
            value: val
        }, {
            duration: 2000,
            easing: "swing",
            step: function () {
                $this.val(Math.ceil(this.value)).trigger("change");
            }
        });
    });


}

function OnError() {

}


//map
var map;
var targetSVG = "M9,0C4.029,0,0,4.029,0,9s4.029,9,9,9s9-4.029,9-9S13.971,0,9,0z M9,15.93 c-3.83,0-6.93-3.1-6.93-6.93S5.17,2.07,9,2.07s6.93,3.1,6.93,6.93S12.83,15.93,9,15.93 M12.5,9c0,1.933-1.567,3.5-3.5,3.5S5.5,10.933,5.5,9S7.067,5.5,9,5.5 S12.5,7.067,12.5,9z";

function CreateMap(data) {
    map = new AmCharts.AmMap();
    map.imagesSettings = {
        rollOverColor: "#ffbd15",
        rollOverScale: 3,
        selectedScale: 5,
        selectedColor: "#575757"
    };
    map.areasSettings = {
        autoZoom: true,
        rollOverBrightness: 10,
        selectedBrightness: 100,
        unlistedAreasColor: "#ffa421"
    };
    var dataProvider = {
        mapVar: AmCharts.maps.iranHigh,
        images: data
    };
    map.dataProvider = dataProvider;
    map.write("map");
}

//Chart
var RepCount = [];

function CreateChartRep(data) {
    console.log(data);
    Morris.Donut({
        element: 'donut',
        data: data,
        //colors: [
        //    '#ffbd15',
        //    '#f55145',
        //    '#13a2a6',
        //    '#14B9D6'
        //],
        formatter: function (x, data) {
            return data.formatted;
        },
        resize: true
    });
}


// Countdown


// Realtime moris chart


// Knob with swing animation
