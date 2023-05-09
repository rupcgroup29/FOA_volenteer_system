var api;
var isLoggedIn;
var usersArr = [];
var currentTeamId = JSON.parse(sessionStorage.getItem("team"));
var currentTeamId = JSON.parse(sessionStorage.getItem("team"));
var currentTeamLeader;


$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    healine = "פרטי צוות " + currentTeamId;
    $("#headline").html(healine);
    GetTeamDetails();
    ReadUsers();

});

function GetTeamDetails() {
    ajaxCall("GET", api + "Teams/teamDetails/" + currentTeamId, "", getTeamDetailsSCB, getTeamDetailsECB);
}
function getTeamDetailsSCB(data) {
    RenderTeamDetails(data);
function GetTeamLeaderName() {
    ajaxCall("GET", api + "UserServices/teamLeader/" + currentTeamId, "", getTeamLeaderSCB, getTeamLeaderECB);
}
function getTeamLeaderSCB(data) {
    currentTeamLeader = data;
    RenderTeamDetails();
}
function getTeamDetailsECB(err) {
    alert(err);
}

function RenderTeamDetails(data) {  
function RenderTeamDetails() {  
    str = '<h3 class="teamDetails">';
    str += data.description
    str += '</h3>';
    str += '<h3 class="teamDetails">';
    str += 'ראש הצוות- ';
    str += data.fullname;
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
