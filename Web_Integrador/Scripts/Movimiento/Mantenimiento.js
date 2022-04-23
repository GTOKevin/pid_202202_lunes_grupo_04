
var colsName = ['ID', 'PROPIETARIO', 'TIPO', 'FECHA'];
var movimientoList = [];

const llenarVariable = (lista, option) => {
    let newArray;
    switch (option) {
        case 'new':
            for (i = 0; i < lista.length; i++) {
                movimientoList.push(lista[i]);
            }
            break;
        case 'edit':
            console.log("edit");
            newArray = movimientoList.map( movimientoList => movimiento.id_movimiento == lista[0].id_movimiento ? lista[0] : movimientoList)
            movimientoList = newArray;
            break;

    }
}

const init = () => {
    showLoading();
    setColumns("example", colsName, true);
    getListaMovimiento();
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
            getMovimientoId(id);
            break;

    }
};

const getListaMovimiento = () => {
    $.ajax({
        method: "GET",
        url: urlGetMovimiento,
        responseType: 'json',
        success: async function (res) {
            console.log(res);
            Swal.close();
            if (res.oHeader.estado) {
                await llenarVariable(res.MovimientoList, 'new');
                await listTable(movimientoList);
            } else {
                Swal.fire('Ooops!', res.oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            console.log(err);
            Swal.close();
        }

    });
}
const listTable = (res) => {
    $('#example').DataTable({
        destroy: true,
        data: res,
        columns: [
            { data: "id_movimiento" },
            { data: "id_propietario" },
            { data: "id_tipo" },
            { data: "fecha_registro", render: function (data) { return convertFecha(data) } },
        buttonsDatatTable("edit")],
        rowId: "id_movimiento",
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
    let id_movimiento = document.getElementsByName("id_movimiento")[0];

    if (id_movimiento.value == "" || id_movimiento.value == undefined) {
        id_movimiento.value = 0;
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
            url: urlSaveMovimiento,
            data: formData,
            responseType: 'json',
            success: async function (res) {
                Swal.close();
                console.log(res);
                let { MovimientoList, oHeader } = res;

                if (oHeader.estado) {
                    if ( id_movimiento.value == "0") {
                        await llenarVariable(MovimientoList, 'new');
                    } else {
                        await llenarVariable(MovimientoList, 'edit');
                    }

                    await listTable(MovimientoList);
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

const getMovimientoId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetMovimiento + "?id_movimiento=" + id,
        responseType: 'json',
        success: async function (res) {
            let { movimientoList, oHeader } = res;
            if (oHeader.estado) {
                await llenarCampos(movimientoList);
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



