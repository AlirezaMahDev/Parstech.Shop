function ToastError(message) {
    Toastify({
        text: message,
        duration: 5000,
        close: true,
        gravity: "top", // `top` or `bottom`
        position: "center", // `left`, `center` or `right`
        stopOnFocus: true, // Prevents dismissing of toast on hover
        className: "h5",
        style: {
            background: "#ff3838",
            color: "black",
            fontSize: "12px",
            fontWeight: "bold"
        },
        onClick: function () { } // Callback after click
    }).showToast();
}
function ToastSuccess(message) {
    Toastify({
        text: message,
        duration: 5000,
        close: true,
        gravity: "top", // `top` or `bottom`
        position: "center", // `left`, `center` or `right`
        stopOnFocus: true, // Prevents dismissing of toast on hover
        className: "h5",
        style: {
            //background: "linear-gradient(to right, #FF6933, #F10E0E)",
            background: "#d2cdc2",
            color: "black",
            fontSize: "12px",
            
        },
        onClick: function () { } // Callback after click
    }).showToast();
}

