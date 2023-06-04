var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));


$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    getManagerDetails();

});

function getManagerDetails() {
    ajaxCall("GET", api + "UserServices/teamLeader/" + currentUser[0], "", getManagerDetailsSCB, getManagerDetailsECB);
}
function getManagerDetailsSCB(data) {
    renderManagerDetails(data);
}
function getManagerDetailsECB(err) {
    alert(err.responseJSON.errorMessage);
}

function renderManagerDetails(data) {
    str = "";
    str += "<h3>מנהל הצוות - " + data.firstName + " " + data.surname +"</h3>";
    str += "<h4>מספר טלפון: " + data.phoneNum +"</h4>";
    str += "<h4>אימייל: " + data.email +"</h4>";
    document.getElementById("CardInfo").innerHTML += str;
}