
var colsName = ['ID', 'NOMBRE', 'APELLIDO', 'TIPO DOCUMENTO','NUMERO DOCUMENTO','NOMBRE DE GENERO', 'FECHA'];
var visitanteList = [];

const llenarVariable = (lista, option) => {
    let newArray;
    switch (option) {
        case 'new':
            for (i = 0; i < lista.length; i++) {
                visitanteList.push(lista[i]);
            }
            break;
        case 'edit':
            console.log("edit");
            newArray = visitanteList.map(visitante => visitante.id_visitante == lista[0].id_visitante ? lista[0] : visitante)

            visitanteList = newArray;
            break;

    }
}

const formulario = document.getElementById("view-form")
const inputs = document.querySelectorAll("#inputs input")
const selects = document.querySelectorAll("#inputs select")


const expresiones = {
    nombre: /^([A-Za-zñáéíóúÁÉÍÓÚ]\s?){1,50}$/,
    apellidos: /^([A-Za-zñáéíóúÁÉÍÓÚ]\s?){1,50}$/,
    nro_documento: /^[0-9]{8}$/,
    nro_documento_v2: /^[0-9]{11}$/

}

var AuxData = 0;
var DataExpresion;

const validarFormulario = (e) => {
    switch (e.target.name) {
        case "nombre":
            validarCampos(expresiones.nombre, e.target, 'inpNombre');
            break;
        case "apellidos":
            validarCampos(expresiones.apellidos, e.target, 'inpApellido');
            break;
        case "tipo_documento":
            validarCombos(e.target, 'inpTipoDocumento');
            AuxData = e.target.value;
            break;
        case "genero":
            validarCombos(e.target, 'inpGenero');
            break;
    }
}

const campos = {
    inpNombre: false,
    inpApellido: false,
    inpDocumento: false,
    inpTipoDocumento: false,
    inpGenero: false,
}

const validarCombos = (input, campo) => {
    if (input.value != "") {
        document.querySelector(`#${campo} select`).classList.remove('border-danger')
        document.querySelector(`#${campo} p`).classList.add('d-none');
        campos[campo] = true;
    }
    else {
        document.querySelector(`#${campo} select`).classList.add('border-danger');
        document.querySelector(`#${campo} p`).classList.remove('d-none');
        campos[campo] = false;
    }
}
const validarCampos = (expresion, input, campo) => {
    if (expresion.test(input.value)) {
        document.querySelector(`#${campo} input`).classList.remove('border-danger');
        document.querySelector(`#${campo} p`).classList.add('d-none');
        campos[campo] = true;
    }
    else {
        document.querySelector(`#${campo} input`).classList.add('border-danger');
        document.querySelector(`#${campo} p`).classList.remove('d-none');
        campos[campo] = false;
    }
}

const ValidComboTipoDocumento = (e) => {
    if (e.target.name == "tipo_documento") {
        const dniinfo = document.getElementById('dniID');
        if (e.target.value == 4) {
            validarCampos(expresiones.nro_documento, dniinfo, "inpDocumento");
            dniinfo.addEventListener('keyup', function () {
                validarCampos(expresiones.nro_documento, dniinfo, "inpDocumento");
            }, false)
        }
        else {
            validarCampos(expresiones.nro_documento_v2, dniinfo, "inpDocumento");
            dniinfo.addEventListener('keyup', function () {
                validarCampos(expresiones.nro_documento_v2, dniinfo, "inpDocumento");
            }, false)
        }
    }
}

inputs.forEach((input) => {
    input.addEventListener('keyup', validarFormulario);
    input.addEventListener('blur', validarFormulario);
    
})

selects.forEach((select) => {
    select.addEventListener('change', ValidComboTipoDocumento);
    select.addEventListener('change', validarFormulario);
})

const init = () => {
    $("#id_sucursal").on("change", function (e) {
        getSector(this.value);
    });
    $("#id_sector").on("change", function (e) {
        getTorre(this.value);
    });
    $("#id_torre").on("change", function (e) {
        getDepartamento(this.value);
    });

    showLoading();
    setColumns("example", colsName, true);

    getListaVisitante();
    Swal.close();
};

const btnAction = (t, tipo) => {
    switch (tipo) {
        case 'new':
            cleanForm();
            $("#view-table").hide(500);
            $("#view-form").show(1000);
            break;
        case 'cancel':
            $("#view-form").hide(500);
            $("#view-table").show(1000);
            break;
        case 'cancel-registrar':
            $("#view-form-registrar").hide(500);
            $("#view-table").show(1000);
            break;
        case 'regvist':
            $("#view-table").hide(500);
            $("#view-form-registrar").show(1000);
            let id2 = ((t.parentElement).parentElement).parentElement.id;
            getVisitanteId(id2)
            break;
        case 'edit':
            $("#view-table").hide(500);
            $("#view-form").show(1000);
            let id = ((t.parentElement).parentElement).parentElement.id;
            getVisitanteId(id)
            break;

    }
};

const getListaVisitante = () => {
    $.ajax({
        method: "GET",
        url: urlGetVisitante,
        responseType: 'json',
        success: async function (res) {
            
            Swal.close();
            if (res.oHeader.estado) {
                await llenarVariable(res.VisitanteList, 'new');
                await listTable(visitanteList);

            } else {
                Swal.fire('Ooops!', res.oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            Swal.close();
        }

    });
}

const listTable = (res) => {
    $('#example').DataTable({
        destroy: true,
        data: res,
        columns: [{ data: "id_visitante" },
            { data: "nombre" },
            { data: "apellidos" },
            { data: "nombre_tipo" },
            { data: "nro_documento" },
            { data: "nombre_genero" },
        { data: "fecha_creacion", render: function (data) { return convertFecha(data) } },
            buttonsDatatTable("mantVist")],
        rowId: "id_visitante",
        columnDefs:
            [
                {
                    "targets": 0,
                    "visible": false,
                }
            ],
        order: [[0, 'des']],
        language: {
            "url": "//cdn.datatables.net/plug-ins/1.10.16/i18n/Spanish.json"
        }
    });
};

$("#view-form").on("submit", function (e) {
    let id_visitante = document.getElementsByName("id_visitante")[0];

    if (id_visitante.value == "" || id_visitante.value == undefined) {
        id_visitante.value = 0;
    }

    e.preventDefault();

    if (campos.inpNombre && campos.inpApellido
        && campos.inpDocumento && campos.inpTipoDocumento && campos.inpGenero) {
        let formData = {};
        let validate = true;
        $("#view-form .cf").each(function (index) {
            if (this.value.trim().length != 0) {
                formData[this.name] = this.value;
            } else {
                validate = false;
            }
        });

        if (validate) {
            showLoading();

            $.ajax({
                method: "POST",
                url: urlSaveVisitante,
                data: formData,
                responseType: 'json',
                success: async function (res) {
                    console.log(res);
                    Swal.close();
                    let { VisitanteList, oHeader } = res;
                    if (oHeader.estado) {
                        if (id_visitante.value == "0") {
                            await llenarVariable(VisitanteList, 'new');
                        } else {
                            await llenarVariable(VisitanteList, 'edit');
                        }
                        
                        await listTable(visitanteList);
                        Swal.fire('ok', oHeader.mensaje, 'success');
                    }

                },
                error: function (err) {
                    Swal.close();
                }
            });

        }
        AuxData = 0;
        $("#view-form").hide(500);
        $("#view-table").show(1000);
    }
    

});

const getVisitanteId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetVisitante + "?id_visitante=" + id,
        responseType: 'json',
        success: async function (res) {
            let {VisitanteList, oHeader } = res;
            if (oHeader.estado) {
                await llenarCampos(VisitanteList);
            } else {
                Swal.fire('Ooops!', res.oHeader.mensaje, 'error');
            }
            
        },
        error: function (err) {
            Swal.close();
        }

    });
}


const llenarCampos = async (list) => {
    let idSucursal = "";
    let idSector = "";
    let idTorre = "";
    let idDepartamento = "";
    let sector = document.getElementsByName("id_sector")[0];
    let torre = document.getElementsByName("id_torre")[0];
    let departamento = document.getElementsByName("id_departamento")[0];
    if (list.length > 0) {
        $(".val").each(function (ind) {
            for (var propName in list[0]) {
                if (this.name === propName) {
                    this.value = list[0][propName];
                }
                if (propName === "id_sucursal") {
                    idSucursal = list[0][propName];
                } else if (propName === "id_sector") {
                    idSector = list[0][propName];
                } else if (propName === "id_torre") {
                    idTorre = list[0][propName];
                } else if (propName === "id_departamento") {
                    idDepartamento = list[0][propName];
                }
            }
        });
        $("#view-form input").each(function (ind) {
            for (var propName in list[0]) {
                if (this.name === propName) {
                    this.value = list[0][propName];
                }
            }

        });
        $("#view-form-registrar input").each(function (ind) {
            for (var propName in list[0]) {
                if (this.name === propName) {
                    this.value = list[0][propName];
                }
            }

        });
    }

    await getSector(idSucursal);
    sector.value = idSector;
    await getTorre(idSector);
    torre.value = idTorre;
    await getDepartamento(idTorre)
    departamento.value = idDepartamento;

}



const cleanSelect = () => {
    let selSector = document.getElementsByName("id_sector")[0];
    selSector.innerHTML = "";
    let str = ``;
    str += `<option value="">...Seleccione...</option>`
    selSector.innerHTML = str;
    selSector.value = "";
}

const getSector = async (id) => {
    await $.ajax({
        method: "GET",
        url: urlGetSector + "?id_sucursal=" + id,
        responseType: 'json',
        success: async function (res) {
            let { SectorList, oHeader } = res;
            if (oHeader.estado) {
                await getSelectSector(SectorList);
            } else {
                Swal.fire('Ooops!', res.oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            Swal.close();
        }

    });
}

const getTorre = async (id) => {
    await $.ajax({
        method: "GET",
        url: urlGetTorre + "?id_sector=" + id,
        responseType: 'json',
        success: async function (res) {
            let { TorreList, oHeader } = res;
            if (oHeader.estado) {
                await getSelectTorre(TorreList);
            } else {
                Swal.fire('Ooops!', res.oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            Swal.close();
        }

    });
}

const getDepartamento = async (id) => {
    await $.ajax({
        method: "GET",
        url: urlGetDepartamento + "?id_torre=" + id,
        responseType: 'json',
        success: async function (res) {
            console.log(res);
            let { lista_Departamento, oHeader } = res;
            if (oHeader.estado) {
                await getSelectDepartamento(lista_Departamento);
            } else {
                Swal.fire('Ooops!', res.oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            Swal.close();
        }

    });
}

const getSelectSector = (list) => {

    let selSector = document.getElementsByName("id_sector")[0];
    selSector.innerHTML = "";
    let str = ``;
    str += `<option value="">...Seleccione...</option>`
    if (list.length > 0) {
        for (i = 0; i < list.length; i++) {
            str += `<option value="${list[i].id_sector}">${list[i].nombre_sector}</option>`;
        }
    }
    selSector.innerHTML = str;
    selSector.value = "";

}

const getSelectTorre = (list) => {

    let selTorre = document.getElementsByName("id_torre")[0];
    selTorre.innerHTML = "";
    let str = ``;
    str += `<option value="">...Seleccione...</option>`
    if (list.length > 0) {
        for (i = 0; i < list.length; i++) {
            str += `<option value="${list[i].id_torre}">Numero : ${list[i].numero}</option>`;
        }
    }
    selTorre.innerHTML = str;
    selTorre.value = "";

}

const getSelectDepartamento = (list) => {

    let selDepartamento = document.getElementsByName("id_departamento")[0];
    selDepartamento.innerHTML = "";
    let str = ``;
    str += `<option value="">...Seleccione...</option>`
    if (list.length > 0) {
        for (i = 0; i < list.length; i++) {
            str += `<option value="${list[i].id_departamento}">${list[i].numero}</option>`;
        }
    }

    selDepartamento.innerHTML = str;
    selDepartamento.value = "";

}



$("#view-form-registrar").on("submit", function (e) {
    e.preventDefault();
    let formData = {};
    let validate = true;

    $("#view-form-registrar input").each(function (index) {
        if (this.value.trim().length != 0) {
            formData[this.name] = this.value;
        } else {
            validate = false;
        }

    });
    $("#view-form-registrar select").each(function (index) {
        if (this.value.trim().length != 0) {
            formData[this.name] = this.value;
        } else {
            validate = false;
        }

    });
    if (validate) {
        showLoading();


        $.ajax({
            method: "POST",
            url: urlSaveVisitaRegistro,
            data: formData,
            responseType: 'json',
            success: async function (res) {
                console.log(res);
                Swal.close();
                let { VisitanteList, oHeader } = res;
                if (oHeader.estado) {
                    await listTable(VisitanteList);
                    Swal.fire('ok', oHeader.mensaje, 'success');
                    $("#view-form-registrar").hide(500);
                    $("#view-table").show(1000);
                }

            },
            error: function (err) {
                Swal.close();
            }
        });

    }

});


init();



