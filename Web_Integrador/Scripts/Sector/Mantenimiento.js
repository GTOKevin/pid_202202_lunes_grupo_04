
var colsName = ['ID', 'SUCURSAL', 'SECTOR', 'FECHA'];
var sectorList = [];

const llenarVariable = (lista, option) => {
    let newArray;
    switch (option) {
        case 'new':
            for (i = 0; i < lista.length; i++) {
                sectorList.push(lista[i]);
            }
            break;
        case 'edit':
            console.log("edit");
            newArray = sectorList.map(sector => sector.id_sector == lista[0].id_sector ? lista[0] : sector)
            sectorList = newArray;
            break;
    }
}

const init = () => {
    showLoading();
    setColumns("example", colsName, true);

    getListaSector();
    Swal.close();
};

const btnAction = (t, tipo) => {
    switch (tipo) {
        case 'new':
            cleanForm();
            mostrarFormulario();
            break;
        case 'cancel':
            mostrarTabla();
            break;
        case 'edit':
            mostrarFormulario();

            let id = ((t.parentElement).parentElement).parentElement.id;
            getSucursalId(id);

            break;

    }
};

const getListaSector = () => {
    $.ajax({
        method: "GET",
        url: urlGetSector,
        responseType: 'json',
        success: async function (res) {
            console.log(res);
            Swal.close();
            if (res.oHeader.estado) {
                
                await llenarVariable(res.SectorList, 'new');
                await listTable(sectorList);
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
        columns: [{ data: "id_sector" },
            { data: "nombre_sucursal" },
            { data: "nombre_sector" },
            { data: "fecha_creacion", render: function (data) { return convertFecha(data) } },
            buttonsDatatTable("edit")],
        rowId: "id_sector",
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
    let id_sector = document.getElementsByName("id_sector")[0];

    if (id_sector.value == "" || id_sector.value == undefined) {
        id_sector.value = 0;
    }

    e.preventDefault();
    let { formData, formEstado } = setValData();


    if (formEstado) {
        showLoading();

        $.ajax({
            method: "POST",
            url: urlSaveSector,
            data: formData,
            responseType: 'json',
            success: async function (res) {
                console.log(res);
                Swal.close();
                let { SectorList, oHeader } = res;
                if (oHeader.estado) {
                    if (id_sector.value == "0") {
                        await llenarVariable(SectorList, 'new');
                    } else {
                        await llenarVariable(SectorList, 'edit');
                    }

                    await listTable(sectorList);
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

const getSucursalId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetSector + "?id_sector=" + id,
        responseType: 'json',
        success: async function (res) {
            let { SectorList, oHeader } = res;
            if (oHeader.estado) {
                await llenarCampos(SectorList);
            } else {
                Swal.fire('Ooops!', res.oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            Swal.close();
        }

    });
}

const llenarCampos = (list) => {

    if (list.length > 0) {
        $(".val").each(function (ind) {
            for (var propName in list[0]) {
                console.log(propName);
                if (this.name === propName) {
                    this.value = list[0][propName];
                }
            }

        });
    }

}

init();

$(".val").click(function (e) {
    this.classList.remove("border-danger");
});


