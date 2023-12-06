$(document).ready(function () {
    const user = localStorage.getItem('user_data');

    if (!user) return window.location.href = '/Login';

    let userData = JSON.parse(user);

    $("#span-user").text(`${userData.user.varFirstName} ${userData.user.varLastName}`);

    $('#btn-logout-x').click(function () {
        localStorage.removeItem('user_data');
        return window.location.href = '/Login';
    });
})