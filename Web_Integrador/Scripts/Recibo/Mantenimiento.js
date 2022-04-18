
var colsName = ['ID', 'SERVICIO', 'MONTO', 'ESTADO', 'FECHA_PAGO', 'FECHA_VENCIMIENTO'];
var reciboList = [];

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
    showLoading();
    setColumns("example", colsName, true);

    getListaRecibo();
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
            console.log(res);
            Swal.close();
            if (res.oHeader.estado) {

                await llenarVariable(res.ReciboList, 'new');
                await listTable(reciboList);
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
    console.log(res);
    $('#example').DataTable({
        destroy: true,
        data: res,       
        columns: [{ data: "id_recibo" },
        { data: "nombre_servicio" },
        { data: "monto" },
        { data: "estado" },
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
    let { formData, formEstado } = setValData();


    if (formEstado) {
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

const getServicioId = (id) => {
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

const llenarCampos = (list) => {
    if (list.length > 0) {
        $(".val").each(function (ind) {
            for (var propName in list[0]) {
                if (this.name === propName) {
                    if (this.type == "date") {
                        this.value = FechaDate(list[0][propName]);
                    } else {
                        this.value = list[0][propName];
                    }
                  
                }
            }

        });
    }

}

init();

$(".val").click(function (e) {
    this.classList.remove("border-danger");
});