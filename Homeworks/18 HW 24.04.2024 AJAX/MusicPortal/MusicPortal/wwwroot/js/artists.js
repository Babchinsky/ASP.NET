$(document).ready(function () {

    getAllArtists();

    $("body").on("click", ".btn-get-artist", function () {
        let id = $(this).data("artistid");
        getArtist(id);
    });

    let rowArtist = function (artist) {
        return "<tr data-rowid='" + artist.Id + "'><td>" + artist.Id + "</td>" +
            "<td>" + artist.Name +
            "<td><a href='javascript: void (0)' data-artistid='" + artist.Id +
            "' class='btn btn-success btn-get-artist'>Get Artist</a></td></tr>";
    };


    function getAllArtists() {
        $.ajax({
            url: '@Url.Action("GetArtists", "Artists")',
            type: 'GET',
            contentType: false,
            processData: false,
            success: function (response) {
                let rows = "";
                let artists = JSON.parse(response);
                $.each(artists, function (index, artist) {
                    rows += rowArtist(artist);
                })
                $("#artist-list").html(rows);
            },
            error: function (jqXHR, statusText, error) {
                console.log(jqXHR.status + '\n' + statusText + '\n' + error);
            }
        });
    }

    function getArtist(artistid) {

        let url = '@Url.Action("GetDetailsById", "Artists")' + '/' + artistid;
        $.ajax({
            type: 'GET',
            url: url,
            contentType: false,
            processData: false,
            success: function (response) {
                var artist = JSON.parse(response);
                $("#name").val(artist.Name);
                $("#hdn-artist-id").val(artist.Id);

            },
            error: function (jqXHR, statusText, error) {
                console.log(jqXHR.status + '\n' + statusText + '\n' + error);
            }
        });
    }


    $("#btn-insert-artist").on("click", function () {
        let formData = new FormData();
        formData.append("name", $("#name").val());
        $.ajax({
            type: 'POST',
            url: '@Url.Action("InsertArtist", "Artists")',
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                alert(response);
                resetForm();
                getAllArtists();
            },
            error: function (jqXHR, statusText, error) {
                console.log(jqXHR.status + '\n' + statusText + '\n' + error);
            }
        });
    });

    $("#btn-update-artist").on("click", function () {
        let formData = new FormData();
        formData.append("id", $("#hdn-artist-id").val());
        formData.append("name", $("#name").val());
        $.ajax({
            type: 'PUT',
            url: '@Url.Action("UpdateArtist", "Artists")',
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                alert(response);
                resetForm();
                getAllArtists();
            },
            error: function (jqXHR, statusText, error) {
                console.log(jqXHR.status + '\n' + statusText + '\n' + error);
            }
        });
    });

    $("#btn-delete-artist").on("click", function () {
        if (!confirm("Вы действительно желаете удалить студента?"))
            return;
        let formData = new FormData();
        formData.append("id", $("#hdn-artist-id").val());
        $.ajax({
            type: 'DELETE',
            url: '@Url.Action("DeleteArtist", "Artists")',
            contentType: false,
            processData: false,
            data: formData,
            success: function (response) {
                alert(response);
                resetForm();
                getAllArtists();
            },
            error: function (jqXHR, statusText, error) {
                console.log(jqXHR.status + '\n' + statusText + '\n' + error);
            }
        });
    });
    function resetForm() {
        $("#hdn-artist-id").val("");
        $("#name").val("");
    }
});