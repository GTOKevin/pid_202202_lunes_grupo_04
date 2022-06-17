
var colsName = ['ID', 'NOMBRE', 'Nro.DOCUMENTO', 'GENERO', 'FECHA REGISTRO'];
var colsVisitante = ['ID', 'FECHA INGRESO','', 'FECHA SALIDA', '','' ,''];

var visitanteList = [];
var historialVisitante = [];

const llenarVariable = (lista, option) => {
    let newArray;
    switch (option) {
        case 'new':
            for (i = 0; i < lista.length; i++) {
                visitanteList.push(lista[i]);
            }
            break;
        case 'historial':
            for (i = 0; i < lista.length; i++) {
                historialVisitante.push(lista[i]);
            }
            break;
        case 'edit':
            newArray = visitanteList.map(visitante => visitante.id_visitante == lista[0].id_visitante ? lista[0] : visitante);
            visitanteList = newArray;
            break;

    }
}

const ObtenerVisitante = () => {
    const getDni = document.getElementById('FiltroDni');
    getFiltroDni(getDni.value);
}


//Comprueba los Select Surcursal, Sector, Torre
const SelectActual = (e) => {
    if (e.target.name != 'id_departamento')
        ActCombobox[e.target.name](e.target.value, e.target);
}

const ActCombobox = {
    'id_sucursal': (value, nombre) => { value != '' ? getSector(value) : cleanSelect(nombre) },
    'id_sector': (value, nombre) => { value != '' ? getTorre(value) : cleanSelect(nombre); },
    'id_torre': (value, nombre) => { value != '' ? getDepartamento(value) : cleanSelect(nombre)  }
}

const selects = document.querySelectorAll('#view-form-registrar #BodyInptus select');

selects.forEach((e) => {
    e.addEventListener('change', SelectActual);
})


const init = () => {

    showLoading();
    setColumns("example", colsName, true);
    setColumns("historial", colsVisitante, false);

    setTimeout(() => { getListaVisitante();}, 500)
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
            ClearValues();
            break;
        case 'cancel-registrar':
            $("#view-form-registrar").hide(500);
            $("#view-table").show(1000);
            ClearValues();
            break;
        case 'regvist':
            const cboSurcusal = document.getElementById('id_sucursal');
            cboSurcusal.value = '';
            $("#view-table").hide(500);
            $("#view-form-registrar").show(1000);
            let id2 = ((t.parentElement).parentElement).parentElement.id;
            getVisitanteId(id2);
            break;
        case 'edit':
            $("#view-table").hide(500);
            $("#view-form").show(1000);
            let id = ((t.parentElement).parentElement).parentElement.id;
            getVisitanteId(id)
            break;
        case 'modalhistorial':
            historialVisitante = [];
            let titulo = document.getElementById('exampleModalLabel');
            let id3 = ((t.parentElement).parentElement).parentElement.id;
            getHistorial(id3);
            titulo.innerHTML = "Historial del Visitante: " + id3;
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
//Filtro de Visitante

const getFiltroDni = (dni) => {
    visitanteList = [];
    $.ajax({
        method: "GET",
        url: urlFiltroDni + "?nro_documento=" + dni,
        responseType: 'json',
        success: async function (res) {
            let {VisitanteList, oHeader } = res
            if (oHeader.estado) {
                if (VisitanteList.length > 0) {
                    await llenarVariable(VisitanteList, 'new');
                    await listTable(visitanteList)
                }
                else {
                    Swal.fire('No data', `no se encontro un usuario con el DNI: ${dni}`, 'error');
                }
            } else {
                Swal.fire('Ooops!', oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            Swal.close();
        }

    });
}


//Obtenemos el Historial del Visitante
const getHistorial = (id) => {
    $.ajax({
        method: "GET",
        url: urlListHistorial + "?id_visistante=" + id,
        responseType: 'json',
        success: async function (res) {
            let { VisitaRegistroList, oHeader } = res
            Swal.close();
            if (oHeader.estado) {
                await llenarVariable(VisitaRegistroList, 'historial');
                await listHistorail(historialVisitante)
            } else {
                Swal.fire('Ooops!', oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            Swal.close();
        }

    });
}

const setRango = (rango) => {
    const lbl = document.getElementById('txtRango');
    getRango[rango](lbl);
}

const getRango = {
    'fi_fi': (lbl) => {  lbl.innerHTML = "Fecha Inicio a Fecha Inicio"  },
    'ff_ff': (lbl) => {  lbl.innerHTML = "Fecha Final a Fecha Final" },
    'fi_ff': (lbl) => {  lbl.innerHTML = "Fecha Inicio a Fecha Final" },
}

//Lista el Historial del Visitante en la Tabla Historial
const listHistorail = (res) => {
    $('#historial').DataTable({
        scrollY: 250,
        searching: false,
        paging:false,
        destroy: true,
        data: res,
        columns: [{ data: "id_visita_registro" },
            { data: "fecha_ingreso", render: function (data) { return GetStatusFecha(data) } },
            { data: "fecha_ingreso", render: function (data) { return GetStatus(data) } },
            { data: "fecha_salida", render: function (data) { return GetStatusFecha(data) } },
            { data: "fecha_salida", render: function (data) { return GetStatus(data) } },
            {
                data: null,
                title: "Surcusal y Secto",
                render: function (data, type, row) {
                    return row['nombre_sucursal'] + ' -- ' + row['nombre_sector'];
                }
            },
            {
                data: null,
                title: "Torre y Dep.",
                render: function (data, type, row) {
                    return ' Nro.Torre: ' + row['numero_torre'] + ' Nro.Dep: ' + row['numero_departamento'];
                }
            }
            //,buttonsDatatTable("edit")
        ],
        rowId: "id_visita_registro",
        columnDefs:
            [
                {
                    "targets": 0,
                    "visible": false,
                }
            ],
        order: [[0, 'des']],
        rowCallback: function (row, data) {
            if (GetStatus(data.fecha_salida) == "No Salio") {
                $(row).addClass('table-danger');
            }
        },
        language: {
            "url": "//cdn.datatables.net/plug-ins/1.10.16/i18n/Spanish.json"
        }
    });
};

const listTable = (res) => {
    $('#example').DataTable({
        destroy: true,
        data: res,
        columns: [{ data: "id_visitante" },
            {
                data: null,
                title: "Nombre Vistante",
                render: function (data, type, row) {
                    return row['nombre'] + ' ' + row['apellidos'];
                }
            },
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

    if (swCamposValid.swNombre && swCamposValid.swApellidoP
        && swCamposValid.swDocumento && swCamposValid.swTipoDocumento && swCamposValid.swGenero) {
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
                    Swal.close();
                    let { VisitanteList, oHeader } = res;
                    if (oHeader.estado) {
                        if (id_visitante.value == "0") {
                            
                            await llenarVariable(res.VisitanteList, 'new');
                        } else {
                            await llenarVariable(VisitanteList, 'edit');
                        }

                        await listTable(visitanteList);
                        Swal.fire('ok', oHeader.mensaje, 'success');
                        $("#view-form").hide(500);
                        $("#view-table").show(1000);
                    }
                    else {
                        Swal.fire('Error', oHeader.mensaje, 'error');
                    }

                },
                error: function (err) {
                    Swal.close();
                }
            });

        }
        else {
            ValidNull();
        }
 
    }
    else {
        Swal.fire('Error', 'Formulario Incorrecto', 'error');
    }
    

});

const getVisitanteId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetVisitante + "?id_visitante=" + id,
        responseType: 'json',
        success: async function (res) {
            let { VisitanteList, oHeader } = res;
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
        $("#view-form select").each(function (ind) {
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



const cleanSelect = (target) => {
    let selSector = document.querySelectorAll("#BodyInptus .cboClear");
    selSector.forEach((e) => {
        if (target.name == 'id_sucursal') {
            if (e.name != 'id_sucursal') {
                let infoCbo = e;
                infoCbo.innerHTML = "";
                let str = ``;
                str += `<option value="">...Seleccione...</option>`
                infoCbo.innerHTML = str;
                infoCbo.value = "";
            }
        }
        else if (target.name == 'id_sector') {
            if (e.name != 'id_surcusal' && e.name != 'id_sector') {
                let infoCbo = e;
                infoCbo.innerHTML = "";
                let str = ``;
                str += `<option value="">...Seleccione...</option>`
                infoCbo.innerHTML = str;
                infoCbo.value = "";
            }
        }
        else if (target.name == 'id_torre') {
            if (e.name == 'id_departamento') {
                let infoCbo = e;
                infoCbo.innerHTML = "";
                let str = ``;
                str += `<option value="">...Seleccione...</option>`
                infoCbo.innerHTML = str;
                infoCbo.value = "";
            }
        }
       
        
    });
   
}


const getSector = async (id) => {
    await $.ajax({
        method: "GET",
        url: urlGetSector + "?id_sector=" + id,
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
        url: urlGetDepartamento + "?id_departamento=" + id,
        responseType: 'json',
        success: async function (res) {
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

    if (swCamposValid.swApellidoP && swCamposValid.swNombre
        && swCamposValid.swGenero && swCamposValid.swDocumento && swCamposValid.swTipoDocumento) {

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
                    Swal.close();
                    let { VisitanteList, oHeader } = res;
                    if (oHeader.estado) {
                        await listTable(visitanteList);
                        Swal.fire('ok', oHeader.mensaje, 'success');
                        ExitFormularioRV();
                    }
                    else {
                        Swal.fire('Error', oHeader.mensaje, 'error');
                    }

                },
                error: function (err) {
                    Swal.close();
                }
            });

        }
        else {
            ValidNull();
        }
    }
    else {
        Swal.fire('Error', 'Formulario incorrecto', 'error');
    }
});

const ExitFormularioRV = () => {
    $("#view-form-registrar").hide(500);
    $("#view-table").show(1000);
};




init();



