
//סקריפט של בני להעלאת תמונות
$(document).ready(function () {

    $('#buttonUpload').on('click', function () {
        var data = new FormData();
        var files = $("#files").get(0).files;

        // Add the uploaded file to the form data collection  
        if (files.length > 0) {
            for (f = 0; f < files.length; f++) {
                data.append("files", files[f]);
            }
        }

        api = "https://localhost:7109/api/";
        imageFolder = "https://localhost:7109/images/";

        // Ajax upload  
        $.ajax({
            type: "POST",
            url: api,
            contentType: false,
            processData: false,
            data: data,
            success: showImages,
            error: error
        });

        return false;
    });

});

function showImages(data) {
    console.log(data);

    var imgStr = "";

    if (Array.isArray(data)) {

        for (var i = 0; i < data.length; i++) {
            src = imageFolder + data[i];
            imgStr += `<img src='${src}'/>`;
        }

    }

    else { // just in case you have an api returning a single string
        src = imageFolder + data;
        imgStr = `<img src='${src}'/>`;
    }

    document.getElementById("ph").innerHTML = imgStr;
}

function error(data) {
    console.log(data);
}
