var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    $('#contactForm').submit(RegisterUser);

    // get the volunteer Program list
    getVolunteerProgramsList();
    // get the teams list
    getTeamsList();
    // get the Permissions list
    getPermissionsList();

    enableOther();
});


function RegisterUser() {
    const newUser = {
        FirstName: $("#firstName").val(),
        Surname: $("#surname").val(),
        UserName: $("#user_name").val(),
        PhoneNum: $("#phone").val(),
        RoleDescription: $("#roleDescription").val(),
        PermissionID: $("#permission").val(),
        TeamID: $("#team").val(),
        ProgramID: $("#volunteerProgram").val(),
        Email: $("#email").val(),
        Password: "", //במטרה לשלוח אובייקט משתמש שלם, ישתנה בדאטה בייס
        ProgramName: $("#Different_school").val(),
    }

    ajaxCall("POST", api + "UserServices", JSON.stringify(newUser), postRegisterSCB, postRegisterECB);
    return false;
}
function postRegisterSCB(data) { // הוספת משתמש הצליחה
    alert("משתמש נוסף בהצלחה");
    window.location.assign("Teams-main.html");
    location.assign("Teams-main.html")
}
function postRegisterECB(err) {
    alert(err.responseJSON.errorMessage);
}



// get the Volunteer Programs list
function getVolunteerProgramsList() {
    ajaxCall("GET", api + "VolunteerPrograms", "", getVolunteerProgramsSCB, getVolunteerProgramsECB);
    return false;
}

function getVolunteerProgramsSCB(data) {
    if (data == null) {
        alert("There's no Volunteer Programs yet");
    } else {
        let str = "";
        str += '<option class="opt" value="0">מסגרת לימודים דרכה מתנדב.ת *</option>';
        for (var i = 0; i < data.length; i++) {
            str += '<option class="opt" value="' + data[i].programID + '">' + data[i].programName + '</option>';
        }
        str += '<option class="opt" value="999">אחר </option>';
        document.getElementById("volunteerProgram").innerHTML += str;
    }
}
function getVolunteerProgramsECB(err) {
    console.log(err);
}

// get the Teams list
function getTeamsList() {
    ajaxCall("GET", api + "Teams", "", getTeamSCB, getTeamECB);
    return false;
}

function getTeamSCB(data) {
    if (data == null) {
        alert("There are no teams yet");
    } else {
        let str = "";
        str += '<option class="opt" value="0">בחירת צוות התנדבות *</option>';
        for (var i = 0; i < data.length; i++) {
            str += '<option class="opt" value="' + data[i].teamID + '">' + data[i].teamName + '</option>';
        }
        document.getElementById("team").innerHTML += str;
    }
}
function getTeamECB(err) {
    console.log(err);
}

// get the Permissions list
function getPermissionsList() {
    ajaxCall("GET", api + "Permissions", "", getPermissionsSCB, getPermissionsECB);
    return false;
}

function getPermissionsSCB(data) {
    if (data == null) {
        alert("There are no teams yet");
    } else {
        let str = "";
        str += '<option class="opt" value="0">סוג הרשאת מערכת *</option>';
        for (var i = 0; i < data.length; i++) {
            str += '<option class="opt" value="' + data[i].permissionID + '">' + data[i].permissionName + '</option>';
        }
        document.getElementById("permission").innerHTML += str;
    }
}
function getPermissionsECB(err) {
    console.log(err);
}

// enable Other volunteer program only if other selected
function enableOther() {
    var sel = document.getElementById('volunteerProgram');

    sel.addEventListener("change", ShowDivIfOtherSelected);

    function ShowDivIfOtherSelected() {

        if (sel.value === '999') {
            $("#Different_school").attr("readonly", false);
        }
        else {
            $("#Different_school").attr("readonly", true);
            document.getElementById('Different_school').value = '';
        }
    }
}

