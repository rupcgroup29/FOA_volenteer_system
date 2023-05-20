var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));
let rowNum = 0;
let ShiftsArr = [];
let ShiftsToSend = [];

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    RenderOneMoreRow();

    $('#contactForm').submit(AddShifts);

});

function AddShifts() {
    for (var i = 0; i < rowNum; i++) {
        let dateValue = $("#SelectedDate" + i).val();
        const date = new Date(dateValue);
        date.setHours(0);
        date.setMinutes(0);
        date.setSeconds(0);
        date.setMilliseconds(0);
        const datetimeValue = date.toISOString();

        const startTimeValue = $("#StartHour" + i).val();
        const [startHours, startMinutes] = startTimeValue.split(":");
        const startTime = new Date(date.getTime());
        startTime.setUTCHours(startHours - date.getTimezoneOffset() / 60);
        startTime.setMinutes(startMinutes);
        startTime.setSeconds(0);
        startTime.setMilliseconds(0);
        const startDateTimeValue = startTime.toISOString();

        const endTimeValue = $("#FinishHour" + i).val();
        const [endHours, endMinutes] = endTimeValue.split(":");
        const endTime = new Date(date.getTime());
        endTime.setUTCHours(endHours - date.getTimezoneOffset() / 60);
        endTime.setMinutes(endMinutes);
        endTime.setSeconds(0);
        endTime.setMilliseconds(0);
        const endDateTimeValue = endTime.toISOString();

        const Shift = {
            userID: currentUser[0],
            date: datetimeValue,
            startTime: startDateTimeValue,
            endTime: endDateTimeValue
        };

        ShiftsToSend[i] = Shift;
    }
    ajaxCall("POST", api + "HourReports", JSON.stringify(ShiftsToSend), postAddShiftsSCB, postAddShiftsECB);
    return false;
}


function postAddShiftsSCB(data) {
    alert("המשמרות נוספו בהצלחה");
    window.location.assign("Hours-Main.html");
    location.assign("Hours-Main.html")
}

function postAddShiftsECB(err) {
    alert("לא הצלחנו להוסיף את המשמרות שהזנת");
}

function RenderOneMoreRow() {
    let str = "";
    str += `<div class="row" dir="rtl">`;
    str += `<div class="col-4 form-headers form-group ">`;
    str += `<p> תאריך</p>`;
    str += `<input id="SelectedDate` + rowNum + `" class="form-control" type="date" value="${$("#SelectedDate" + (rowNum - 1)).val() || ''}" />`; // Populate with previous row's value
    str += `</div>`;
    str += `<div class="col-4 form-headers form-group ">`;
    str += `<p> שעת התחלה</p>`;
    str += `<input id="StartHour` + rowNum + `" class="form-control" type="time" value="${$("#StartHour" + (rowNum - 1)).val() || ''}" />`; // Populate with previous row's value
    str += `</div>`;
    str += `<div class="col-4 form-headers form-group ">`;
    str += `<p> שעת סיום</p>`;
    str += `<input id="FinishHour` + rowNum + `" class="form-control" type="time" value="${$("#FinishHour" + (rowNum - 1)).val() || ''}" />`; // Populate with previous row's value
    str += `</div>`;
    str += `</div>`;
    document.getElementById("RenderShifts").innerHTML += str;

    rowNum++; // Increment the row number
}

