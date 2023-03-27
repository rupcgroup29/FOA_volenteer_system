var api;

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/Users";
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

    ajaxCall("POST", api + "/login", JSON.stringify(loginUser), postLoginUserSCB, postLoginUserECB);
    return false;
}

function postLoginUserSCB(data) { // התחברות הצליחה
    isLoggedIn = true;
    sessionStorage.setItem("user", JSON.stringify(data));   //לטם: מחקתי מהמשתנים למעלה את מה שהוספת על הסשיין כי זה לא רלוונטי לעשות גט-אייטם מהזיכרון כשאין עדין אף משתמש שהתחבר בשביל שהוא ישתמר בזכרון. בדף הלוג אין למעשה מאתחלים את שמירת היוזר שמתחבר בזיכרון
    window.location.assign("HomePage.html");
}
function postLoginUserECB(err) { // התחברות כשלה
    isLoggedIn = false;
    alert(err);
}
