var api;

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    // לעדכן את הכתובת החלופית !!
    //else api = "https://proj.ruppin.ac.il/cgroup29/test2/tar1/api/Users/";

    $("#LogInForm").submit(loginUser);

});


function loginUser() {
    const loginUser = {
        Email: $("#email-input").val(),
        Password: $("#Password-input").val()
    }

    ajaxCall("POST", api + "UserServices/login", JSON.stringify(loginUser), postLoginUserSCB, postLoginUserECB);
    return false;
}

function postLoginUserSCB(data) { // התחברות הצליחה
    isLoggedIn = true;
    sessionStorage.setItem("user", JSON.stringify(data));   
    window.location.assign("HomePage.html");
}
function postLoginUserECB(err) { // התחברות כשלה
    isLoggedIn = false;
    alert(err);
}
