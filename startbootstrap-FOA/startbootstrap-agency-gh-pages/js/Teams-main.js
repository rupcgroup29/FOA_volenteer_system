var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));
var teamsList = [];

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    // לעדכן את הכתובת החלופית !!
    //else api = "https://proj.ruppin.ac.il/cgroup29/test2/tar1/api/Users/";

    $('#contactForm').submit(RegisterUser);

    // get the team list
    getTeamsList();


});


//dataTable contains all teams
function usersTable() {
    getTeamsList();

}


// get the Teams list
function getTeamsList() {
    ajaxCall("GET", api + "Teams", "", getTeamSCB, getTeamECB);
    return false;
}
function getTeamSCB(data) {
    teamsList = data;
    drawTable(teamsList);
}
function getTeamECB(err) {
    console.log(err);
}


// draw the teams - dataTable in the div
function drawTable(arr) {
    try {
        tbl = $('#teamsTable').DataTable({
            data: arr,
            pageLength: 10,     //כמה שורות יהיו בכל עמוד

            columns: [
                //{
                //    render: function (data, type, row, meta) {      //יצירת כפתור עריכה
                //        let dataUser = "data-userId='" + row.email + "'";
                //        '<input type="checkbox" ' + row + ' onclick="checkboxEdit(this)" />';
                //    }
                //},

                { data: "firstName" },
                { data: "familyName" },
                { data: "email" },
                {
                    data: "isActive",
                    render: function (data, type, row, meta) {
                        let dataUser = "data-userId='" + row.email + "'";

                        if (data == true)
                            return '<input type="checkbox" checked ' + dataUser + ' onclick="checkboxEdit(this)" />';
                        else
                            return '<input type="checkbox" ' + dataUser + ' onclick="checkboxEdit(this)" />';
                    }
                },
                {
                    data: "isAdmin",
                    render: function (data, type, row, meta) {
                        if (data == true)
                            return '<input type="checkbox" checked disabled="disabled" />';
                        else
                            return '<input type="checkbox" disabled="disabled"/>';
                    }
                }
            ]
        });
    }
    catch (err) {
        alert(err);
    }
}


