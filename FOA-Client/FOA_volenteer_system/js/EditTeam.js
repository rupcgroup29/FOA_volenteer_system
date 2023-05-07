var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));
var currentTeamId = JSON.parse(sessionStorage.getItem("team"));
var currentTeamDetails;
var TeamLeadersList = [];



$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    $('#contactForm').submit(AddNewTeam);

    // get the Team Leaders list
    getTeamLeadersList();

    // get the Team's Details
    getTeamDetails();
});


function EditTeam() {
    const Team = {
        teamID: currentTeamId, 
        teamName: $("#TeamName").val(),
        description: $("#description").val(),
        teamLeader: $("#teamLeader").val()
    }

    ajaxCall("PUT", api + "Teams/" + currentTeamId, JSON.stringify(Team), putEditTeamSCB, putEditTeamECB);
    return false;
}
function putEditTeamSCB(data) { 
    alert("הצוות התעדכן בהצלחה");
    window.location.assign("Teams-main.html");
    location.assign("Teams-main.html")
}

function putEditTeamECB(err) {
    alert(err);
}

// get the Team Details
function getTeamDetails() {
    // לוודא את התוספת של האייפיאיי כי כתבתי את זה לפני שהייתה קיימת הפקודה
    ajaxCall("GET", api + "Teams/teamDetails/" + currentTeamId, "", getTeamDetailsSCB, getTeamDetailsECB);
    return false;
}

function getTeamDetailsSCB(data) {
    if (data == null) {
        alert("There's no team leader that haven't been assigned yet");
    } else {
        currentTeamDetails = data;
        RenderDetails();
    }
}
function getTeamDetailsECB(err) {
    console.log(err);
}

function RenderDetails() {
    //Card Header
    let str_header = "";
    str_header += `<h2 class="section-heading text-uppercase"> צוות מספר ` + currentTeamId + `</h2>`;
    document.getElementById("CardHeader").innerHTML += str_header;
    //teamLeader
    let teamLeader = "";
    teamLeader += '<option class="opt" value="' + currentTeamDetails.userID + '">' + relevantUserObject.fullname + '</option>';
    for (var i = 0; i < data.length; i++) {
        teamLeader += '<option class="opt" value="' + TeamLeadersList[i].userID + '">' + TeamLeadersList[i].firstName + ' ' + TeamLeadersList[i].lastName + '</option>';
    }
    document.getElementById("teamLeader").innerHTML += teamLeader;
    //TeamName
    $("#TeamName").val(currentTeamDetails.teamName);
    //description
    $("#description").val(currentTeamDetails.description);
}

// get the Team Leaders List
function getTeamLeadersList() {
    ajaxCall("GET", api + "Teams/teamLeadersWithoutTeam", "", getTeamLeadersListSCB, getTeamLeadersListECB);
    return false;
}

function getTeamLeadersListSCB(data) {
    if (data == null) {
        alert("There's no team leader that haven't been assigned yet");
    } else {
        TeamLeadersList = data;
    }
}
function getTeamLeadersListECB(err) {
    console.log(err);
}
