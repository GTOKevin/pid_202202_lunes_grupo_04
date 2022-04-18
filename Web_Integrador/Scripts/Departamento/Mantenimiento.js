
var colsName = ['ID', 'SUCURSAL', 'SECTOR', 'TORRE','DEPARTAMENTO'];
var departamentoList = [];

const removeDanger = () => {
    $(".val").click(function (e) {
        this.classList.remove("border-danger");
    });

    $(".valProp").click(function (e) {
        this.classList.remove("border-danger");
    });

}

const llenarVariable = (lista, option) => {
    let newArray;
    switch (option) {
        case 'new':
            for (i = 0; i < lista.length; i++) {
                departamentoList.push(lista[i]);
            }
            break;
        case 'edit':
            console.log("edit");
            newArray = departamentoList.map(departamento => departamento.id_departamento == lista[0].id_departamento ? lista[0] : departamento)
            departamentoList = newArray;
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
    removeDanger();
    showLoading();
    setColumns("example", colsName, true);
    getListaTorre();
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
            mostrarForm();
            break;
        case 'cancel':
            mostrarTable();
            break;
        case 'edit':
            mostrarForm();
            let id = ((t.parentElement).parentElement).parentElement.id;
            getDepartamentoId(id);

            break;

    }
};
const getListaTorre = () => {
    $.ajax({
        method: "GET",
        url: urlGetDepartamento,
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
const listTable = (res) => {
    $('#example').DataTable({
        destroy: true,
        data: res,
        columns: [{ data: "id_departamento" },
        { data: "nombre_sucursal" },
        { data: "nombre_sector" },
        { data: "numero_torre" },
        {data:"numero"},
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
$("#view-form").on("submit", function (e) {
    let id_departamento = document.getElementsByName("id_departamento")[0];

    if (id_departamento.value == "" || id_departamento.value == undefined) {
        id_departamento.value = 0;
    }

    e.preventDefault();
    let { formData, formEstado } = setValData();
    console.log(formData);
    console.log(formEstado);
  
    obtenerDataTable();

    if (formEstado) {
        showLoading();

        $.ajax({
            method: "POST",
            url: urlSaveDepartamento,
            data: formData,
            responseType: 'json',
            success: async function (res) {
                console.log(res);
                Swal.close();
                let { lista_Departamento, oHeader } = res;
                if (oHeader.estado) {
                    if (id_departamento.value == "0") {
                        await llenarVariable(lista_Departamento, 'new');
                    } else {
                        await llenarVariable(lista_Departamento, 'edit');
                    }

                    await listTable(departamentoList);
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
const getDepartamentoId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetDepPropietario + "?id_departamento=" + id,
        responseType: 'json',
        success: async function (res) {
            let { lista_Departamento, lista_Propietario, oHeader } = res;
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
const llenarCampos = async (list) => {
    let idSucursal = "";
    let idSector = "";
    let idTorre = "";
    let sector = document.getElementsByName("id_sector")[0];
    let torre = document.getElementsByName("id_torre")[0];
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
                    idTorre=list[0][propName];
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

const getTorre = async(id) => {
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



const agregarProp = () => {
    let { propietario, estado } = obtenerProp();
    console.log(propietario, estado);
    if (estado) {
        listaTableData(propietario);
    }
}

const obtenerProp = () => {
    let resJson = {
        propietario: {},
        estado: true
    };
    let id_dep = document.getElementsByName("id_departamento")[0].value;
    $(".valProp").each(function (e) {
        if (this.id !== "id_prop") {
            if (this.value.trim().length > 0) {
                resJson.propietario[this.name] = this.value;
            } else {
                this.classList.add("border-danger");
                resJson.estado = false;
            }
           
        } else {
            resJson.propietario[this.name] = this.value;
        }
    });
    resJson.propietario["id_departamento"] = id_dep;
    return resJson;
}
const listaTableData = (propietario) => {
    let table_prop = document.getElementById("table_prop");
    let row = ``;
    if (propietario.id_propietario == "" || propietario.id_propietario == undefined) {
        row += `<tr data-id="${propietario.id_propietario}">`;
    } else {
        row += `<tr>`;
    }
    row += `<td name="nombres">${propietario.nombres}</td>
            <td name="primer_apellido">${propietario.primer_apellido}</td>
            <td name="segundo_apellido">${propietario.segundo_apellido}</td>
            <td name="tipo_documento" class="d-none">${propietario.tipo_documento}</td>
            <td name="nro_documento">${propietario.nro_documento}</td>
            <td name="nacionalidad" class="d-none">${propietario.nacionalidad}</td>
            <td name="id_tipo" class="d-none">${propietario.id_tipo}</td>
            </tr>`;

    table_prop.innerHTML += row;
}

const obtenerDataTable = () => {

    $("#table_prop tr").each(function (ind) {
        (this.childElementCount)
   
            
    })
}