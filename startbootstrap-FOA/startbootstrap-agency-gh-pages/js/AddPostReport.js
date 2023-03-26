var api;
var isLoggedIn;
var usersArr = [];
var CurrentUser = JSON.parse(sessionStorage.getItem("user"));

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    // לעדכן את הכתובת החלופית !!
    //else api = "https://proj.ruppin.ac.il/cgroup29/test2/tar1/api/Users/";

    $('#contactForm').submit(RegisterUser);

    GetPlatformsList();
    GetCountriesList();
    GetLanguagesList();

    HideRemovalStatusDiv();
    HideManagerStatusDiv();
    ShowOther();
});

// get the Platforms list
function GetPlatformsList() {
    ajaxCall("GET", api + "Platforms", "", getPlatformsListSCB, getPlatformsListECB);
    return false;
}

function getPlatformsListSCB(data) {
    if (data == null) {
        alert("אין פלטפורמות עדיין");
    } else {
        let str = "";
        str += '<option class="opt" value="0">רשת חברתית *</option>';
        for (var i = 0; i < data.length; i++) {
            str += '<option class="opt" value="' + data[i].platformID + '">' + data[i].platformName + '</option>';
        }
        str += '<option class="opt" value="999">אחר </option>';
        document.getElementById("platform").innerHTML += str;
    }
}
function getPlatformsListECB(err) {
    console.log(err);
}

// get the Countries list
function GetCountriesList() {
    ajaxCall("GET", api + "Countries", "", getCountriesSCB, getCountriesECB);
    return false;
}

function getCountriesSCB(data) {
    if (data == null) {
        alert("אין מדינות עדיין");
    } else {
        let str = "";
        str += '<option class="opt" value="0">בחר מדינה *</option>';
        for (var i = 0; i < data.length; i++) {
            str += '<option class="opt" value="' + data[i].countryID + '">' + data[i].countryName + '</option>';
        }
        str += '<option class="opt" value="999">אחר </option>';
        document.getElementById("country").innerHTML += str;
    }
}
function getCountriesECB(err) {
    console.log(err);
}

// get the Languages list
function GetLanguagesList() {
    ajaxCall("GET", api + "Languages", "", getLanguagesSCB, getLanguagesECB);
    return false;
}

function getLanguagesSCB(data) {
    if (data == null) {
        alert("אין שפות עדיין");
    } else {
        let str = "";
        str += '<option class="opt" value="0">בחר שפה *</option>';
        for (var i = 0; i < data.length; i++) {
            str += '<option class="opt" value="' + data[i].languageID + '">' + data[i].languageName + '</option>';
        }
        str += '<option class="opt" value="999">אחר </option>';
        document.getElementById("language").innerHTML += str;
    }
}
function getLanguagesECB(err) {
    console.log(err);
}

function AddNewPost() {
    let urlLink = $("#UrlLink").val();
    let description = $("#Description").val();
    let keyWordsAndHashtages = $("#UrlLink").val();
    let threat = $("#UrlLink").val();
    let screenshot = $("#UrlLink").val();
    let amoutOfLikes = $("#UrlLink").val();
    let AmoutOfShares = $("#UrlLink").val();
    let AmoutOfComments = $("#UrlLink").val();
    let userID = CurrentUser.userID;
    let platformID = $("#platform").val();
    let categoryID = $("#category").val();
    let country = $("#country").val();
    let language = $("#language").val();

    const newPost = {
        //PostID: "1", 
        UrlLink: urlLink,
        Description: description,
        KeyWordsAndHashtages: keyWordsAndHashtages,
        Threat: threat,
        Screenshot: screenshot,
        AmoutOfLikes: amoutOfLikes,
        AmoutOfShares: amoutOfShares,
        AmoutOfComments: amoutOfComments,
        //PostStatus: "1",
        //RemovalStatus: "1", 
        UserID: userID,
        PlatformID: platformID,
        CategoryID: categoryID,
        PostStatusManager: "1018",// במטרה לשלוח עם יוזר איידי קיים, ישתנה בעריכת פוסט
        RemovalStatusManager: "1018",// במטרה לשלוח עם יוזר איידי קיים, ישתנה בעריכת פוסט
        Country: country,
        Language: language

    }

    ajaxCall("POST", api + "Posts", JSON.stringify(newPost), postAddNewPostSCB, postAddNewPostECB);
    return false;
}
function postAddNewPostSCB(data) { // הוספת משתמש הצליחה
    alert("דיווח הפוסט נוסף בהצלחה");
    window.location.assign("HomePage.html");
    location.assign("HomePage.html")
}

function postAddNewPostECB(err) {
    alert("שגיאה בהוספת הדיווח, אנא נסו שוב");
}


//Hide RemovalStatus div
function HideRemovalStatusDiv() {
    var element = document.getElementById("RemovalStatus");
    element.style.display = "none";
}
//Hide ManagerStatus div
function HideManagerStatusDiv() {
    var element = document.getElementById("ManagerStatus");
    element.style.display = "none";
}

// enable Other platform only if other selected
function enableOtherPlatform() {
    var sel = document.getElementById('platform');

    sel.addEventListener("change", enableDivIfOtherSelected);

    function enableDivIfOtherSelected() {

        if (sel.value === '999') {
            $("#platform_diff").attr("readonly", false);
        }
        else $("#platform_diff").attr("readonly", true);
    }
}
// enable Other country only if other selected
function enableOtherCountry() {
    var sel = document.getElementById('country');

    sel.addEventListener("change", enableDivIfOtherSelected);

    function enableDivIfOtherSelected() {

        if (sel.value === '999') {
            $("#country_diff").attr("readonly", false);
        }
        else $("#country_diff").attr("readonly", true);
    }
}
// enable Other language only if other selected
function enableOtherLanguage() {
    var sel = document.getElementById('language');

    sel.addEventListener("change", enableDivIfOtherSelected);

    function enableDivIfOtherSelected() {

        if (sel.value === '999') {
            $("#language_diff").attr("readonly", false);
        }
        else $("#language_diff").attr("readonly", true);
    }
}