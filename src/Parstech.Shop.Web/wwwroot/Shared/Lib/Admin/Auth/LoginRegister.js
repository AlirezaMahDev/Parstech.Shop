const firstInput = document.getElementById('firstInput');
const secondInput = document.getElementById('secondInput');
const thirdInput = document.getElementById('thirdInput');
const forthInput = document.getElementById('forthInput');
const SubmitButton = document.getElementById('SubmitButton');

firstInput.addEventListener('input', function (event) {
    // بررسی وقتی یک رقم وارد شده
    if (event.target.value.length > 0) {
        // تغییر فوکوس به ورودی دوم بعد از وارد شدن عدد
        secondInput.focus();
    }
});
secondInput.addEventListener('input', function (event) {
    // بررسی وقتی یک رقم وارد شده
    if (event.target.value.length > 0) {
        // تغییر فوکوس به ورودی دوم بعد از وارد شدن عدد
        thirdInput.focus();
    }
});
thirdInput.addEventListener('input', function (event) {
    // بررسی وقتی یک رقم وارد شده
    if (event.target.value.length > 0) {
        // تغییر فوکوس به ورودی دوم بعد از وارد شدن عدد
        forthInput.focus();
    }
});
forthInput.addEventListener('input', function (event) {
    // بررسی وقتی یک رقم وارد شده
    if (event.target.value.length > 0) {
        // تغییر فوکوس به ورودی دوم بعد از وارد شدن عدد
        SubmitButton.focus();
    }
});


var loginmobile = document.getElementById("loginmobile");
var smsSection = document.getElementById("smsSection");
var loginSection = document.getElementById("loginSection");
var ActiveRegister;


function LoginReqUser() {
    $.ajax({
        type: "POST",
        url: "/auth/loginregister?handler=LoginRequest",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            "loginmobile": loginmobile.value,
        },
        success: function (response) {
            if (response.isSuccessed) {

                smsSection.classList.remove("show");
                smsSection.classList.add("hidden");
                loginSection.classList.remove("hidden");
                loginSection.classList.add("show");
                loginSection.insertAdjacentHTML("afterbegin", "<span class='Yellow'>کد تائید برای شما ارسال گردید</span>");
                if (response.object2.object != null) {
                    ActiveRegister = response.object2.object;
                }
            } else {
                RunSwl("ورود ناموفق", "error", response.message);
            }
            console.log(response);

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}


function LoginUser() {
    var activeCode = firstInput.value + secondInput.value + thirdInput.value + forthInput.value;
    console.log(activeCode);
    $.ajax({
        type: "POST",
        url: "/auth/loginregister?handler=Login",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            "loginmobile": loginmobile.value,
            loginactiveCode: activeCode,
            "ActiveRegister": ActiveRegister
        },
        success: function (response) {
            console.log(response);
            if (response.isSuccessed) {
                console.log(response.object2);
                localStorage.setItem('ActiveSite', response.object2.object);

                RunSwl("ورود موفق", "success", response.message);
                setTimeout(Refresh, 2000);
            } else {
                RunSwl("ورود ناموفق", "error", response.message);
            }
            console.log(response);

        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function Refresh() {
    window.location.href = "/";
}


