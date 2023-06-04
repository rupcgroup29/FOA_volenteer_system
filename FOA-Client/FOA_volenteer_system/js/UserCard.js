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
var permissionsArr = [];


$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    $('#contactForm').submit(updateUser);
    showMode();

    // get the volunteer Program list
    getVolunteerProgramsList();
    // get the teams list
    getTeamsList();
    // get permission list
    getPermissions()

    enableOther();

    if (currentUser[0] != relevantUserID)  // if the user is editing someone else's user details- get without password
    {
        getAnotherUserDetails();
    }
    else getMyUserDetails();

});


// GET permission list
function getPermissions() {
    ajaxCall("GET", api + "Permissions", "", getPermissionsSCB, getPermissionsECB);
    return false;
}
function getPermissionsSCB(data) {
    permissionsArr = data;
}
function getPermissionsECB(err) {
    console.log(err);
}

// GET Another User Details
function getAnotherUserDetails() {
    ajaxCall("GET", api + "UserServices/user_details/" + relevantUserID, "", getAnotherUserDetailsSCB, getUserDetailsECB);
}
function getAnotherUserDetailsSCB(data) {
    relevantUserObject = data;
    renderUserDetails();
}
function getUserDetailsECB(err) {
    alert(err.responseJSON.errorMessage);
}

// GET My User Details
function getMyUserDetails() {
    ajaxCall("GET", api + "UserServices/" + currentUser[0], "", getMyUserDetailsSCB, getUserDetailsECB);
}
function getMyUserDetailsSCB(data) {
    relevantUserObject = data;
    renderUserDetails();
}

//render User Details
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
        if (programsArr[i].programID != relevantUserObject.programID)
            str_Prog += '<option class="opt" value="' + programsArr[i].programID + '">' + programsArr[i].programName + '</option>';
    }
    str_Prog += '<option class="opt" value="999">אחר </option>';
    document.getElementById("volunteerProgram").innerHTML += str_Prog;

    //permission
    let str_perm = "";
    str_perm += '<option class="opt" value="' + relevantUserObject.permissionID + '">' + relevantUserObject.permissionName + '</option>';
    for (var i = 0; i < permissionsArr.length; i++) {
        if (permissionsArr[i].permissionID != relevantUserObject.permissionID) {
            str_perm += '<option class="opt" value="' + permissionsArr[i].permissionID + '">' + permissionsArr[i].permissionName + '</option>';
        }
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

// PUT User Details
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
    alert(err.responseJSON.errorMessage);
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
    var difSchoolDiv = document.getElementById('DifSchoolDiv');
    difSchoolDiv.style.display = 'none';

    var volunteerProgramSelect = document.getElementById('volunteerProgram');
    volunteerProgramSelect.addEventListener('change', function () {
        if (volunteerProgramSelect.value === '999') {
            difSchoolDiv.style.display = 'block';
            document.getElementById('Different_school').removeAttribute('readonly');
            schoolDiv.classList.add('col-6');
        } else {
            difSchoolDiv.style.display = 'none';
            document.getElementById('Different_school').setAttribute('readonly', 'readonly');
            schoolDiv.classList.remove('col-6');
        }
    });
}

//all values are disabled 
function showMode() {
    $("#firstName").attr("disabled", true);
    $("#surname").attr("disabled", true);
    $("#volunteerProgram").attr("disabled", true);
    $("#permission").attr("disabled", true);
    $("#team").attr("disabled", true);
    $("#roleDescription").attr("disabled", true);
    $("#user_name").attr("disabled", true);
    $("#phone").attr("disabled", true);
    $("#email").attr("disabled", true);
    $("#Password").attr("disabled", true);
    $("#IsActive").attr("disabled", true);
    $("#submitButton").hide();
}

//values are editable by permission 
function editMode() {
    $("#submitButton").show();
    if (currentUser[1] == 4 || currentUser[1] == 3) {
        $("#isActive").hide();
        enableEditingFieldsVolenteeen();
    } else if (currentUser[1] == 2) enableEditingFieldsManager();
    else enableEditingFieldsAdmin();
}

// אם מתנדב מחובר אז שהוא לא יוכל לערוך חלק מהשדות
function enableEditingFieldsVolenteeen() {
    $("#firstName").attr("disabled", false);
    $("#surname").attr("disabled", false);
    $("#phone").attr("disabled", false);
    $("#email").attr("disabled", false);
    $("#Password").attr("disabled", false);
}

// אם מנהל מחובר אז הוא יכול לערוך את כל השדות
function enableEditingFieldsManager() {
    enableEditingFieldsVolenteeen();
    $("#volunteerProgram").attr("disabled", false);
    $("#team").attr("disabled", false);
    $("#user_name").attr("disabled", false);
}


function enableEditingFieldsAdmin() {
    enableEditingFieldsManager();
    $("#roleDescription").attr("disabled", false);
    $("#permission").attr("disabled", false);
}