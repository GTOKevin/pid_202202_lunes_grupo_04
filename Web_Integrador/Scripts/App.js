
$(".psw").click(function () {
    let password = (this.parentElement).children[0];
    const type = password.getAttribute("type") === "password" ? "text" : "password";
    password.setAttribute("type", type);
    this.classList.toggle("fa-eye-slash");
});

const cleanForm = () => {
    let form = document.getElementById("view-form");
    form.reset();
}

const showLoading = () => {
    Swal.fire({
        title: 'Cargando !!',
        html: 'Espere por favor...',
        showConfirmButton: false,
        allowOutsideClick: false,
        willOpen: () => {
            Swal.showLoading();
        }
    });
}


const setColumns=(dt, cols, btn)=> {
    let t = "";
    t += "<thead>";
    for (var i = 0; i < cols.length; i++) {

        t += "<th style='" + (i == 0 ? "display:none" : "") + "'>";
        t += cols[i];
        t += "</th>";
    }
    if (btn) {
        t += "<th></th>";
    }
    t += "</thead><tbody></tbody>";
    $("#" + dt).html(t);
}

const convertFecha = (fecha) => {
    var fechaConvt = "";
    if (fecha != undefined && fecha != null) {
        var fechaString = fecha.substr(6);
        var fechaActual = new Date(parseInt(fechaString));
        var mes = fechaActual.getMonth() + 1;
        var dia = fechaActual.getDate();
        var anio = fechaActual.getFullYear();
        fechaConvt = dia + "/" + mes + "/" + anio;
    }



    return fechaConvt;
}

const buttonsDatatTable = (opcion) => {
    let buttonJson = {};

    switch (opcion) {
        case 'edit':
            buttonJson = {
                data: null,
                defaultContent:`
                <div class="w-100">
                <button type='button' onclick='btnAction(this,"edit");' class='btn btn-sm btn-warning' style='padding:2px 4px;'>
                <i class='bx bx-edit'></i>
                </button>
                </div>`

            }

         break;

        case 'delete':

            break;

        case 'edit-delete':

            break;
    }

    return buttonJson;
}
