var Amount = document.getElementById("Amount");
var Coin = document.getElementById("Coin");
var Fecilities = document.getElementById("Fecilities");

function GetAmount() {
    // console.log("ggggg");
    //$("#GetAmountForm").submit();
}

function GetAmount() {
    $("#GetCoinForm").submit();
}

function GetAmount() {
    $("#GetFecilitiesForm").submit();
}

function OnCompleteAmount(xhr) {
    var Date = xhr.responseJSON.object;
    Amount.innerText = separate(Data);
}

function OnCompleteCoin(xhr) {
    var Date = xhr.responseJSON.object;
    Coin.innerText = Data;
}

function OnCompleteFecilities(xhr) {
    // console.log(xhr);
    var Date = xhr.responseJSON.object;
    Fecilities.innerText = separate(Data);
}