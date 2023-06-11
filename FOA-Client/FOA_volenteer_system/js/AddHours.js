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


function RenderOneMoreRow() {
    var table = document.getElementById('shiftTable'); // Get the table element

    // Create a new row element
    var newRow = table.insertRow();
    newRow.id = 'newRow' + rowNum; // Unique ID

    // Create cell for delete icon
    var deleteCell = newRow.insertCell();
    deleteCell.className = 'delete-icon';

    // Create a new button element
    var deleteButton = document.createElement('button');
    deleteButton.id = 'deleteBtn';
    deleteButton.onclick = function () {
        confirmDelete(newRow.id);
    };

    // Create trashcan icon
    var trashcanIcon = document.createElement('i');
    trashcanIcon.className = 'fas fa-trash-alt';
    deleteButton.appendChild(trashcanIcon);
    deleteCell.appendChild(deleteButton);

    // Create cell for date input
    var dateCell = newRow.insertCell();
    dateCell.className = 'form-headers';

    var dateLabel = document.createElement('p');
    dateLabel.textContent = 'תאריך';
    dateCell.appendChild(dateLabel);

    var dateInput = document.createElement('input');
    dateInput.type = 'date';
    dateInput.name = 'date';
    dateInput.className = 'form-control';
    dateInput.id = 'dateInput' + rowNum; // Unique ID
    dateCell.appendChild(dateInput);

    // Create cell for start time input
    var startTimeCell = newRow.insertCell();
    startTimeCell.className = 'form-headers';

    var startTimeLabel = document.createElement('p');
    startTimeLabel.textContent = 'שעת התחלה';
    startTimeCell.appendChild(startTimeLabel);

    var startTimeInput = document.createElement('input');
    startTimeInput.type = 'time';
    startTimeInput.name = 'startTime';
    startTimeInput.className = 'form-control';
    startTimeInput.id = 'startTimeInput' + rowNum; // Unique ID
    startTimeCell.appendChild(startTimeInput);

    // Create cell for end time input
    var endTimeCell = newRow.insertCell();
    endTimeCell.className = 'form-headers';

    var endTimeLabel = document.createElement('p');
    endTimeLabel.textContent = 'שעת סיום';
    endTimeCell.appendChild(endTimeLabel);

    var endTimeInput = document.createElement('input');
    endTimeInput.type = 'time';
    endTimeInput.name = 'endTime';
    endTimeInput.className = 'form-control';
    endTimeInput.id = 'endTimeInput' + rowNum; // Unique ID
    endTimeCell.appendChild(endTimeInput);

    rowNum++;
}

// delete report
function confirmDelete(rowIndex) {
    document.getElementById(rowIndex).remove();
}
