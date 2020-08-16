$(document).ready(function () {
    $('#btnlogin').click(function () { login() });

    $('#frmlogin').validate({
        errorElement: 'div',
        errorClass: 'error',
        rules: {
            txtname: {
                required: true
            },
            txtpassword: {
                required: true
            }
        },
        messages: {
            txtname: {
                required: 'Falta ingresar el Usuario.'
            },
            txtpassword: {
                required: 'Falta ingresar la Contraseña.'
            }
        }
    });
});

function login() {
    if ($('#frmlogin').validate().form()) {
        constructora.ajax.login('/Login/Validate', { Name: $('#txtname').val(), Password: $('#txtpassword').val() });
    } else {
        constructora.message.warningList($('#txtname-error').text() + $('#txtpassword-error').text());
    }
}