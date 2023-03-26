var api;
var isLoggedIn;
var usersArr = [];
var CurrentUser = JSON.parse(sessionStorage.getItem("user"));
var NewProgram;

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    // לעדכן את הכתובת החלופית !!
    //else api = "https://proj.ruppin.ac.il/cgroup29/test2/tar1/api/Users/";

    $('#contactForm').submit(RegisterUser);

    // get the volunteer Program list
    GetVolunteerProgramsList();
    // get the teams list
    GetTeamsList();

    HideIsActiveDiv();
    enableOther();
});

function RegisterUser() {
    let firstName = $("#firstName").val();
    let surname = $("#surname").val();
    let user_name = $("#user_name").val();
    let email = $("#email").val();
    let phone = $("#phone").val();
    let volunteerProgram = $("#volunteerProgram").val();
    let permission = $("#permission").val();
    let team = $("#team").val();
    let roleDescription = $("#roleDescription").val();
    let programName = $("#Different_school").val();

    //if (volunteerProgram == 999) {
    //    let newProgramName = $("#Different_school").val();
    //    const newVolunteerProgram = {
    //        ProgramID: 999,
    //        ProgramName: newProgramName
    //    }
    //    ajaxCall("POST", api + "VolunteerPrograms", JSON.stringify(newvolunteerProgram), postOtherVolunteerProgramSCB, postOtherVolunteerProgramECB);
    //    searchNewProgramID();
    //    volunteerProgram = sessionStorage.getItem("NewProgram");
    ////    return false;
    //}

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
        Password: "", //במטרה לשלוח אובייקט משתמש שלם, ישתנה בדאטה בייס
        ProgramName: programName,

    }

    ajaxCall("POST", api + "Users", JSON.stringify(newUser), postRegisterSCB, postRegisterECB);
    return false;
}
function postRegisterSCB(data) { // הוספת משתמש הצליחה
    //let volunteerProgram = $("#volunteerProgram").val();
    //if (volunteerProgram == 999) { otherVolunteerProgram(); } //הוספת אפשרות חדשה למסגרת התנדבות
    alert("משתמש נוסף בהצלחה");
    window.location.assign("Teams-main.html");
    location.assign("Teams-main.html")
}

function postRegisterECB(err) {
    alert("שגיאה בהוספת המשתמש, אנא נסו שוב");
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
        str += '<option class="opt" value="999">אחר </option>';
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
//function otherVolunteerProgram() {
//    let programName = $("#Different_school").val();
//    const newvolunteerProgram = {
//        ProgramID: 1,
//        ProgramName: programName
//    }
//    ajaxCall("POST", api + "VolunteerPrograms", JSON.stringify(newvolunteerProgram), postOtherVolunteerProgramSCB, postOtherVolunteerProgramECB);
//    return false;
//}

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

// enable Other volunteer program only if other selected
function enableOther() {
    var sel = document.getElementById('volunteerProgram');

    sel.addEventListener("change", ShowDivIfOtherSelected);

    function ShowDivIfOtherSelected() {

        if (sel.value === '999') {
            $("#Different_school").attr("readonly", false);
        }
        else $("#Different_school").attr("readonly", true);
    }
}


// חיפוש של המסגרת התנדבות החדשה שנוספה
function searchNewProgramID() {
    ajaxCall("GET", api + "VolunteerPrograms", "", getNewProgramIDSCB, getNewProgramIDECB);
    return false;
}

function getNewProgramIDSCB(data) {
    if (data == null) {
        alert("There's no Volunteer Programs yet");
    }
    else {
        let NewVolunteerProgram = $("#Different_school").val();
        for (var i = 0; i < data.length; i++) {
            if (NewVolunteerProgram == data[i].programName) {
                let NewProgramID = data[i].programID;
                sessionStorage.setItem("NewProgramID", JSON.stringify(data));
            }
        }
    }
}


function getNewProgramIDECB(err) {
    console.log(err);
}