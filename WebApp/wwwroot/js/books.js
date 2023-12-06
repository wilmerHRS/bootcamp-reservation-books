$(document).ready(function () {
    let idBook, isReservation;
    const user = localStorage.getItem('user_data');

    if (!user) return window.location.href = '/Login';

    let userData = JSON.parse(user);

    // BUSCAR LIBROS
    $("#btn-search").click(function () {
        getBooks();
    })

    $("#table-book").on("click", "#table-body-book tr", function () {
        // obtener id del th
        idBook = $(this).find(".th-book").text();
        isReservation = $(this).find(".td-book").text() == "RESERVADO" ? true : false;

        if (!isReservation) {
            openModalConfirmation(); // Mostrar modal confirmation
        }
        else {
            createEvent();
            openModalAlert(); // Mostrar modal alert
        }
    })

    // CREATE EVENT
    function createEvent(title = "Intento reservar") {
        if (idBook === undefined || isReservation === undefined) return;

        const { user } = userData;

        const body = {
            idUser: user.idUser,
            user: `${user.varFirstName} ${user.varLastName}`,
            book: idBook,
            description: title,
            registered: new Date()
        }

        $.ajax({
            url: `http://localhost:3000/api/events`,
            type: "POST",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(body),
            processData: false,
            success: function (res) {
                console.log("respuesta ", res);
            },
            error: function () {
                // openModalAlert("El libro no se puedo reservar");
            }
        })
    }

    // OBTENER LIBROS
    function getBooks() {
        let search = $("#input-search").val();
        search = encodeURIComponent(search);
        //$("#table-body-book").load(`/Book/_TableBodyPartial?search=${search}`);

        $.ajax({
            url: `/Book/_TableBodyPartial?search=${search}`,
            type: "GET",
            headers: {
                'Authorization': `Bearer ${userData.accessToken}`
            },
            success: function (res) {
                $("#table-body-book").html(res);
            },
            error: function (xhr, status, err) {

                if (xhr.status === 401) {
                    window.location.href = '/Login")';
                    return;
                }

                alert("Error al cargar los datos");
            }
        })
    }

    // Confirmacion Modal LIBROS
    $("#btn-modal-confirmation").on("click", function () {
        const { user } = userData;

        $.ajax({
            url: `CreateReservation?idBook=${idBook}&idUser=${user.idUser}`,
            type: "POST",
            headers: {
                'Authorization': `Bearer ${userData.accessToken}`
            },
            processData: false,
            contentType: false,
            success: function (res) {
                closeModalConfirmation();

                if (res != null) {
                    createEvent("Realizo reserva");
                    getBooks();
                } else {
                    createEvent();
                    openModalAlert("El libro no se puedo reservar");
                }
            },
            error: function () {
                createEvent();
                closeModalConfirmation();
                openModalAlert("El libro no se puedo reservar");
            }
        })
    });

    // Cancelacion Modal LIBROS
    $("#btn-modal-cancelation").on("click", function () {
        console.log("LIBRO CANCELADO");
        createEvent("Cancelo reserva");
        closeModalConfirmation(); // Ocultar modal
    });

    // Cancelacion Modal ALERT
    $("#btn-modal-alert").on("click", function () {
        console.log("CANCELADO");
        closeModalAlert(); // Ocultar modal
    });

    getBooks();
})

// MODALES
function openModalConfirmation() {
    $("#modal-confirmation").fadeIn();
}

function closeModalConfirmation() {
    $("#modal-confirmation").fadeOut();
}

function openModalAlert(title = "¡Este libro ya esta reservado!") {
    $("#modal-alert").fadeIn();
    $("#title-modal-alert").text(title);
}

function closeModalAlert() {
    $("#modal-alert").fadeOut();
}
// MODALES