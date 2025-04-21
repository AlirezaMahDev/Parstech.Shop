$(document).ready(function () {
    checkActiveUser();
    $("#GetDataForm").submit();

});

var SlideShowSection = document.getElementById("thumbProgress");
var SlideCaptionSection = document.getElementById("thumbProgressNav");


function CleanData() {



}
function FillData(Data) {
    //Data.sectionDetails.forEach(function (element) {
    //    SlideShowSection.insertAdjacentHTML("beforeend", "<div class='js-slide' ><img class='SlideBack' style='position:absolute' src='/Shared/Images/a198a46183514e219da4cf47fe4ffbc9.webp' /><div class='row height-410-xl mx-0 align-items-center'><div class='col-md-6 mt-6 mt-md-0 mb-4 mb-md-0'><div class='ml-xl-8'><h1 class='font-size-sl-72 text-lh-1 font-weight-light mb-2 mb-lg-3 mb-wd-4'data-scs-animation-delay='100'data-scs-animation-in='fadeInUp'></h1><h3 class='font-size-xl-15 font-weight-bold mb-0'data-scs-animation-in='fadeInUp'data-scs-animation-delay='200'>HURRY UP BEFORE OFFER WILL END</h3></div></div><div class='col-md-6 d-flex align-items-center ml-auto ml-md-0 mb-4 mb-md-0'data-scs-animation-in='zoomIn'data-scs-animation-delay='400'><img class='img-fluid' src='/Shared/Images/" + element.image + "' alt='" + element.alt + "'></div></div></div>");
    //});
}




function OnLoading() {
    CleanData();
}

function OnComplete(xhr) {
    console.log(xhr);
    //FillData(xhr.responseJSON.object);
}

function OnError() {

}


function checkActiveUser() {
    var activeSite = localStorage.getItem('ActiveSite');
    if (activeSite != null) {
        ReLogin(activeSite);
    }
    else {
        console.log(activeSite);
    }
}

function ReLogin(active) {
    $.ajax({
        type: "POST",
        url: "/Index?handler=ReLogin",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { "activeSite": active },
        success: function (response) {
            if (response.isSuccessed) {
                window.location.href = "/";
            }
            else {
                if (response.message == "expired") {
                    localStorage.removeItem('ActiveSite');
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



// بررسی وجود کوکی با نام مشخص
function checkCookie(cookieName) {
    var cookiesLocal = localStorage.getItem('AcceptCookies');
    if (cookiesLocal != null) {
        var cookies = document.cookie.split(';'); // تقسیم کردن کوکی‌ها بر اساس ';'
        for (var i = 0; i < cookies.length; i++) {
            var cookie = cookies[i].trim(); // حذف فاصله‌های اضافی
            // بررسی آیا کوکی با نام مشخص وجود دارد یا خیر
            if (cookie.indexOf(cookieName + '=') === 0) {
                return true; // کوکی یافت شده است
            }
        }
        localStorage.removeItem('AcceptCookies');
        return false; // کوکی یافت نشده است
    }
    else {
        return false;
    }
}

// مثال استفاده
var myCookieExists = checkCookie('cookies_accepted');
if (!myCookieExists) {
    $("#CooiesModal").modal('show');
}



function acceptCookies() {
    closeModal();
    document.cookie = "cookies_accepted=true; expires=Fri, 31 Dec 9999 23:59:59 GMT;secure=true; path=/";
    localStorage.setItem('AcceptCookies', true);
    
}

function rejectCookies() {
    closeModal();
    document.cookie = "cookies_accepted=false; expires=Fri, 31 Dec 9999 23:59:59 GMT; path=/";
    localStorage.setItem('AcceptCookies', false);
    
}

function closeModal() {
    $('#CooiesModal').modal('hide');
}

