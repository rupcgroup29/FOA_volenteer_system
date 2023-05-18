var api;
var currentUser = JSON.parse(sessionStorage.getItem("user"));
//var listIHRA;

$(document).ready(function () {
    if (location.hostname === "localhost" || location.hostname === "127.0.0.1") {
        api = "https://localhost:7109/api/";
    }
    else api = "https://proj.ruppin.ac.il/cgroup29/prod/api/";

    RemovedPost_vs_IHRAcategory();


});

function RemovedPost_vs_IHRAcategory() {
    ajaxCall("GET", api + "BI_Charts/Get_RemovedPost_vs_IHRAcategory", "", getRemovedPost_vs_IHRAcategorySCB, getRemovedPost_vs_IHRAcategoryECB);
    return false;
}
function getRemovedPost_vs_IHRAcategorySCB(data) {
    //GetIHRAList();
    Render_RemovedPost_vs_IHRAcategory(data);
}
function getRemovedPost_vs_IHRAcategoryECB(err) {
    alert(err);
}

// get the IHRA list                     
//function GetIHRAList() {
//    ajaxCall("GET", api + "IHRAs", "", getIHRAsSCB, getIHRAsECB);
//    return false;
//}
//function getIHRAsSCB(data) {
//    listIHRA = data;
//}
//function getIHRAsECB(err) {
//    console.log(err);
//}


// chartjs.org

//function Render_RemovedPost_vs_IHRAcategorySCB(data) {
//    var xValues = ["Italy", "France", "Spain", "USA", "Argentina"];
//    var yValues = [55, 49, 44, 24, 15];
//    var barColors = [
//        "#b91d47",
//        "#00aba9",
//        "#2b5797",
//        "#e8c3b9",
//        "#1e7145"
//    ];

//    new Chart("removedPost_vs_IHRAcategory", {
//        type: "pie",
//        data: {
//            labels: xValues,
//            datasets: [{
//                backgroundColor: barColors,
//                data: yValues
//            }]
//        },
//        options: {
//            title: {
//                display: true,
//                text: "World Wide Wine Production 2018"
//            }
//        }
//    });
//}


// ag-grid.com


function Render_RemovedPost_vs_IHRAcategory(data) {
    const options = {
        container: document.getElementById('removedPost_vs_IHRAcategory'),
        title: {
            text: "IHRA סטטוס הפוסט ברשתות החברתית כתלות בסוג קטגוריית",
        },
        //subtitle: {
        //    text: 'in billion U.S. dollars',
        //},
        data: data,
        series: [
            {
                type: 'bar',
                xKey: 'Category',   // ihra caterogy
                yKey: 'NotRemovedPosts', // סטטוס לא הוסר
                yName: 'לא הוסר',
                stacked: true,
                stroke: ['#646464'],
                fill: ['#016660']
            },
            {
                type: 'bar',
                xKey: 'Category', // ihra caterogy
                yKey: 'RemovedPosts',  // סטטוס הוסר
                yName: 'הוסר',
                stacked: true,
                stroke: ['#646464'],
                fill: ['#ffc800']
            },
        ],
    };

    agCharts.AgChart.create(options);
}