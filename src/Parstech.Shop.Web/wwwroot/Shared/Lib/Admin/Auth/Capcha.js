let url = "/Auth/Register/?handler=Captcha";
let capchKey;
let CapchaSection = document.getElementById("CapchaSection");
let CaptchaKeyInput = document.getElementById("CaptchaKey");
function GetCapcha() {
    $.ajax({
        type: "POST",
        url: url,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        //data: { "skip": skip, "store": Store.value },
        success: function (response) {
            console.log(response);
            //StoreName.innerText = response.object2.storeName;
            //FillDataSet(response.object);
            capchKey = response.id;
            CaptchaKeyInput.value = capchKey;
            CapchaSection.innerHTML = null;
            CapchaSection.insertAdjacentHTML("beforeend", "<img width='250' src='data:image/png;base64," + response.image + "' alt='Description of the image'>");
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}