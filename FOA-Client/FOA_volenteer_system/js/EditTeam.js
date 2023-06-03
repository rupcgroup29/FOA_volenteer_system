var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));
var currentTeamId = JSON.parse(sessionStorage.getItem("team"));
var TeamLeadersList = [];



$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    $('#contactForm').submit(EditTeamDetails);

    // get the Team Leaders list
    getTeamLeadersList();

    // get the Team's Details
    getTeamDetails();

    // get the users details
    getUsersDetails();

});


function EditTeamDetails() {
    const Team = {
        teamID: currentTeamId,
        teamName: $("#TeamName").val(),
        description: $("#description").val(),
        teamLeader: $("#teamLeader").val(),
    }

    ajaxCall("PUT", api + "Teams", JSON.stringify(Team), putEditTeamSCB, putEditTeamECB);

    return false;
}
function putEditTeamSCB(data) {
    ChangeUsersTeam();
}
function putEditTeamECB(err) {
    alert(err.responseJSON.errorMessage);
}

// Updating the teamID to all the volunteers that the user added to the team
function ChangeUsersTeam() {
    ajaxCall("PUT", api + "UserServices/team", JSON.stringify(selectedUsers), putChangeUsersTeamSCB, putChangeUsersTeamECB);
    return false;
}
function putChangeUsersTeamSCB(data) {
    alert("הצוות התעדכן בהצלחה");
    window.location.assign("Teams-main.html");
    location.assign("Teams-main.html")
}
function putChangeUsersTeamECB(err) {
    alert(err.responseJSON.errorMessage);
}


// get the users details
function getUsersDetails() {
    ajaxCall("GET", api + "UserServices", "", getUsersDetailsSCB, getUsersDetailsECB);
    return false;
}
function getUsersDetailsSCB(data) {
    if (data == null) {
        alert("There's no users yet");
    } else {
        var teamMembers = '<option class="opt" value="0">בחר.י ממתנדב.ת להוספה לצוות</option>';
        for (var i = 0; i < data.length; i++) {
            if (data[i].permissionID == 4 && data[i].teamID != currentTeamId) {
                teamMembers += '<option class="opt" value="' + data[i].userID + '">' + data[i].userName + '</option>';
            }
        }
        $("#teamMembers").html(teamMembers);
        $("#teamMembers").change(addToSelectedUsers);
    }
}
function getUsersDetailsECB(err) {
    console.log(err);
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
    str_header += `<h2 class="section-heading text-uppercase"> עריכת צוות ` + data.teamName + `</h2>`;
    document.getElementById("CardHeader").innerHTML += str_header;
    //team Leader
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

//adding members
var selectedUsers = [];
function addToSelectedUsers() {
    var selectedUserId = $("#teamMembers").val();
    var selectedUserName = $("#teamMembers option:selected").text();

    if (selectedUserId !== "0" && !isUserAlreadySelected(selectedUserId)) {
        var user = {
            userID: parseInt(selectedUserId),
            teamID: parseInt(currentTeamId),
            name: selectedUserName
        };

        selectedUsers.push(user);
        displaySelectedUsers();
    }
}


function isUserAlreadySelected(userId) {
    return selectedUsers.some(function (user) {
        return user.id === userId;
    });
}

function removeFromSelectedUsers(userId) {
    selectedUsers = selectedUsers.filter(function (user) {
        return user.id !== userId;
    });
    displaySelectedUsers();
}

function displaySelectedUsers() {
    var selectedUsersList = "";
    for (var i = 0; i < selectedUsers.length; i++) {
        selectedUsersList += "<li>" + selectedUsers[i].name + " <button class='delete-button' onclick='removeFromSelectedUsers(\"" + selectedUsers[i].id + "\")'><i class='fas fa-times'></i></button></li>";
    }
    $("#selectedUsersList").html(selectedUsersList);
}




