var api;
var isLoggedIn;
var usersArr = [];
var CurrentUser = sessionStorage.getItem("user");

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/Users";
    }
    // לעדכן את הכתובת החלופית !!
    //else api = "https://proj.ruppin.ac.il/cgroup29/test2/tar1/api/Users/";

    readUsers();

  //  $("#submitButton").click(loginUser);
    $("#LogInForm").submit(loginUser)
});


function loginUser() {
    const loginUser = {
        Email: $("#email-input").val(),
        Password: $("#Password-input").val()
    }

    ajaxCall("POST", api + "/login", JSON.stringify(loginUser), postLoginUserSCB, postLoginUserECB);
    return false;
}

function postLoginUserSCB(data) { // התחברות הצליחה
    isLoggedIn = true;
    sessionStorage.setItem("CurrentUser", JSON.stringify(data));
    name = CurrentUser.firstName;
    $(".hello").val("hello " + name + "!");
    window.location.assign("HomePage.html");
    location.assign("HomePage.html")

}
function postLoginUserECB(err) { // התחברות כשלה
    isLoggedIn = false;
    alert("שם המשתמש או הסיסמא אינם נכונים");
}

// read all users
function readUsers() {
    ajaxCall("GET", api, "", getAllUsersSCB, getAllUsersECB);
}
function getAllUsersSCB(data) {
    usersArr = data;
}
function getAllUsersECB(err) {
    alert("Input Error");
}