
var colsName = ['ID', 'FECHA INGRESO', 'FECHA SALIDA', 'SUCURSAL', 'SECTOR', 'TORRE', 'DEPARTAMENTO','VISITANTE'];
var visitanteregList = [];

const removeDanger = () => {
    $(".val").click(function (e) {
        this.classList.remove("border-danger");
    });

}
const llenarVariable = (lista, option) => {
    let newArray;
    switch (option) {
        case 'new':
            for (i = 0; i < lista.length; i++) {
                visitanteregList.push(lista[i]);
            }
            break;
        case 'edit':
            console.log("edit");
            newArray = visitanteregList.map( visitanteList => visitanteList.id_visita_registro == lista[0].id_visita_registro ? lista[0] : visitanteList)
            visitanteregList = newArray;
            break;

    }
}

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

    removeDanger();
    showLoading();
    setColumns("example", colsName, true);
    getListaVisitaRegistro();
    Swal.close();
};
const mostrarForm = () => {
    $("#view-table").hide(500);
    $("#div-form").show(1000);
}
const mostrarTable = () => {
    $("#div-form").hide(500);
    $("#view-table").show(1000);
}
const btnAction = (t, tipo) => {
 
    switch (tipo) {
        
        case 'new':
            cleanForm();
            cleanSelect();
            $("#view-table").hide(500);
            $("#view-form").show(1000);
            break;
        case 'cancel':
            $("#view-form").hide(500);
            $("#view-table").show(1000);
            break;
        case 'edit':
            $("#view-table").hide(500);
            $("#view-form").show(1000);

            let id = ((t.parentElement).parentElement).parentElement.id;     
            getVisitaRegistroId(id);
            break;

    }
};
const getListaVisitaRegistro= () => {
    $.ajax({
        method: "GET",
        url: urlGetVisitaRegistro,
        responseType: 'json',
        success: async function (res) {
            let { VisitaRegistroList, oHeader } = res
            Swal.close();
            if (oHeader.estado) {
                console.log(res);
                await llenarVariable(VisitaRegistroList, 'new');
                await listTable(visitanteregList);
            } else {
                Swal.fire('Ooops!', oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            Swal.close();
        }

    });
}

//const getListaVisitaRegistro = () => {

//    $.ajax({
//        method: "GET",
//        url: urlGetVisitaRegistro,
//        responseType: 'json',
//        success: async function (res) {
//            Swal.close();
//            if (res.oHeader.estado) {
//                await llenarVariable(res.VisitaRegistroList, 'new');
//                await listTable(visitanteregList);
//            } else {
//                Swal.fire('Ooops!', res.oHeader.mensaje, 'error');
//            }
//        },
//        error: function (err) {
//            Swal.close();
//        }

//    });
//}
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
        columns: [
            { data: "id_visita_registro" },
            { data: "fecha_ingreso", render: function (data) { return FechaDate(data) } },
            { data: "fecha_salida", render: function (data) { return FechaDate(data) } },
            { data: "nombre_sucursal" },
            { data: "nombre_sector" },
            { data: "numero_torre" },
            { data: "numero_departamento" },
            { data: "nombre_visitante" },
        buttonsDatatTable("edit")],
        rowId: "id_visita_registro",
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
    let id_visita_registro = document.getElementsByName("id_visita_registro")[0];

    if (id_visita_registro.value == "" || id_visita_registro.value == undefined) {
        id_visita_registro.value = 0;
    }

    e.preventDefault();
    let formData = {};
    let validate = true;

    $("#view-form input").each(function (index) {
        if (this.value.trim().length != 0) {
            formData[this.name] = this.value;
        } else {
            validate = false;
        }

    });
    $("#view-form select").each(function (index) {
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
                let { VisitaRegistroList, oHeader } = res;
                if (oHeader.estado) {
                    if (id_visita_registro.value == "0") {
                        await llenarVariable(VisitaRegistroList, 'new');
                    } else {
                        await llenarVariable(VisitaRegistroList, 'edit');
                    }

                    await listTable(visitanteregList);
                    Swal.fire('ok', oHeader.mensaje, 'success');
                    await mostrarTabla();
                }

            },
            error: function (err) {
                Swal.close();
            }
        });

    }

});

const getVisitaRegistroId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetVisitaRegistro + "?id_visita_registro=" + id,
        responseType: 'json',
        success: async function (res) {
            let { VisitaRegistroList , oHeader } = res;
            if (oHeader.estado) {
                await llenarCampos(VisitaRegistroList);
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
    }

    await getSector(idSucursal);
    sector.value = idSector;
    await getTorre(idSector);
    torre.value = idTorre;
    await getDepartamento(idTorre)
    departamento.value = idDepartamento;

}


//$("#id_sucursal").on('change', function (index) {
//    let idsuc = this.value;

//});

init();


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


