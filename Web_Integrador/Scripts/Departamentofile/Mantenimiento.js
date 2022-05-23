
var colsName = ['ID', 'SUCURSAL', 'SECTOR', 'TORRE', 'DEPARTAMENTO'];
var departamentoList = [];


const llenarVariable = (lista, option) => {
    let newArray;
    switch (option) {
        case 'new':
            for (i = 0; i < lista.length; i++) {
                departamentoList.push(lista[i]);
            }
            break;
        case 'edit':
            console.log("edit");
            newArray = departamentoList.map(departamento => departamento.id_departamento == lista[0].id_departamento ? lista[0] : departamento)
            departamentoList = newArray;
            break;
    }
}

const init = () => {
    showLoading();
    setColumns("example", colsName, true);
    setTimeout(function () {
        getListaTorre();
    }, 500
        )
   

    //getListaDepartamentoFile();
    Swal.close();
};

const listTable = (res) => {
    $('#example').DataTable({
        destroy: true,
        data: res,
        columns: [{ data: "id_departamento" },
        { data: "nombre_sucursal" },
        { data: "nombre_sector" },
        { data: "numero_torre" },
        { data: "numero" },
        buttonsDatatTable("edit")],
        rowId: "id_departamento",
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
const btnAction = (t, tipo) => {
    switch (tipo) {
        case 'new':
            cleanForm();
            cleanSelect();
            mostrarForm();
            break;
        case 'cancel':
            var body = "";
            var divimagen = document.getElementById('divimagen');
            divimagen.innerHTML = body;
            $("#form-file").hide(500);
            $("#view-table").show(1000);
            break;
        case 'edit':
            $("#view-table").hide(500);
            $("#form-file").show(1000);

            let id = ((t.parentElement).parentElement).parentElement.id;
            getDepartamentoId(id);
            getImagen(id);

            break;

    }
};

const getListaDepartamentoFile = () => {
    $.ajax({
        method: "GET",
        url: urlGetDepartamentoFile,
        responseType: 'json',
        success: async function (res) {
            Swal.close();
            if (res.oHeader.estado) {
                await llenarVariable(res.DepartamentofileList, 'new');
                await listTable(departamentofileList);
            } else {
                Swal.fire('Ooops!', res.oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            Swal.close();
        }

    });
}
const getListaTorre = () => {
    $.ajax({
        method: "GET",
        url: urlGetDepartamento,
        responseType: 'json',
        success: async function (res) {
            let { lista_Departamento, oHeader } = res
            Swal.close();
            if (oHeader.estado) {

                await llenarVariable(lista_Departamento, 'new');
                await listTable(departamentoList);
            } else {
                Swal.fire('Ooops!', oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            Swal.close();
        }

    });
}
const getDepartamentoId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetDepartamento + "?id_departamento=" + id,
        responseType: 'json',
        success: async function (res) {
            let { lista_Departamento, lista_Propietario, oHeader } = res;
            if (oHeader.estado) {
                await llenarCampos(lista_Departamento);
            } else {
                Swal.fire('Ooops!', res.oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            Swal.close();
        }

    });
}


const getImagen = (id) => {
    $.ajax({
        method: "GET",
        url: imagen + "?id_departamento=" + id,
        responseType: 'json',
        success: async function (res) {
            let { DepartamentoFileList, oHeader } = res;
            if (oHeader.estado) {
                var body = "";
                var divimagen = document.getElementById('divimagen');
                divimagen.innerHTML = body;
                for (let data in DepartamentoFileList) {
                    body += "<div class ='col-md-3 dimg'><img class='bimg' src='" + DepartamentoFileList[data].url_imagen +"' /></div> <br/>"
                }
                divimagen.innerHTML = body;
               
            } else {
                Swal.fire('Ooops!', res.oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            Swal.close();
        }

    });
}


$("#view-form").on("submit", function (e) {
    let id_departamento_file = document.getElementsByName("id_departamento_file")[0];

    if (id_departamento_file.value == "" || id_departamento_file.value == undefined) {
        id_departamento_file.value = 0;
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
            url: urlSaveDepartamentoFile,
            data: formData,
            responseType: 'json',
            success: async function (res) {
                Swal.close();
                let { DepartamentofileList, oHeader } = res;
                if (oHeader.estado) {
                    if (id_departamento_file.value == "0") {
                        await llenarVariable(DepartamentofileList, 'new');
                    } else {
                        await llenarVariable(DepartamentofileList, 'edit');
                    }

                    await listTable(departamentofileList);
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

const getDepartamentoFileId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetDepartamentoFile + "?id_departamento_file=" + id,
        responseType: 'json',
        success: async function (res) {
            let { DepartamentoFileList, oHeader } = res;
            if (oHeader.estado) {
                await llenarCampos(DepartamentoFileList);
            } else {
                Swal.fire('Ooops!', res.oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            Swal.close();
        }

    });
}

const llenarCampos = async (list) => {
    
    if (list.length > 0) {
        $(".val").each(function (ind) {
            for (var propName in list[0]) {
                if (this.name === propName) {
                    this.value = list[0][propName];
                }

            }
        });
    }
    }


init();
