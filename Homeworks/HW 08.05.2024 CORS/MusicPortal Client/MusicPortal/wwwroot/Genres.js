GetGenres();

let rowGenre = function (genre) {
    return "<tr class='genre' data-rowid='" + genre.id + "'><td>" + genre.id + "</td>" +
        "<td>" + genre.name + "</td> <td>" +
        "<td><a class='editLinkGenre btn btn-warning btn-sm' data-id='" + genre.id + "'>Изменить</a> " + // btn-sm для маленьких кнопок
        "| <a class='removeLinkGenre btn btn-danger btn-sm' data-id='" + genre.id + "'>Удалить</a></td></tr>"; // btn-danger для удаления
};


function GetGenres() {
    $.ajax({
        url: 'https://localhost:7059/api/genres',
        method: "GET",
        contentType: "application/json",
        success: function (genres) {

            let rows = "";
            $.each(genres, function (index, genre) {
                // добавляем полученные элементы в таблицу
                rows += rowGenre(genre);
            })
            $("table.genres tbody").append(rows);
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR.status + '\n' + exception);
        }
    });
}


function GetGenre(id) {
    $.ajax({
        url: 'https://localhost:7059/api/genres/' + id,
        method: 'GET',
        contentType: "application/json",
        success: function (genre) {
            let form = document.forms["genreForm"];
            form.elements["Id"].value = genre.id;
            form.elements["name"].value = genre.name;
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR.status + '\n' + exception);
        }
    });
}


function CreateGenre(genreName) {

    $.ajax({
        url: 'https://localhost:7059/api/Genres',  // Конечный пункт URL
        method: 'POST',  // Метод запроса
        contentType: 'application/json',  // Тип содержимого
        data: JSON.stringify({
            name: genreName,
        }),
        success: function (genre) {
            console.log('Success:', genre);  // Действие при успешном ответе

            $("table.genres tbody").append(rowGenre(genre));
            let form = document.forms["genreForm"];
            form.reset();
            form.elements["Id"].value = 0;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('Error:', textStatus, errorThrown);  // Действие при ошибке
            console.log('Response:', jqXHR.responseText);  // Дополнительные детали ошибки
        }
    });

}

function EditGenre(genreId, genreName) {
    let request = JSON.stringify({
        id: genreId,
        name: genreName,
    });
    $.ajax({
        url: "https://localhost:7059/api/genres",
        contentType: "application/json",
        method: "PUT",
        data: request,
        success: function (genre) {
            $("tr.genre[data-rowid='" + genre.id + "']").replaceWith(rowGenre(genre));
            let form = document.forms["genreForm"];
            form.reset();
            form.elements["Id"].value = 0;
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR.status + '\n' + exception);
        }
    })
}

function DeleteGenre(id) {
    if (!confirm("Вы действительно желаете удалить пользователя?"))
        return;
    $.ajax({
        url: "https://localhost:7059/api/genres/" + id,
        contentType: "application/json",
        method: "DELETE",
        success: function (genre) {
            $("tr.genre[data-rowid='" + genre.id + "']").remove();
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR.status + '\n' + exception);
        }
    })
}

// сброс значений формы
$("#resetGenre").click(function (e) {
    e.preventDefault();
    let form = document.forms["genreForm"];
    form.reset();
    form.elements["Id"].value = 0;
});

// отправка формы
$("#submitGenre").click(function (e) {
    e.preventDefault();
    let form = document.forms["genreForm"];
    let id = form.elements["Id"].value;
    let name = form.elements["name"].value;
    if (id == 0)
        CreateGenre(name);
    else
        EditGenre(id, name);
});

// нажимаем на ссылку Изменить
$("body").on("click", ".editLinkGenre", function () {
    let id = $(this).data("id");
    GetGenre(id);
});

// нажимаем на ссылку Удалить
$("body").on("click", ".removeLinkGenre", function () {
    let id = $(this).data("id");
    DeleteGenre(id);
});