var api;
var isLoggedIn;
var usersArr = [];
var teamsArr = [];
/*    נשמר במטרה לחסוך את ההתחברות בעת בדיקות   */
var user = {
    userID: 1024,
    firstName: "ענת",
    surname: "אביטל",
    userName: "anat_a",
    phoneNum: "0529645123",
    roleDescription: "מנהל צוות ניטור",
    permissionID: 3,
    isActive: true,
    password: "6DCA4533",
    teamID: 1,
    programID: 1026,
    email: "anat_a@gmail.com",
    programName: null
}
sessionStorage.setItem("user", JSON.stringify(user));
var CurrentUser = sessionStorage.getItem("user");


$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";



    //Nav ber - Permission
    if (CurrentUser.permission == 4) // a volunteer is logged in
    {
        $(".ManagerNav").hide();
        $(".VolunteerNav").show();
    }
    else //Manager is logged in
    {
        $(".ManagerNav").show();
        $(".VolunteerNav").hide();
    }
    readUsers();
    readTeams();
    FilterByTeamName();
});

function FilterByTeamName() {
    // Declare variables
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

// read all users
function readUsers() {
    ajaxCall("GET", api + "UserServices", "", getAllUsersSCB, getAllUsersECB);
}
function getAllUsersSCB(data) {
    usersArr = data;
    RenderUsersList();

}
function getAllUsersECB(err) {
    alert("Input Error");
}

// render the users list
function RenderUsersList() {
    if (usersArr[0] == null) {
        alert("There's no users yet");
    } else {
        let str = "";
        str += '<table dir="rtl" id="myTable">';
        str += '<tr class="header">';
        str += '<th style="width:22.5%;">צוות</th>';
        str += '<th style="width:22.5%;">שם מלא</th>';
        str += '<th style="width:22.5%;">שם משתמש</th>';
        str += '<th style="width:22.5%;">אימייל</th>';
        str += '<th style="width:10%;"></th>';
        str += '</tr>';
        for (var i = 0; i < usersArr.length; i++) {
            let currentTeamName;
            for (var j = 0; j < teamsArr.length; j++) {
                if (usersArr[i].teamID == teamsArr[j].teamID)
                    currentTeamName = teamsArr[j].teamName;
            }
            str += '<tr>';
            str += '<td class="teamName_display">' + currentTeamName + '</td>';
            str += '<td class="fullName_display">' + usersArr[i].firstName + " " + usersArr[i].surname + '</td>';
            str += '<td class="userName_display">' + usersArr[i].userName + '</td>';
            str += '<td class="email_display">' + usersArr[i].email + '</td>';
            str += '<td class="viewButton_display""><button onclick="OpenUserCard(` + usersArr[i].userID + `)">כרטיס מתנדב</a></button></td>';
            str += '</tr>';
        }
        str += '</table>';
        document.getElementById("UsersTable").innerHTML += str;
    }
}
// save the relevant user to open in edit\view mode (Depends on permission)
function OpenUserCard(userID) {
    sessionStorage.setItem("userCard", JSON.stringify(userID));
    location.replace("UserCard.html");
}

// get the Teams list
function readTeams() {
    ajaxCall("GET", api + "Teams", "", readTeamsSCB, readTeamsECB);
    return false;
}

function readTeamsSCB(data) {
    teamsArr = data;
}
function readTeamsECB(err) {
    console.log(err);
}
