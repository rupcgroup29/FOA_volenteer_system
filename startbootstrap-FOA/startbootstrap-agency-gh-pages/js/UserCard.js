var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));
var relevantUserID = JSON.parse(sessionStorage.getItem("userCard"));;
var relevantUserObject;
var programsArr = [];
var teamsArr = [];

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    // לעדכן את הכתובת החלופית !!
    //else api = "https://proj.ruppin.ac.il/cgroup29/test2/tar1/api/Users/";

    $('#contactForm').submit(updateUser);

    // get the volunteer Program list
    getVolunteerProgramsList();
    // get the teams list
    getTeamsList();

    enableOther();

    if (currentUser.userID != relevantUserID)  // if the user is editing someone else's user details- get without password
    {
        getAnotherUserDetails();
    }
    else getMyUserDetails();

});

// GET Another User Details
function getAnotherUserDetails() {
    ajaxCall("GET", api + "UserServices/relevantUserID", "", getAnotherUserDetailsSCB, getAnotherUserDetailsECB);
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
    ajaxCall("GET", api + "UserServices/relevantUserID", "", getMyUserDetailsSCB, getMyUserDetailsECB);
}
function getMyUserDetailsSCB(data) {
    relevantUserObject = data;
    renderUserDetails();
}
function getMyUserDetailsECB(err) {
    alert("Input Error");
}

function renderUserDetails() {
    //is active
    let str_isActive = "";
    if (relevantUserObject.isActive == 0) //לוודא את שם השדה
    {
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
    str_Prog += '<option class="opt" value="' + relevantUserObject.programID + '">' + relevantUserObject.volunteerProgram + '</option>';// לוודא את שם השדה
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
    str_team += '<option class="opt" value="' + relevantUserObject.teamID + '">' + relevantUserObject.teamName + ' *</option>'; //לוודא את שם השדה
    for (var i = 0; i < teamsArr.length; i++) {
        str_team += '<option class="opt" value="' + teamsArr[i].teamID + '">' + teamsArr[i].teamName + '</option>';
    }
    document.getElementById("team").innerHTML += str_team;
    //roleDescription
    $("#roleDescription").val(relevantUserObject.roleDescription); //לוודא את שם השדה
    //firstName
    $("#firstName").val(relevantUserObject.firstName);//לוודא את שם השדה
    //surname
    $("#surname").val(relevantUserObject.surname);//לוודא את שם השדה
    //user_name
    $("#user_name").val(relevantUserObject.userName);//לוודא את שם השדה
    //email
    $("#email").val(relevantUserObject.email);//לוודא את שם השדה
    //phone
    $("#phone").val(relevantUserObject.phone);//לוודא את שם השדה
    if (currentUser.userID == relevantUserID)  // if the user is editing his own user details- show password
    {
        let str_pass = "";
        str_pass += `<div class="form-headers">`;
        str_pass += `<p> סיסמא</p>`;
        str_pass += `</div>`;
        str_pass += `<input dir="rtl" class="form-control" id="Password" />`;
        document.getElementById("PasswordDiv").innerHTML += str_pass;
        $("#Password").val(relevantUserObject.password);//לוודא את שם השדה
    }
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
    if (currentUser.userID == relevantUserID)  // if the user is editing his own user details- show password
    {
        let password = $("Password").val();
    }

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
        Password: password, 
        ProgramName: programName
    }

    ajaxCall("PUT", api + "Users/" + relevantUserID, JSON.stringify(newUser), updateUserSCB, updateUserECB);
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