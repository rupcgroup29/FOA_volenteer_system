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


//function RenderOneMoreRow() {
//    // Create a new div element with the desired class
//    var parentDiv = document.createElement('div');
//    parentDiv.className = 'row';
//    parentDiv.id = 'row' + rowNum; // Unique ID
//    parentDiv.style.direction = 'rtl';

//    //trash -start
//    var trashcanHtml = document.createElement('span');
//    trashcanHtml.className = 'delete-icon col-1';
//    trashcanHtml.onclick = function () {
//        confirmDelete(parentDiv.id);
//    };
//    var trashcanIcon = document.createElement('i');
//    trashcanIcon.className = 'fas fa-trash-alt';
//    trashcanHtml.appendChild(trashcanIcon);

//    parentDiv.appendChild(trashcanHtml);
//    //trash -end

//    // Create a new div element
//    var newDivDate = document.createElement('div');
//    newDivDate.className = 'col-3 form-headers form-group';

//    var paragraph = document.createElement('p');
//    paragraph.textContent = 'תאריך'; // Set the content of the <p> element
//    newDivDate.appendChild(paragraph);

//    // Create date input
//    var dateInput = document.createElement('input');
//    dateInput.type = 'date';
//    dateInput.id = 'dateInput' + rowNum; // Unique ID
//    dateInput.name = 'date';
//    dateInput.className = 'form-control'; // Add class
//    newDivDate.appendChild(dateInput);

//    // Create a new div element
//    var newDivStartTime = document.createElement('div');
//    newDivStartTime.className = 'col-3 form-headers form-group';

//    var paragraph = document.createElement('p');
//    paragraph.textContent = 'שעת התחלה'; // Set the content of the <p> element
//    newDivStartTime.appendChild(paragraph);

//    // Create startTime input
//    var startTimeInput = document.createElement('input');
//    startTimeInput.type = 'time';
//    startTimeInput.id = 'startTimeInput' + rowNum; // Unique ID
//    startTimeInput.name = 'startTime';
//    startTimeInput.className = 'form-control'; // Add class
//    newDivStartTime.appendChild(startTimeInput);

//    // Create a new div element
//    var newDivEndTime = document.createElement('div');
//    newDivEndTime.className = 'col-3 form-headers form-group';

//    var paragraph = document.createElement('p');
//    paragraph.textContent = 'שעת סיום'; // Set the content of the <p> element
//    newDivEndTime.appendChild(paragraph);

//    // Create endTime input
//    var endTimeInput = document.createElement('input');
//    endTimeInput.type = 'time';
//    endTimeInput.id = 'endTimeInput' + rowNum; // Unique ID
//    endTimeInput.name = 'endTime';
//    endTimeInput.className = 'form-control'; // Add class
//    newDivEndTime.appendChild(endTimeInput);

//    // Append the newDiv to the parentDiv
//    parentDiv.appendChild(newDivDate);
//    parentDiv.appendChild(newDivStartTime);
//    parentDiv.appendChild(newDivEndTime);

//    // Add the new div to the container
//    var inputsContainer = document.getElementById('RenderShifts');
//    inputsContainer.appendChild(parentDiv);

//    rowNum++; // Increment the counter for the next set of inputs
//}

//// delete report
//function confirmDelete(reportID) {
//    // Get the parent element of the row to be deleted
//    var parentDiv = document.getElementById("RenderShifts");

//    // Get the row element to be deleted based on the rowNum
//    var rowToDelete = document.getElementById(reportID);

//    // Remove the row from the parent element
//    if (rowToDelete) {
//        parentDiv.removeChild(rowToDelete);
//    }
//}


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
