var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    readLogs();

});

// read all posts
function readLogs() {
    ajaxCall("GET", api + "Logs", "", readLogsSCB, readLogsECB);
}
function readLogsSCB(data) {
    drawLogsDataTable(data);
}
function readLogsECB(err) {
    alert(err.responseJSON.errorMessage);
}

// render the log list
function drawLogsDataTable(array) {
    let str = "";
    if (array.length == 0) {
        alert("There's no logs yet");
    } else {
        try {
            tbl = $('#dataTable').DataTable({
                "info": false,
                data: array,
                pageLength: 10,     //כמה שורות יהיו בכל עמוד

                columns: [
                    { data: 'id', orderSequence: ['desc', 'asc'] }, // specify orderSequence
                    {
                        data: "timestamp",
                        render: function (data, type, row, meta) {
                            const withoutMilisec = data.toLocaleString().split('.')[0];  // cut the mili secounds from the DateTime format
                            return withoutMilisec.toLocaleString().replace('T', ' ');  // seperate date & time from the DateTime format
                        }
                    },
                    { data: "action" },
                    { data: "table_name" },
                    { data: "description" }
                ],
                order: [[0, 'desc']],  // sort the first column (postID) in descending order
                language: {
                    url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/he.json'
                }
            });
        } catch (err) {
            alert(err.responseJSON.errorMessage);
        }
    }
}
