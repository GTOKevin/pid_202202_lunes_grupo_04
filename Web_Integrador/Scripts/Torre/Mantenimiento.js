
var colsName = ['ID', 'SUCURSAL', 'SECTOR', 'TORRE'];
var torreList = [];

const llenarVariable = (lista, option) => {
    let newArray;
    switch (option) {
        case 'new':
            for (i = 0; i < lista.length; i++) {
                torreList.push(lista[i]);
            }
            break;
        case 'edit':
            console.log("edit");
            newArray = torreList.map(torre => torre.id_torre == lista[0].id_torre ? lista[0] : torre)
            torreList = newArray;
            break;
    }
}
const init = () => {
    $("#id_sucursal").on("change", function (e) {
        getSector(this.value);
    });
    showLoading();
    setColumns("example", colsName, true);
    getListaTorre();
    Swal.close();
};
const btnAction = (t, tipo) => {
    switch (tipo) {
        case 'new':
            cleanForm();
            cleanSelect();
            mostrarFormulario();
            break;
        case 'cancel':
            mostrarTabla();
            break;
        case 'edit':
            mostrarFormulario();

            let id = ((t.parentElement).parentElement).parentElement.id;
            getTorreId(id);

            break;

    }
};
const getListaTorre = () => {
    $.ajax({
        method: "GET",
        url: urlGetTorre,
        responseType: 'json',
        success: async function (res) {
            console.log(res);
            Swal.close();
            if (res.oHeader.estado) {

                await llenarVariable(res.TorreList, 'new');
                await listTable(torreList);
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
        columns: [{ data: "id_torre" },
        { data: "nombre_sucursal" },
        { data: "nombre_sector" },
        { data: "numero" },
        buttonsDatatTable("edit")],
        rowId: "id_torre",
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
    let id_torre = document.getElementsByName("id_torre")[0];

    if (id_torre.value == "" || id_torre.value == undefined) {
        id_torre.value = 0;
    }

    e.preventDefault();
    let { formData, formEstado } = setValData();


    if (formEstado) {
        showLoading();

        $.ajax({
            method: "POST",
            url: urlSaveTorre,
            data: formData,
            responseType: 'json',
            success: async function (res) {
                console.log(res);
                Swal.close();
                let { TorreList, oHeader } = res;
                if (oHeader.estado) {
                    if (id_torre.value == "0") {
                        await llenarVariable(TorreList, 'new');
                    } else {
                        await llenarVariable(TorreList, 'edit');
                    }

                    await listTable(torreList);
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
const getTorreId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetTorre + "?id_torre=" + id,
        responseType: 'json',
        success: async function (res) {
            let { TorreList, oHeader } = res;
            if (oHeader.estado) {
                await llenarCampos(TorreList);
            } else {
                Swal.fire('Ooops!', res.oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            Swal.close();
        }

    });
}
const llenarCampos =async (list) => {
    let idSucursal = "";
    let idSector=""
    let sector = document.getElementsByName("id_sector")[0];
    if (list.length > 0) {
        $(".val").each(function (ind) {
            for (var propName in list[0]) {
              if (this.name === propName) {
                    this.value = list[0][propName];
                }
                if (propName === "id_sucursal") {
                    idSucursal = list[0][propName];
                } else if (propName ==="id_sector"){
                    idSector = list[0][propName];
                }
            }
        });
    }

    await getSector(idSucursal);
    sector.value = idSector;




}
init();
$(".val").click(function (e) {
    this.classList.remove("border-danger");
});


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
            console.log(res);
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
const getSelectSector = (list) => {

    let selSector = document.getElementsByName("id_sector")[0];
    selSector.innerHTML = "";
    let str = ``;
    str +=`<option value="">...Seleccione...</option>`
    if (list.length > 0) {
        for (i = 0; i < list.length; i++) {
            str += `<option value="${list[i].id_sector}">${list[i].nombre_sector}</option>`;
        }
    }
    selSector.innerHTML = str;
    selSector.value = "";

}