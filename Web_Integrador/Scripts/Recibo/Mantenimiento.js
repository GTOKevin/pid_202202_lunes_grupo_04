
var colsName = ['BOLETA','MONTO', 'PROPIETARIO', 'SERVICIO', 'FECHA DE PAGO', 'ESTADO'];
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
    setColumnsRecibo("example", colsName, true);

    getListaRecibo();
    Swal.close();
};



const setColumnsRecibo=(dt, cols, btn)=> {
    let t = "";
    t += "<thead>";
    for (var i = 0; i < cols.length; i++) {

        t += "<th>";
        t += cols[i];
        t += "</th>";
    }
    if (btn) {
        t += "<th></th>";
    }
    t += "</thead><tbody></tbody>";
    $("#" + dt).html(t);
}
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
    $('#example').DataTable({
        destroy: true,
        data: res,       
        columns: [{ data: "id_recibo" },
        { data: "monto" },
            { data: "oPropietario", render: function (data) { return data.nombres + " " + data.primer_apellido + " " +data.segundo_apellido }  },
        { data: "servicio" },
        { data: "fecha_pago", render: function (data) { return FechaDate(data) } },
        { data: "estado", render: function (data) { return buttonsReciboDatatTable(data) } },
        ],
        rowId: "id_recibo",
       
        columnDefs:
            [
                
                {
                    "targets": 0,
                    "visible": true,
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
            this.classList.add("border-danger");
            validate = false;
        }

    });
    $("#view-form select").each(function (index) {
        if (this.name == "id_cliente") {
            formData[this.name] = this.value;
        } else {
            if (this.value.trim().length != 0) {
                formData[this.name] = this.value;
            } else {
                this.classList.add("border-danger");
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
                Swal.close();
                console.log(res);
          
                let { ReciboList, oHeader } = res;
                if (oHeader.estado) {  
                    $("#view-form")[0].reset();
                    $("#closeModal").click();
                    await llenarVariable(ReciboList, 'new');


                    await listTable(reciboList);
                    Swal.fire('ok', oHeader.mensaje, 'success');

                } else {
                    $("#view-form")[0].reset();
                    $("#closeModal").click();
                    Swal.fire('Info', oHeader.mensaje, 'info');
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



const obtenerRecibosFiltro = () => {
    let dni = document.getElementById("dni");
    let nombre = document.getElementById("nombre");
    let servicio = document.getElementById("servicio");
    let estado = document.getElementById("estado");

    console.log(dni.value, nombre.value, servicio.value, estado.value)
    //public JsonResult ListarFiltro
    $.ajax({
        method: "GET",
        url: urlListarFiltro + "?dni=" + dni.value + "&nombre=" + nombre.value + "&servicio=" + servicio.value + "&estado=" + estado.value,
        responseType: 'json',
        success: async function (res) {
            let { ReciboList, oHeader } = res;
            if (oHeader.estado) {
                await listTable(ReciboList);
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




const buttonsReciboDatatTable = (opcion) => {
    let buttonJson = ``;
    switch (opcion) {
        case true:
            buttonJson =`
                <div class="w-100">
                <button type='button' class='btn btn-sm btn-success' style='padding:2px 4px;'>
                Pagado
                </button>
                 </div>
                `;
            
            break;
        case false:
            buttonJson=`
                <div class="w-100">
                <button type='button' onclick='PagarRecibo(this,"1");'  class='btn btn-sm btn-danger' style='padding:2px 4px;'>
                 No Pagado
                </button>
                </div>
                `;
            break;

    }

    return buttonJson;
}


const PagarRecibo = (btn, valor) => {
    let elemento = ((btn.parentElement).parentElement).parentElement;
    let id = elemento.id;
    let state = valor;

    if (id != "" && estado) {

        $.ajax({
            method: "POST",
            url: urlPagarRecibo,
            data: { id_recibo: id, estado: state },
            responseType: 'json',
            success: async function (res) {
                Swal.close();
                let { ReciboList, oHeader } = res;
                if (oHeader.estado) {
                    await construirPagado(elemento);
                    Swal.fire("Ok!!", "Se ha pagado la Boleta " + id, "success");
                }

            },
            error: function (err) {
                Swal.close();
            }
        });

    }  
}

const construirPagado = (elemento) => {
    let divBtn = elemento.children[5].children[0];
    divBtn.removeChild(divBtn.children[0]);

    let botonAprobado = `<button type='button' class='btn btn-sm btn-success' style='padding:2px 4px;'>
                         Pagado
                         </button>`;

    divBtn.innerHTML = botonAprobado;
}