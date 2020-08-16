window.addEventListener('load', function () {
    if (!constructora.session.validate() && !window.location.href.indexOf('/Login')) {
        constructora.session.remove();
        window.location.href = "/Login";
    }
    document.querySelector('.loader').className += ' hidden';
});

function error(response) {
    console.log(response);
}

constructora = {
    message: {
        notice: function (message) {
            var messageHtml = '<p style="text-align: justify">' + message + '</p>';
            Swal.fire({
                html: messageHtml,
                imageUrl: '../../../img/constructora.png',
                imageWidth: 200,
                imageHeight: 200,
                confirmButtonText: 'Aceptar',
                showCloseButton: true
            });
        },
        correct: function (title, message) {
            Swal.fire({
                type: 'success',
                title: title,
                text: message,
                confirmButtonText: 'Aceptar',
                showCloseButton: true
            });
        },
        warning: function (message) {
            Swal.fire({
                type: 'warning',
                title: 'Advertencia',
                text: message,
                confirmButtonText: 'Aceptar',
                showCloseButton: true
            });
        },
        warningList: function (message) {
            var array = Array();
            var messageHtml = '';
            array = message.split('.');
            array.forEach(function (elementArray) {
                if (elementArray.length != 0) {
                    messageHtml += '<li class="text-left">' + elementArray + '.</li>';
                }
            });
            Swal.fire({
                type: 'warning',
                title: 'Advertencia',
                html: messageHtml,
                confirmButtonText: 'Aceptar',
                showCloseButton: true
            });
        },
        error: function (message) {
            Swal.fire({
                type: 'error',
                title: 'Error',
                text: message,
                confirmButtonText: 'Aceptar',
                showCloseButton: true
            });
        },
        errorList: function (message) {
            var array = Array();
            var messageHtml = '';
            array = message.split('.');
            array.forEach(function (elementArray) {
                if (elementArray.length != 0) {
                    messageHtml += '<li class="text-left">' + elementArray + '.</li>';
                }
            });
            Swal.fire({
                type: 'error',
                title: 'Error',
                html: messageHtml,
                confirmButtonText: 'Aceptar',
                showCloseButton: true
            });
        },
        question: function (title, message) {
            return new Promise((resolve) => {
                Swal.fire({
                    type: 'warning',
                    title: title,
                    text: message,
                    confirmButtonText: 'Aceptar',
                    cancelButtonText: 'Cancelar',
                    showCloseButton: true,
                    showCancelButton: true
                }).then((result) => {
                    resolve(result.value ? true : false);
                });
            });
        }
    },
    session: {
        register: function (request, time) {
            var expiration = new Date(new Date().getTime() + (60000 * time));
            var token = { value: request.item1, expirationDate: expiration.toISOString() };
            var type = { value: request.item2, expirationDate: expiration.toISOString() };
            sessionStorage.setItem('token', JSON.stringify(token));
            sessionStorage.setItem('type', JSON.stringify(type));
            window.location.href = "/Home";
        },
        get: function () {
            return { token: JSON.parse(sessionStorage.getItem('token')), type: JSON.parse(sessionStorage.getItem('type')) };
        },
        validate: function () {
            var token = JSON.parse(sessionStorage.getItem('token'));
            var type = JSON.parse(sessionStorage.getItem('type'));
            if (token != null && type != null) {
                if (token.expirationDate > new Date() || type.expirationDate > new Date()) {
                    return true;
                }
            }
            return false;
        },
        remove: function () {
            sessionStorage.removeItem('token');
            sessionStorage.removeItem('type');
        }
    },
    ajax: {
        get: function (url, success) {
            $.ajax({
                type: 'GET',
                url: url,
                cache: false,
                processData: false,
                headers: {
                    'Authorization': 'Bearer ' + JSON.parse(sessionStorage.getItem('token')).value
                },
                contentType: 'Application/JSON; Charset=UTF-8',
                dataType: 'JSON',
                data: null,
                success: success,
                error: error
            });
        },
        post: function (url, data, success) {
            $.ajax({
                type: 'POST',
                url: url,
                cache: false,
                processData: false,
                headers: {
                    'Authorization': 'Bearer ' + JSON.parse(sessionStorage.getItem('token')).value
                },
                contentType: 'Application/JSON; Charset=UTF-8',
                dataType: 'JSON',
                data: JSON.stringify(data),
                success: success,
                error: error
            });
        },
        put: function (url, data, success) {
            $.ajax({
                type: 'PUT',
                url: url,
                cache: false,
                processData: false,
                headers: {
                    'Authorization': 'Bearer ' + JSON.parse(sessionStorage.getItem('token')).value
                },
                contentType: 'Application/JSON; Charset=UTF-8',
                dataType: 'JSON',
                data: JSON.stringify(data),
                success: success,
                error: error
            });
        },
        delete: function (url, success) {
            $.ajax({
                type: 'DELETE',
                url: url,
                cache: false,
                processData: false,
                headers: {
                    'Authorization': 'Bearer ' + JSON.parse(sessionStorage.getItem('token')).value
                },
                contentType: 'Application/JSON; Charset=UTF-8',
                dataType: 'JSON',
                data: null,
                success: success,
                error: error
            });
        },
        upload: function (url, formData, success) {
            $.ajax({
                type: 'POST',
                url: url,
                cache: false,
                processData: false,
                headers: {
                    'Authorization': 'Bearer ' + JSON.parse(sessionStorage.getItem('token')).value
                },
                contentType: false,
                data: formData,
                success: success,
                error: error
            });
        },
        login: function (url, data) {
            $.ajax({
                type: 'POST',
                url: url,
                cache: false,
                processData: false,
                contentType: 'Application/JSON; Charset=UTF-8',
                dataType: 'JSON',
                data: JSON.stringify(data),
                success: function (response) {
                    if (response) {
                        constructora.session.register(response, 60);
                    } else {
                        Swal.fire({
                            type: 'warning',
                            title: 'Advertencia',
                            text: 'Usuario y/o Contraseña incorrecto',
                            confirmButtonText: 'Aceptar',
                            showCloseButton: true
                        });
                    }
                },
                error: error
            });
        }
    }
};