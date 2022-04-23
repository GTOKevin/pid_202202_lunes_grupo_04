
var colsName = ['ID', 'FECHA INGRESO', 'FECHA SALIDA', 'DEPARTAMENTO','VISITANTE'];
var visitanteregList = [];


const llenarVariable = (lista, option) => {
    let newArray;
    switch (option) {
        case 'new':
            for (i = 0; i < lista.length; i++) {
                visitanteregList.push(lista[i]);
            }
            break;
        case 'edit':
            console.log("edit");
            newArray = visitanteregList.map( visitanteList => visitanteList.id_visita_registro == lista[0].id_visita_registro ? lista[0] : visitanteList)
            visitanteregList = newArray;
            break;

    }
}

const init = () => {
    showLoading();
    setColumns("example", colsName, true);

    getListaVisitaRegistro();
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
           
            getVisitaRegistroId(id);
            break;

    }
};

const getListaVisitaRegistro = () => {

    $.ajax({
        method: "GET",
        url: urlGetVisitaRegistro,
        responseType: 'json',
        success: async function (res) {
            Swal.close();
            if (res.oHeader.estado) {
                await llenarVariable(res.VisitaRegistroList, 'new');
                await listTable(visitanteregList);
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
        columns: [
            { data: "id_visita_registro" },{ data: "fecha_ingreso", render: function (data) { return convertFecha(data) } }, {
                data: "fecha_salida", render: function (data) { return convertFecha(data) }
            }, { data: "id_departamento" },
            { data: "id_visitante" },
        buttonsDatatTable("edit")],
        rowId: "id_visita_registro",
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
    let id_visita_registro = document.getElementsByName("id_visita_registro")[0];

    if (id_visita_registro.value == "" || id_visita_registro.value == undefined) {
        id_visita_registro.value = 0;
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
            url: urlSaveVisitaRegistro,
            data: formData,
            responseType: 'json',
            success: async function (res) {
                Swal.close();
                let { VisitaRegistroList, oHeader } = res;
                if (oHeader.estado) {
                    if (id_visita_registro == "0") {
                        await llenarVariable(VisitaRegistroList, 'new');
                    } else {
                        await llenarVariable(VisitaRegistroList, 'edit');
                    }

                    await listTable(visitanteregList);
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

const getVisitaRegistroId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetVisitaRegistro + "?id_visita_registro=" + id,
        responseType: 'json',
        success: async function (res) {
            let { VisitaRegistroList , oHeader } = res;
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


$("#id_sucursal").on('change', function (index) {
    let idsuc = this.value;

});

init();




