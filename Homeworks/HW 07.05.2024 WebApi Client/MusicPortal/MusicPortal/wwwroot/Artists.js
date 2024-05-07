GetArtists();

let rowArtist = function (artist) {
    return "<tr class='artist' data-rowid='" + artist.id + "'><td>" + artist.id + "</td>" +
        "<td>" + artist.name + "</td> <td>" +
        "<td><a class='editLinkArtist btn btn-warning btn-sm' data-id='" + artist.id + "'>Изменить</a> " + // btn-sm для маленьких кнопок
        "| <a class='removeLinkArtist btn btn-danger btn-sm' data-id='" + artist.id + "'>Удалить</a></td></tr>"; // btn-danger для удаления
};


function GetArtists() {
    $.ajax({
        url: 'https://localhost:7059/api/artists',
        method: "GET",
        contentType: "application/json",
        success: function (artists) {

            let rows = "";
            $.each(artists, function (index, artist) {
                // добавляем полученные элементы в таблицу
                rows += rowArtist(artist);
            })
            $("table.artists tbody").append(rows);
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR.status + '\n' + exception);
        }
    });
}


function GetArtist(id) {
    $.ajax({
        url: 'https://localhost:7059/api/artists/' + id,
        method: 'GET',
        contentType: "application/json",
        success: function (artist) {
            let form = document.forms["artistForm"];
            form.elements["Id"].value = artist.id;
            form.elements["name"].value = artist.name;
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR.status + '\n' + exception);
        }
    });
}


function CreateArtist(artistName) {

    $.ajax({
        url: 'https://localhost:7059/api/Artists',  // Конечный пункт URL
        method: 'POST',  // Метод запроса
        contentType: 'application/json',  // Тип содержимого
        data: JSON.stringify({
            name: artistName,
        }),
        success: function (artist) {
            console.log('Success:', artist);  // Действие при успешном ответе

            $("table.artists tbody").append(rowArtist(artist));
            let form = document.forms["artistForm"];
            form.reset();
            form.elements["Id"].value = 0;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('Error:', textStatus, errorThrown);  // Действие при ошибке
            console.log('Response:', jqXHR.responseText);  // Дополнительные детали ошибки
        }
    });

}

function EditArtist(artistId, artistName) {
    let request = JSON.stringify({
        id: artistId,
        name: artistName,
    });
    $.ajax({
        url: "https://localhost:7059/api/artists",
        contentType: "application/json",
        method: "PUT",
        data: request,
        success: function (artist) {
            $("tr.artist[data-rowid='" + artist.id + "']").replaceWith(rowArtist(artist));
            let form = document.forms["artistForm"];
            form.reset();
            form.elements["Id"].value = 0;
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR.status + '\n' + exception);
        }
    })
}

function DeleteArtist(id) {
    if (!confirm("Вы действительно желаете удалить пользователя?"))
        return;
    $.ajax({
        url: "https://localhost:7059/api/artists/" + id,
        contentType: "application/json",
        method: "DELETE",
        success: function (artist) {
            $("tr.artist[data-rowid='" + artist.id + "']").remove();
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR.status + '\n' + exception);
        }
    })
}

// сброс значений формы
$("#resetArtist").click(function (e) {
    e.preventDefault();
    let form = document.forms["artistForm"];
    form.reset();
    form.elements["Id"].value = 0;
});

// отправка формы
$("#submitArtist").click(function (e) {
    e.preventDefault();
    let form = document.forms["artistForm"];
    let id = form.elements["Id"].value;
    let name = form.elements["name"].value;
    if (id == 0)
        CreateArtist(name);
    else
        EditArtist(id, name);
});

// нажимаем на ссылку Изменить
$("body").on("click", ".editLinkArtist", function () {
    let id = $(this).data("id");
    GetArtist(id);
});

// нажимаем на ссылку Удалить
$("body").on("click", ".removeLinkArtist", function () {
    let id = $(this).data("id");
    DeleteArtist(id);
});