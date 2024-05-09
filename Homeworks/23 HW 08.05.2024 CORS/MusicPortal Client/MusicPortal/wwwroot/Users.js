GetUsers();

let rowUser = function (user) {
    return "<tr class='user' data-rowid='" + user.id + "'><td>" + user.id + "</td>" +
        "<td>" + user.login + "</td> <td>" + user.password + "</td>" +
        "<td>" + user.accessLevel + "</td>" +  // Закрываем <td>
        "<td><a class='editLink btn btn-warning btn-sm' data-id='" + user.id + "'>Изменить</a> " + // btn-sm для маленьких кнопок
        "| <a class='removeLink btn btn-danger btn-sm' data-id='" + user.id + "'>Удалить</a></td></tr>"; // btn-danger для удаления
};


function GetUsers() {
    $.ajax({
        url: 'https://localhost:7059/api/users',
        method: "GET",
        contentType: "application/json",
        success: function (users) {

            let rows = "";
            $.each(users, function (index, user) {
                // добавляем полученные элементы в таблицу
                rows += rowUser(user);
            })
            $("table.users tbody").append(rows);
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR.status + '\n' + exception);
        }
    });
}


function GetUser(id) {
    $.ajax({
        url: 'https://localhost:7059/api/users/' + id,
        method: 'GET',
        contentType: "application/json",
        success: function (user) {
            let form = document.forms["userForm"];
            form.elements["Id"].value = user.id;
            form.elements["login"].value = user.login;
            form.elements["password"].value = user.password;
            form.elements["accessLevel"].value = user.accessLevel;
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR.status + '\n' + exception);
        }
    });
}


function CreateUser(userLogin, userPassword, userAccessLevel) {

    $.ajax({
        url: 'https://localhost:7059/api/Users',  // Конечный пункт URL
        method: 'POST',  // Метод запроса
        contentType: 'application/json',  // Тип содержимого
        data: JSON.stringify({
            login: userLogin,
            password: userPassword,
            accessLevel: parseInt(userAccessLevel, 10)
        }),
        success: function (user) {
            console.log('Success:', user);  // Действие при успешном ответе

            $("table.users tbody").append(rowUser(user));
            let form = document.forms["userForm"];
            form.reset();
            form.elements["Id"].value = 0;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('Error:', textStatus, errorThrown);  // Действие при ошибке
            console.log('Response:', jqXHR.responseText);  // Дополнительные детали ошибки
        }
    });

}

function EditUser(userId, userLogin, userPassword, userAccessLevel) {
    let request = JSON.stringify({
        id: userId,
        login: userLogin,
        password: userPassword,
        accessLevel: parseInt(userAccessLevel, 10)
    });
    $.ajax({
        url: "https://localhost:7059/api/users",
        contentType: "application/json",
        method: "PUT",
        data: request,
        success: function (user) {
            $("tr.user[data-rowid='" + user.id + "']").replaceWith(rowUser(user));
            let form = document.forms["userForm"];
            form.reset();
            form.elements["Id"].value = 0;
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR.status + '\n' + exception);
        }
    })
}

function DeleteUser(id) {
    if (!confirm("Вы действительно желаете удалить пользователя?"))
        return;
    $.ajax({
        url: "https://localhost:7059/api/users/" + id,
        contentType: "application/json",
        method: "DELETE",
        success: function (user) {
            $("tr.user[data-rowid='" + user.id + "']").remove();
        },
        error: function (jqXHR, exception) {
            console.log(jqXHR.status + '\n' + exception);
        }
    })
}

// сброс значений формы
$("#reset").click(function (e) {
    e.preventDefault();
    let form = document.forms["userForm"];
    form.reset();
    form.elements["Id"].value = 0;
});

// отправка формы
$("#submit").click(function (e) {
    e.preventDefault();
    let form = document.forms["userForm"];
    let id = form.elements["Id"].value;
    let login = form.elements["login"].value;
    let password = form.elements["password"].value;
    let accessLevel = form.elements["accessLevel"].value;
    if (id == 0)
        CreateUser(login, password, accessLevel);
    else
        EditUser(id, login, password, accessLevel);
});

// нажимаем на ссылку Изменить
$("body").on("click", ".editLink", function () {
    let id = $(this).data("id");
    GetUser(id);
});

// нажимаем на ссылку Удалить
$("body").on("click", ".removeLink", function () {
    let id = $(this).data("id");
    DeleteUser(id);
});