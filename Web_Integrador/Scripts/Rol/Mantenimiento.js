
var colsName = ['ID', 'NOMBRE', 'DESCRIPCION'];
var rolList = [];

const formulario = document.getElementById('view-form');
const inputNom = document.querySelectorAll('#bodyinputs input');



const campos = {
    nombreID: false,
    descripcionID: false,
}

const validarFormulario = (e) => {
    switch (e.target.name) {
        case "nombre":
            validarCampos(expresiones.nombre, e.target, 'nombreID');
            break;
        case "descripcion":
            validarCampos(expresiones.descripcion, e.target, 'descripcionID');
            break;
    }
}

const validarCampos = (expresion, input, campo) => {
    if (expresion.test(input.value)) {
        document.querySelector(`#${campo} input`).classList.remove('border-danger');
        document.querySelector(`#${campo} p`).classList.add('d-none');
        campos[campo] = true;
    }
    else {
        document.querySelector(`#${campo} input`).classList.add('border-danger');
        document.querySelector(`#${campo} p`).classList.remove('d-none');
        campos[campo] = false;
    }
}

inputNom.forEach((input) => {
    input.addEventListener('keyup', validarFormulario);
    input.addEventListener('blur', validarFormulario);
});


const expresiones = {
    nombre: /^([A-Za-zÁÉÍÓÚáéíóúÑñ]\s?){3,50}$/,
   
    descripcion: /^([A-Za-zÁÉÍÓÚáéíóúÑñ]\s?){3,50}$/
}

const llenarVariable = (lista, option) => {
    let newArray;
    switch (option) {
        case 'new':
            for (i = 0; i < lista.length; i++) {
                rolList.push(lista[i]);
            }
            break;
        case 'edit':
            console.log("edit");
            newArray = rolList.map(rol => rol.id_rol == lista[0].id_rol ? lista[0] : rol)
            rolList = newArray;
            break;

    }
}

const init = () => {
    showLoading();
    setColumns("example", colsName, false);

    setTimeout(function () {getListaRol()}
        , 500);
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
            getRolId(id);

            break;

    }
};

const getListaRol = () => {
    $.ajax({
        method: "GET",
        url: urlGetRol,
        responseType: 'json',
        success: async function (res) {
            Swal.close();
            if (res.oHeader.estado) {
                await llenarVariable(res.RolList, 'new');
                await listTable(rolList);
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
        columns: [{ data: "id_rol" }, { data: "nombre" }, { data: "descripcion" }],
        rowId: "id_rol",
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
    let id_rol = document.getElementsByName("id_rol")[0];

    if (id_rol.value == "" || id_rol.value == undefined) {
        id_rol.value = 0;
    }


    e.preventDefault();


    if (campos.nombreID && campos.descripcionID) {



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
                url: urlSaveRol,
                data: formData,
                responseType: 'json',
                success: async function (res) {
                    Swal.close();
                    let { RolList, oHeader } = res;
                    console.log("Resp", res);
                    if (oHeader.estado) {
                        if (id_rol.value == "0") {
                            await llenarVariable(RolList, 'new');
                        }
                        else {
                            await llenarVariable(RolList, 'edit');
                        }
                        await listTable(rolList);
                        Swal.fire('ok', oHeader.mensaje, 'success');
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: oHeader.mensaje
                        })
                    }


                },
                error: function (err) {
                    Swal.close();
                }
            });

        }

        $("#view-form").hide(500);
        $("#view-table").show(1000);
    }

    

});

const getRolId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetRol + "?id_rol=" + id,
        responseType: 'json',
        success: async function (res) {
            let { RolList, oHeader } = res;
            if (oHeader.estado) {
                await llenarCampos(RolList);
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
