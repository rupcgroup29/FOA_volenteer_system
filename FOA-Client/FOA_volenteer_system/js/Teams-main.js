var api;
var isLoggedIn;
var usersArr = [];
var currentUser = sessionStorage.getItem("user");


$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    readUsers();
    FilterByTeamName();
});

function FilterByTeamName() {
    // Declare variables
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("dataTable");
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
    ajaxCall("GET", api + "UserServices/AllUsers", "", getAllUsersSCB, getAllUsersECB);
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
        str += '<th>צוות</th>';
        str += '<th>שם מלא</th>';
        str += '<th>שם משתמש</th>';
        str += '<th>אימייל</th>';
        str += '<th></th>';
        str += '</tr>';
        for (var i = 0; i < usersArr.length; i++) {
            str += '<tr>';
            str += '<td class="teamName_display">' + usersArr[i].teamName + '</td>';
            str += '<td class="fullName_display">' + usersArr[i].firstName + " " + usersArr[i].surname + '</td>';
            str += '<td class="userName_display">' + usersArr[i].userName + '</td>';
            str += '<td class="email_display">' + usersArr[i].email + '</td>';
            str += '<td class="viewButton_display""><button onclick="OpenUserCard(' + usersArr[i].userID + ')">כרטיס מתנדב</a></button></td>';
            str += '</tr>';
        }
        str += '</table>';
        document.getElementById("UsersTable").innerHTML += str;
    }
}
// save the relevant user to open in edit\view mode (Depends on permission)
function OpenUserCard(userID) {
    sessionStorage.setItem("userCard", JSON.stringify(userID));
    window.location.href = "UserCard.html";
}
