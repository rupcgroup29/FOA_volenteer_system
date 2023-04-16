var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));
var programsArr = [];
var teamsArr = [];

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    //Nav bar - Permission
    if (currentUser.permissionID == 4) // a volunteer is logged in
    {
        $(".ManagerNav").hide();
        $(".VolunteerNav").show();
    }
    else //Manager is logged in
    {
        $(".ManagerNav").show();
        $(".VolunteerNav").hide();
    }
    $("#u39").mouseenter(UserEnterSubManu);
    $("#u39").mouseleave(UserExitSubManu);
    $("#u40").mouseleave(UserExitSubManu);

    $("#logout").click(logout);

    $('#contactForm').submit(updateUser);

    // get the volunteer Program list
    getVolunteerProgramsList();
    // get the teams list
    getTeamsList();

    enableOther();
    renderMyUserDetails();

});

//NAVBAR USER

function UserEnterSubManu() {
    $("#u40").css("visibility", "inherit")
    $("#u40").show();
}
function UserExitSubManu() {
    $("#u40").css("visibility", "hidden")
    $("#u40").hide();
}

//logout function
function logout() {
    isLogIn = false;
    sessionStorage.clear();
    window.location.assign("Log-In.html");
}

//END - NAVBAR USER


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
    $("#team").val(currentUser.teamName);
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