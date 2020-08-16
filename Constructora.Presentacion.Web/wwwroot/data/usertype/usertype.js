var listusertype = new Array();

$(document).ready(function () {
    listtable();
    $('#btncreate').click(function () { clear(); $('#maintenance').modal('show'); });
    $('#btnsave').click(function () { save(); });
});

window.onload = function () {
    if (JSON.parse(sessionStorage.getItem('token')) == null) {
        window.location.href = "/Login";
    }
}

function listtable() {
    $.ajax({
        type: 'GET',
        url: '/UserType/GetAllUserType',
        cache: false,
        processData: false,
        headers: {
            'Authorization': 'Bearer ' + JSON.parse(sessionStorage.getItem('token'))
        },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: null,
        success: function (response) {
            listusertype = new Array();
            listusertype = response;
            table(response);
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function table() {
    $("#tbllist tbody").empty();

    var tbody = listusertype.length > 0 ? '' : '<tr class="text-center"><td colspan="3">No hay tipos de usuarios.</td></tr>';

    listusertype.forEach(function (element) {
        if (element.Remove == false || element.Remove == undefined) {
            tbody += '<tr>';
            tbody += '<td><div class="float-left"><button type="button" class="btn btn-outline-primary btn-sm" onclick="editbyid(' + element.id + ');"><span class="fa fa-pencil-alt"></span></button></div>';
            tbody += '<div class="float-left"><button type="button" class="btn btn-outline-danger btn-sm" onclick="deletebyid(' + element.id + ');"><span class="fa fa-trash"></span></button></div></td>';
            tbody += '<td>' + element.id + '</td>';
            tbody += '<td>' + element.name + '</td>';
            tbody += '</tr>';
        }
    });

    $('#tbllist tbody').append(tbody);
}

function save() {
    var userType = { Id: parseInt($('#txtusertypeid').val()), Name: $('#txtname').val(), Remove: false };

    $.ajax({
        type: 'POST',
        url: '/UserType/Save',
        cache: false,
        processData: false,
        headers: {
            'Authorization': 'Bearer ' + JSON.parse(sessionStorage.getItem('token'))
        },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(userType),
        success: function (response) {
            $('#maintenance').modal('hide');
            listtable();
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function editbyid(id) {
    $.ajax({
        type: 'GET',
        url: '/UserType/GetUserType?Id=' + id,
        cache: false,
        processData: false,
        headers: {
            'Authorization': 'Bearer ' + JSON.parse(sessionStorage.getItem('token'))
        },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: null,
        success: function (response) {
            clear();
            $('#maintenance').modal('show');
            $('#txtusertypeid').val(response.id);
            $('#txtname').val(response.name);
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function deletebyid(id) {
    $.ajax({
        type: 'DELETE',
        url: '/UserType/Delete?Id=' + id,
        cache: false,
        processData: false,
        headers: {
            'Authorization': 'Bearer ' + JSON.parse(sessionStorage.getItem('token'))
        },
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: null,
        success: function (response) {
            listtable();
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function clear() {
    $('#txtusertypeid').val(0);
    $('#txtname').val('');
}