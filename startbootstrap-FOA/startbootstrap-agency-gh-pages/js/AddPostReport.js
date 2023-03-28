var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    // לעדכן את הכתובת החלופית !!
    //else api = "https://proj.ruppin.ac.il/cgroup29/test2/tar1/api/Users/";

    $('#contactForm').submit(AddNewPost);       //לטם: שיניתי את שם הפונקציה

    GetPlatformsList();
    GetCountriesList();
    GetLanguagesList();
    GetIHRAList();                          // לטם: הוספתי פונק' שתרנדר את הרשימה הזו מהשרת > מהדאטה בייס

    //HideRemovalStatusDiv();       //לטם: זמנית הורדתי את זה כי זה עשה תקלה בהרצה
    //HideManagerStatusDiv();       //לטם: זמנית הורדתי את זה כי זה עשה תקלה בהרצה
    enableOtherPlatform();           //לטם: הוספתי לכאן את הפונק' כי צריך לקרוא לפונק' שרושמים בשביל שהיא תתבצע
    enableOtherCountry();            //לטם: הוספתי לכאן את הפונק' כי צריך לקרוא לפונק' שרושמים בשביל שהיא תתבצע
    enableOtherLanguage()            //לטם: הוספתי לכאן את הפונק' כי צריך לקרוא לפונק' שרושמים בשביל שהיא תתבצע
    // ShowOther();                  //לטם: הפכתי את זה להערה כי זו קריאה לפונק' בלי שהפונק' עצמה כתובה'


});


// add new post - submit
function AddNewPost() {
    let urlLink = $("#urlLink").val();
    let description = $("#description").val();
    let keyWordsAndHashtages = $("#keywords_hashtags").val();
    let threat = $("#content_threat").val();
    // let screenshot = $("#UrlLink").val();            //לטם: כרגע אנחנו מנסות לעשות את זה בלי הוספת התמונה, באופן זמני בלבד
    let amoutOfLikes = $("#exposure_likes").val();      //לטם: עדכנתי פה לכל הפרמטרים את האיי-די לפי מה שיש בדף של הוספת פוסט וגם בדף עצמו הפכתי שדברים שהם מספרים כמו כמות לייקים יהיה מסוג מספר ולא מסוג טקסט כמו שהיה רשום שם
    let amoutOfShares = $("#exposure_shares").val();        //לטם: החלפי לאות ראשונה קטנה, במקום גדולה כמו שהיה רשום- זאת בשביל שיתאים לאובייקט שיוצרים למטה 
    let amoutOfComments = $("#exposure_Comments").val();    //לטם: החלפי לאות ראשונה קטנה, במקום גדולה כמו שהיה רשום- זאת בשביל שיתאים לאובייקט שיוצרים למטה 
    let userID = currentUser.userID;
    let platformID = $("#platform").val();
    let categoryID = getChecked();
    let country = $("#country").val();
    let language = $("#language").val();

    const newPost = {
        //PostID: "1", 
        UrlLink: urlLink,
        Description: description,
        KeyWordsAndHashtages: keyWordsAndHashtages,
        Threat: threat,
        //Screenshot: screenshot,       //עד שננסה לשמור תמונות, כרגע זה בהערה גם בצד שרת
        AmoutOfLikes: amoutOfLikes,
        AmoutOfShares: amoutOfShares,
        AmoutOfComments: amoutOfComments,
        //PostStatus: "1",
        //RemovalStatus: "1", 
        UserID: userID,
        PlatformID: platformID,
        CategoryID: categoryID,
        //PostStatusManager: "1016",      // במטרה לשלוח עם יוזר איידי קיים, ישתנה בעריכת פוסט
        //RemovalStatusManager: "1016",   // במטרה לשלוח עם יוזר איידי קיים, ישתנה בעריכת פוסט
        CountryID: country,
        LanguageID: language
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


// get the IHRA list                     //לטם: הוספתי את זה שיורונדר לדף של הוספת פוסט כי זה צריך לצאת מתוך הדאטה בייס
function GetIHRAList() {
    ajaxCall("GET", api + "IHRAs", "", getIHRAsSCB, getIHRAsECB);
    return false;
}
function getIHRAsSCB(data) {
    let str = "";
    for (var i = 0; i < data.length; i++) {
        str += '<label class="mdl-checkbox mdl-js-checkbox mdl-js-ripple-effect" for="' + data[i].categoryID + '">';
        str += data[i].categoryName;
        str += '<input type="checkbox" name="ihraOption" class="mdl-checkbox__input" id="' + data[i].categoryID + '" value="' + data[i].categoryID + '"" /';
        str += '<span class="checkmark mdl-checkbox__label"></span> </label>';
    }
    document.getElementById("checkBoxIHRA").innerHTML += str;
}
function getIHRAsECB(err) {
    console.log(err);
}

// get the value from the check box
function getChecked() {
    var checkboxes = document.getElementsByName("ihraOption");
    var checkedValue;

    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked) {
            checkedValue = checkboxes[i].value;
        }
    }
    return checkedValue;

    ////לטם: אחרי שנגדיר את הקטגוריות שיהיו מערך בדאטה בייס נשתמש בקוד הבא בשביל הפונקציה

    //var checkboxes = document.getElementsByName("ihraOption");
    //var checkedValues = [];

    //for (var i = 0; i < checkboxes.length; i++) {
    //    if (checkboxes[i].checked) {
    //        checkedValues.push(checkboxes[i].value);
    //    }
    //}
    //return checkedValues;
}

////Hide RemovalStatus div
//function HideRemovalStatusDiv() {
//    var element = document.getElementById("RemovalStatus");
//    element.style.display = "none";
//}


////Hide ManagerStatus div
//function HideManagerStatusDiv() {
//    var element = document.getElementById("ManagerStatus");
//    element.style.display = "none";
//}

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