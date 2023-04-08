var api;
var isLoggedIn;
var postsArr = [];
var PlatformsArr = [];
var languageArr = [];
/*    נשמר במטרה לחסוך את ההתחברות בעת בדיקות   */
//var user = {
//    userID: 1024,
//    firstName: "ענת",
//    surname: "אביטל",
//    userName: "anat_a",
//    phoneNum: "0529645123",
//    roleDescription: "מנהל צוות ניטור",
//    permissionID: 2,
//    isActive: true,
//    password: "6DCA4533",
//    teamID: 1,
//    programID: 1026,
//    email: "anat_a@gmail.com",
//    programName: null
//}
//sessionStorage.setItem("user", JSON.stringify(user));
var CurrentUser = JSON.parse(sessionStorage.getItem("user"));
var JustLoggedIn = JSON.parse(sessionStorage.getItem("JustLoggedIn"));

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    // לעדכן את הכתובת החלופית !!
    //else api = "https://proj.ruppin.ac.il/cgroup29/test2/tar1/api/Users/";

    //Nav ber - Permission
    if (CurrentUser.permissionID == 4) // a volunteer is logged in
    {
        $(".ManagerNav").hide();
        $(".VolunteerNav").show();
    }
    else //Manager is logged in
    {
        $(".ManagerNav").show();
        $(".VolunteerNav").hide();
    }

    // alerts for manager if just logged in
    if (JustLoggedIn == true) {
        if (CurrentUser.permissionID == 2) // a manager is logged in
        {
            AlertPostsForApproval();
        }
        sessionStorage.setItem("JustLoggedIn", JSON.stringify(false));
    }
    readPosts();
    FilterByPost();
});

function FilterByPost() {
    // Declare variables
    var input = document.getElementById("myInput");
    var filter = input.value.toUpperCase();
    var table = document.getElementById("myTable");
    var tr = table.getElementsByTagName("tr");
    var txtValue;

    // Loop through all table rows, and hide those who don't match the search query
    for (var i = 0; i < tr.length; i++) {
        var td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

// read all posts
function readPosts() {
    ajaxCall("GET", api + "ReadPosts", "", readPostsSCB, readPostsECB);
}
function readPostsSCB(data) {
    postsArr = data;
    RenderPostsList();
}
function readPostsECB(err) {
    alert("Input Error");
}

// render the posts list
// יש להוסיף פילטור של פוסטים מאושרים ע"י מנהל בלבד
function RenderPostsList() {
    let str = "";
    if (postsArr == null) {
        alert("There's no posts yet");
    } else {
        str += '<table dir="rtl" id="myTable">';
        str += '<tr class="header">';
        str += '<th style="width:5%;">מס"ד</th>';
        str += '<th style="width:10%;">פלטפורמה</th>';
        str += '<th style="width:10%;">קישור</th>';
        str += '<th style="width:10%;">שפה</th>';
        str += '<th style="width:10%;">שיתופים</th>';
        str += '<th style="width:10%;">תגובות</th>';
        str += '<th style="width:10%;">לייקים</th>';
        str += '<th style="width:10%;">סטטוס בפלטפורמה</th>';
        str += '<th style="width:10%;">משתמש מדווח</th>';
        str += '<th style="width:10%;">תאריך</th>';
        str += '<th style="width:5%;"></th>';
        str += '</tr>';
        for (var i = 0; i < postsArr.length; i++) {
            var currenRemovalStatus;
            if (postsArr[i].removalStatus == 0)
                currenRemovalStatus = "דווח";
            else currenRemovalStatus = "הוסר";

            str += '<tr>';
            str += '<td class="postID_display">' + postsArr[i].postID + '</td>';
            str += '<td class="Platform_display">' + postsArr[i].platformName + '</td>';
            str += '<td class="urlLink_display">' + postsArr[i].urlLink + '</td>';
            str += '<td class="language_display">' + postsArr[i].languageName + '</td>';
            str += '<td class="amountOfShares_display">' + postsArr[i].amountOfShares + '</td>';
            str += '<td class="amountOfComments_display">' + postsArr[i].amountOfComments + '</td>';
            str += '<td class="amountOfLikes_display">' + postsArr[i].amountOfLikes + '</td>';
            str += '<td class="currenRemovalStatus_display">' + currenRemovalStatus + '</td>';
            str += '<td class="userName_display">' + postsArr[i].userName + '</td>';
            let DateOfPost = (postsArr[i].insertDate).split('T')[0];  // cut the time from the DateTime format
            str += '<td class="insertDate_display">' + DateOfPost + '</td>';
            str += `<td class="viewButton_display"><button onclick="OpenPostCard(` + postsArr[i].postID + `)">צפייה</button></td>`;
            str += '</tr>';
        }
    }
    str += '</table>';
    document.getElementById("PostsTable").innerHTML += str;
}

//// save the relevant post to open in edit\view mode (Depends on permission)
function OpenPostCard(postID) {
    sessionStorage.setItem("post", JSON.stringify(postID));
    location.replace("PostReportCard%20.html");
}

// התראות למנהל על כמות פוסטים שטרם אושרו
function AlertPostsForApproval() {
    //להתאים את הקריאה למה שלטם שולחת לי, זה עוד לא מותאם!!!
    ajaxCall("GET", api + "Posts/numberOfNoneStatusPosts", "", AlertPostsForApprovalSCB, AlertPostsForApprovalECB);
}
function AlertPostsForApprovalSCB(data) {
    alert(data + " פוסטים ממתינים לאישור מנהל");
}
function AlertPostsForApprovalECB(err) {
    alert("Input Error");
}