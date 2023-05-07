var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));
var currentTeamId = JSON.parse(sessionStorage.getItem("team"));
var TeamLeadersList = [];



$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    $('#contactForm').submit(EditTeam);

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

    ajaxCall("PUT", api + "Teams", JSON.stringify(Team), putEditTeamSCB, putEditTeamECB);
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
    ajaxCall("GET", api + "Teams/teamDetails/" + currentTeamId, "", getTeamDetailsSCB, getTeamDetailsECB);
    return false;
}

function getTeamDetailsSCB(data) {
    if (data == null) {
        alert("There's no team leader that haven't been assigned yet");
    } else {
        
        RenderDetails(data);
    }
}
function getTeamDetailsECB(err) {
    console.log(err);
}

function RenderDetails(data) {
    //Card Header
    let str_header = "";
    str_header += `<h2 class="section-heading text-uppercase"> צוות מספר ` + currentTeamId + `</h2>`;
    document.getElementById("CardHeader").innerHTML += str_header;
    //teamLeader
    let teamLeader = "";
    teamLeader += '<option class="opt" value="' + data.userID + '">' + data.fullname + '</option>';
    for (var i = 0; i < TeamLeadersList.length; i++) {
        teamLeader += '<option class="opt" value="' + TeamLeadersList[i].userID + '">' + TeamLeadersList[i].fullname + '</option>';
    }
    document.getElementById("teamLeader").innerHTML += teamLeader;
    //TeamName
    $("#TeamName").val(data.teamName);
    //description
    $("#description").val(data.description);
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
