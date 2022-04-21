
$(".psw").click(function () {
    let password = (this.parentElement).children[0];
    const type = password.getAttribute("type") === "password" ? "text" : "password";
    password.setAttribute("type", type);
    this.classList.toggle("fa-eye-slash");
});

const cleanForm = () => {
    if (document.getElementById("view-form")) {
        var form = document.getElementById("view-form");
    }
    if (document.getElementById("form-create")) {
        var form = document.getElementById("form-create");
    }
    if (document.getElementById("form-create-perfil")) {
        var form = document.getElementById("form-create-perfil");
    }
    if (document.getElementById("form-usuario-estado")) {
        var form = document.getElementById("form-usuario-estado");
    }
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
        if (mes.toString().length == 1) {
            mes = "0" + mes;
        }
        var dia = fechaActual.getDate();
        if (dia.toString().length == 1) {
            dia = "0"+dia
        }
        var anio = fechaActual.getFullYear();
        fechaConvt = dia + "/" + mes + "/" + anio;
    }
    return fechaConvt;
}

const convertGenero = (genero) => {
    var generoConvert = "";
    if (genero != undefined && genero != null) {
        switch (genero) {
            case "1":
                generoConvert = "Masculino"
                break;
            case "2":
                generoConvert = "Femenino"
                break;
            case "3":
                generoConvert = "Sin Especificar"
                break;
            default:
                generoConvert = "#####"
                break;
        }

        return generoConvert;
        
    }
}

const convertNacionalidad = (nacionalidad) => {
    var nacionalidadConvert = "";
    if (nacionalidad != undefined && nacionalidad != null) {
        switch (nacionalidad) {
            case "1":
                nacionalidadConvert = "Peruano"
                break;
            case "2":
                nacionalidadConvert = "Extranjero"
                break;
            default:
                nacionalidadConvert = "#####"
                break;
        }

        return nacionalidadConvert;

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
                `
            }

            break;

        case 'mantUsP':
            buttonJson = {
                data: null,
                defaultContent: `
                <div class="w-100">
                <button type='button' onclick='btnAction(this,"edit");'  class='btn btn-sm btn-warning' style='padding:2px 4px;'>
                <i class='bx bx-edit'></i>
                </button>
                <button type='button' onclick='btnAction(this,"est_1");'  class='btn btn-sm btn-success btnEstado' style='padding:2px 4px;'>
                <i class="bx bx-check"></i>
                </button>
                <button type='button' onclick='btnAction(this,"est_2");'  class='btn btn-sm btn-danger btnEstado' style='padding:2px 4px;'>
                <i class="bx bx-x"></i>
                </button>
                <button type='button' onclick='btnAction(this,"est_3");'  class='btn btn-sm btn-secondary btnEstado' style='padding:2px 4px;'>
                <i class="bx bx-alarm-exclamation"></i>
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


const setValData = () => {
    let valores = {
        formData: {},
        formEstado:true,
    };
    $('.val').each(function (e) {
        if (this.id != "id") {
            if (this.value.trim().length > 0) {
                valores.formData[this.name] = this.value;
            } else {
                valores.formEstado = false;
                this.classList.add("border-danger");
            }
        } else {
            valores.formData[this.name] = this.value;
        }
    });
    return valores;
}

const mostrarTabla = () => {
    $("#view-form").hide(500);
    $("#view-table").show(1000);
}

const mostrarFormulario = () => {
    $("#view-table").hide(500);
    $("#view-form").show(1000);
   
}




const FechaDate = (fecha) => {
    var fechaConvt = "";
    if (fecha != undefined && fecha != null) {
        var fechaString = fecha.substr(6);
        var fechaActual = new Date(parseInt(fechaString));
        var mes = fechaActual.getMonth() + 1;
        if (mes.toString().length == 1) {
            mes = "0" + mes;
        }
        var dia = fechaActual.getDate();
        if (dia.toString().length == 1) {
            dia = "0" + dia
        }
        var anio = fechaActual.getFullYear();
        fechaConvt = anio + "-" + mes + "-" + dia  ;
    }
    return fechaConvt;
}