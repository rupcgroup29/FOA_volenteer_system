var api;
var isLoggedIn;
var postsArr = [];
var postsIDs = [];
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
        if (currentUser[1] == 2) // a manager is logged in
        {
            AlertPostsForApproval();
        }
        sessionStorage.setItem("justLoggedIn", JSON.stringify(false));
    }

    $("#RecommendationSection").dblclick(goToBIpage);

    readPosts();
    getRecommendation();

    // for the popup alert for manegers:  
    closePopup();


});

function goToBIpage() {
    window.location.assign("BI.html");
}

// read all posts
function readPosts() {
    ajaxCall("GET", api + "ReadPosts", "", readPostsSCB, readPostsECB);
}
function readPostsSCB(data) {
    postsArr = data;
    savePostIDs(postsArr);
    drawPostsDataTable(postsArr);
}
function readPostsECB(err) {
    alert(err.responseJSON.errorMessage);
}

//save posts IDs to array
function savePostIDs(array) {
    for (var i = 0; i < array.length; i++) {
        var id = array[i].postID;
        postsIDs.push(id);
    }
    sessionStorage.setItem("postsIDs", JSON.stringify(postsIDs));
}

// render the posts list
function drawPostsDataTable(array) {
    let str = "";
    if (array.length == 0) {
        alert("אין פוסטים במערכת");
    } else {
        try {
            tbl = $('#dataTable').DataTable({
                "info": false,
                data: array,
                pageLength: 10,     //כמה שורות יהיו בכל עמוד

                columns: [
                    { data: 'postID', orderSequence: ['desc', 'asc'] }, // specify orderSequence
                    { data: "platformName" },
                    {
                        data: "postStatus",
                        render: function (data, type, row, meta) {
                            if (data == 0)
                                return 'ממתין לאישור';
                            if (data==1)
                                return 'מאושר';
                            else return 'נדחה'
                        }
                    },
                    { data: "languageName" },
                    { data: "amountOfShares" },
                    { data: "amountOfComments" },
                    { data: "amountOfLikes" },
                    {
                        data: "postStatus",
                        render: function (data, type, row, meta) {
                            if (data == 0)
                                return 'דווח';
                            else
                                return 'הוסר';
                        }
                    },
                    { data: "userName" },
                    {
                        data: "insertDate",
                        render: function (data, type, row, meta) {
                            return data.split('T')[0];  // cut the time from the DateTime format
                        }
                    },
                    {
                        render: function (data, type, row, meta) {      //יצירת כפתור צפייה בפוסט
                            viewBtn = '<button class="view-button" onclick="OpenPostCard(' + row.postID + ')">צפייה</button>';;
                            return viewBtn;
                        }
                    }
                ],
                order: [[0, 'desc']],  // sort the first column (postID) in descending order
                language: {
                    url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/he.json'
                }
            });
        } catch (err) {
            alert(err);
        }
    }
}

// save the relevant post to open in edit\view mode (Depends on permission)
function OpenPostCard(postID) {
    sessionStorage.setItem("post", JSON.stringify(postID));
    window.location.href = "PostReportCard%20.html";
}

// התראות למנהל על כמות פוסטים שטרם אושרו
function AlertPostsForApproval() {
    ajaxCall("GET", api + "Posts/numberOfNoneStatusPosts", "", AlertPostsForApprovalSCB, AlertPostsForApprovalECB);
}
function AlertPostsForApprovalSCB(data) {
    str_popup = 'ישנם ' + data + ' פוסטים הממתינים לאישור מנהל';
    $('#popupHeadline').text(str_popup);
    openPopup();
}
function AlertPostsForApprovalECB(err) {
    alert(err.responseJSON.errorMessage);
}

function openPopup() {
    let popup = document.getElementById('popup');
    popup.classList.add('open-popup');
}
function closePopup() {
    let popup = document.getElementById('popup');
    popup.classList.remove('open-popup');
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
    alert(err.responseJSON.errorMessage);
}

function renderRecommendation() {
    let str_Reco = "";
    str_Reco += "<h4>היום מומלץ לך לנטר פוסטים</h4>";
    str_Reco += "<h4>בפלטפורמת " + exposure.platform + "</h4>";
    str_Reco += "<h4> בשפה " + exposure.language + "</h4>";
    str_Reco += "<h4> ובשימוש בהאשטאג " + exposure.keyWordsAndHashtages + "</h4>";

    document.getElementById("RecommendationSection").innerHTML += str_Reco;
}

