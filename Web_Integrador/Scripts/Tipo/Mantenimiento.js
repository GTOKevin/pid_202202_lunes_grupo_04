
var colsName = ['ID', 'NOMBRE', 'UNIDAD'];
var tipoList = [];

const llenarVariable = (lista, option) => {
    let newArray;
    switch (option) {
        case 'new':
            for (i = 0; i < lista.length; i++) {
                tipoList.push(lista[i]);
            }
            break;
        case 'edit':
            console.log("edit");
            newArray = tipoList.map(tipo => tipo.id_tipo == lista[0].id_tipo ? lista[0] : tipo)
            tipoList = newArray;
            break;

    }
}

const init = () => {
    showLoading();
    setColumns("example", colsName, true);

    getListaTipo();
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
            getTipoId(id);

            break;

    }
};

const getListaTipo = () => {
    $.ajax({
        method: "GET",
        url: urlGetTipo,
        responseType: 'json',
        success: async function (res) {
            Swal.close();
            if (res.oHeader.estado) {
                await llenarVariable(res.TipoList, 'new');
                await listTable(tipoList);
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
        columns: [{ data: "id_tipo" }, { data: "nombre" }, { data: "unidad" },
        buttonsDatatTable("edit")],
        rowId: "id_tipo",
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
    let id_tipo = document.getElementsByName("id_tipo")[0];

    if (id_tipo.value == "" || id_tipo.value == undefined) {
        id_tipo.value = 0;
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
            url: urlSaveTipo,
            data: formData,
            responseType: 'json',
            success: async function (res) {
                Swal.close();
                let { TipoList, oHeader } = res;
                if (oHeader.estado) {
                    if (id_tipo.value == "0") {
                        await llenarVariable(TipoList, 'new');
                    } else {
                        await llenarVariable(TipoList, 'edit');
                    }

                    await listTable(tipoList);
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

const getTipoId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetTipo + "?id_tipo=" + id,
        responseType: 'json',
        success: async function (res) {
            let { TipoList, oHeader } = res;
            if (oHeader.estado) {
                await llenarCampos(TipoList);
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