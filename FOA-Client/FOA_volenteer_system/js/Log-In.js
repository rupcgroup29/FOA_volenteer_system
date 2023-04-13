var api;

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    // לעדכן את הכתובת החלופית !!
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    $("#LogInForm").submit(loginUser);// Log In button clicked

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
    sessionStorage.setItem("isLoggedIn", JSON.stringify(isLoggedIn));
    sessionStorage.setItem("justLoggedIn", JSON.stringify(true));
    window.location.assign("HomePage.html");
}
function postLoginUserECB(err) { // התחברות כשלה
    isLoggedIn = false;
    alert(err.responseJSON.errorMessage);
}

// פונקציית רנדור במידה ושכחתי סיסמא
function RenderEmailBoxIfForgotPassword() {
    str_email = "";
    str_email += `<input dir="rtl" class="form - control" id="ForgotEmail" type="email" placeholder="אימייל * " data-sb-validations="required" />`;
    str_email += `<button id="sendNewPassword" onclick="ForgotPassword()">שלח סיסמא חדשה</button>`;
    document.getElementById("forgotPassEmail").innerHTML += str_email;
}

// שליחת סיסמא חדשה
function ForgotPassword() {
    const forgotEmail = $("#ForgotEmail").val();
    ajaxCall("POST", api + "UserServices/" + forgotEmail, JSON.stringify(forgotEmail), ForgotPasswordSCB, ForgotPasswordECB);
    return false;
}
function ForgotPasswordSCB(data) { 
    alert("הסיסמא נשלחה בהצלחה למייל שהזנת");
    window.location.assign("Log-In.html");

}
function ForgotPasswordECB(err) { 
    alert(err);
}