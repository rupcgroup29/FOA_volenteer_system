var api;
var usersArr = [];
var currentTeamId = JSON.parse(sessionStorage.getItem("team"));


$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    GetTeamDetails();
    ReadUsers();

});

function GetTeamDetails() {
    ajaxCall("GET", api + "Teams/teamDetails/" + currentTeamId, "", getTeamDetailsSCB, getTeamDetailsECB);
}
function getTeamDetailsSCB(data) {
    RenderTeamDetails(data);
}
function getTeamDetailsECB(err) {
    alert(err.responseJSON.errorMessage);
}

function RenderTeamDetails(data) {
    healine = "פרטי צוות " + data.teamName;
    $("#headline").html(healine);

    str = '<h3 class="teamDetails">';
    str += data.description
    str += '</h3>';
    str += '<h3 class="teamDetails">';
    str += 'ראש הצוות- ';
    str += data.fullname;
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
    alert(err.responseJSON.errorMessage);
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
                            viewBtn = '<button class="view-button" onclick="OpenUserCard(' + row.userID + ')">צפייה</button>';;
                            return viewBtn;
                        }
                    }
                ],
                language: {
                    url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/he.json'
                }
            });
        } catch (err) {
            alert(err.responseJSON.errorMessage);
        }
    }
}

// save the relevant user to open in edit\view mode (Depends on permission)
function OpenUserCard(userID) {
    sessionStorage.setItem("userCard", JSON.stringify(userID));
    window.location.href = "UserCard.html";
}
