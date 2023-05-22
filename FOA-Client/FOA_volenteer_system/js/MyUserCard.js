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

    // if volanteer is login
    if (currentUser[1] == 4)
        enableEditingFields();

    $('#contactForm').submit(updateUser);

    // get the volunteer Program list
    getVolunteerProgramsList();
    // get the teams list
    getTeamsList();

    enableOther();
    getMyUserDetails();

    // show/hide password
    const togglePassword = document.querySelector("#togglePassword");
    const password = document.querySelector("#Password");
    togglePassword.addEventListener("click", function () {
        // toggle the type attribute
        const type = password.getAttribute("type") === "Password" ? "text" : "Password";
        password.setAttribute("type", type);
        // toggle the icon
        this.classList.toggle("bi-eye");
    });

});

// GET My User Details
function getMyUserDetails() {
    ajaxCall("GET", api + "UserServices/" + currentUser[0], "", getMyUserDetailsSCB, getMyUserDetailsECB);
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
    str_header += `<h2 class="section-heading text-uppercase"> מתנדב מספר ` + relevantUserObject.userID + `</h2>`;
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
    if (relevantUserObject.permissionID == 4) {
        str_perm += `<option class="opt" value="4" >מתנדב.ת</option>`;
        //str_perm += `<option class="opt" value="3">מנהל.ת צוות</option>`;
        //str_perm += `<option class="opt" value="2">מנהל.ת</option>`;
    } else if (relevantUserObject.permissionID == 3) {
        str_perm += `<option class="opt" value="3">מנהל.ת צוות</option>`;
        //str_perm += `<option class="opt" value="4">מתנדב.ת</option>`;
        //str_perm += `<option class="opt" value="2">מנהל.ת</option>`;
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
    //Password
    $("#Password").val(relevantUserObject.password);
}

function updateUser() {
    const updateUser = {
        UserID: currentUser[0],
        FirstName: $("#firstName").val(),
        Surname: $("#surname").val(),
        UserName: $("#user_name").val(),
        PhoneNum: $("#phone").val(),
        RoleDescription: $("#roleDescription").val(),
        PermissionID: $("#permission").val(),
        TeamID: $("#team").val(),
        ProgramID: $("#volunteerProgram").val(),
        Email: $("#email").val(),
        ProgramName: $("#Different_school").val(),
        Password: $("#Password").val()
    }

    ajaxCall("PUT", api + "UserServices/myUser", JSON.stringify(updateUser), updateUserSCB, updateUserECB);
    return false;
}
function updateUserSCB(data) {
    alert("משתמש עודכן בהצלחה");
    window.location.assign("MyUserCard.html");
}

function updateUserECB(err) {
    alert("שגיאה בעדכון המשתמש, אנא נסו שוב");
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
//function enableOther() {
//    var sel = document.getElementById('volunteerProgram');
//    sel.addEventListener("change", ShowDivIfOtherSelected);
//}
//function ShowDivIfOtherSelected() {
//    if (sel.value === '999') {
//        $("#Different_school").attr("readonly", false);
//    }
//    else {
//        $("#Different_school").attr("readonly", true);
//        document.getElementById('Different_school').value = '';
//    }
//}

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

// אם מתנדב מחובר אז שהוא לא יוכל לערוך חלק מהשדות
function enableEditingFields() {
    $("#IsActive").attr("disabled", true);
    $("#volunteerProgram").attr("disabled", true);
    $("#permission").attr("disabled", true);
    $("#team").attr("disabled", true);
    $("#roleDescription").attr("disabled", true);
    $("#user_name").attr("disabled", true);
}