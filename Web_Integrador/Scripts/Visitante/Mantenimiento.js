
var colsName = ['ID', 'NOMBRE', 'APELLIDO', 'TIPO DOCUMENTO','NUMERO DOCUMENTO','NOMBRE DE GENERO', 'FECHA'];
var visitanteList = [];

const llenarVariable = (lista, option) => {
    let newArray;
    switch (option) {
        case 'new':
            for (i = 0; i < lista.length; i++) {
                visitanteList.push(lista[i]);
            }
            break;
        case 'edit':
            console.log("edit");
            newArray = visitanteList.map(visitante => visitante.id_visitante == lista[0].id_visitante ? lista[0] : visitante)

            visitanteList = newArray;
            break;

    }
}

const init = () => {
    showLoading();
    setColumns("example", colsName, true);

    getListaVisitante();
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
            getVisitanteId(id)
            break;

    }
};

const getListaVisitante = () => {
    $.ajax({
        method: "GET",
        url: urlGetVisitante,
        responseType: 'json',
        success: async function (res) {
            
            Swal.close();
            if (res.oHeader.estado) {
                await llenarVariable(res.VisitanteList, 'new');
                await listTable(visitanteList);

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
        columns: [{ data: "id_visitante" },
            { data: "nombre" },
            { data: "apellidos" },
            { data: "nombre_tipo" },
            { data: "nro_documento" },
            { data: "nombre_genero" },
        { data: "fecha_creacion", render: function (data) { return convertFecha(data) } },
        buttonsDatatTable("edit")],
        rowId: "id_visitante",
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
    let id_visitante = document.getElementsByName("id_visitante")[0];

    if (id_visitante.value == "" || id_visitante.value == undefined) {
        id_visitante.value = 0;
    }

    e.preventDefault();
    let formData = {};
    let validate = true;
    $("#view-form .cf").each(function (index) {
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
            url: urlSaveVisitante,
            data: formData,
            responseType: 'json',
            success: async function (res) {
                console.log(res);
                Swal.close();
                let { VisitanteList, oHeader } = res;
                if (oHeader.estado) {
                    if (id_visitante.value == "0") {
                        await llenarVariable(VisitanteList, 'new');
                    } else {
                        await llenarVariable(VisitanteList, 'edit');
                    }

                    await listTable(visitanteList);
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

const getVisitanteId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetVisitante + "?id_visitante=" + id,
        responseType: 'json',
        success: async function (res) {
            let {VisitanteList, oHeader } = res;
            if (oHeader.estado) {
                await llenarCampos(VisitanteList);
            } else {
                Swal.fire('Ooops!', res.oHeader.mensaje, 'error');
            }
            
        },
        error: function (err) {
            Swal.close();
        }

    });
}

$(".val").click(function (e) {
    this.classList.remove("border-danger");
    (this.parentElement).lastElementChild.classList.add("d-none");
});



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



