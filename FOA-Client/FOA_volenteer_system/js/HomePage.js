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
    drawPostsDataTable(postsArr);
}
function readPostsECB(err) {
    alert("Error " + err);
}

// render the posts list
// יש להוסיף פילטור של פוסטים מאושרים ע"י מנהל בלבד
function drawPostsDataTable(array) {
    let str = "";
    if (array.length == 0) {
        alert("There's no posts yet");
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
                        data: "urlLink",
                        render: function (data, type, row, meta) {
                            return '<a href="' + data + '" target="_blank" class="urlLink_display" >קישור לפוסט</a>';
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
                            viewBtn = '<button onclick="OpenPostCard(' + row.postID + ')">צפייה</button>';;
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
    /*str += '<table dir="rtl" id="myTable">';
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
     for (var i = postsArr.length - 1; i > 0; i--) {
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
 document.getElementById("dataTable").innerHTML += str; */
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
    alert("Input Error");
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

