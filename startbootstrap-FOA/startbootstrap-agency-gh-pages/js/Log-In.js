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

    $("#LogInForm").submit(loginUser);

});


function loginUser() {
    const loginUser = {
        Email: $("#email-input").val(),
        Password: $("#Password-input").val()
    }

    ajaxCall("POST", api + "/login", JSON.stringify(loginUser), postSCB, postECB);
    return false;
    //let succeed = false;
    //for (var i = 0; i < usersArr.length; i++) {
    //    if (usersArr[i].UserName == $("#UserName-input").val() && usersArr[i].password == $("#Password-input").val()) {
    //        if (usersArr[i].IsActive === false) {
    //            alert("משתמש זה הוגדר משתמש לא פעיל במערכת");
    //            return;
    //        } else {
    //            succeed = true;
    //            postSCB(usersArr[i]);
    //            break;
    //        }
    //    }
    //}
    //if (succeed == false) { //אם ההתחברות כשלה
    //    isLoggedIn = false;
    //    alert("שם המשתמש או הסיסמא אינם נכונים");
    //}
}

function postSCB(data) { // התחברות הצליחה
    isLoggedIn = true;
    sessionStorage.setItem("CurrentUser", JSON.stringify(data));
    //name = CurrentUser.firstName;
    //$(".hello").val("hello " + name + "!");
    window.location.assign("HomePage.html");
    //location.assign("HomePage.html")

}
function postECB(err) { // התחברות כשלה
    isLoggedIn = false;
    alert(err);
}
