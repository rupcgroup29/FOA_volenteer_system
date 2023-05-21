var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));

var relevantUserID = JSON.parse(sessionStorage.getItem("userCard"));
if (relevantUserID == undefined) {
    sessionStorage.setItem("userCard", JSON.stringify(currentUser[0]));
    relevantUserID = JSON.parse(sessionStorage.getItem("userCard"));
}

var relevantUserObject;
var programsArr = [];
var teamsArr = [];

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    $('#contactForm').submit(updateUser);

    // get the volunteer Program list
    getVolunteerProgramsList();
    // get the teams list
    getTeamsList();

    enableOther();

    if (currentUser[0] != relevantUserID)  // if the user is editing someone else's user details- get without password
    {
        getAnotherUserDetails();
    }
    else getMyUserDetails();

});

// GET Another User Details
function getAnotherUserDetails() {
    ajaxCall("GET", api + "UserServices/user_details/" + relevantUserID, "", getAnotherUserDetailsSCB, getAnotherUserDetailsECB);
}
function getAnotherUserDetailsSCB(data) {
    relevantUserObject = data;
    renderUserDetails();
}
function getAnotherUserDetailsECB(err) {
    alert("Input Error");
}

// GET My User Details
function getMyUserDetails() {
    ajaxCall("GET", api + "UserServices/" + currentUser[0], "", getMyUserDetailsSCB, getMyUserDetailsECB);
}
function getMyUserDetailsSCB(data) {
    relevantUserObject = data;
    renderUserDetails();
}
function getMyUserDetailsECB(err) {
    alert("Input Error");
}

function renderUserDetails() {
    //Card Header
    let str_header = "";
    str_header += `<h2 class="section-heading text-uppercase"> מתנדב מספר ` + relevantUserID + `</h2>`;
    document.getElementById("CardHeader").innerHTML += str_header;
    //is active
    let str_isActive = "";
    if (relevantUserObject.isActive == true) {
        str_isActive += `<option class="opt" value="0">כן </option>`;
        str_isActive += `<option class="opt" value="1">לא </option>`;
    }
    else {
        str_isActive += `<option class="opt" value="1">לא </option>`;
        str_isActive += `<option class="opt" value="0">כן </option>`;
    }
    document.getElementById("IsActive").innerHTML += str_isActive;
    //volunteerProgram
    let str_Prog = "";
    str_Prog += '<option class="opt" value="' + relevantUserObject.programID + '">' + relevantUserObject.programName + '</option>';
    for (var i = 0; i < programsArr.length; i++) {
        str_Prog += '<option class="opt" value="' + programsArr[i].programID + '">' + programsArr[i].programName + '</option>';
    }
    str_Prog += '<option class="opt" value="999">אחר </option>';
    document.getElementById("volunteerProgram").innerHTML += str_Prog;
    //permission
    let str_perm = "";
    if (relevantUserObject.permission == 4) {
        str_perm += `<option class="opt" value="4">מתנדב.ת</option>`;
        str_perm += `<option class="opt" value="3">מנהל.ת צוות</option>`;
        str_perm += `<option class="opt" value="2">מנהל.ת</option>`;
    }
    if (relevantUserObject.permission == 3) {
        str_perm += `<option class="opt" value="3">מנהל.ת צוות</option>`;
        str_perm += `<option class="opt" value="4">מתנדב.ת</option>`;
        str_perm += `<option class="opt" value="2">מנהל.ת</option>`;
    }
    else {
        str_perm += `<option class="opt" value="2">מנהל.ת</option>`;
        str_perm += `<option class="opt" value="3">מנהל.ת צוות</option>`;
        str_perm += `<option class="opt" value="4">מתנדב.ת</option>`;
    }
    document.getElementById("permission").innerHTML += str_perm;
    //team
    let str_team = "";
    str_team += '<option class="opt" value="' + relevantUserObject.teamID + '">' + relevantUserObject.teamName + '</option>';
    for (var i = 0; i < teamsArr.length; i++) {
        str_team += '<option class="opt" value="' + teamsArr[i].teamID + '">' + teamsArr[i].teamName + '</option>';
    }
    document.getElementById("team").innerHTML += str_team;
    //roleDescription
    $("#roleDescription").val(relevantUserObject.roleDescription);
    //firstName
    $("#firstName").val(relevantUserObject.firstName);
    //surname
    $("#surname").val(relevantUserObject.surname);
    //user_name
    $("#user_name").val(relevantUserObject.userName);
    //email
    $("#email").val(relevantUserObject.email);
    //phone
    $("#phone").val(relevantUserObject.phoneNum);
    //password
    if (currentUser[0] == relevantUserID)  // if the user is editing his own user details- show password
    {
        let str_pass = "";
        str_pass += `<div class="form-headers">`;
        str_pass += `<p> סיסמא</p>`;
        str_pass += `</div>`;
        str_pass += `<input dir="rtl" class="form-control" id="Password" type="Password"/>`;
        document.getElementById("PasswordDiv").innerHTML += str_pass;
        $("#Password").val(relevantUserObject.password);
    }
}

function updateUser() {
    const newUser = {
        UserID: relevantUserID,
        FirstName: $("#firstName").val(),
        Surname: $("#surname").val(),
        UserName: $("#user_name").val(),
        PhoneNum: $("#phone").val(),
        RoleDescription: $("#roleDescription").val(),
        PermissionID: $("#permission").val(),
        TeamID: $("#team").val(),
        ProgramID: $("#volunteerProgram").val(),
        Email: $("#email").val(),
        ProgramName: $("#Different_school").val()
    }

    ajaxCall("PUT", api + "UserServices", JSON.stringify(newUser), updateUserSCB, updateUserECB);
    sessionStorage.setItem("userCard", JSON.stringify());
    return false;
}
function updateUserSCB(data) {
    alert("משתמש עודכן בהצלחה");
    window.location.assign("Teams-main.html");
    location.assign("Teams-main.html");
}

function updateUserECB(err) {
    alert("שגיאה בעדכון המשתמש, אנא נסו שוב");
}


// get the Volunteer Programs list
function getVolunteerProgramsList() {
    ajaxCall("GET", api + "VolunteerPrograms", "", getVolunteerProgramsSCB, getVolunteerProgramsECB);
    return false;
}

function getVolunteerProgramsSCB(data) {
    if (data == null)
        alert("There's no Volunteer Programs yet");
    else {
        programsArr = data;
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
    if (data == null)
        alert("There are no teams yet");
    else teamsArr = data;
}

function getTeamECB(err) {
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