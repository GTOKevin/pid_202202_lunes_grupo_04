
var colsName = ['ID', 'VISITANTE', 'FECHA INGRESO','', 'FECHA SALIDA','', '', ''];
var visitanteregList = [];
var visitanteList = [];


const llenarVariable = (lista, option) => {
    let newArray;
    switch (option) {
        case 'new':
            for (i = 0; i < lista.length; i++) {
                visitanteregList.push(lista[i]);
            }
            break;
        case 'newVisitante':
            for (i = 0; i < lista.length; i++) {
                visitanteList.push(lista[i]);
            }
            break;
        case 'edit':
            newArray = visitanteregList.map( visitanteList => visitanteList.id_visita_registro == lista[0].id_visita_registro ? lista[0] : visitanteList)
            visitanteregList = newArray;
            break;

    }
}

const SelectActual = (e) => {
    if (e.target.name != 'id_departamento')
        ActCombobox[e.target.name](e.target.value, e.target);
}

const ActCombobox = {
    'id_sucursal': (value, nombre) => { value != '' ? getSector(value) : cleanSelect(nombre) },
    'id_sector': (value, nombre) => { value != '' ? getTorre(value) : cleanSelect(nombre) },
    'id_torre': (value, nombre) => { value != '' ? getDepartamento(value) : cleanSelect(nombre) }
}

const selects = document.querySelectorAll('#view-form #BodyInptus select');

selects.forEach((e) => {
    e.addEventListener('change', SelectActual);
})


const init = () => {
    showLoading();
    setColumns("example", colsName, true);
    setTimeout(() => { getListaVisitaRegistro(); }, 500);
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
            ClearValues();
            $("#view-table").hide(500);
            $("#view-form").show(1000);
            break;
        case 'cancel':
            $("#view-form").hide(500);
            $("#view-table").show(1000);
            ClearValues();
            break;
        case 'edit':
            $("#view-table").hide(500);
            $("#view-form").show(1000);
            let id = ((t.parentElement).parentElement).parentElement.id;     
            getVisitaRegistroId(id);
            break;
        case 'regvistsalid':
            let id2 = ((t.parentElement).parentElement).parentElement.id;
            let obtenerNombre = document.getElementById(`${id2}`);
            let bodyTD = obtenerNombre.querySelector('td');
            AlertSalida(id2, bodyTD.innerText);
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

const ObtenerVisitanteRegistrado = () => {
    const getDni = document.getElementById('FiltroDniReg');
    let selectEstado = document.querySelectorAll("input[name=inlineRadioOptions]");
    if (selectEstado[0].checked) {
        getFiltroDniReg(getDni.value, 1);
    }
    if (selectEstado[1].checked) {
        getFiltroDniReg(getDni.value, 2);
    }
    if (selectEstado[2].checked) {
        getFiltroDniReg(getDni.value, 3);
    }

}

//Filtro DNI
const getFiltroDniReg = (dni, selectEstado) => {
    visitanteregList = [];
    $.ajax({
        method: "GET",
        url: urlFiltroRegVis + "?nro_documento=" + dni + "&estado=" + selectEstado,
        responseType: 'json',
        success: async function (res) {
            let { VisitaRegistroList , oHeader } = res
            if (oHeader.estado) {
                await llenarVariable(VisitaRegistroList, 'new');
                await listTable(visitanteregList);
                const getDni = document.getElementById('FiltroDniReg');
                getDni.value = '';
            } else {
                Swal.fire('Ooops!', oHeader.mensaje, 'error');
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
            { data: "nombre_visitante" },
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
            },
            buttonsDatatTable("mantVistSalida")],
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



function setSalidaRegistro() {

    let id_visita_registro = document.getElementsByName("id_visita_registro")[0];
    let id_visitante = document.getElementsByName("id_visitante")[0];

    if (id_visita_registro.value == "" || id_visita_registro.value == undefined) {
        id_visita_registro.value = 0;
    }
    if (id_visitante.value == "" || id_visitante.value == undefined) {
        id_visitante.value = 0;
    }


    let formData = {};
    let validate = true;

    $("#view-form input").each(function (index) {
        if (this.name != "") {
            if (this.value.trim().length != 0) {
                formData[this.name] = this.value;
            } else {
                validate = false;
            }
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
            url: urlSaveSalida,
            data: formData,
            responseType: 'json',
            success: async function (res) {
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

}

const getVisitaRegistroId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetVisitaRegistro + "?id_visita_registro=" + id,
        responseType: 'json',
        success: async function (res) {
            let { VisitaRegistroList, oHeader } = res;
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

const getVisitaRegistroFechaId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetVisitaRegistro + "?id_visita_registro=" + id,
        responseType: 'json',
        success: async function (res) {
            let { VisitaRegistroList, oHeader } = res;
            if (oHeader.estado) {
                await llenarCampos(VisitaRegistroList);
                await setSalidaRegistro();
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

const ObtenerVisitante = () => {
    const getDni = document.getElementById('buscarVisitanteDNI');
    getFiltroDni(getDni.value);
}


const getFiltroDni = (dni) => {
    const getDni = document.getElementById('buscarVisitanteDNI');
    $.ajax({
        method: "GET",
        url: urlFiltroDni + "?nro_documento=" + dni,
        responseType: 'json',
        success: async function (res) {
            let { VisitanteList, oHeader } = res
            if (oHeader.estado) {
                if (VisitanteList.length > 0) {

                    if (visitanteList.length > 0) {
                        let newArray = visitanteList.filter(value => value.nro_documento == VisitanteList[0].nro_documento);
                        let newValue = visitanteList.indexOf(newArray[0]);
                        if (newValue == -1) {
                            llegarTablaVisitante(VisitanteList);
                            llenarVariable(VisitanteList, 'newVisitante');
                            getDni.value = "";
                        }
                        else {
                            Swal.fire('No data', 'Ya Ingreso este Visitante a la lista', 'error');
                            setTimeout(() => { Swal.close() }, 1500);
                        }
                    }
                    else {
                        llegarTablaVisitante(VisitanteList);
                        llenarVariable(VisitanteList, 'newVisitante');
                        getDni.value = "";
                    }
                }
                else {
                    Swal.fire('No data', oHeader.mensaje, 'error');
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


const llegarTablaVisitante = (visitante) => {
    let tblvist = document.getElementById('table_Visitante');
    let newRow = '';
    if (visitante.length > 0) {
        for (let v of visitante) {
            newRow += `<tr data-id="${v.id_visitante}">`;
            newRow +=
                `<td class="d-none" >${v.id_visitante}</td>
                <td >${v.nombre}</td>
                <td >${v.apellidos}</td>
                <td>${v.nro_documento}</td>
                <td >${v.nombre_genero}</td>
                <td >
                <button onclick='removeRow(this,${v.id_visitante});'  class='btn btn-sm btn-danger' style='padding:2px 4px;'>
                <i class="bx bx-x"></i>
                </button>
                </td>
                </tr>
                `;
        }
    }

    tblvist.innerHTML += newRow;
}

const removeRow = (btn, id) => {
    let deleterow = btn.parentNode.parentNode;
    let newArray = visitanteList.filter(value => value.id_visitante == id);
    let newValue = visitanteList.indexOf(newArray[0]);
    visitanteList.splice(newValue, 1);
    deleterow.parentNode.removeChild(deleterow);
}


$("#view-form").on("submit", function (e) {
    visitanteregList = [];
    let id_departamento = document.getElementById("id_departamento");

    if (id_departamento.value == "" || id_departamento.value == undefined) {
        id_departamento.value = 0;
    }
    let form = {};

    e.preventDefault();

    let visitantes = visitanteList;

    form["id_departamento"] = id_departamento.value;
    form["visitantes"] = visitantes;
        showLoading();

    if (id_departamento.value != 0) {
        if (swCamposValid.swCboSucursal && swCamposValid.swCboSector
            && swCamposValid.swCboTorre && swCamposValid.swCboDepartamento) {
            if (visitantes.length > 0) {
                $.ajax({
                    method: "POST",
                    url: addNewRegistro,
                    data: form,
                    responseType: 'json',
                    success: async function (res) {
                        Swal.close();
                        if (res.oHeader) {
                            getListaVisitaRegistro();
                            $("#view-form").hide(500);
                            $("#view-table").show(1000);

                        }
                    },
                    error: function (err) {
                        Swal.close();
                    }
                });

            }
            else {
                Swal.fire('Error', 'Ingrese al menos un Visitante a la lista', 'error');
            }
        }
        else {
            ValidNull();
        }
    }
    else {
        id_departamento.value = '';
        Swal.fire('Error', 'Seleccione un departamento', 'error');
        ValidNull();
    }
    


});



init();


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

const AlertSalida = (id,nombre) => {
    Swal.fire({
        title: 'Seguro que deseas marca hora de salida?',
        text: "Esta punto de darle la hora de Salida a: " + nombre,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, marcar salida'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire(
                'Se Confirmo',
                'visitante salio',
                'success'
            );
            getVisitaRegistroFechaId(id);
        }
    })
}

$("#view-form-nuevo").on("submit", function (e) {
    let id_visitante = document.getElementsByName("id_visitante")[0];

    if (id_visitante.value == "" || id_visitante.value == undefined) {
        id_visitante.value = 0;
    }

    e.preventDefault();

    if (swCamposValid.swNombre && swCamposValid.swApellidoP
        && swCamposValid.swDocumento && swCamposValid.swTipoDocumento && swCamposValid.swGenero) {
        let formData2 = {};
        let validate = true;
        $("#view-form-nuevo .cf").each(function (index) {
            if (this.value.trim().length != 0) {
                formData2[this.name] = this.value;
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
                        $('#modalVisitante').dispose();
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