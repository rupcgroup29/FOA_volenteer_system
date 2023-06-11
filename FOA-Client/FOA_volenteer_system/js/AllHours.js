var api;
var usersArr = [];
var currentTeamId = JSON.parse(sessionStorage.getItem("team"));


$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    healine = "כלל דיווחי השעות ";
    $("#headline").html(healine);
    GetHoursDetails();
    ReadUsers();

});

function GetHoursDetails() {
    ajaxCall("GET", api + "HourReports", "", getHoursDetailsSCB, getHoursDetailsECB);
}
function getHoursDetailsSCB(data) {
    RenderHourReports(data);
}
function getHoursDetailsECB(err) {
    alert(err);
}


// Render Hour Reports
function RenderHourReports(array) {
    if (array.length == 0) {
        alert("There's no Hour Reports yet");
    } else {
        try {
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

            tbl = $('#dataTable').DataTable({
                "info": false,
                data: array,
                pageLength: 10,     //כמה שורות יהיו בכל עמוד

                columns: [
                    { data: "teamName" },
                    { data: 'userName' },
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
                    }
                ],
                order: [[2, 'desc']],  // sort the second column (date) in descending order
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
