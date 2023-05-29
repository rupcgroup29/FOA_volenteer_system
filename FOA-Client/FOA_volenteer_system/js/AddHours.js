var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));
let rowNum = 0;
let ShiftsToSend = [];

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    RenderOneMoreRow();

    $('#contactForm').submit(AddShifts);

});

//Add Shifts
function AddShifts() {
    for (var i = 0; i < rowNum; i++) {
        let dateValue = $("#dateInput" + i).val();
        const date = new Date(dateValue);

        const startTimeValue = $("#startTimeInput" + i).val();
        const [startHours, startMinutes] = startTimeValue.split(":");

        const endTimeValue = $("#endTimeInput" + i).val();
        const [endHours, endMinutes] = endTimeValue.split(":");

        const datetimeValue = `${date.getFullYear()}-${padNumber(date.getMonth() + 1)}-${padNumber(date.getDate())}T${padNumber(startHours)}:${padNumber(startMinutes)}:00`;

        const startDateTimeValue = `${date.getFullYear()}-${padNumber(date.getMonth() + 1)}-${padNumber(date.getDate())}T${padNumber(startHours)}:${padNumber(startMinutes)}:00`;

        const endDateTimeValue = `${date.getFullYear()}-${padNumber(date.getMonth() + 1)}-${padNumber(date.getDate())}T${padNumber(endHours)}:${padNumber(endMinutes)}:00`;

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

function padNumber(number) {
    return number.toString().padStart(2, '0');
}

function postAddShiftsSCB(data) {
    alert("המשמרות נוספו בהצלחה");
    window.location.assign("Hours-Main.html");
    location.assign("Hours-Main.html")
}

function postAddShiftsECB(err) {
    alert("אחד או יותר מהדיווחים שהכנסת לא נקלטו, " + err.responseJSON.errorMessage);
}

//function RenderOneMoreRow() {
//    let str = "";
//    str += `<div class="row" dir="rtl">`;
//    str += `<div class="col-4 form-headers form-group ">`;
//    str += `<p> תאריך</p>`;
//    str += `<input id="SelectedDate` + rowNum + `" class="form-control" type="date" value="${$("#SelectedDate" + (rowNum - 1)).val() || ''}" />`; // Populate with previous row's value
//    str += `</div>`;
//    str += `<div class="col-4 form-headers form-group ">`;
//    str += `<p> שעת התחלה</p>`;
//    str += `<input id="StartHour` + rowNum + `" class="form-control" type="time" value="${$("#StartHour" + (rowNum - 1)).val() || ''}" />`; // Populate with previous row's value
//    str += `</div>`;
//    str += `<div class="col-4 form-headers form-group ">`;
//    str += `<p> שעת סיום</p>`;
//    str += `<input id="FinishHour` + rowNum + `" class="form-control" type="time" value="${$("#FinishHour" + (rowNum - 1)).val() || ''}" />`; // Populate with previous row's value
//    str += `</div>`;
//    str += `</div>`;
//    document.getElementById("RenderShifts").innerHTML += str;

//    rowNum++; // Increment the row number
//}


function RenderOneMoreRow() {
    // Create a new div element with the desired class
    var parentDiv = document.createElement('div');
    parentDiv.className = 'row';
    parentDiv.style.direction = 'rtl';

    // Create a new div element
    var newDivDate = document.createElement('div');
    newDivDate.className = 'col-4 form-headers form-group';

    var paragraph = document.createElement('p');
    paragraph.textContent = 'תאריך'; // Set the content of the <p> element
    newDivDate.appendChild(paragraph);

    // Create date input
    var dateInput = document.createElement('input');
    dateInput.type = 'date';
    dateInput.id = 'dateInput' + rowNum; // Unique ID
    dateInput.name = 'date';
    dateInput.className = 'form-control'; // Add class
    newDivDate.appendChild(dateInput);

    // Create a new div element
    var newDivStartTime = document.createElement('div');
    newDivStartTime.className = 'col-4 form-headers form-group';

    var paragraph = document.createElement('p');
    paragraph.textContent = 'שעת התחלה'; // Set the content of the <p> element
    newDivStartTime.appendChild(paragraph);

    // Create startTime input
    var startTimeInput = document.createElement('input');
    startTimeInput.type = 'time';
    startTimeInput.id = 'startTimeInput' + rowNum; // Unique ID
    startTimeInput.name = 'startTime';
    dateInput.className = 'form-control'; // Add class
    newDivStartTime.appendChild(startTimeInput);

    // Create a new div element
    var newDivEndTime = document.createElement('div');
    newDivEndTime.className = 'col-4 form-headers form-group';

    var paragraph = document.createElement('p');
    paragraph.textContent = 'שעת סיום'; // Set the content of the <p> element
    newDivEndTime.appendChild(paragraph);

    // Create endTime input
    var endTimeInput = document.createElement('input');
    endTimeInput.type = 'time';
    endTimeInput.id = 'endTimeInput' + rowNum; // Unique ID
    endTimeInput.name = 'endTime';
    dateInput.className = 'form-control'; // Add class
    newDivEndTime.appendChild(endTimeInput);

    // Append the newDiv to the parentDiv
    parentDiv.appendChild(newDivDate);
    parentDiv.appendChild(newDivStartTime);
    parentDiv.appendChild(newDivEndTime);

    // Add the new div to the container
    var inputsContainer = document.getElementById('RenderShifts');
    inputsContainer.appendChild(parentDiv);

    rowNum++; // Increment the counter for the next set of inputs
}