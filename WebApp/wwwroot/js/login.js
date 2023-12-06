$(document).ready(function () {
    $('#form-login').validate({
        rules: {
            VarEmail: {
                required: true,
                minlength: 8
            },
            VarPassword: {
                required: true,
                minlength: 6
            }
        },
        messages: {
            VarEmail: {
                required: "Por favor, ingresa tu nombre de usuario.",
                email: "Ingresa un correo electrónico válido.",
                minlength: "El nombre de usuario debe tener al menos 8 caracteres."
            },
            VarPassword: {
                required: "Por favor, ingresa tu contraseña.",
                minlength: "La contraseña debe tener al menos 6 caracteres."
            }
        },
        errorElement: "span",
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");
            element.closest(".form-group").append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass("is-invalid").removeClass("is-valid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass("is-invalid").addClass("is-valid");
        },
        submitHandler: function (form) {
            const body = new FormData(form);
            authentication(body);

            return false;
        }
    });
});

// autenticacion
function authentication(body) {
    $.ajax({
        url: `Login/Authenticate`,
        type: "POST",
        processData: false,
        contentType: false,
        data: body,
        success: function (res) {
            console.log(res);
            localStorage.setItem('user_data', JSON.stringify(res));
            window.location.href = '/Book';
        },
        error: function () {
            alert("Crendenciales incorrectas");
        }
    })
}