window.onload = function () {
    if (JSON.parse(sessionStorage.getItem('token')) == null) {
        window.location.href = "/Login";
    }
}