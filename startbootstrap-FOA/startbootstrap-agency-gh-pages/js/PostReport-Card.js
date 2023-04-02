var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));
var currentPostID = JSON.parse(sessionStorage.getItem("post"));;
var currentPostObject;

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    // לעדכן את הכתובת החלופית !!
    //else api = "https://proj.ruppin.ac.il/cgroup29/test2/tar1/api/Users/";

    readPostByID();
    $('#contactForm').submit(editPost);

    enableInitialReportingFields();

});

// read Post By ID
function readPostByID() {
    //  לבדוק האם צריך להוסיף  parseInt
    ajaxCall("GET", api + "Posts/" + currentPostID, "", readPostByIDSCB, readPostByIDECB);
}
function readPostByIDSCB(data) {
    currentPostObject = data;
    RenderRelevantDetails();
}
function readPostByIDECB(err) {
    alert("Input Error");
}


function RenderRelevantDetails() {
    let str_urlLink = "";
    let str_description = "";
    let str_keyWordsAndHashtages = "";
    let str_threat = "";
    let str_amoutOfLikes = "";
    let str_amoutOfShares = "";
    let str_amoutOfComments = "";
    let str_postStatus = "";
    let str_removalStatus = "";
    let str_user = "";
    let str_platform = "";
    let str_category = "";
    let str_country = "";
    let str_language = "";
    // ענת: אנחנו רוצות להציג מי המנהל שאישר או דיווח הסרה? או שזה רק לדף לוגים?
    //let str_postStatusManager = "";
    //let str_removalStatusManager = "";

    if (currentPostObject.removalStatus == 0)// status have'nt changed yet
    {
        str_removalStatus += '<option class="opt" value="0">דווח</option>';
        str_removalStatus += '<option class="opt" value="1">הוסר</option>';
    }
    else {
        //RemovalStatus
        str_removalStatus += '<option class="opt" value="' + currentPostObject.removalStatus + '">' + currentPostObject.removalStatus + '</option>';
    }
    if (currentPostObject.postStatus == 0)// status have'nt changed yet
    {
        str_postStatus += '<option class="opt" value="0">ממתין לסטטוס</option>';
        str_postStatus += '<option class="opt" value="1">אושר</option>';
        str_postStatus += '<option class="opt" value="2">נדחה</option>';
    }
    else {
        //ManagerStatus
        str_postStatus += '<option class="opt" value="' + currentPostObject.postStatus + '">' + currentPostObject.postStatus + '</option>';
    }

    //ReportedUserName
    str_user += '<input dir="rtl" class="form-control" type="text" placeholder="' + currentPostObject.userName + '"/>';
    //content_threat
    str_threat += '    <option class="opt" >' + currentPostObject.threat + '</option>';
    //platform
    str_platform += '<option class="opt">' + currentPostObject.platformName + '</option>';
    //Country
    str_country += '<option class="opt">' + currentPostObject.countryName + '</option>';
    //language
    str_language += '<option class="opt">' + currentPostObject.languageName + '</option>';
    //IHRA
    for (var i = 0; i < currentPostObject.categoryID.length; i++) {
        str_category += '<p>' + currentPostObject.categoryID[i] + '<p>';
    }
    //exposure_likes
    str_amoutOfLikes += '<option class="opt">' + currentPostObject.amoutOfLikes + '</option>';
    //exposure_Comments
    str_amoutOfComments += '<option class="opt">' + currentPostObject.amoutOfComments + '</option>';
    //exposure_shares
    str_amoutOfShares += '<option class="opt">' + currentPostObject.amoutOfShares + '</option>';
    //urlLink
    str_urlLink += '<input dir="rtl" class="form-control" id="urlLink" type="text" placeholder="' + currentPostObject.urlLink + '"/>';
    //description
    str_description += '<input dir="rtl" class="form-control" id="urlLink" type="text" placeholder="' + currentPostObject.description + '"/>';
    //Keywords_hashtags
    let KnH = "";
    for (var i = 0; i < currentPostObject.keyWordsAndHashtages.length -1; i++) {
        KnH += currentPostObject.keyWordsAndHashtages[i] + " , ";
    }
    KnH += currentPostObject.keyWordsAndHashtages[currentPostObject.keyWordsAndHashtages.length - 1];
    str_keyWordsAndHashtages += '<input dir="rtl" class="form-control" id="urlLink" type="text" placeholder="' + KnH + '"/>';
}
document.getElementById("RemovalStatus").innerHTML += str_removalStatus;
document.getElementById("ManagerStatus").innerHTML += str_postStatus;
document.getElementById("ReportedUserName").innerHTML += str_user;
document.getElementById("content_threat").innerHTML += str_threat;
document.getElementById("platform").innerHTML += str_platform;
document.getElementById("Country").innerHTML += str_country;
document.getElementById("language").innerHTML += str_language;
document.getElementById("IHRA").innerHTML += str_category;
document.getElementById("exposure_likes").innerHTML += str_amoutOfLikes;
document.getElementById("exposure_Comments").innerHTML += str_amoutOfComments;
document.getElementById("exposure_shares").innerHTML += str_amoutOfShares;
document.getElementById("urlLink").innerHTML += str_urlLink;
document.getElementById("Keywords_hashtags").innerHTML += str_keyWordsAndHashtages;

}

// edit post - submit
function editPost() {
    let postStatus = $("#ManagerStatus").val();
    let postStatusManager = "1";   // default if no one changed it yet
    let removalStatus = $("#ManagerStatus").val();
    let removalStatusManager = "1"; // default if no one changed it yet

    if (postStatus != 0) { // status changed
        postStatusManager = currentUser.userID;
    }
    if (removalStatus != 0) { // status changed
        removalStatusManager = currentUser.userID;
    }

    const editedPost = {
        PostStatus: postStatus,
        PostStatusManager: postStatusManager,
        RemovalStatus: removalStatus,
        RemovalStatusManager: removalStatusManager
    }

    ajaxCall("PUT", api + "Posts", JSON.stringify(editedPost), editPostSCB, editPostECB);
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
