var listuser = new Array();
var listusertype = new Array();

$(document).ready(function () {
    listtable();
    $('#btncreate').click(function () { clear(); listcombo(0); $('#maintenance').modal('show'); });
    $('#btnsave').click(function () { save(); });

    $('#frmuser').validate({
        errorElement: 'div',
        errorClass: 'error',
        rules: {
            cbousertype: {
                min: 1
            },
            txtname: {
                required: true
            },
            txtpassword: {
                required: true
            }
        },
        messages: {
            cbousertype: {
                min: 'Falta seleccionar un Tipo de Usuario válido.'
            },
            txtname: {
                required: 'Falta ingresar el Nombre.'
            },
            txtpassword: {
                required: 'Falta ingresar la Contraseña.'
            }
        }
    });
});

function listtable() {
    constructora.ajax.get('/User/GetAllUser', success);

    function success(response) {
        listuser = new Array();
        listuser = response;
        table(response);
    };
}

function table() {
    $("#tbllist tbody").empty();

    var tbody = listuser.length > 0 ? '' : '<tr class="text-center"><td colspan="4">No hay usuarios.</td></tr>';

    listuser.forEach(function (element) {
        if (element.Remove == false || element.Remove == undefined) {
            tbody += '<tr>';
            tbody += '<td><div class="float-left"><button type="button" class="btn btn-outline-primary btn-sm" onclick="editbyid(' + element.id + ');"><span class="fa fa-pencil-alt"></span></button></div>';
            tbody += '<div class="float-left"><button type="button" class="btn btn-outline-danger btn-sm" onclick="deletebyid(' + element.id + ');"><span class="fa fa-trash"></span></button></div></td>';
            tbody += '<td>' + element.id + '</td>';
            tbody += '<td>' + element.userType.name + '</td>';
            tbody += '<td>' + element.name + '</td>';
            tbody += '</tr>';
        }
    });

    $('#tbllist tbody').append(tbody);
}

function save() {
    if ($('#frmuser').validate().form()) {
        var data = { Id: parseInt($('#txtuserid').val()), UserTypeId: parseInt($('#cbousertype').val()), Name: $('#txtname').val(), Password: $('#txtpassword').val(), Remove: false, UserType: null };

        constructora.ajax.post('/User/Save', data, success);

        function success(response) {
            $('#maintenance').modal('hide');
            listtable();
        };
    } else {
        constructora.message.warningList($('#cbousertype-error').text() + $('#txtname-error').text() + $('#txtpassword-error').text());
    }    
}

function editbyid(id) {
    constructora.ajax.get('/User/GetUser?Id=' + id, success);

    function success(response) {
        clear();
        listcombo(response.userTypeId);
        $('#maintenance').modal('show');
        $('#txtuserid').val(response.id);
        $('#txtname').val(response.name);
    };
}

function deletebyid(id) {
    constructora.ajax.delete('/User/Delete?Id=' + id, success);

    function success(response) {
        listtable();
    };
}

function listcombo(option) {
    constructora.ajax.get('/UserType/GetAllUserType', success);

    function success(response) {
        listusertype = new Array();
        listusertype = response;
        combo(option);
    };
}

function combo(option) {
    $('#cbousertype').empty();
    $('#cbousertype').append(new Option('Seleccione', 0));
    listusertype.forEach(function (element) {
        $('#cbousertype').append(new Option(element.name, element.id));
    });
    $('#cbousertype').val(option);
}

function clear() {
    $('#txtuserid').val(0);
    $('#cbousertype').val(0);
    $('#txtname').val('');
    $('#txtpassword').val('');
}