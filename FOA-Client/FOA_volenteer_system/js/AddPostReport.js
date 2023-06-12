var api;
var imageFolder;
var currentUser = JSON.parse(sessionStorage.getItem("user"));

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

    //"other" sections will be readonly when page is up
    $("#platform_diff").attr("readonly", true);
    $("#country_diff").attr("readonly", true);
    $("#language_diff").attr("readonly", true);

    $('#contactForm').submit(SaveImage);

    GetPlatformsList();
    GetCountriesList();
    GetLanguagesList();
    GetIHRAList();

    enableOtherPlatform();
    enableOtherCountry();
    enableOtherLanguage()

});

// submit new post
function SaveImage() {
    var data = new FormData();
    var files = $("#screenshotFiles").get(0).files;

    // Add the uploaded file to the form data collection  
    if (files.length > 0) {
        data.append("files", files[0]);
    } else { alert(" You must upload a file "); return false; }

    // Ajax upload  
    $.ajax({
        type: "POST",
        url: api + "Posts/screenshot",
        contentType: false,
        processData: false,
        data: data,
        success: AddNewPost,
        error: error
    });
    return false;
}

function error(data) {
    console.log(data);
}

// add new post 
function AddNewPost(data) {
    let keyWordsAndHashtages = separatekeyWordsAndHashtages();  // ענת: מפעיל פונקציה שתופסת את כל הטקסט, מפרידה לפי פסיק ומחזירה מערך
    let categoryID = getChecked();
    let countryID = isKnownOrNotCountry();      // למקרה ומשתמש לא בחר מדינה מפני שאינו יודע איזו

    const newPost = {
        PostID: "1",
        UrlLink: $("#urlLink").val(),
        Description: $("#description").val(),
        KeyWordsAndHashtages: keyWordsAndHashtages,
        Threat: $("#content_threat").val(),
        Screenshot: data[0],
        AmountOfLikes: $("#exposure_likes").val(),
        AmountOfShares: $("#exposure_shares").val(),
        AmountOfComments: $("#exposure_Comments").val(),
        PostStatus: "1",
        RemovalStatus: "1",
        UserID: currentUser[0],
        PlatformID: $("#platform").val(),
        CategoryID: categoryID,
        PostStatusManager: "1",      // ישתנה בעריכת פוסט
        RemovalStatusManager: "1",   // ישתנה בעריכת פוסט
        CountryID: countryID,
        LanguageID: $("#language").val(),
        PlatformName: $("#platform_diff").val(),
        CountryName: $("#country_diff").val(),
        LanguageName: $("#language_diff").val()
    }

    ajaxCall("POST", api + "Posts", JSON.stringify(newPost), postAddNewPostSCB, postAddNewPostECB);
    return false;
}
function postAddNewPostSCB(data) { // הוספת דיווח הצליחה
    alert("דיווח הפוסט נוסף בהצלחה");
    location.assign("HomePage.html")
}
function postAddNewPostECB(err) {
    alert("שגיאה בהוספת הדיווח, " + err.responseJSON.errorMessage + " אנא נסו שוב");
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
        str += '<option class="opt" value="0">* בחירת רשת חברתית</option>';
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
        str += '<option class="opt" value="0">בחירת מדינה</option>';
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
        str += '<option class="opt" value="0">* בחירת שפה</option>';
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
    const checkedValue = [];

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
    var difPlatforDiv = document.getElementById('diffPlatformDiv');
    difPlatforDiv.style.display = 'none';

    var platformSelect = document.getElementById('platform');
    platformSelect.addEventListener('change', function () {
        if (platformSelect.value === '999') {
            difPlatforDiv.style.display = 'block';
            document.getElementById('platform_diff').removeAttribute('readonly');
            platformDiv.classList.add('col-6');
        } else {
            difPlatforDiv.style.display = 'none';
            document.getElementById('platform_diff').setAttribute('readonly', 'readonly');
            platformDiv.classList.remove('col-6');
            document.getElementById('platform_diff').value = '';
        }
    });
}
//function enableOtherPlatform() {
//var sel = document.getElementById('platform');
//sel.addEventListener("change", enableDivIfOtherSelected);
//function enableDivIfOtherSelected() {

//    if (sel.value === '999') {
//        $("#platform_diff").attr("readonly", false);
//    }
//    else {
//        $("#platform_diff").attr("readonly", true);
//        document.getElementById('platform_diff').value = '';
//    }
//}
//}

// enable Other country only if other selected
function enableOtherCountry() {
    var difCountryForDiv = document.getElementById('diffCountryDiv');
    difCountryForDiv.style.display = 'none';

    var countrySelect = document.getElementById('country');
    countrySelect.addEventListener('change', function () {
        if (countrySelect.value === '999') {
            difCountryForDiv.style.display = 'block';
            document.getElementById('country_diff').removeAttribute('readonly');
            countryDiv.classList.add('col-6');
        } else {
            difCountryForDiv.style.display = 'none';
            document.getElementById('country_diff').setAttribute('readonly', 'readonly');
            document.getElementById('country_diff').value = '';;
            countryDiv.classList.remove('col-6');
        }
    });
}
//function enableOtherCountry() {
//    var sel = document.getElementById('country');
//    sel.addEventListener("change", enableDivIfOtherSelected);
//    function enableDivIfOtherSelected() {
//        if (sel.value === '999') {
//            $("#country_diff").attr("readonly", false);
//        }
//        else {
//            $("#country_diff").attr("readonly", true);
//            document.getElementById('country_diff').value = '';
//        }
//    }
//}
// enable Other language only if other selected
function enableOtherLanguage() {
    var difLanguageForDiv = document.getElementById('diffLanguageDiv');
    difLanguageForDiv.style.display = 'none';

    var languageSelect = document.getElementById('language');
    languageSelect.addEventListener('change', function () {
        if (languageSelect.value === '999') {
            difLanguageForDiv.style.display = 'block';
            document.getElementById('language_diff').removeAttribute('readonly');
            languageDiv.classList.add('col-6');
        } else {
            difLanguageForDiv.style.display = 'none';
            document.getElementById('language_diff').setAttribute('readonly', 'readonly');
            document.getElementById('language_diff').value = '';;
            languageDiv.classList.remove('col-6');
        }
    });
}
//function enableOtherLanguage() {
//    var sel = document.getElementById('language');
//    sel.addEventListener("change", enableDivIfOtherSelected);
//    function enableDivIfOtherSelected() {
//        if (sel.value === '999') {
//            $("#language_diff").attr("readonly", false);
//        }
//        else {
//            $("#language_diff").attr("readonly", true);
//            document.getElementById('language_diff').value = '';
//        }
//    }
//}

