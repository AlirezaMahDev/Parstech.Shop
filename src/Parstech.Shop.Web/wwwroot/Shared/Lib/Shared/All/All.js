var SearchFilter = document.getElementById("SearchFilter");
var SearchFilterMobile = document.getElementById("SearchFilterMobile");
var SearchSection = document.getElementById("SearchSection");
var SearchSectionMobile = document.getElementById("SearchSectionMobile");

$(document).ready(function () {
    console.log("fsvdfffffffffffff");
    GetCountOfOpenOrder();
    DiscountGet();
});

SearchFilter.onkeyup = function (e) {
    if (e.key == " " ||
        e.code == "Space" ||
        e.keyCode == 32 ||
        e.code == "Enter"
    ) {
        Search();
    }
}

function Search() {
    $.ajax({
        type: "POST",
        url: "/Index?handler=Search",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {"Filter": SearchFilter.value},
        success: function (response) {
            SearchResult(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}


function SearchResult(response) {
    console.log(response);
    SearchSection.innerHTML = null;
    SearchSection.insertAdjacentHTML("beforeend", "<div class='w-100' style='position:absolute;'><div class='card'><div class='card-header'><button type='button' class='close ' onclick='CloseSearch()'>×</button><span class='modal-title pr-1 font-weight-bold'>جستجوی محصولات </span></div><div class='card-body border-top-primary border-top border-width-2 rtl' ><div class='row' id='SearchResult'></div></div></div></div>");
    var SearchResult = document.getElementById("SearchResult");
    response.object.forEach(function (element) {
        SearchResult.insertAdjacentHTML("beforeend", "<div class='bg-gray-7  col-lg-3 col-xl-3  mb-md-0 border-width-2 border-color-1 productImageBackRadius'><div class='productImage m-1 mt-3'><div class='product-item__outer h-100'><div class='product-item__inner px-xl-2 p-2'><div class='product-item__body pb-xl-2'><div class='mb-2'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class='font-size-12 text-gray-5' tabindex='0'></a></div><div class='mb-2'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class='d-block text-center' tabindex='0'><img class='img-fluid' src='/Shared/Images/Products/" + element.image + "' alt=''></a></div><h6 class='font-size-12 mb-1 product-item__title'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "' class=' font-weight-bold' tabindex='0'>" + element.name + "</a></h6><div class='flex-center-between mt-4 mb-1'></div></div></div></div></div></div>")
    });

    SearchResult.insertAdjacentHTML("beforeend", "<div><button onclick='GoMore()' class='btn btn-default pointer'>جستجوی موارد بیشتر <i class='ec ec-arrow-right-categproes'></i> </button></div>");
}

function GoMore() {
    window.location.href = "/Products/Filter/" + SearchFilter.value
}

function CloseSearch() {
    SearchSection.innerHTML = null;
}


//search mobile
SearchFilterMobile.onkeyup = function (e) {
    if (e.key == " " ||
        e.code == "Space" ||
        e.keyCode == 32 ||
        e.code == "Enter"
    ) {
        SearchMobile();
    }
}

function SearchMobile() {
    $.ajax({
        type: "POST",
        url: "/Index?handler=Search",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {"Filter": SearchFilterMobile.value},
        success: function (response) {
            SearchResultMobile(response);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function SearchResultMobile(response) {
    console.log(response);
    SearchSectionMobile.innerHTML = null;
    SearchSectionMobile.insertAdjacentHTML("beforeend", "<div class='w-100 rtl' style='position:absolute;'><div class='card rtl'><div class='card-header rtl'><button type='button' class='close ' onclick='CloseSearchMobile()'>×</button><h6 class='modal-title'><i class=' fas fa-bag-shopping Orange m-1'></i>نتایج جستجو</h6></div><div style='text-align: right;' class='card-body border-top-primary border-top border-width-2 rtl' id='SearchResultMobile'></div></div></div>");
    var SearchResultMobile = document.getElementById("SearchResultMobile");
    response.object.forEach(function (element) {
        SearchResultMobile.insertAdjacentHTML("beforeend", "<div class='d-flex rtl'><a href='/Products/Detail/" + element.shortLink + "/" + element.productStockPriceId + "'><img width='30' src='/Shared/Images/Products/" + element.image + "' /><small class='Black pr-3 font-weight-bold'>" + element.name.substring(0, 40) + " ...</small></a></div>")
    });
    SearchResultMobile.insertAdjacentHTML("beforeend", "<div><button onclick='GoMoreMobile()' class='btn btn-default pointer'>جستجوی موارد بیشتر <i class='ec ec-arrow-right-categproes'></i> </button></div>");

}


function GoMoreMobile() {
    window.location.href = "/Products/Filter/" + SearchFilterMobile.value
}

function CloseSearchMobile() {
    SearchSectionMobile.innerHTML = null;
}


function AddToFavorite(productId) {
    $.ajax({
        type: "POST",
        url: "/Index?handler=Favorite",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {"ProductId": productId},
        success: function (response) {
            console.log(response);
            RunSwl("محصول مورد علاقه شما ثبت شد", "success", response.message);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}


function AddToCompare(productId) {
    $.ajax({
        type: "POST",
        url: "/Index?handler=Compare",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {"ProductId": productId},
        success: function (response) {
            console.log(response);
            if (response.isSuccessed) {
                RunSwl("مقایسه محصول", "success", response.message);
            } else {
                RunSwl("مقایسه محصول", "error", response.message);
            }
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function DeleteFromCompare(userproductId) {
    $.ajax({
        type: "POST",
        url: "/Index?handler=CompareDelete",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {"userProductId": userproductId},
        success: function (response) {
            console.log(response);
            if (response.isSuccessed) {
                RunSwl("مقایسه محصول", "success", response.message);
                $("#GetDataForm").submit();
            } else {
                RunSwl("مقایسه محصول", "error", response.message);
            }
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}


function CreateCheckout(id) {
    $.ajax({
        type: "POST",
        url: "/Index?handler=CheckIsAuthenticated",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (response) {
            if (response.isSuccessed) {
                CreateOrder(id);

            } else {
                RunSwl("وارد شوید", "error", response.message);
            }

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function CreateOrder(id) {
    $.ajax({
        type: "POST",
        url: "/Index?handler=CreateCheckout",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {"productId": id},
        success: function (response) {
            if (response.isSuccessed) {
                GetCountOfOpenOrder();
                RunSwl("سبد خرید", "success", response.message);
            } else {
                RunSwl("سب خرید", "error", response.message);
            }

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}


function UserNotLogin() {
    RunSwl("وارد شوید", "error", "ابتدا وارد حساب کاربری خود شوید");
}

function RunSwl(caption, type, message) {
    console.log(message);
    swal({
            title: caption,
            type: type,
            text: message,
            showCancelButton: false,
            confirmButtonColor: '#45ff89',
            cancelButtonColor: '#777',
            confirmButtonText: 'متوجه شدم'
        }
    );
}

function RunSwlSoon() {

    swal({
            title: "به زودی",
            type: "success",
            text: "به زودی ای بخش اضافه خواهد شد",
            showCancelButton: false,
            confirmButtonColor: '#45ff89',
            cancelButtonColor: '#777',
            confirmButtonText: 'متوجه شدم'
        }
    );
}


var countOfOpenOrder = document.getElementById("countOfOpenOrder");
var MobileMenu = document.getElementById("MobileMenu");

function GetCountOfOpenOrder() {
    $.ajax({
        type: "POST",
        url: "/Index?handler=CountOpenOrder",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (response) {
            if (response.isSuccessed) {
                countOfOpenOrder.innerHTML = null;
                if (response.object > 0) {
                    countOfOpenOrder.insertAdjacentHTML("afterbegin", "<a href='/Checkout' class='text-gray-90 position-relative d-flex ' data-toggle='tooltip' data-placement='top' title='سبد خرید'><div class='badge badge-primary p-2 text-white font-weight-bold' style='border-radius:70px'>" + response.object + "</div><i class='font-size-22 ec ec-shopping-bag'></i></a>");

                } else {
                    countOfOpenOrder.insertAdjacentHTML("afterbegin", "<a href='/Checkout' class='text-gray-90 position-relative d-flex ' data-toggle='tooltip' data-placement='top' title='سبد خرید'><i class='font-size-22 ec ec-shopping-bag'></i></a>");

                }
            }
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}


var buttons = document.querySelectorAll('.MyBtn');


buttons.forEach(function (btn) {
    btn.addEventListener('mouseover', function () {
        //console.log("migmig");
        btn.style.transform = 'scale(0.9)';
        btn.style.transition = 'transform 0.3s ease';
    });

    btn.addEventListener('mouseout', function () {
        btn.style.transform = 'scale(1)';
        btn.style.transition = 'transform 0.3s ease';
    });
});


function DisountCounter(id) {
    console.log("dddd");
    let date = document.getElementById("date-" + id);
    if (date.value != null) {
        const myArray = date.value.split("/");
        let newDate = myArray[0] + "-" + myArray[1] + "-" + myArray[2];
        console.log(newDate);
        var targetDate = new Date(newDate).getTime();

        // تابعی برای محاسبه زمان باقی‌مانده
        function updateCountdown() {
            var now = new Date().getTime();
            var distance = targetDate - now;

            var days = Math.floor(distance / (1000 * 60 * 60 * 24));
            var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            var seconds = Math.floor((distance % (1000 * 60)) / 1000);


            document.getElementById(id).innerHTML = "<button class='btn transition-3d-hover font-weight-bold text-red' > <i class=' fas fa-clock-rotate-left text-red' style='font-size:10px !important'> </i> <small class='text-black font-weight-bold' style='font-size:10px !important'>تخفیف تا </small >" + seconds + " : " + minutes + " : " + hours + " : " + days + "  </button><div class='rounded-pill bg-gray-3 height-10 position-relative'><span class='position-absolute left-0 top-0 bottom-0 rounded-pill w-60 bg-Red'></span></div>";

            //days + " روز " + hours + " ساعت "
            //    + minutes + " دقیقه " + seconds + " ثانیه ";

            if (distance < 0) {
                clearInterval(interval);
                document.getElementById(id).innerHTML = "<button class='btn transition-3d-hover font-weight-bold text-red' > <i class=' fas fa-clock-rotate-left text-red' style='font-size:10px !important'> </i> <small class='text-black font-weight-bold' style='font-size:10px !important'>تخفیف </small >منقضی شده</button><div class='rounded-pill bg-gray-3 height-10 position-relative'><span class='position-absolute left-0 top-0 bottom-0 rounded-pill w-60 bg-Red'></span></div>";
            }
        }

        // اجرای تابع بالا هر ثانیه
        var interval = setInterval(updateCountdown, 1000);
    }


}

//// تاریخ شمسی را به شکل Timestamp تنظیم می‌کنیم
//var targetDate = new Date('2024-03-19').getTime(); // مثال: 19 مارس 2024

//// فراخوانی تابع برای شمارش معکوس با تاریخ شمسی
//persianDateCountdown(targetDate);


function DiscountGet() {
    var IdList = document.querySelectorAll('.Discount');
    IdList.forEach(function (input) {
        DisountCounter(input.value);
        //input.value = id;
    });
}


