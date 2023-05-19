var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    RemovedPost_vs_IHRAcategory();
    Top5KeyWordsAndHashtages();
    PostsUploadedByMonth();
    PercentagePostsRemoved();
    Last7DaysPostUploaded();
    PostsPerPlatfom();

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
        autoSize: true,
        title: {
            text: "IHRA סטטוס הפוסט ברשתות החברתית כתלות בסוג קטגוריית",
            fontSize: '23',
        },
        data: data,
        series: [
            {
                type: 'bar',
                xKey: 'Category',   // ihra caterogy
                yKey: 'NotRemovedPosts', // סטטוס לא הוסר
                yName: 'לא הוסר',
                stroke: '#646464',
                fill: '#016660',
            },
            {
                type: 'bar',
                xKey: 'Category', // ihra caterogy
                yKey: 'RemovedPosts',  // סטטוס הוסר
                yName: 'הוסר',
                stroke: '#646464',
                fill: '#c79e28',
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
    str = '<h3 class="top5">MOST COMMON</h3></br>';
    for (var i = 0; i < data.length; i++) {
        str += '<h4 class="top5">' + data[i] + '</h4>';
    }
    document.getElementById('top5KeyWordsAndHashtages').innerHTML += str;
}



///// Posts Uploaded By Month /////

function PostsUploadedByMonth() {
    ajaxCall("GET", api + "BI_Charts/Get_PostsUploadedByMonth", "", getPostsUploadedByMonthSCB, getECB);
    return false;
}
function getPostsUploadedByMonthSCB(data) {
    Render_PostsUploadedByMonth(data);
}

function Render_PostsUploadedByMonth(data) {
    const options = {
        container: document.getElementById('PostsUploadedByMonth'),
        autoSize: true,
        title: {
            text: 'כמות הפוסטים שהועלו למערכת לפי חודשים',
            fontSize: '23',
        },
        data: data,
        series: [
            {
                xKey: 'Month',
                yKey: 'PostsCounter',
                stroke: '#016660',
                marker: {
                    fill: '#646464',
                    stroke: '#016660',
                },
            },
        ],
        legend: {
            enabled: false,
        },
    };

    agCharts.AgChart.create(options);
}



///// Percentage of posts removed from social networks /////

function PercentagePostsRemoved() {
    ajaxCall("GET", api + "BI_Charts/Get_PercentagePostsRemoved", "", getPercentagePostsRemovedSCB, getECB);
    return false;
}
function getPercentagePostsRemovedSCB(data) {
    Render_PercentagePostsRemoved(data);
}

function Render_PercentagePostsRemoved(data) {
    const total = data.reduce((sum, d) => sum + d.count, 0);
    const percentage = (value) => `${((value / total) * 100).toFixed()}%`;

    const options = {
        container: document.getElementById('percentageOfPostsRemoved'),
        autoSize: true,
        title: {
            text: 'אחוז פוסטים שהוסרו מהרשתות החברתיות',
            fontSize: '23',
            color: 'white',
        },
        background: {
            fill: 'rgba(128, 128, 128, 0.5)',
        },
        data,
        series: [
            {
                type: 'pie',
                angleKey: 'count',
                fills: ['#c79e28', '#f2e2b3'],  //ראשון צבע כהה, שני צבע בהיר
                strokeWidth: 0,
                innerRadiusOffset: -20,
                innerLabels: [
                    {
                        text: percentage(data[0].count),
                        color: '#b88f1a',   //צבע כהה
                        fontSize: 64,
                    },
                ],
                innerCircle: {
                    fill: '#f7ebc8',    //צבע בהיר
                },
            },
        ],
    };

    agCharts.AgChart.create(options);
}



///// NUMBER OF POSTS UPLOADED IN THE LAST 7 DAYS /////

function Last7DaysPostUploaded() {
    ajaxCall("GET", api + "BI_Charts/Get_ReadPostsCountLast7Days", "", getLast7DaysPostUploadedSCB, getECB);
    return false;
}
function getLast7DaysPostUploadedSCB(data) {
    RenderLast7DaysPostUploaded(data);
}

function RenderLast7DaysPostUploaded(data) {
    str = '<h3 class="top5">בשבוע האחרון נוספו </br> עוד ' + data + ' פוסטים חדשים</h3>';
    document.getElementById('PostsCountLast7Days').innerHTML += str;
}



///// Removed Post vs IHRA category /////

function PostsPerPlatfom() {
    ajaxCall("GET", api + "BI_Charts/Get_ReadPostsPerPlatfom", "", getPostsPerPlatfomSCB, getECB);
    return false;
}
function getPostsPerPlatfomSCB(data) {
    Render_PostsPerPlatfom(data);
}

function Render_PostsPerPlatfom(data) {
    const options = {
        container: document.getElementById('PostsPerPlatfom'),
        title: {
            text: "כמות הפוסטים שהועלו בכל פלטפורמה",       
            fontSize: '23',
        },
        data: data,
        series: [
            {
                type: 'column',
                xKey: 'PlatformName',  
                yKey: 'Count',
                stroke: '#646464',
                fill: '#c79e28',
                FontFamily: 'Arimo',
            },
        ],
        legend: {
            enabled: false,
        },
    };

    agCharts.AgChart.create(options);
}













