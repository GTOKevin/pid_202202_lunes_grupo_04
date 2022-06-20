var colsName = ['ID', 'NOMBRE', 'NRO DOCUMENTO', 'DESCRIPCION', 'ESTADO', '',''];
var incidentesList = [];

const llenarVariable = (lista, option) => {
    let newArray;
    switch (option) {
        case 'new':
            for (i = 0; i < lista.length; i++) {
                incidentesList.push(lista[i]);
            }
            break;
        case 'edit':
            newArray = incidentesList.map(incidente => incidente.id_incidente == lista[0].id_incidente ? lista[0] : incidente)
            incidentesList = newArray;
            break;
    }
}

const btnAction = (t, tipo) => {
    switch (tipo) {
        case 'modalhistorial':
            let id = ((t.parentElement).parentElement).parentElement.id;
            getIncidenteId(id);
            break;

    }
};

const init = () => {
    showLoading();
    setColumns("example", colsName, true);
    getListaTorre();
    Swal.close();
};

$("#FormFiltro").on("submit", function (e) {
    let estado = '';
    let selectEstado = document.querySelectorAll("input[name=inlineRadioOptions]");
    if (selectEstado[1].checked) {
        estado = '0';
    }
    if (selectEstado[2].checked) {
        estado = '1';
    }


    let formData = {estado_f: estado };
    $('.inFiltro').each(function (e) {
        formData[this.name] = this.value;
    });

    e.preventDefault();
        showLoading();
        $.ajax({
            method: "GET",
            url: urlGetIncidentes,
            data: formData,
            responseType: 'json',
            success: async function (res) {
                let { lista_Incidente, oHeader } = res
                Swal.close();
                if (oHeader.estado) {
                    incidentesList = [];
                    await llenarVariable(lista_Incidente, 'new');
                    await listTable(incidentesList);
                } else {
                    Swal.fire('Ooops!', oHeader.mensaje, 'error');
                }
            },
            error: function (err) {
                Swal.close();
            }
        });

    


});

$("#FormHistorial").on("submit", function (e) {

    let formData = {};
    $('.val').each(function (e) {
        formData[this.name] = this.value;
    });

    e.preventDefault();
    showLoading();
    let validate = true;
    $("#FormHistorial .val").each(function (index) {
        if (this.value.trim().length != 0) {
            formData[this.name] = this.value;
        } else {
            validate = false;
        }
    });
    if (swCamposValid.swAcciones) {
        if (validate) {
            $.ajax({
                method: "POST",
                url: urlSaveHistorialIncidente,
                data: formData,
                responseType: 'json',
                success: async function (res) {
                    Swal.close();
                    let { lista_Incidente, oHeader } = res
                    if (res.oHeader.estado) {
                        incidentesList = [];
                        getListaTorre();
                        $('#modalincidente').modal('hide');
                    } else {
                        Swal.fire('Ooops!', oHeader.mensaje, 'error');
                    }
                },
                error: function (err) {
                    Swal.close();
                }
            });
        }
        else {
            ValidNull();
            Swal.close();
        }
    }
    else {
        $('#modalincidente').modal('hide');
        Swal.fire('Ooops!', 'Error en Formulario', 'error');
    }

});

const getListaTorre = () => {
    $.ajax({
        method: "GET",
        url: urlGetIncidentes,
        responseType: 'json',
        success: async function (res) {
            let { lista_Incidente, oHeader } = res
            Swal.close();
            if (oHeader.estado) {
                await llenarVariable(lista_Incidente, 'new');
                await listTable(incidentesList);
            } else {
                Swal.fire('Ooops!', oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            Swal.close();
        }

    });
}

const getIncidenteId = (id) => {
    let formData = { id_incidente_f: id };
    $.ajax({
        method: "GET",
        url: urlGetIncidentes,
        data: formData,
        responseType: 'json',
        success: async function (res) {
            let { lista_Incidente, oHeader } = res
            if (oHeader.estado) {
                await llenarCampos(lista_Incidente);
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
        columns: [{ data: "id_incidente" },
        { data: "nombre_reportado" },
        { data: "nro_documento" },
        { data: "descripcion" },
        { data: "estado", render: function (data) { return data ? "Resuelto" : "No Resuelto" } },
        {
            data: null,
            title: "Surcusal y Secto",
            render: function (data, type, row) {
                return row['sucursal'] + ' -- ' + row['sector'];
            }
        },
        {
            data: null,
            title: "Torre y Dep.",
            render: function (data, type, row) {
                return ' Nro.Torre: ' + row['torre'] + ' Nro.Dep: ' + row['departamento'];
            }
        },
        {
            data: "estado", render: function (data) { return data ? null : buttonsDatatTable("mantIncidentesH").defaultContent } 
            }
            ],
        rowId: "id_incidente",
        columnDefs:
            [
                {
                    "targets": 0,
                    "visible": false,
                }
            ],
        order: [[0, 'des']],
        rowCallback: function (row, data) {
            if (!data.estado) {
                $(row).addClass('table-danger');
            }
            else {
                $(row).addClass('table-success');
               
            }
        },
        language: {
            "url": "//cdn.datatables.net/plug-ins/1.10.16/i18n/Spanish.json"
        }
    });
};


const llenarCampos = async (list) => {
    if (list.length > 0) {
        $(".val").each(function (ind) {
            if (this.name == "nro_documento" || this.name == "nombre_reportado" || this.name == "descripcion") {
                this.disabled = true;
            }
            let titulo = document.getElementById('tituloModal');
            for (var propName in list[0]) {
                if (this.name === propName) {
                    this.value = list[0][propName];
                }
                if (propName === "departamento") {
                    titulo.innerHTML = "Incidente del departamento Nro: " + list[0][propName];
                }
            }
        });
    }


}

init();