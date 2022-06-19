
var colsName = ['ID','MONTO', 'PROPIETARIO', 'SERVICIO', 'FECHA DE PAGO', 'ESTADO'];
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
        { data: "monto" },
        { data: "id_departamento" },
        { data: "servicio" },
        { data: "fecha_pago", render: function (data) { return FechaDate(data) }  },
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
    //let id_recibo = document.getElementsByName("id_recibo")[0];

    //if (id_recibo.value == "" || id_recibo.value == undefined) {
    //    id_recibo.value = 0;
    //}

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
        if (this.name == "id_cliente" || this.name=="servicio") {
            formData[this.name] = this.value;
        } else {
            if (this.value.trim().length != 0) {
                formData[this.name] = this.value;
            } else {
                validate = false;
            }
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
                        //await llenarVariable(ReciboList, 'new');
                    } else {
                        //await llenarVariable(ReciboList, 'edit');
                    }

                    //await listTable(reciboList);
                    //Swal.fire('ok', oHeader.mensaje, 'success');
                    //await mostrarTabla();
                }

            },
            error: function (err) {
                Swal.close();
            }
        });

    }


});
const agregarProp = () => {
    let { boleta, estado } = obtenerProp();
    if (estado) {
        listaTableData(boleta);
        $("#closeModal")[0].click();
    }
}


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



const getReciboId = () => {
    let dni = document.getElementById("dni");
    let nombre = document.getElementById("nombre");
    let servicio = document.getElementById("servicio")[0];
    let estado = document.getElementById("estado")[0];
    //public JsonResult ListarFiltro
    $.ajax({
        method: "GET",
        url: urlGetRecibo + "?dni=" + dni.value + "&nombre=" + nombre.value + "&servicio=" + servicio.value + "&estado=" + estado.value,
        responseType: 'json',
        success: async function (res) {
            let { recibo_list, oHeader } = res;
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

init();


$(".val").click(function (e) {
    this.classList.remove("border-danger");
});