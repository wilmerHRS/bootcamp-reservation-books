$(document).ready(function () {
    const user = localStorage.getItem('user_data');

    if (!user) return window.location.href = '/Login';

    let userData = JSON.parse(user);

    // BUSCAR EVENTOS
    $("#btn-search").click(function () {
        getEventsSearch();
    })

    function getEvents() {
        $.ajax({
            url: `http://localhost:3000/api/events`,
            type: "GET",
            processData: false,
            contentType: false,
            success: function (res) {
                console.log("respuesta ", res);
                generateBodyTable(res)
            },
            error: function () {
                alert("Ocurrio un error al obtener los eventos");
            }
        })
    }

    // OBTENER EVENTOS POR BUSQUEDA
    function getEventsSearch() {
        let search = $("#input-search").val();
        search = encodeURIComponent(search);

        if (search == "") return getEvents();

        $.ajax({
            url: `http://localhost:3000/api/events/search?search=${search}`,
            type: "GET",
            processData: false,
            contentType: false,
            success: function (res) {
                console.log("respuesta search ", res);
                generateBodyTable(res)
            },
            error: function () {
                // openModalAlert("El libro no se puedo reservar");
                alert("el libro no se puede reservar");
            }
        })
    }

    function generateBodyTable(events) {
        let bodyHtml = "";

        events.forEach((e) => {
            bodyHtml += `
            <tr class="tr-book">
                <td>${e.book.code}</td>
                <td>${e.book.title}</td>
                <td>${e.user}</td>
                <td>${e.description}</td>
                <td>${convertDate(e.registered)}</td>
            </tr>
            `;
        })

        $("#table-body-event").html(bodyHtml);
    }

    function convertDate(date) {
        const dateClass = new Date(date);
        const data = `${dateClass.toLocaleDateString()} ${dateClass.toLocaleTimeString()}`;

        return data;
    }

    getEvents();
})