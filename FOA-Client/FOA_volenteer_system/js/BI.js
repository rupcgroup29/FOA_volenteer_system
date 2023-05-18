var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    RemovedPost_vs_IHRAcategory();
    Top5KeyWordsAndHashtages();

});


///// Removed Post vs IHRA category /////

function RemovedPost_vs_IHRAcategory() {
    ajaxCall("GET", api + "BI_Charts/Get_RemovedPost_vs_IHRAcategory", "", getRemovedPost_vs_IHRAcategorySCB, getECB);
    return false;
}
function getRemovedPost_vs_IHRAcategorySCB(data) {
    Render_RemovedPost_vs_IHRAcategory(data);
}
function getECB(err) {
    alert(err);
}

function Render_RemovedPost_vs_IHRAcategory(data) {
    const options = {
        container: document.getElementById('removedPost_vs_IHRAcategory'),
        title: {
            text: "IHRA סטטוס הפוסט ברשתות החברתית כתלות בסוג קטגוריית",
            font: ['Arimo'],
        },
        data: data,
        series: [
            {
                type: 'bar',
                xKey: 'Category',   // ihra caterogy
                yKey: 'NotRemovedPosts', // סטטוס לא הוסר
                yName: 'לא הוסר',
                stroke: ['#646464'],
                fill: ['#016660'],
                font: ['Arimo'],
            },
            {
                type: 'bar',
                xKey: 'Category', // ihra caterogy
                yKey: 'RemovedPosts',  // סטטוס הוסר
                yName: 'הוסר',
                stroke: ['#646464'],
                fill: ['#c79e28'],
                font: ['Arimo'],
            },
        ],
    };

    agCharts.AgChart.create(options);
}


///// TOP 5 Key Words and Hashtages /////

function Top5KeyWordsAndHashtages() {
    ajaxCall("GET", api + "BI_Charts/Get_Top5KeyWordsAndHashtages", "", getTop5SCB, getECB);
    return false;
}
function getTop5SCB(data) {
    RenderTop5(data);
}

function RenderTop5(data) {
    str = "";
    for (var i = 0; i < data.length; i++) {
        str += '<h4 class="top5">' + (i + 1) + '. ' + data[i] + '</h4>';
    }
    document.getElementById('top5KeyWordsAndHashtages').innerHTML += str;
}

