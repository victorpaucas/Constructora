$(document).ready(function () {
    $('#btnlogout').click(function () { logout(); });
});

function logout() {
    sessionStorage.removeItem('token');
    window.location.href = "/Login";
}