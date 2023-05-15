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
        // Assuming you have a date value in the format "YYYY-MM-DD"
        let dateValue = $("#SelectedDate" + i + "").val();
        // Create a new Date object with the date value
        const date = new Date(dateValue);
        // Set the time portion to the desired value (e.g., 12:00 PM)
        date.setHours(12);
        date.setMinutes(0);
        date.setSeconds(0);
        date.setMilliseconds(0);
        // The resulting datetime value
        const datetimeValue = date.toISOString(); // "2023-05-15T12:00:00.000Z"

        // Assuming you have a time value in the format "HH:mm"
        const startTimeValue = $("#StartHour" + i + "").val();
        // Create a new Date object with the current date
        const startTime = new Date();
        // Set the time portion to the desired value (using the time value)
        const [startHours, startMinutes] = startTimeValue.split(":");
        startTime.setHours(startHours);
        startTime.setMinutes(startMinutes);
        startTime.setSeconds(0);
        startTime.setMilliseconds(0);
        // The resulting datetime value
        const startDateTimeValue = startTime.toISOString();

        // Assuming you have a time value in the format "HH:mm"
        const endTimeValue = $("#FinishHour" + i + "").val();
        // Create a new Date object with the current date
        const endTime = new Date();
        // Set the time portion to the desired value (using the time value)
        const [endHours, endMinutes] = endTimeValue.split(":");
        endTime.setHours(endHours);
        endTime.setMinutes(endMinutes);
        endTime.setSeconds(0);
        endTime.setMilliseconds(0);
        // The resulting datetime value
        const endDateTimeValue = endTime.toISOString();

        const Shift = {
            userID: currentUser[0],
            date: datetimeValue,
            startTime: startDateTimeValue,
            endTime: endDateTimeValue
        }

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

