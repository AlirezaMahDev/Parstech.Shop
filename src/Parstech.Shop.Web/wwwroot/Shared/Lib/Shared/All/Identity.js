var registermobile = document.getElementById("registermobile");

var registername = document.getElementById("registername");
var registerfamily = document.getElementById("registerfamily");
var registerstate = document.getElementById("registerstate");
var registercity = document.getElementById("registercity");
var registeraddress = document.getElementById("registeraddress");
var registerMeliCode = document.getElementById("registerMeliCode");


function RegisterUser() {
    $.ajax({
        type: "POST",
        url: "/Index?handler=Register",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            "registerMobile": registermobile.value,
            "registerName": registername.value,
            "registerFamily": registerfamily.value,
            "registerState": registerstate.value,
            "registerCity": registercity.value,
            "registerAddress": registeraddress.value,
            "registerMeliCode": registerMeliCode.value,
        },
        success: function (response) {
            if (response.isSuccessed) {
                RunSwl("به فروشگاه پارس تک خوش آمدید", "success", response.message);
            } else {
                RunSwl("خطایی رخ داده است", "error", response.message);
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


var loginmobile = document.getElementById("loginmobile");
var smsSection = document.getElementById("smsSection");
var loginSection = document.getElementById("loginSection");
var loginactiveCode = document.getElementById("loginactiveCode");
var loginPassword = document.getElementById("loginPassword");

function LoginReqUser() {
    $.ajax({
        type: "POST",
        url: "/Index?handler=LoginRequest",
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
    $.ajax({
        type: "POST",
        url: "/Index?handler=Login",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            "loginmobile": loginmobile.value,
            loginactiveCode: loginactiveCode.value
        },
        success: function (response) {
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


function LogoutUser() {
    $.ajax({
        type: "POST",
        url: "/Index?handler=LogoutUser",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        success: function (response) {
            console.log(response);
            if (response.isSuccessed) {

                RunSwl("خروج از حساب", "error", response.message);
                localStorage.removeItem('ActiveSite');
                setTimeout(Home, 2000);

            } else {
                RunSwl("ورود ناموفق", "error", response.message);
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

function Home() {
    window.location.href = "/";
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


var PasswordloginSection = document.getElementById("PasswordloginSection");
var PasswordSection = document.getElementById("PasswordSection");

function PasswordLoginReqUser() {
    PasswordSection.classList.remove("show");
    PasswordSection.classList.add("hidden");
    PasswordloginSection.classList.remove("hidden");
    PasswordloginSection.classList.add("show");
}

function PasswordLoginUser() {
    $.ajax({
        type: "POST",
        url: "/Index?handler=PasswordLogin",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: {
            "loginmobile": loginmobile.value,
            loginPassword: loginPassword.value
        },
        success: function (response) {
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
