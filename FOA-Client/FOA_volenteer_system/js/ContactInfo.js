var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));


$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    //getManagerDetails();

});


//function getManagerDetails() {
//    //לעדכן את הקריאה כאן כי היא לא מעודכנת
//    ajaxCall("GET", api + "UserServices/" + currentUser[0], "", getManagerDetailsSCB, getManagerDetailsECB);
//}
//function getManagerDetailsSCB(data) {
//    renderManagerDetails(data);
//}
//function getManagerDetailsECB(err) {
//    alert("Input Error");
//}

//function renderManagerDetails(data) {



//    document.getElementById("CardInfo").innerHTML += str;
//}