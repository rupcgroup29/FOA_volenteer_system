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

    HideIsActiveDiv();
    ShowOther();
    $("#Different_school").attr("readonly", true);
});

function RegisterUser() {
    let firstName = $("#firstName").val();
    let surname = $("#surname").val();
    let user_name = $("#user_name").val();
    let email = $("#email").val();
    let phone = $("#phone").val();
    let volunteerProgram = $("#volunteerProgram").val();
    if (volunteerProgram == 999) { otherVolunteerProgram(); } //הוספת אפשרות חדשה למסגרת התנדבות
    let permission = $("#permission").val();
    let team = $("#team").val();
    let roleDescription = $("#roleDescription").val();

    const newUser = {
        FirstName: firstName,
        Surname: surname,
        UserName: user_name,
        PhoneNum: phone,
        RoleDescription: roleDescription,
        PermissionID: permission,
        TeamID: team,
        ProgramID: volunteerProgram,
        Email: email,
<<<<<<< Updated upstream
        Password: "1" //במטרה לשלוח אובייקט משתמש שלם, ישתנה בדאטה בייס
=======
        Password: ""
>>>>>>> Stashed changes
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


//send other volunteer program
function otherVolunteerProgram() {
    let programName = $("#Different_school").val();
    let volunteerProgram = {
        ProgramID: "",
        ProgramName: programName
    }
    ajaxCall("POST", api + "VolunteerPrograms", JSON.stringify(volunteerProgram), postOtherVolunteerProgramSCB, postOtherVolunteerProgramECB);
    return false;

}

function postOtherVolunteerProgramSCB(data) {
    console.log("מסגרת התנדבות חדשה נוספה בהצלחה");
}
function postOtherVolunteerProgramECB(err) {
    alert("Input Error");
}

//Hide IsActive div
function HideIsActiveDiv() {
    var element = document.getElementById("IsActive");
    element.style.display = "none";
}

// Show Other volunteer program only if other selected
function ShowOther() {
    var sel = document.getElementById('volunteerProgram');

    sel.addEventListener("change", ShowDivIfOtherSelected);

    function ShowDivIfOtherSelected() {

        if (sel.value === '999') {
            $("#Different_school").attr("readonly", false);
        }
        else $("#Different_school").attr("readonly", true);
    }
}