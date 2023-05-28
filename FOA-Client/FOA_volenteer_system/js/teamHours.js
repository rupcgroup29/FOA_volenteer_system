var api;
var usersArr = [];
var currentTeamId = sessionStorage.getItem("teamID");


$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    healine = " דיווחי השעות שנותר לי לאשר";
    $("#headline").html(healine);
    GetHoursDetails();

});

//GET the shifts that this team leader still needs to aprove
function GetHoursDetails() {
    ajaxCall("GET", api + "Teams/GetUsersHourReportsInTeam/" + currentTeamId, "", getHoursDetailsSCB, getHoursDetailsECB);
}
function getHoursDetailsSCB(data) {
    RenderHoursList(data);
}
function getHoursDetailsECB(err) {
    alert(err);
}


//Render Hours List
function RenderHoursList(array) {
    if (array.length == 0) {
        alert("סיימת לאשר את כל דיווחי השעות של הצוות שלך!");
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
                    { data: 'userName' },
                    {
                        data: 'date',
                        render: function (data, type, row) {
                            // Convert the datetime value to date
                            const datetime = new Date(data);
                            const day = datetime.getDate().toString().padStart(2, '0');
                            const month = (datetime.getMonth() + 1).toString().padStart(2, '0');
                            const year = datetime.getFullYear();
                            const formattedDate = day + '/' + month + '/' + year;

                            return formattedDate;
                        }
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
                    { data: 'count' },
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
                                    <select class="status-select" onchange="handleSelectionChange('${data.reportID}', '${data.userID}', this.value)">
                                        <option value="0" selected>טרם נקבע</option>
                                        <option value="1">אושר</option>
                                        <option value="2">נדחה</option>
                                    </select>`;
                            }
                            return selectionHtml;
                        },
                    }
                ],
                order: [[1, 'desc']],  // sort the sec column (date) in descending order
                language: {
                    url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/he.json'
                }
            });
        } catch (err) {
            alert(err);
        }
    }
}

//handle selection change and add hour report to the selectionChanges array
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
    alert(err);
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
    alert("Failed to delete the hour report. Error: " + err.responseText);
}