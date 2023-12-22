$(document).ready(function () {
    let idBook, dtimeReservation;
    const user = localStorage.getItem('user_data');

    if (!user) return window.location.href = '/Login';

    let userData = JSON.parse(user);

    // BUSCAR LIBROS
    $("#btn-search").click(function () {
        getBooks();
    })

    $("#table-book").on("click", "#table-body-book tr", function () {

        idBook = $(this).find(".txt-idbook").val();
        dtimeReservation = $(this).find(".dtime-reservation").val();
        let available = $(this).find(".check-available").prop("checked");
        let reservedMe = $(this).find(".check-reserved-me").prop("checked");
        let waitReservedMe = $(this).find(".check-wait-reserved-me").prop("checked");
        let title = $(this).find(".td-title").text();

        //console.log({ idBook, dtimeReservation, available, reservedMe, waitReservedMe, title });

        if (reservedMe) {
            return;
        } else if (waitReservedMe) {
            openModalAlert("Usted se encuentra en la lista de espera, por favor espere su liberación");
            return;
        } else if (available && !dtimeReservation) {
            openModalConfirmation();
        } else if (available) {
            createReservation();
        } else {
            createEvent();
            openModalAlert(`No es posible reservar el libro "${title}""`); // Mostrar modal alert
        }
    })

    // CREATE EVENT
    function createEvent(title = "Intento de reserva") {
        if (idBook === undefined || dtimeReservation === undefined) return;

        const { user } = userData;

        const body = {
            idUser: user.idUser,
            user: `${user.firstName} ${user.lastName}`,
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
                    window.location.href = "/Login";
                    return;
                }

                alert("Error al cargar los datos");
            }
        })
    }

    // CREAR RESERVACION
    function createReservation() {
        //const { user } = userData;

        console.log("entro");

        $.ajax({
            url: `/Book/CreateReservation?idBook=${idBook}`,
            type: "POST",
            headers: {
                'Authorization': `Bearer ${userData.accessToken}`
            },
            processData: false,
            contentType: false,
            success: function (res) {
                getBooks();

                if (res.message === "reservado") {
                    createEvent("Realizo reserva");
                } else {
                    openModalAlert(`Usted se encuentra en la lista de espera, ${res.message}`);
                    createEvent(`Lista de espera ${res.message}`);
                }
            },
            error: function () {
                createEvent();
                openModalAlert("El libro no se puedo reservar");
            }
        })
    }

    // Confirmacion Modal LIBROS
    $("#btn-modal-confirmation").on("click", function () {
        closeModalConfirmation();
        createReservation();
    });

    // Cancelacion Modal LIBROS
    $("#btn-modal-cancelation").on("click", function () {
        //console.log("LIBRO CANCELADO");
        createEvent("Cancelo reserva");
        closeModalConfirmation(); // Ocultar modal
    });

    // Cancelacion Modal ALERT
    $("#btn-modal-alert").on("click", function () {
        //console.log("CANCELADO");
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