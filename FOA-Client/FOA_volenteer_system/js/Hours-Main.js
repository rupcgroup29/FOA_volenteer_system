var api;
var isLoggedIn;
var currentUser = sessionStorage.getItem("user");
var currentUserDetails;
var usersArr = [];


$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    getMyHours();
    if (currentUser[1] != 4) // Not a volunteer
    {
        readMyDetails();
    }

});

// get My Hours
function getMyHours() {
    ajaxCall("GET", api + "HourReports/" + currentUser[0], "", getMyHoursSCB, getMyHoursECB);
    return false;
}
function getMyHoursSCB(data) {
    RenderHoursList(data);
}
function getMyHoursECB(err) {
    alert("Input Error");
}

function RenderHoursList(array) {
    if (array.length == 0) {
        alert(" עדיין לא דווחו שעות למשתמש.ת");
    } else {
        try {
            // Check if the DataTable is already initialized
            if ($.fn.DataTable.isDataTable('#dataTable')) {
                // Destroy the existing DataTable instance
                $('#dataTable').DataTable().destroy();
            }

            // Clear the table container
            $('#dataTableContainer').empty();

            // Create the new table
            tbl = $('#dataTable').DataTable({
                "info": false,
                data: array,
                pageLength: 10,
                columns: [
                    {
                        data: 'date',
                        render: function (data, type, row) {
                            // Convert the datetime value to date
                            const datetime = new Date(data);
                            const date = datetime.toISOString().split('T')[0];

                            return date;
                        }
                    },
                    {
                        data: "startTime",
                        render: function (data, type, row) {
                            // Convert the datetime value to time
                            const datetime = new Date(data);
                            const time = datetime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });

                            return time;
                        }
                    },
                    {
                        data: "endTime",
                        render: function (data, type, row) {
                            // Convert the datetime value to time
                            const datetime = new Date(data);
                            const time = datetime.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });

                            return time;
                        }
                    },
                    {
                        data: "status",
                        render: function (data, type, row) {
                            let statusText = "";
                            switch (data) {
                                case 0:
                                    statusText = "טרם נקבע";
                                    break;
                                case 1:
                                    statusText = "אושר";
                                    break;
                                case 2:
                                    statusText = "נדחה";
                                    break;
                                default:
                                    statusText = "";
                                    break;
                            }
                            return statusText;
                        }
                    },
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

// read My Details
function readMyDetails() {
    ajaxCall("GET", api + "UserServices/user_details/" + currentUser[0], "", readMyDetailsSCB, readMyDetailsECB);
    return false;
}

function readMyDetailsSCB(data) {
    currentUserDetails = data;
    if (currentUser[1] == 3) { //team leader
        getVolunteersInMyTeam();
    }
    if (currentUser[1] == 2) { //manager
        getAllVolunteers();
    }
}

function readMyDetailsECB(err) {
    alert("Input Error");
}

// get Volunteers In My Team
function getVolunteersInMyTeam() {
    ajaxCall("GET", api + "UserServices/usersInTeam/" + currentUserDetails.teamID, "", getVolunteersInMyTeamSCB, getVolunteersInMyTeamECB);
}

function getVolunteersInMyTeamSCB(data) {
    usersArr = data;
    renderVolunteersInMyTeam(usersArr);
}

function getVolunteersInMyTeamECB(err) {
    alert("לא הצלחתי למצוא את המשתמשים בצוות שלך");
}

// get all Volunteers 
function getAllVolunteers() {
    ajaxCall("GET", api + "UserServices", "", getAllVolunteersSCB, getAllVolunteersECB);
}

function getAllVolunteersSCB(data) {
    usersArr = data;
    renderVolunteersInMyTeam(usersArr);
}

function getAllVolunteersECB(err) {
    alert("לא הצלחתי למצוא את המשתמשים");
}

function renderVolunteersInMyTeam(usersArr) {
    let str = "";
    str += `<option value="` + currentUserDetails.userID + `">` + currentUserDetails.userName + `</option>`;
    for (var i = 0; i < usersArr.length; i++) {
        if (currentUserDetails.userID != usersArr[i].userID) {
            str += `<option value="` + usersArr[i].userID + `">` + usersArr[i].userName + `</option>`;
        }

    }
    document.getElementById("usersList").innerHTML += str;
}

function ChangeVolHours() {
    getVolunteerHours($("#usersList").val());
}

// get Volunteer Hours
function getVolunteerHours(userID) {
    ajaxCall("GET", api + "HourReports/" + userID, "", getVolunteerHoursSCB, getVolunteerHoursECB);
    return false;
}
function getVolunteerHoursSCB(data) {
    RenderHoursList(data);
}
function getVolunteerHoursECB(err) {
    alert("Input Error");
}