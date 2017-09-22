$(document).ready(function () {
    document.getElementById("form").onsubmit = function () {
        var genrsinput = new Array();
        $("input:checkbox[name=genrs]:checked").each(function () {
            genrsinput.push($(this).val());
        });

        var relatedArtistsinput = new Array();
        $("input:checkbox[name=relatedArtists]:checked").each(function () {
            relatedArtistsinput.push($(this).val());
        });
        if (relatedArtistsinput.length === 0 || genrsinput.length === 0) {
            document.getElementById('genrsLabel').innerHTML = "Du behöver välja minst en från genres och minst en artitser.";
            document.getElementById('genrsLabel').style.visibility = "visible";
            $("#genrsRelatedDiv").css("border", "1px solid");
            document.getElementById('genrsRelatedDiv').style.borderColor = "red";
            return false;
        }
        return true;
    };
});



function getArtist(ele) {
    var input = ele.value;
    var diven = ele.parentElement;
    var divenchildren = diven.childNodes;
    var dataList = divenchildren[3];
    $.ajax({
        url: '/home/artistsName',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ name: input }),
        success: function (data) {
            var options = '';
            for (var i = 0; i < data.length; i++)
                options += '<option value="' + data[i] + '" />';

            dataList.innerHTML = options;
        },
        error: function (data) { }
    });
    document.getElementById('getData').style.visibility = "visible";

}



function addArtist() {
    var artistsDiv = $('#artistsDiv');
    var element = "<label style='margin-right: 20px;'>Välj artist: </label><input list='artists' name='artists' type='text' onkeyup='getArtist(this)' placeholder='Sök' /><datalist id='artists'></datalist><div><br /></div>";
    artistsDiv.append(element);
}

function getDataToFilter() {
    var elem = document.getElementsByName('artists');
    $('#genres').empty();
    document.getElementById('genrsLabel').style.visibility = "hidden";
    $("#genrsRelatedDiv").css("border", "none");
    document.getElementById('genrsRelatedDiv').style.borderColor = "transparant";
    $('#relatedArtists').empty();
    $("#genres").append("<h2>Genre</h2>");

    var names = [];
    for (var i = 0; i < elem.length; ++i) {
        if (typeof elem[i].value !== "undefined") {
            names.push(elem[i].value);
        }
    }
    $.ajax({
        url: '/home/getGenrs',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ name: names }),
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#genres").append("<label for='" + data[i] + "'>" + data[i] + "</label><input type='checkbox' value='" + data[i] + "' onchange='notMoreThenFive(this)' name='genrs' id=" + data[i] + ">");
            }
        },
        error: function (data) { }
    });
    $("#relatedArtists").append("<h2>Artister</h2>");

    $.ajax({
        url: '/home/getRelatedArtists',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ name: names }),
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#relatedArtists").append("<label for='" + data[i] + "'>" + data[i] + "</label><input type='checkbox' value='" + data[i] + "' onchange='notMoreThenFive(this)' name='relatedArtists' id=" + data[i] + ">");
            }
        },
        error: function (data) { }
    });
    document.getElementById('getResult').style.visibility = "visible";
    document.getElementById('artistsPopularity').style.visibility = "visible";
    document.getElementById('artistslimit').style.visibility = "visible";
}

function notMoreThenFive(ele) {
    var values = new Array();
    $("input:checkbox[name=genrs]:checked").each(function () {
        values.push($(this).val());
    });
    $("input:checkbox[name=relatedArtists]:checked").each(function () {
        values.push($(this).val());
    });

    if (values.length > 5) {
        ele.checked = false;
        document.getElementById('genrsLabel').innerHTML = "Max 5 val.";
        document.getElementById('genrsLabel').style.visibility = "visible";
        $("#genrsRelatedDiv").css("border", "1px solid");
        document.getElementById('genrsRelatedDiv').style.borderColor = "red";
    }
    else {
        document.getElementById('genrsLabel').style.visibility = "hidden";
        $("#genrsRelatedDiv").css("border", "none");
        document.getElementById('genrsRelatedDiv').style.borderColor = "transparant";
    }
}