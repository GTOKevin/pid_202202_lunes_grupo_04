
var colsName = ['ID', 'SUCURSAL', 'SECTOR', 'TORRE','DEPARTAMENTO'];
var departamentoList = [];

const removeDanger = () => {
    $(".val").click(function (e) {
        this.classList.remove("border-danger");
        (this.parentElement).lastElementChild.classList.add("d-none");
    });

    $(".valProp").click(function (e) {
        this.classList.remove("border-danger");
        (this.parentElement).lastElementChild.classList.add("d-none");
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
            limpiarTable();
            mostrarForm();
            break;
        case 'cancel':
            mostrarTable();
            break;
        case 'edit':
            limpiarTable();
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
    let form = {};

    e.preventDefault();
    let { formData, formEstado } = setValDataLab();


    let propietarios =obtenerDataTable();

    form["departamento"] = formData;
    form["propietarios"] = propietarios;


    if (formEstado) {
        showLoading();
        $.ajax({
            method: "POST",
            url: urlSaveDepartamento,
            data: form,
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
                    await mostrarTable();
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
            console.log("Res",res);
            let { lista_Departamento, propietarios, oHeader } = res;
            if (oHeader.estado) {
                await llenarCampos(lista_Departamento);
                await llenarTable(propietarios);
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
const llenarTable = async (propietario) => {
    console.log(propietario);
        let table_prop = document.getElementById("table_prop");
    let row = ``;


    if (propietario.length > 0) {


        for (i = 0; i < propietario.length; i++) {
            if (propietario[i].id_propietario != "" || propietario[i].id_propietario != undefined) {
                row += `<tr data-id="${propietario[i].id_propietario}">`;
            } else {
                row += `<tr>`;
            }
            row += `
            <td name="id_propietario" class="d-none">${propietario[i].id_propietario}</td>
            <td name="nombres">${propietario[i].nombres}</td>
            <td name="primer_apellido">${propietario[i].primer_apellido}</td>
            <td name="segundo_apellido">${propietario[i].segundo_apellido}</td>
            <td name="tipo_documento" class="d-none">${propietario[i].tipo_documento}</td>
            <td name="nro_documento">${propietario[i].nro_documento}</td>
            <td name="nombre_tipo">${propietario[i].nombre_tipo}</td>
            <td name="nacionalidad" class="d-none">${propietario[i].nacionalidad}</td>
            <td name="id_tipo" class="d-none">${propietario[i].id_tipo}</td>
            <td name="id_departamento" class="d-none">${propietario[i].id_departamento}</td>
            </tr>`;
        }
       
    }
        

        table_prop.innerHTML += row;
    
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
        $("#closeModal")[0].click();
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
                if (this.name == "id_tipo") {
                    resJson.propietario[this.name] = this.value;
                    resJson.propietario["nombre_tipo"] = $(this)[0].selectedOptions[0].innerText;
                } else {
                    resJson.propietario[this.name] = this.value;
                }
                
            } else {
                this.classList.add("border-danger");
                (this.parentElement).lastElementChild.classList.remove("d-none");
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
            <td name="nro_documento">${propietario.nombre_tipo}</td>
            <td name="nacionalidad" class="d-none">${propietario.nacionalidad}</td>
            <td name="id_tipo" class="d-none">${propietario.id_tipo}</td>
            <td name="id_departamento" class="d-none">${propietario.id_departamento}</td>
            </tr>`;

    table_prop.innerHTML += row;
}

const obtenerDataTable = () => {

    let tableProp = [];

    $("#table_prop tr").each(function (ind) {
        console.log(this.childElementCount);
        let jsonProp = {};
        for (i = 0; i < this.childElementCount; i++) {
            let name = $(this.children[i])[0].attributes.name.value;
            jsonProp[name] = $(this.children[i])[0].textContent;
        }
        tableProp.push(jsonProp);
    });

    return tableProp;
}


const limpiarTable = () => {
    let table_prop = document.getElementById("table_prop");

    while (table_prop.firstChild) {
        table_prop.removeChild(table_prop.firstChild);
    }

    $("#div-form .border-danger").each(function (e) {
        this.classList.remove("border-danger");
    });

    $("#div-form .label-error").each(function (e) {
        this.classList.add("d-none");
    })

}


