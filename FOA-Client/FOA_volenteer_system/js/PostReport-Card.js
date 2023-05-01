var api;
var imageFolder;
var currentUser = JSON.parse(sessionStorage.getItem("user"));
var currentPostID = JSON.parse(sessionStorage.getItem("post"));;
var currentPostObject;

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    //for image folder 
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        imageFolder = "https://localhost:7109/Images/";
    }
    else imageFolder = "https://proj.ruppin.ac.il/cgroup29/prod/Images/";

    let str_PostCardHeader = "";
    str_PostCardHeader += '<h2 class="section-heading text-uppercase">עריכת דיווח מספר ' + currentPostID + '</h2>';
    document.getElementById("PostCardHeader").innerHTML += str_PostCardHeader;

    readPostByID();

    $('#contactForm').submit(editPost);

    enableInitialReportingFields();

});

// read Post By ID
function readPostByID() {
    ajaxCall("GET", api + "ReadPosts/" + currentPostID, "", readPostByIDSCB, readPostByIDECB);
}
function readPostByIDSCB(data) {
    currentPostObject = data;
    RenderRelevantDetails();
}
function readPostByIDECB(err) {
    alert("Input Error");
}


function RenderRelevantDetails() {
    let str_postStatus = "";
    let str_removalStatus = "";
    let str_category = "";
    let str_KnH = "";

    //removalStatus
    if (currentPostObject.removalStatus == 0) // if status haven't changed yet
    {
        str_removalStatus += '<option class="opt" value="0">דווח</option>';
        str_removalStatus += '<option class="opt" value="1">הוסר</option>';
    }
    else {
        str_removalStatus += '<option class="opt" value="1">הוסר</option>';
        $("#removalStatus").attr("disabled", true);
    }
    document.getElementById("removalStatus").innerHTML += str_removalStatus;
    //managerStatus
    if (currentPostObject.postStatus == 0) // if status have'nt changed yet
    {
        str_postStatus += '<option class="opt" value="0">ממתין לסטטוס</option>';
        str_postStatus += '<option class="opt" value="1">אושר</option>';
        str_postStatus += '<option class="opt" value="2">נדחה</option>';

    } else if (currentPostObject.postStatus == 1) {
        str_postStatus += '<option class="opt" value="1">אושר</option>';
        $("#ManagerStatus").attr("disabled", true);
    } else {
        str_postStatus += '<option class="opt" value="2">נדחה</option>';
        $("#ManagerStatus").attr("disabled", true);
    }
    document.getElementById("ManagerStatus").innerHTML += str_postStatus;

    //ReportedUserName
    $("#reportedUserName").val(currentPostObject.userName);

    //reportedDate
    let DateOfPost = (currentPostObject.insertDate).split('T')[0];  // cut the time from the DateTime format
    $("#reportedDate").val(DateOfPost);

    //content_threat
    if (currentPostObject.threat == 1) {
        $("#content_threat").val("כן");
    } else if (currentPostObject.postStatus == 2) {
        $("#content_threat").val("לא");
    } else $("#content_threat").val("לא בטוח");

    //platform
    $("#platform").val(currentPostObject.platformName);

    //country
    $("#country").val(currentPostObject.countryName);

    //language
    $("#language").val(currentPostObject.languageName);

    //IHRA
    // ענת: לא לשכוח לדאוג לתא גדול יותר במידה ויש הרבה קטגוריות
    for (var i = 0; i < currentPostObject.categoryName.length - 1; i++) {
        str_category += '<p>' + currentPostObject.categoryName[i] + '</p>';
    }
    str_category += currentPostObject.categoryName[currentPostObject.categoryName.length - 1];
    $("#ihraCategory").val(str_category);

    //exposure_likes
    $("#exposure_likes").val(currentPostObject.amountOfLikes);

    //exposure_Comments
    $("#exposure_Comments").val(currentPostObject.amountOfComments);

    //exposure_shares
    $("#exposure_shares").val(currentPostObject.amountOfShares);

    //urlLink
    //$("#urlLink").val(currentPostObject.urlLink);
    str_urlLink = "";
    str_urlLink += '<a href="' + currentPostObject.urlLink + '" target="_blank" class="urlLink_display" >קישור לפוסט</a>'
    document.getElementById("urlLink").innerHTML += str_urlLink;

    //description
    $("#description").val(currentPostObject.description);

    //keywords_hashtags
    for (var i = 0; i < currentPostObject.keyWordsAndHashtages.length - 1; i++) {
        str_KnH += currentPostObject.keyWordsAndHashtages[i] + ", ";
    }
    str_KnH += currentPostObject.keyWordsAndHashtages[currentPostObject.keyWordsAndHashtages.length - 1];
    $("#kw_Hashtags").val(str_KnH);

    //Screenshot
    var imgStr = "";
    src =  imageFolder + currentPostObject.screenshot;
    imgStr = `<img class="screenshotImg" src='${src}'/>`;

    document.getElementById("showScreenshot").innerHTML = imgStr;

}




// edit post - submit
function editPost() {
    let postStatus = $("#ManagerStatus").val();
    let postStatusManager = "1";   // default if no one changed it yet
    let removalStatus = $("#removalStatus").val();
    let removalStatusManager = "1"; // default if no one changed it yet

    if (postStatus != 0) { // status changed
        postStatusManager = currentUser.userID;
    }
    if (removalStatus != 0) { // status changed
        removalStatusManager = currentUser.userID;
    }
    if (removalStatus != 0 && postStatus == 0) { //alert if removed from platform and havn't been approved yet
        alert("שים לב שלא עדכנת סטטוס האם הפוסט אנטישמי או לא!");
        return false;
    }

    const editedPost = {
        PostID: currentPostID,
        PostStatus: parseInt(postStatus),
        PostStatusManager: postStatusManager,
        RemovalStatus: parseInt(removalStatus),
        RemovalStatusManager: removalStatusManager
    }

    ajaxCall("PUT", api + "ReadPosts", JSON.stringify(editedPost), editPostSCB, editPostECB);
    return false;
}
function editPostSCB(data) {
    alert("דיווח הפוסט עודכן בהצלחה");
    window.location.assign("HomePage.html");
    location.assign("HomePage.html")
}
function editPostECB(err) {
    alert("שגיאה בעדכון הדיווח, אנא נסו שוב");
}


// enable Initial reporting fields
function enableInitialReportingFields() {
    $("#ReportedUserName").attr("readonly", false);
    $("#content_threat").attr("readonly", false);
    $("#platform").attr("readonly", false);
    $("#Country").attr("readonly", false);
    $("#language").attr("readonly", false);
    $("#IHRA").attr("readonly", false);
    $("#exposure_likes").attr("readonly", false);
    $("#exposure_Comments").attr("readonly", false);
    $("#exposure_shares").attr("readonly", false);
    $("#urlLink").attr("readonly", false);
    $("#Keywords_hashtags").attr("readonly", false);
}

