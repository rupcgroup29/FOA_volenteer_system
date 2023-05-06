var api;
var isLoggedIn;
var TeamsArr = [];
var currentUser = sessionStorage.getItem("user");


$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    readTeams();
});



// read all Teams
function readTeams() {
    ajaxCall("GET", api + "Teams/teamsDetails", "", getAllTeamsSCB, getAllTeamsECB);
}
function getAllTeamsSCB(data) {
    TeamsArr = data;
    RenderTeams();

}
function getAllTeamsECB(err) {
    alert("Input Error");
}

//Render Teams
function RenderTeams()
{
    if (TeamsArr == null) {
        alert("There are no teams yet");
    } else {
        let str = "";
        for (var i = 0; i < TeamsArr.length; i++) {
            str += '<div class="col-4 TeamBox">';
            str += '<div class="BoxBorder">';
            str += '<h3>צוות ' + TeamsArr[i].teamName + '</h3>';
            str += '<p>מנהל.ת הצוות: ' + TeamsArr[i].fullname + '</p>';
            str += '<p>כמות מתנדבים בצוות: ' + TeamsArr[i].noOfVolunteerUsers + '</p>';
            str += '<button onclick="OpenTeamCard(' + TeamsArr[i].teamID + ')">צפייה</button>';
            str += '<button onclick="EditTeam(' + TeamsArr[i].teamID + ')">עריכה</button>';
            str += '</div>';
            str += '</div>';
        }
        document.getElementById("TeamBoxesRenderDiv").innerHTML += str;
    }
}

// save the relevant team to open it
function OpenTeamCard(teamID) {
    sessionStorage.setItem("team", JSON.stringify(teamID));
    window.location.href = "UsersInTeam.html";
}


function EditTeam(teamID) {
    sessionStorage.setItem("team", JSON.stringify(teamID));
    window.location.href = "EditTeam.html";
}
