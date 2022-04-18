
var colsName = ['ID', 'ID_TIPO', 'ID_DEPARTAMENTO', 'NOMBRE', 'FECHA'];
var servicioList = [];

const llenarVariable = (lista, option) => {
    let newArray;
    switch (option) {
        case 'new':
            for (i = 0; i < lista.length; i++) {
                servicioList.push(lista[i]);
            }
            break;
        case 'edit':
            console.log("edit");
            newArray = servicioList.map(servicio => servicio.id_servicio == lista[0].id_servicio ? lista[0] : servicio)
            servicioList = newArray;
            break;

    }
}

const init = () => {
    showLoading();
    setColumns("example", colsName, true);

    getListaServicio();
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
        case 'edit':
            $("#view-table").hide(500);
            $("#view-form").show(1000);

            let id = ((t.parentElement).parentElement).parentElement.id;
            getServicioId(id);

            break;

    }
};

const getListaServicio = () => {
    $.ajax({
        method: "GET",
        url: urlGetServicio,
        responseType: 'json',
        success: async function (res) {
            Swal.close();
            if (res.oHeader.estado) {
                await llenarVariable(res.ServicioList, 'new');
                await listTable(servicioList);
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
        columns: [{ data: "id_servicio" },
            { data: "id_tipo" },
            { data: "id_departamento" },
            { data: "nombre" },
            { data: "fecha_registro", render: function (data) { return convertFecha(data) } },
        buttonsDatatTable("edit")],
        rowId: "id_servicio",
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
    let id_servicio = document.getElementsByName("id_servicio")[0];

    if (id_servicio.value == "" || id_servicio.value == undefined) {
        id_servicio.value = 0;
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

    if (validate) {
        showLoading();

        $.ajax({
            method: "POST",
            url: urlSaveServicio,
            data: formData,
            responseType: 'json',
            success: async function (res) {
                Swal.close();
                let { ServicioList, oHeader } = res;
                if (oHeader.estado) {
                    if (id_servicio.value == "0") {
                        await llenarVariable(ServicioList, 'new');
                    } else {
                        await llenarVariable(ServicioList, 'edit');
                    }

                    await listTable(servicioList);
                    Swal.fire('ok', oHeader.mensaje, 'success');
                }

            },
            error: function (err) {
                Swal.close();
            }
        });

    }

    $("#view-form").hide(500);
    $("#view-table").show(1000);

});

const getServicioId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetServicio + "?id_servicio=" + id,
        responseType: 'json',
        success: async function (res) {
            let { ServicioList, oHeader } = res;
            if (oHeader.estado) {
                await llenarCampos(ServicioList);
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
        $("#view-form input").each(function (ind) {
            for (var propName in list[0]) {
                if (this.name === propName) {
                    this.value = list[0][propName];
                }
            }

        });
    }

}

init();