var listfile = new Array();

$(document).ready(function () {
    listtable();
    $('#btncreate').click(function () { clear(); $('#maintenance').modal('show'); });
    $('#btnsave').click(function () { save(); });

    $('#frmfile').validate({
        errorElement: 'div',
        errorClass: 'error',
        rules: {
            txtname: {
                required: true
            },
            fupurl: {
                required: true
            }
        },
        messages: {
            txtname: {
                required: 'Falta ingresar el Usuario.'
            },
            fupurl: {
                required: 'Falta seleccionar el Archivo.'
            }
        }
    });
});

function listtable() {
    constructora.ajax.get('/File/GetAllFile', success);

    function success (response) {
        listfile = new Array();
        listfile = response;
        table(response);
    };
}

function table() {
    $("#tbllist tbody").empty();

    var tbody = listfile.length > 0 ? '' : '<tr class="text-center"><td colspan="4">No hay archivos.</td></tr>';

    listfile.forEach(function (element) {
        if (element.Remove == false || element.Remove == undefined) {
            tbody += '<tr>';
            tbody += '<td><div class="float-left"><button type="button" class="btn btn-outline-primary btn-sm" onclick="editbyid(' + element.id + ');"><span class="fa fa-pencil-alt"></span></button></div>';
            tbody += '<div class="float-left"><button type="button" class="btn btn-outline-danger btn-sm" onclick="deletebyid(' + element.id + ');"><span class="fa fa-trash"></span></button></div></td>';
            tbody += '<td>' + element.id + '</td>';
            tbody += '<td>' + element.name + '</td>';
            tbody += '<td><div class="float-left"><a class="btn btn-outline-primary btn-sm" href="/File/Download?Id=' + element.id + '"><span class="fa fa-download"></span></a></div></td>';
            tbody += '</tr>';
        }
    });

    $('#tbllist tbody').append(tbody);
}

function save() {
    if ($('#frmlogin').validate().form()) {
        var data = { Id: parseInt($('#txtfileid').val()), Name: $('#txtname').val(), Url: $('#fupurl')[0].files[0].name, ContentType: $('#fupurl')[0].files[0].type, Remove: false };

        constructora.ajax.post('/File/Save', data, success);

        function success(response) {
            $('#maintenance').modal('hide');
            listtable();
        };

        var formData = new FormData();
        formData.append('file', $('#fupurl')[0].files[0]);

        constructora.ajax.upload('/File/Upload', formData, null);
    } else {
        constructora.message.warningList($('#txtname-error').text() + $('#fupurl-error').text());
    }
}

function editbyid(id) {
    constructora.ajax.get('/File/GetFile?Id=' + id, success);

    function success(response) {
        clear();
        $('#maintenance').modal('show');
        $('#txtfileid').val(response.id);
        $('#txtname').val(response.name);
        $('#fupurl').hide();
    };
}

function deletebyid(id) {
    constructora.ajax.delete('/File/Delete?Id=' + id, success);

    function success(response) {
        listtable();
    };
}

function downloadbyid(id) {
    constructora.ajax.get('/File/Download?Id=' + id, null);
}

function clear() {
    $('#txtfileid').val(0);
    $('#txtname').val('');
    $('#fupurl').val('');
    $('#fupurl').show();
}