var api;
var isLoggedIn;
var postsArr = [];
var PlatformsArr = [];
var languageArr = [];
var exposure;
var currentUser = JSON.parse(sessionStorage.getItem("user"));
var justLoggedIn = JSON.parse(sessionStorage.getItem("justLoggedIn"));

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    // alerts for manager if just logged in
    if (justLoggedIn == true) {
        if (currentUser[1] === 2) // a manager is logged in
        {
            AlertPostsForApproval();
        }
        sessionStorage.setItem("justLoggedIn", JSON.stringify(false));
    }
    readPosts();
    getRecommendation();

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
        for (var i = postsArr.length-1 ; i > 0 ; i--) {
            var currenRemovalStatus;
            if (postsArr[i].removalStatus == 0)
                currenRemovalStatus = "דווח";
            else currenRemovalStatus = "הוסר";

            str += '<tr>';
            str += '<td class="postID_display">' + postsArr[i].postID + '</td>';
            str += '<td class="Platform_display">' + postsArr[i].platformName + '</td>';
            str += '<td ><a href="' + postsArr[i].urlLink + '" target="_blank" class="urlLink_display" >קישור לפוסט</a></td>';
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
    ajaxCall("GET", api + "Posts/numberOfNoneStatusPosts", "", AlertPostsForApprovalSCB, AlertPostsForApprovalECB);
}
function AlertPostsForApprovalSCB(data) {
    alert(data + " פוסטים ממתינים לאישור מנהל");
}
function AlertPostsForApprovalECB(err) {
    alert("Input Error");
}

// GET Exposure 
function getRecommendation() {
    ajaxCall("GET", api + "Recommendations", "", getRecommendationSCB, getRecommendationECB);
}
function getRecommendationSCB(data) {
    exposure = data;
    renderRecommendation();
}
function getRecommendationECB(err) {
    alert("Input Error");
}

function renderRecommendation() {
    let str_Reco = "";
    str_Reco += "<h4>היום מומלץ לך לנטר פוסטים</h4>";
    str_Reco += "<h4>בפלטפורמת " + exposure.platform + "</h4>";
    str_Reco += "<h4> בשפה " + exposure.language + "</h4>";
    str_Reco += "<h4> ובשימוש בהאשטאג " + exposure.keyWordsAndHashtages + "</h4>";

    document.getElementById("RecommendationSection").innerHTML += str_Reco;
}

