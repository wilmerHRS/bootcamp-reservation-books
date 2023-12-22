$(document).ready(function () {
    const data = localStorage.getItem('user_data');

    if (!data) return window.location.href = '/Login';

    let { user, accessToken } = JSON.parse(data);

    $("#span-user").text(`${user.firstName} ${user.lastName}`);

    $('#btn-logout-x').click(function () {
        localStorage.removeItem('user_data');
        return window.location.href = '/Login';
    });

    // generar enlace con token
    function angularLink() {
        const href = `http://localhost:4200/?access_token=${accessToken}`;

        $('#link-angular').attr('href', href);
    }

    angularLink();
})