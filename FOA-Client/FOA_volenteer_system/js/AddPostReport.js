var api;
/*    נשמר במטרה לחסוך את ההתחברות בעת בדיקות   */
//var user = {
//    userID: 1024,
//    firstName: "ענת",
//    surname: "אביטל",
//    userName: "anat_a",
//    phoneNum: "0529645123",
//    roleDescription: "מנהל צוות ניטור",
//    permissionID: 3,
//    isActive: true,
//    password: "6DCA4533",
//    teamID: 1,
//    programID: 1026,
//    email: "anat_a@gmail.com",
//    programName: null
//}
//sessionStorage.setItem("user", JSON.stringify(user));

var currentUser = JSON.parse(sessionStorage.getItem("user"));

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    //Nav bar - Permission
    if (currentUser.permissionID == 4) // a volunteer is logged in
    {
        $(".ManagerNav").hide();
        $(".VolunteerNav").show();
    }
    else //Manager is logged in
    {
        $(".ManagerNav").show();
        $(".VolunteerNav").hide();
    }

    $("#u39").mouseenter(UserEnterSubManu);
    $("#u39").mouseleave(UserExitSubManu);
    $("#u40").mouseleave(UserExitSubManu);

    $("#logout").click(logout);

    //"other" sections will be readonly when page is up
    $("#platform_diff").attr("readonly", true);
    $("#country_diff").attr("readonly", true);
    $("#language_diff").attr("readonly", true);

    $('#contactForm').submit(AddNewPost); 

    GetPlatformsList();
    GetCountriesList();
    GetLanguagesList();
    GetIHRAList();           

    enableOtherPlatform();           
    enableOtherCountry();            
    enableOtherLanguage()            

});

//NAVBAR USER

function UserEnterSubManu() {
    $("#u40").css("visibility", "inherit")
    $("#u40").show();
}
function UserExitSubManu() {
    $("#u40").css("visibility", "hidden")
    $("#u40").hide();
}

//logout function
function logout() {
    isLogIn = false;
    sessionStorage.clear();
    window.location.assign("Log-In.html");
}

//END - NAVBAR USER
// add new post - submit
function AddNewPost() {
    let urlLink = $("#urlLink").val();
    let description = $("#description").val();
    let keyWordsAndHashtages = separatekeyWordsAndHashtages();  // ענת: מפעיל פונקציה שתופסת את כל הטקסט, מפרידה לפי פסיק ומחזירה מערך
    let threat = $("#content_threat").val();
    // let screenshot = $("#UrlLink").val();            //לטם: כרגע אנחנו מנסות לעשות את זה בלי הוספת התמונה, באופן זמני בלבד
    let amountOfLikes = $("#exposure_likes").val();     
    let amountOfShares = $("#exposure_shares").val();    
    let amountOfComments = $("#exposure_Comments").val();  
    let userID = currentUser.userID;
    let platformID = $("#platform").val();
    let categoryID = getChecked();
    let countryID = isKnownOrNotCountry();      // למקרה ומשתמש לא בחר מדינה מפני שאינו יודע איזו
    let languageID = $("#language").val();
    let platformName = $("#platform_diff").val();
    let countryName = $("#country_diff").val();
    let languageName = $("#language_diff").val();

    const newPost = {
        PostID: "1", 
        UrlLink: urlLink,
        Description: description,
        KeyWordsAndHashtages: keyWordsAndHashtages,
        Threat: threat,
        //Screenshot: screenshot,       //עד שננסה לשמור תמונות, כרגע זה בהערה גם בצד שרת
        AmountOfLikes: amountOfLikes,
        AmountOfShares: amountOfShares,
        AmountOfComments: amountOfComments,
        PostStatus: "1",
        RemovalStatus: "1", 
        UserID: userID,
        PlatformID: platformID,
        CategoryID: categoryID,
        PostStatusManager: "1",      // במטרה לשלוח עם יוזר איידי קיים, ישתנה בעריכת פוסט
        RemovalStatusManager: "1",   // במטרה לשלוח עם יוזר איידי קיים, ישתנה בעריכת פוסט
        CountryID: countryID,
        LanguageID: languageID,
        PlatformName: platformName,
        CountryName: countryName,
        LanguageName: languageName
    }

    ajaxCall("POST", api + "Posts", JSON.stringify(newPost), postAddNewPostSCB, postAddNewPostECB);
    return false;
}
function postAddNewPostSCB(data) { // הוספת משתמש הצליחה
    alert("דיווח הפוסט נוסף בהצלחה");
    //window.location.assign("HomePage.html");
    location.assign("HomePage.html")
}
function postAddNewPostECB(err) {
    alert("שגיאה בהוספת הדיווח, אנא נסו שוב");
}

//
function isKnownOrNotCountry() {
    let countryID = $("#country").val();
    if (countryID == 0) {
        return 285;
    }
    else return countryID;
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
        str += '<option class="opt" value="0">בחר מדינה</option>';
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


// get the IHRA list                     
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
    const checkboxes = document.querySelectorAll('input[type="checkbox"]');
    const checkedValue =[];

    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked) {
            checkedValue.push(checkboxes[i].value);
        }
    }
    return checkedValue;
}

// separate by , from key words and hashtages filed 
function separatekeyWordsAndHashtages() {
    let keyWordsAndHashtages = $("#keywords_hashtags").val();
    const separated = keyWordsAndHashtages.split(",");
    return separated;
}

// enable Other platform only if other selected
function enableOtherPlatform() {
    var sel = document.getElementById('platform');

    sel.addEventListener("change", enableDivIfOtherSelected);

    function enableDivIfOtherSelected() {

        if (sel.value === '999') {
            $("#platform_diff").attr("readonly", false);
        }
        else {
            $("#platform_diff").attr("readonly", true);
            document.getElementById('platform_diff').value = '';
        }
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
        else {
            $("#country_diff").attr("readonly", true);
            document.getElementById('country_diff').value = '';
        }
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
        else {
            $("#language_diff").attr("readonly", true);
            document.getElementById('language_diff').value = '';
        }
    }
}

