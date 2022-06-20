var colsName = ['ID', 'SUCURSAL', 'SECTOR', 'TORRE', 'DEPARTAMENTO'];
var departamentoList = [];

const Formulario = document.getElementById('view-form');

const llenarVariable = (lista, option) => {
    let newArray;
    switch (option) {
        case 'new':
            for (i = 0; i < lista.length; i++) {
                departamentoList.push(lista[i]);
            }
            break;
        case 'edit':
            newArray = departamentoList.map(departamento => departamento.id_departamento == lista[0].id_departamento ? lista[0] : departamento)
            departamentoList = newArray;
            break;
    }
}

const mostrarForm = () => {
    $("#view-table").hide(500);
    $("#view-form").show(1000);
}
const mostrarTable = () => {
    $("#view-form").hide(500);
    $("#view-table").show(1000);
}

const btnAction = (t, tipo) => {
    switch (tipo) {
        case 'cancel':
            mostrarTable();
            Formulario.reset();
            break;
        case 'edit':
            Formulario.reset();
            mostrarForm();
            ClearValues();
            let id = ((t.parentElement).parentElement).parentElement.id;
            getDepartamentoId(id);
            break;

    }
};

const init = () => {
    showLoading();
    setColumns("example", colsName, true);
    getListaTorre();
    Swal.close();
};

const getDepartamentoId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetDepaInfo + "?id_departamento=" + id,
        responseType: 'json',
        success: async function (res) {
            let { lista_Departamento, propietarios, oHeader } = res;
            if (oHeader.estado) {
                await llenarCampos(lista_Departamento);
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
        columns: [{ data: "id_departamento" },
        { data: "nombre_sucursal" },
        { data: "nombre_sector" },
        { data: "numero_torre" },
        { data: "numero" },
        buttonsDatatTable("edit")],
        rowId: "id_departamento",
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


$("#FormFiltro").on("submit", function (e) {
    
    let formData = {};
    $('.inFiltro').each(function (e) {
        formData[this.name] = this.value;
    });

    e.preventDefault();
        showLoading();
        $.ajax({
            method: "GET",
            url: urlGetDepartamentos,
            data: formData,
            responseType: 'json',
            success: async function (res) {
                let { lista_Departamento, oHeader } = res
                Swal.close();
                if (oHeader.estado) {
                    departamentoList = [];
                    await llenarVariable(lista_Departamento, 'new');
                    await listTable(departamentoList);
                } else {
                    Swal.fire('Ooops!', oHeader.mensaje, 'error');
                }
            },
            error: function (err) {
                Swal.close();
            }
        });

    


});



const getListaTorre = () => {
    $.ajax({
        method: "GET",
        url: urlGetDepartamentos,
        responseType: 'json',
        success: async function (res) {
            let { lista_Departamento, oHeader } = res
            Swal.close();
            if (oHeader.estado) {

                await llenarVariable(lista_Departamento, 'new');
                await listTable(departamentoList);
            } else {
                Swal.fire('Ooops!', oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            Swal.close();
        }

    });
}

$("#view-form").on("submit", function (e) {

    let formData = {};
    $('.val').each(function (e) {
        formData[this.name] = this.value;
    });

    e.preventDefault();
    showLoading();
    let validate = true;
    $("#view-form .val").each(function (index) {
        if (this.value.trim().length != 0) {
            formData[this.name] = this.value;
        } else {
            validate = false;
        }

    });
    if (swCamposValid.swNombreReportado &&
        swCamposValid.swTipoDocumento &&
        swCamposValid.swDocumento &&
        swCamposValid.swDescripcion) {
        if (validate) {
            $.ajax({
                method: "POST",
                url: urlSaveIncidente,
                data: formData,
                responseType: 'json',
                success: async function (res) {
                    Swal.close();
                    let { lista_Incidente, oHeader } = res
                    if (res.oHeader.estado) {
                        mostrarTable();
                        Swal.fire('Ooops!', oHeader.mensaje, 'success');
                    } else {
                        Swal.fire('Ooops!', oHeader.mensaje, 'error');
                    }
                },
                error: function (err) {
                    Swal.close();
                }
            });
        }
        else {
            Swal.fire('Ooops!', 'Formulario Incorrecto', 'error');
            ValidNull();
        }
    }
    else {
        Swal.fire('Ooops!', 'Formulario Incorrecto', 'error');
    }
    
});

//Comprueba los Select Surcursal, Sector, Torre
const SelectActual = (e) => {
    if (e.target.name != 'id_departamento')
        ActCombobox[e.target.name](e.target.value, e.target);
}

const ActCombobox = {
    'id_sucursal_f': (value, nombre) => { value != '' ? getSector(value) : cleanSelect(nombre) },
    'id_sector_f': (value, nombre) => { value != '' ? getTorre(value) : cleanSelect(nombre); },
    'id_torre_f': (value, nombre) => {  }
}

const selects = document.querySelectorAll('#FormFiltro select');

selects.forEach((e) => {
    e.addEventListener('change', SelectActual);
})


const getSector = async (id) => {
    await $.ajax({
        method: "GET",
        url: urlGetSector + "?id_sector=" + id,
        responseType: 'json',
        success: async function (res) {
            let { SectorList, oHeader } = res;
            if (oHeader.estado) {
                await getSelectSector(SectorList);
                await getSelectSectorRegistro(SectorList);
                
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
                await getSelectTorreRegistro(TorreList);
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

    let selSector = document.getElementsByName("id_sector_f")[0];
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

    let selTorre = document.getElementsByName("id_torre_f")[0];
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


const getSelectSectorRegistro = (list) => {

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

const getSelectTorreRegistro = (list) => {

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

const cleanSelect = (target) => {
    let selSector = document.querySelectorAll("#FormFiltro .cboClear");
    selSector.forEach((e) => {
        if (target.name == 'id_sucursal_f') {
            if (e.name != 'id_sucursal_f') {
                let infoCbo = e;
                infoCbo.innerHTML = "";
                let str = ``;
                str += `<option value="">...Seleccione...</option>`
                infoCbo.innerHTML = str;
                infoCbo.value = "";
            }
        }
        else if (target.name == 'id_sector_f') {
            if (e.name != 'id_surcusal_f' && e.name != 'id_sector_f') {
                let infoCbo = e;
                infoCbo.innerHTML = "";
                let str = ``;
                str += `<option value="">...Seleccione...</option>`
                infoCbo.innerHTML = str;
                infoCbo.value = "";
            }
        }
        else if (target.name == 'id_torre_f') {
            if (e.name == 'id_departamento_f') {
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

const llenarCampos = async (list) => {
    let idSucursal = "";
    let idSector = "";
    let idTorre = "";
    let sector = document.getElementsByName("id_sector")[0];
    let torre = document.getElementsByName("id_torre")[0];
    let departamento = document.getElementsByName("id_departamento")[0];
    if (list.length > 0) {
        $(".val").each(function (ind) {
            if (this.name == "id_sucursal" || this.name == "id_sector" || this.name == "id_torre" || this.name == "piso" || this.name == "numero") {
                this.disabled = true;
            }
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
                }
            }
        });
    }

    await getSector(idSucursal);
    sector.value = idSector;
    await getTorre(idSector)
    torre.value = idTorre;


}

init();