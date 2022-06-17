
var colsName = ['ID', 'SUCURSAL', 'SECTOR', 'TORRE', 'DEPARTAMENTO', 'SERVICIO', 'MONTO', 'FECHA_PAGO', 'FECHA_VENCIMIENTO'];
var reciboList = [];

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
                reciboList.push(lista[i]);
            }
            break;
        case 'edit':
            console.log("edit");
            newArray = reciboList.map(recibo => recibo.id_recibo == lista[0].id_recibo ? lista[0] : recibo)
            reciboList = newArray;
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
    $("#id_departamento").on("change", function (e) {
        getServicio(this.value);
    });

    removeDanger();
    showLoading();
    setColumns("example", colsName, true);

    getListaRecibo();
    Swal.close();
};
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
            getServicioId(id);

            break;

    }
};

const getListaRecibo = () => {
    $.ajax({
        method: "GET",
        url: urlGetRecibo,
        responseType: 'json',
        success: async function (res) {
            let { ReciboList, oHeader } = res
            Swal.close();
            if (oHeader.estado) {
                console.log(res);
                await llenarVariable(ReciboList, 'new');
                await listTable(reciboList);
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
    console.log(res);
    $('#example').DataTable({
        destroy: true,
        data: res,       
        columns: [{ data: "id_recibo" },
        { data: "nombre_sucursal" },
        { data: "nombre_sector" },
        { data: "numero_torre" },
        { data: "numero_departamento" },
        { data: "nombre_servicio" },
        { data: "monto" },
        { data: "fecha_pago", render: function (data) { return FechaDate(data) } },
        { data: "fecha_vencimiento", render: function (data) { return FechaDate(data) } },
        buttonsDatatTable("edit")],
        rowId: "id_recibo",
       
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
    let id_recibo = document.getElementsByName("id_recibo")[0];

    if (id_recibo.value == "" || id_recibo.value == undefined) {
        id_recibo.value = 0;
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
            url: urlSaveRecibo,
            data: formData,
            responseType: 'json',
            success: async function (res) {
                console.log(res);
                Swal.close();
                let { ReciboList, oHeader } = res;
                if (oHeader.estado) {
                    if (id_recibo.value == "0") {
                        await llenarVariable(ReciboList, 'new');
                    } else {
                        await llenarVariable(ReciboList, 'edit');
                    }

                    await listTable(reciboList);
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

const getReciboId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetRecibo +"?id_recibo=" + id,
        responseType: 'json',
        success: async function (res) {
            let { ReciboList, oHeader } = res;
            console.log(reciboList);
            if (oHeader.estado) {
                await llenarCampos(ReciboList);
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
    console.log(list);
    let idSucursal = "";
    let idSector = "";
    let idTorre = "";
    let idDepartamento = "";
    let idServicio = "";
    let sector = document.getElementsByName("id_sector")[0];
    let torre = document.getElementsByName("id_torre")[0];
    let departamento = document.getElementsByName("id_departamento")[0];
    let servicio = document.getElementsByName("id_servicio")[0];
    if (list.length > 0) {
        $(".val").each(function (ind) {
            for (var propName in list[0]) {
                if (this.name === propName) {
                    if (this.type == "date") {
                        this.value = FechaDate(list[0][propName]);
                    } else {
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
                    } else if (propName === "id_servicio") {
                        idServicio = list[0][propName];
                    }                       
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
    await getServicio(idDepartamento)
    servicio.value = idServicio;
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
const getServicio = async (id) => {
    await $.ajax({
        method: "GET",
        url: urlGetServicio + "?id_departamento=" + id,
        responseType: 'json',
        success: async function (res) {
            console.log(res);
            let { ServicioList, oHeader } = res;
            if (oHeader.estado) {
                await getSelectServicio(ServicioList);
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
const getSelectServicio = (list) => {
    let selServicio = document.getElementsByName("id_servicio")[0];
    selServicio.innerHTML = "";
    let str = ``;
    str += `<option value="">...Seleccione...</option>`
    console.log(list);
    if (list.length > 0) {
        for (i = 0; i < list.length; i++) {
            str += `<option value="${list[i].id_servicio}">${list[i].nombre}</option>`;
        }
    }
    selServicio.innerHTML = str;
    selServicio.value = "";

}
$(".val").click(function (e) {
    this.classList.remove("border-danger");
});