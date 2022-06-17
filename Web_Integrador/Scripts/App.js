
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
    if (document.getElementById("form-create-editpass")) {
        var form = document.getElementById("form-create-editpass");
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

const convertFechav2 = (fecha) => {
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
        fechaConvt = anio + "-" + mes + "-" + dia;
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
        case 'mantVistSalida':
            buttonJson = {
                data: null,
                defaultContent: `
                <div class="w-100">
                <button type='button' onclick='btnAction(this,"regvistsalid");'  class='btn btn-sm btn-danger btnEstado' style='padding:2px 4px;'>
                <i class="bx bx-x"></i>
                </button>
                </div>
                `
            }
            break;
        case 'mantVist':
            buttonJson = {
                data: null,
                defaultContent: `
                <div class="w-100">
                <button type='button' onclick='btnAction(this,"edit");' class='btn btn-sm btn-warning' style='padding:2px 4px;'>
                <i class='bx bx-edit'></i>
                </button>
                <button type='button' onclick='btnAction(this,"modalhistorial");' data-bs-toggle="modal" data-bs-target="#exampleModal"  class='btn btn-sm btn-info btnEstado' style='padding:2px 4px;'>
                <i class="bx bx-list-ul"></i>
                </button>
                </div>
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

const setValDataLab = () => {
    let valores = {
        formData: {},
        formEstado: true,
    };
    $('.val').each(function (e) {
        if (this.id != "id") {
            if (this.value.trim().length > 0) {
                valores.formData[this.name] = this.value;
            } else {
                valores.formEstado = false;
                this.classList.add("border-danger");
                (this.parentElement).lastElementChild.classList.remove("d-none");
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

const GetStatusFecha = (fecha) => {
    var fechaConvt = "";
    if (fecha != undefined && fecha != null) {
        var fechaString = fecha.substr(6);
        if (parseInt(fechaString) < 0) {
            fechaConvt = "---";
        }
        else {
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
            var hora = fechaActual.getHours() + ":" + fechaActual.getMinutes() + ":" + (fechaActual.getSeconds().toString().length != 1 ? fechaActual.getSeconds().toString() : "0" + fechaActual.getSeconds().toString());

            fechaConvt = anio + "/" + mes + "/" + dia;
        }

    }
    return fechaConvt;
}


const GetStatus = (fecha) => {
    var fechaConvt = "";
    if (fecha != undefined && fecha != null) {
        var fechaString = fecha.substr(6);
        if (parseInt(fechaString) < 0) {
            fechaConvt = "No Salio";
        }
        else {
            var fechaActual = new Date(parseInt(fechaString));
            let hourD = fechaActual.getHours();
            if (hourD.toString().length == 1) {
                hourD = "0" + hourD;
            }
            let minD = fechaActual.getMinutes();
            if (minD.toString().length == 1) {
                minD = "0" + minD
            }
            let secD = fechaActual.getSeconds();
            if (secD.toString().length == 1) {
                secD = "0" + secD
            }
   
            var hora = hourD + ":" + minD + ":" + secD;

            fechaConvt = hora;
        }

    }
    return fechaConvt;
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
      

        fechaConvt = anio + "-" + mes + "-" + dia;
    }
    return fechaConvt;
}




const soloLetras = (e) => {
    let key = e.keyCode || e.which;
    let tecla = String.fromCharCode(key).toString();
    let letras = "ABCDEFGHIJKLMNÑOPQRSTUVWYXabcdefghijklmnñopqrstuvwxyzáéíóú";
    let especiales = [8, 13, 32,164,165];
    let tecla_especial = false;
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}





function soloNumeros(e) {
    var key = e.charCode;
    return (key >= 48 && key <= 57 || key == 8 || key == 13 || key == 32);
}


function alphaNumero(e) {
    let key = e.keyCode || e.which;
    let tecla = String.fromCharCode(key).toString();
    var especiales = [8, 13, 32, 45, 46, 95,164,165]
    let tecla_especial = false;
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }
    let letras = "ABCDEFGHIJKLMNÑOPQRSTUVWYXabcdefghijklmnñopqrstuvwxyzáéíóú";
    return (letras.indexOf(tecla)!=-1 || key >= 48 && key <= 57 || tecla_especial);
}