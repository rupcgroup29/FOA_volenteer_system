var api;
var isLoggedIn;
var usersArr = [];
var CurrentUser = sessionStorage.getItem("user");

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    // לעדכן את הכתובת החלופית !!
    //else api = "https://proj.ruppin.ac.il/cgroup29/test2/tar1/api/Users/";

    readUsers();

    //// hide DifSchoolDiv
    //document.getElementById("Different_school").disabled = true;

    $('#contactForm').submit(RegisterUser);

    // get the volunteer Program list
    GetVolunteerProgramsList();
    // get the teams list
    GetTeamsList();

});

function RegisterUser() {
    let firstName = $("#firstName").val();
    let surname = $("#surname").val();
    let user_name = $("#user_name").val();
    let email = $("#email").val();
    let phone = $("#phone").val();
    let volunteerProgram = $("#volunteerProgram").val();
    if (volunteerProgram == 999) {
        volunteerProgram = $("#Different_school").val();
    }
    let permission = $("#permission").val();
    let team = $("#team").val();
    let roleDescription = $("#roleDescription").val();

    const newUser = {
        FirstName: firstName,
        Surname: surname,
        UserName: user_name,
        Email: email,
        PhoneNum: phone,
        ProgramID: volunteerProgram,
        PermissionID: permission,
        RoleDescription: roleDescription,
        TeamID: team
    }

    ajaxCall("POST", api + "Users", JSON.stringify(newUser), postRegisterSCB, postRegisterECB);
    return false;
}
function postRegisterSCB(data) { // הוספת משתמש הצליחה
    alert("משתמש נוסף בהצלחה");
    window.location.assign("Teams-main.html");
    location.assign("Teams-main.html")
}

function postRegisterECB(err) {
    alert("שגיאה בהוספת המשתמש, אנא נסו שוב");
}

// read all users
function readUsers() {
    ajaxCall("GET", api + "Users", "", getAllUsersSCB, getAllUsersECB);
    return false;
}
function getAllUsersSCB(data) {
    usersArr = data;
}
function getAllUsersECB(err) {
    alert("Input Error");
}

// get the Volunteer Programs list
function GetVolunteerProgramsList() {
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
        str += '<option class="opt" value="999" onclick="showOtherSchoolDiv()">אחר </option>';
        document.getElementById("volunteerProgram").innerHTML += str;
    }
}
function getVolunteerProgramsECB(err) {
    console.log(err);
}

// get the Teams list
function GetTeamsList() {
    ajaxCall("GET", api + "Teams", "", getTeamSCB, getTeamECB);
    return false;
}

function getTeamSCB(data) {
    if (data == null) {
        alert("There are no teams yet");
    } else {
        let str = "";
        str += '<option class="opt" value="0">בחר צוות התנדבות *</option>';
        for (var i = 0; i < data.length; i++) {
            str += '<option class="opt" value="' + data[i].teamID + '">' + data[i].teamName + '</option>';
        }
        document.getElementById("team").innerHTML += str;
    }
}
function getTeamECB(err) {
    console.log(err);
}
