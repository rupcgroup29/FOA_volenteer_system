var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));

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

    $('#contactForm').submit(RegisterUser);

    // get the volunteer Program list
    getVolunteerProgramsList();
    // get the teams list
    getTeamsList();

    hideIsActiveDiv();
    enableOther();
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

    ajaxCall("POST", api + "UserServices", JSON.stringify(newUser), postRegisterSCB, postRegisterECB);
    return false;
}
function postRegisterSCB(data) { // הוספת משתמש הצליחה
    alert("משתמש נוסף בהצלחה");
    window.location.assign("Teams-main.html");
    location.assign("Teams-main.html")
}

function postRegisterECB(err) {
    alert(err);
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


function hideIsActiveDiv() {
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
        else {
            $("#Different_school").attr("readonly", true);
            document.getElementById('Different_school').value = '';
        }
    }
}

