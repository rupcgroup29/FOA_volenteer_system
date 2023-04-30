var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));
var programsArr = [];
var teamsArr = [];
var relevantUserObject;


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
    getMyUserDetails();

});

// GET My User Details
function getMyUserDetails() {
    ajaxCall("GET", api + "UserServices/" + currentUser.userID, "", getMyUserDetailsSCB, getMyUserDetailsECB);
}
function getMyUserDetailsSCB(data) {
    relevantUserObject = data;
    renderMyUserDetails();
}
function getMyUserDetailsECB(err) {
    alert("Input Error");
}

function renderMyUserDetails() {
    //Card Header
    let str_header = "";
    str_header += `<h2 class="section-heading text-uppercase"> מתנדב מספר ` + currentUser.userID + `</h2>`;
    document.getElementById("CardHeader").innerHTML += str_header;
    //is active
    let str_isActive = "";
    if (currentUser.isActive == true) {
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
    str_Prog += '<option class="opt" value="' + currentUser.programID + '">' + currentUser.programName + '</option>';
    for (var i = 0; i < programsArr.length; i++) {
        str_Prog += '<option class="opt" value="' + programsArr[i].programID + '">' + programsArr[i].programName + '</option>';
    }
    str_Prog += '<option class="opt" value="999">אחר </option>';
    document.getElementById("volunteerProgram").innerHTML += str_Prog;
    //permission
    let str_perm = "";
    if (currentUser.permission == 4) {
        str_perm += `<option class="opt" value="4">מתנדב.ת</option>`;
        str_perm += `<option class="opt" value="3">מנהל.ת צוות</option>`;
        str_perm += `<option class="opt" value="2">מנהל.ת</option>`;
    }
    if (currentUser.permission == 3) {
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
    str_team += '<option class="opt" value="' + currentUser.teamID + '">' + currentUser.teamName + '</option>';
    for (var i = 0; i < teamsArr.length; i++) {
        str_team += '<option class="opt" value="' + teamsArr[i].teamID + '">' + teamsArr[i].teamName + '</option>';
    }
    document.getElementById("team").innerHTML += str_team;
    //roleDescription
    $("#roleDescription").val(currentUser.roleDescription);
    //firstName
    $("#firstName").val(currentUser.firstName);
    //surname
    $("#surname").val(currentUser.surname);
    //user_name
    $("#user_name").val(currentUser.userName);
    //email
    $("#email").val(currentUser.email);
    //phone
    $("#phone").val(currentUser.phoneNum);
    //Password
    $("#Password").val(currentUser.password);
}

function updateUser() {
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
    let password = $("Password").val();

    const updateUser = {
        FirstName: firstName,
        Surname: surname,
        UserName: user_name,
        PhoneNum: phone,
        RoleDescription: roleDescription,
        PermissionID: permission,
        TeamID: team,
        ProgramID: volunteerProgram,
        Email: email,
        Password: password,
        ProgramName: programName
    }

    ajaxCall("PUT", api + "Users/" + relevantUserID, JSON.stringify(updateUser), updateUserSCB, updateUserECB);
    sessionStorage.setItem("userCard", JSON.stringify());
    return false;
}
function updateUserSCB(data) {
    alert("משתמש עודכן בהצלחה");
    window.location.assign("Teams-main.html");
    location.assign("Teams-main.html")
}

function updateUserECB(err) {
    alert("שגיאה בעדכון המשתמש, אנא נסו שוב");
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

// get the Volunteer Programs list
function getVolunteerProgramsList() {
    ajaxCall("GET", api + "VolunteerPrograms", "", getVolunteerProgramsSCB, getVolunteerProgramsECB);
    return false;
}

function getVolunteerProgramsSCB(data) {
    if (data == null)
        alert("There's no Volunteer Programs yet");
    else programsArr = data;
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