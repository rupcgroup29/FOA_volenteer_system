var api;
var currentUser = sessionStorage.getItem("user");
var currentUserDetails;
var usersArr = [];
var currentTeamId;

// Define an array to store the changed selection values
var selectionChanges = [];

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    getMyHours();
    readMyDetails();
    ViewByPermissions();

    // for the popup alert for team leaders:  
    closePopup();

});

//headline 
function renderHeadline(data) {
    if (data.hoursCount == 0) {
        str = "<h2>";
        str += data.firstName + ", טרם אושרו לך שעות התנדבות";
        str += "</h2>";
        document.getElementById("subHeadline").innerHTML += str;
    }
    else {
        str = "<h2>";
        str += data.firstName + ", אושרו לך עד היום " + data.hoursCount + " שעות התנדבות! כל הכבוד!";
        str += "</h2>";
        document.getElementById("subHeadline").innerHTML += str;
    }
}

// get My Hours
function getMyHours() {
    ajaxCall("GET", api + "HourReports/" + currentUser[0], "", getMyHoursSCB, getMyHoursECB);
    return false;
}
function getMyHoursSCB(data) {
    RenderHoursList(data);
}
function getMyHoursECB(err) {
    alert(err.responseJSON.errorMessage);
}

//Render Hours List
function RenderHoursList(array) {
    if (array.length == 0) {
        alert("עדיין לא דווחו שעות למשתמש.ת");
    } else {
        try {
            // Check if the DataTable is already initialized
            if ($.fn.DataTable.isDataTable('#dataTable')) {
                // Destroy the existing DataTable instance
                $('#dataTable').DataTable().destroy();
            }

            // Clear the table container
            $('#dataTableContainer').empty();

            // Custom sorting plugin for date column
            jQuery.extend(jQuery.fn.dataTableExt.oSort, {
                "date-eu-pre": function (a) {
                    const dateParts = a.split('/');
                    return new Date(dateParts[2], dateParts[1] - 1, dateParts[0]);
                },

                "date-eu-asc": function (a, b) {
                    return ((a < b) ? -1 : ((a > b) ? 1 : 0));
                },

                "date-eu-desc": function (a, b) {
                    return ((a < b) ? 1 : ((a > b) ? -1 : 0));
                }
            });

            // Create the new table
            tbl = $('#dataTable').DataTable({
                "info": false,
                data: array,
                pageLength: 10,
                columns: [
                    {
                        data: null,
                        render: function (data, type, row) {
                            let trashcanHtml = '';
                            if (data.status === 0) {
                                trashcanHtml = `
                                    <span class="delete-icon" onclick="confirmDelete('${data.reportID}')">
                                        <i class="fas fa-trash-alt"></i>
                                    </span>`;
                            }
                            return trashcanHtml;
                        }
                    },
                    {
                        data: 'date',
                        render: function (data, type, row) {
                            // Convert the datetime value to date
                            const datetime = new Date(data);
                            const formattedDate = datetime.toLocaleDateString('en-GB', {
                                day: '2-digit',
                                month: '2-digit',
                                year: 'numeric'
                            });

                            return formattedDate;
                        },
                        type: 'date-eu', // Use the custom sorting plugin for date column
                    },
                    {
                        data: "startTime",
                        render: function (data, type, row) {
                            // Convert the datetime value to time
                            const datetime = new Date(data);
                            const hours = datetime.getHours().toString().padStart(2, '0');
                            const minutes = datetime.getMinutes().toString().padStart(2, '0');
                            const time = hours + ':' + minutes;

                            return time;
                        }
                    },
                    {
                        data: "endTime",
                        render: function (data, type, row) {
                            // Convert the datetime value to time
                            const datetime = new Date(data);
                            const hours = datetime.getHours().toString().padStart(2, '0');
                            const minutes = datetime.getMinutes().toString().padStart(2, '0');
                            const time = hours + ':' + minutes;

                            return time;
                        }
                    },
                    { data: 'shiftTime' },
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
                    {
                        data: null,
                        render: function (data, type, row) {
                            let selectionHtml = "";
                            if (data.status === 0 && currentUser[1] !== 4) {
                                selectionHtml = `
                                    <select class="hoursStatus status-select" onchange="handleSelectionChange('${data.reportID}', '${data.userID}', this.value)">
                                        <option value="0" selected>טרם נקבע</option>
                                        <option value="1">אושר</option>
                                        <option value="2">נדחה</option>
                                    </select>`;
                            }
                            return selectionHtml;
                        },
                    }
                ],
                order: [[1, 'desc']],  // sort the second column (date) in descending order
                language: {
                    url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/he.json'
                }
            });
        } catch (err) {
            alert(err.responseJSON.errorMessage);
        }
    }
}

// Function to handle selection change and add object to the selectionChanges array
function handleSelectionChange(reportID, userID, status) {
    const selectionObj = {
        reportId: reportID,
        status: parseInt(status),
        userId: userID
    };
    selectionChanges.push(selectionObj);
}

//save team leader statuses
function updateStatus() {
    ajaxCall("PUT", api + "HourReports", JSON.stringify(selectionChanges), updateStatusSCB, updateStatusECB);
    return false;
}
function updateStatusSCB(data) {
    alert("דיווחים התעדכנו בהצלחה");
    window.location.assign("Hours-Main.html");
    location.assign("Hours-Main.html")
}
function updateStatusECB(err) {
    alert(err.responseJSON.errorMessage);
}


//confirm delete Hours
function confirmDelete(reportID) {
    const confirmation = confirm("בטוח.ה שברצונך למחוק את משמרת זו?");
    if (confirmation) {
        deleteHours(reportID);
    }
}

//delete Hours
function deleteHours(reportID) {
    ajaxCall("DELETE", api + "HourReports/" + reportID, "", deleteHoursSCB, deleteHoursECB);
    return false;
}
function deleteHoursSCB(num) {
    alert("דיווח השעות נמחק בהצלחה!");
    window.location.assign("Hours-Main.html");
    location.assign("Hours-Main.html")
}
function deleteHoursECB(err) {
    alert("Failed to delete the hour report. Error: " + err.responseJSON.errorMessage);
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
    if (currentUser[1] === 2 || currentUser[1] === 1) { //manager
        getAllVolunteers();
    }
    renderHeadline(data);
}
function readMyDetailsECB(err) {
    alert(err.responseJSON.errorMessage);
}



// get Volunteers In My Team
function getVolunteersInMyTeam() {
    currentTeamId = currentUserDetails.teamID;
    sessionStorage.setItem("teamID", JSON.stringify(currentTeamId));
    ajaxCall("GET", api + "UserServices/usersInTeam/" + currentTeamId, "", getVolunteersInMyTeamSCB, getVolunteersInMyTeamECB);
}
function getVolunteersInMyTeamSCB(data) {
    usersArr = data;
    renderVolunteersInMyTeam(usersArr);
}
function getVolunteersInMyTeamECB(err) {
    alert(err.responseJSON.errorMessage);
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
    alert(err.responseJSON.errorMessage);
}

//render Volunteers In My Team (to selection list- permission 3)
function renderVolunteersInMyTeam(usersArr) {
    let str = "";
    str += `<option value="` + currentUserDetails.userID + `">` + currentUserDetails.userName + `</option>`;
    for (var i = 0; i < usersArr.length; i++) {
        if (currentUserDetails.userID != usersArr[i].userID) {
            str += `<option value="` + usersArr[i].userID + `">` + usersArr[i].userName + `</option>`;
        }
    }
    document.getElementById("usersList").innerHTML += str;

    if (currentUser[1] === 3) { //team leader
        AlertShiftsForApproval();
    }
}

function ChangeVolHours() {
    getVolunteerHours($("#usersList").val());
}

// get all Volunteer Hours (to selection list- permission 2+1)
function getVolunteerHours(userID) {
    ajaxCall("GET", api + "HourReports/" + userID, "", getVolunteerHoursSCB, getVolunteerHoursECB);
    return false;
}
function getVolunteerHoursSCB(data) {
    RenderHoursList(data);
}
function getVolunteerHoursECB(err) {
    alert("Error in getting hours " + err.responseJSON.errorMessage);
}

//hide Hours Main Buttons from premmition 4
function ViewByPermissions() {
    if (currentUser[1] === 4) //volunteer
    {
        document.getElementById("allUsersReportsButton").style.display = "none";
        document.getElementById("teamUsersReportsButton").style.display = "none";
        document.getElementById("UsersOptions").style.display = "none";
    }
    if (currentUser[1] === 3)//team leader
    {
        document.getElementById("allUsersReportsButton").style.display = "none";
        document.getElementById("UsersOptions").classList.add("col-4");
        document.getElementById("teamUsersReportsButton").classList.add("col-4");
        document.getElementById("addNewHourReportButton").classList.add("col-4");
    }
    if (currentUser[1] === 2 || currentUser[1] === 1)
    {
        document.getElementById("teamUsersReportsButton").style.display = "none";
        document.getElementById("allUsersReportsButton").classList.add("col-4");
        document.getElementById("addNewHourReportButton").classList.add("col-4");
        document.getElementById("UsersOptions").classList.add("col-4");
    }
}

// התראות למנהל צוות על כמות דיווחים שטרם אושרו
function AlertShiftsForApproval() {
    ajaxCall("GET", api + "Teams/GetUsersHourReportsInTeam/" + currentTeamId, "", AlertShiftsForApprovalSCB, AlertShiftsForApprovalECB);
    return false;
}
function AlertShiftsForApprovalSCB(data) {
    if (data.length == 0) {
        str_popup = 'אין דיווחי שעות הממתינים לאישורך';
    }
    else {
        str_popup = 'ישנם ' + data.length + ' דיווחי שעות הממתינים לאישורך';
    }
    $('#popupHeadline1').text(str_popup);
    openPopup1();
}
function AlertShiftsForApprovalECB(err) {
    alert(err.responseJSON.errorMessage);
}

function openPopup1() {
    let popup = document.getElementById('popup1');
    popup.classList.add('open-popup');
}
function closePopup() {
    let popup = document.getElementById('popup1');
    popup.classList.remove('open-popup');
}