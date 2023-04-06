var api;
var isLoggedIn;
var postsArr = [];
var PlatformsArr = [];
var languageArr = [];
var CurrentUser = sessionStorage.getItem("user");

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
    readPosts();
    readPlatforms();
    readLanguages();
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
        var td = tr[i].getElementsByTagName("td")[0];
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
function RenderPostsList() {
    if (postsArr == null) {
        alert("There's no posts yet");
    } else {
        let str = "";
        str += '<table dir="rtl" id="myTable">';
        str += '<tr class="header">';
        str += '<th style="width:4%;">מס"ד</th>';
        str += '<th style="width:22%;">פלטפורמה</th>';
        str += '<th style="width:22%;">קישור</th>';
        str += '<th style="width:22%;">שפה</th>';
        str += '<th style="width:22%;">סטטוס בפלטפורמה</th>';
        str += '<th style="width:10%;"></th>';
        str += '</tr>';
        for (var i = 0; i < postsArr.length; i++) {
            //var currentPlatformName;
            //for (var j = 0; j < PlatformsArr.length; j++) {
            //    if (postsArr[i].platformID == PlatformsArr[j].platformID)
            //        currentPlatformName = PlatformsArr[j].platformName;
            //}
            var currenLanguageName;
            //for (var j = 0; j < languageArr.length; j++) {
            //    if (postsArr[i].language == languageArr[j].language)
            //        currenLanguageName = languageArr[j].languageName;
            //}
            var currenRemovalStatus;
            if (postsArr[i].removalStatusManager == 0)
                currenRemovalStatus = "דווח";
            else currenRemovalStatus = "הוסר";

            if (postsArr[i].language == null) {
                currenLanguageName = "";
            } else {
                currenLanguageName = postsArr[i].language;
            }

            str += '<tr>';
            str += '<td class="postID_display">' + postsArr[i].postID + '</td>';
            str += '<td class="Platform_display">' + postsArr[i].platform + '</td>';
            str += '<td class="urlLink_display">' + postsArr[i].urlLink + '</td>';
            str += '<td class="language_display">' + currenLanguageName + '</td>';
            str += '<td class="RemovalStatus_display">' + currenRemovalStatus + '</td>';
            str += '<td class="viewButton_display"><button onclick="OpenPostCard(' + postsArr[i].postID + ')">צפייה</button></td>';
            str += '</tr>';
        }
    }
    onclick = "AddToFavoriets(" + this.id + ")";        //לטם: השורה הזאת לא תקינה, מה התכוונת שזה יעשה?
    str += '</table>';
    document.getElementById("PostsTable").innerHTML += str;
}

// get the platforms list
function readPlatforms() {
    ajaxCall("GET", api + "Platforms", "", readPlatformsSCB, readPlatformsECB);
    return false;
}

function readPlatformsSCB(data) {
    PlatformsArr = data;
}
function readPlatformsECB(err) {
    console.log(err);
}

// get the languages list
function readLanguages() {
    ajaxCall("GET", api + "Languages", "", readLanguagesSCB, readLanguagesECB);
    return false;
}

function readLanguagesSCB(data) {
    PlatformsArr = data;
}
function readLanguagesECB(err) {
    console.log(err);
}

// save the relevant post to open in edit\view mode (Depends on permission)
function OpenPostCard(postID) {
    sessionStorage.setItem("post", JSON.stringify(postID));
    window.location.assign("PostReport-Card.html");
}
