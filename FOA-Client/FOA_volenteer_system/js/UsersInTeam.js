﻿var api;
var isLoggedIn;
var usersArr = [];
//user = {
//    0: 10,
//    1: 3
//}
//sessionStorage.setItem("user", JSON.stringify(user));
//var currentUser = JSON.parse(sessionStorage.getItem("user"));

//team = {
//    "teamID": 1,
//    "teamName": "ניטור 01",
//    "description": "צוות ניטור תכנים אנטישמיים ברשת, מספר 1",
//    "teamLeader": 10
//}
//sessionStorage.setItem("team", JSON.stringify(team));
var currentTeamId = JSON.parse(sessionStorage.getItem("team"));
var currentTeamLeader;


$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    healine = "פרטי צוות " + currentTeamId;
    $("#headline").html(healine);
    GetTeamLeaderName();
    ReadUsers();

});

function GetTeamLeaderName() {
    ajaxCall("GET", api + "UserServices/teamLeader/" + currentTeamId, "", getTeamLeaderSCB, getTeamLeaderECB);
}
function getTeamLeaderSCB(data) {
    currentTeamLeader = data;
    RenderTeamDetails();
}
function getTeamLeaderECB(err) {
    alert(err);
}

function RenderTeamDetails() {  
    str = '<h3 class="teamDetails">';
    str += 'להשלים פה את תיאור הקבוצה';
    str += '</h3>';
    str += '<h3 class="teamDetails">';
    str += 'הצוות מנוהל ע"י ';
    str += currentTeamLeader.firstName + ' ' + currentTeamLeader.surname;
    str += '</h3>';
    document.getElementById("teamDetails").innerHTML += str;
}


// read all users
function ReadUsers() {
    ajaxCall("GET", api + "UserServices/usersInTeam/" + currentTeamId, "", getAllUsersSCB, getAllUsersECB);
}
function getAllUsersSCB(data) {
    usersArr = data;
    RenderUsersList(usersArr);
}
function getAllUsersECB(err) {
    alert("Error " + err);
}

// render the team's users list
function RenderUsersList(array) {
    if (array.length == 0) {
        alert("There's no users in this team yet");
    } else {
        try {
            tbl = $('#dataTable').DataTable({
                "info": false,
                data: array,
                pageLength: 10,     //כמה שורות יהיו בכל עמוד

                columns: [
                    { data: 'userName' },
                    { data: "firstName" },
                    { data: "surname" },
                    { data: "email" },
                    { data: "phoneNum" },
                    {
                        render: function (data, type, row, meta) {      //יצירת כפתור צפייה במשתמש הנבחר
                            viewBtn = '<button onclick="OpenUserCard(' + row.userID + ')">צפייה</button>';;
                            return viewBtn;
                        }
                    }
                ],
                language: {
                    url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/he.json'
                }
            });
        } catch (err) {
            alert(err);
        }
    }
}
// save the relevant user to open in edit\view mode (Depends on permission)
function OpenUserCard(userID) {
    sessionStorage.setItem("userCard", JSON.stringify(userID));
    window.location.href = "UserCard.html";
}
