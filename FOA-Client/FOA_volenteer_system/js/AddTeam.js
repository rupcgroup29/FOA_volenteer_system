var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    $('#contactForm').submit(AddNewTeam);

    // get the Team Leaders list
    getTeamLeadersList();
});


function AddNewTeam() {
    const newTeam = {
        teamID: 0, //במטרה לשלוח אובייקט צוות שלם, ישתנה בדאטה בייס
        teamName: $("#TeamName").val(),
        description: $("#description").val(),
        teamLeader: $("#teamLeader").val()
    }

    ajaxCall("POST", api + "Teams", JSON.stringify(newTeam), postAddNewTeamSCB, postAddNewTeamECB);
    return false;
}
function postAddNewTeamSCB(data) { 
    alert("הצוות נוסף בהצלחה");
    window.location.assign("Teams-main.html");
    location.assign("Teams-main.html")
}

function postAddNewTeamECB(err) {
    alert(err.responseJSON.errorMessage);
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
        let str = "";
        str += '<option class="opt" value="0">בחירת ראש צוות *</option>';
        for (var i = 0; i < data.length; i++) {
            str += '<option class="opt" value="' + data[i].userID + '">' + data[i].fullname + '</option>';
        }
        document.getElementById("teamLeader").innerHTML += str;
    }
}
function getTeamLeadersListECB(err) {
    console.log(err.responseJSON.errorMessage);
}
