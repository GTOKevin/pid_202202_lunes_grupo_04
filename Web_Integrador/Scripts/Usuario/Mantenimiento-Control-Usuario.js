var colsName = ['ID', 'USERNAME', 'FECHA REGISTRO', 'ROL', 'ID_PERFIL', 'ESTADO'];
var usuarioList = [];
var idEstado = "";

const Formulario = document.getElementById('form-create-perfil');


const llenarVariable = (lista, option) => {
    let newArray;
    switch (option) {
        case 'new':
            for (i = 0; i < lista.length; i++) {
                usuarioList.push(lista[i]);
            }
            break;
        case 'edit':
            console.log("edit");
            newArray = usuarioList.map(usuario => usuario.id_perfil == lista[0].id_perfil ? lista[0] : usuario)
            usuarioList = newArray;
            break;

    }
}

const init = () => {
    showLoading();
    setColumns("tblUsuario", colsName, true);

    setTimeout(function () {
        getListaUsuario();
    }, 500)
    Swal.close();
};

const btnAction = (t, tipo) => {
    switch (tipo) {
        case 'new':
            cleanForm();
            $("#view-table").hide(500);
            $("#form-create").show(1000);
            break;
        case 'cancel':
            $("#form-create").hide(500);
            $("#view-table").show(1000);
            Formulario.reset();
            ClearValues();
            break;
        case 'cancel-perfil':
            $("#form-create-perfil").hide(500);
            $("#view-table").show(1000);
            Formulario.reset();
            ClearValues();
            break;
        case 'edit':
            $("#view-table").hide(500);
            $("#form-create-perfil").show(1000);
            $("#form-create-editpass").show(1000);
            let id = ((t.parentElement).parentElement).parentElement.id;
            getUsuariolId(id);
            break;
    }
};

const getListaUsuario = () => {
    $.ajax({
        method: "GET",
        url: urlGetUsuario,
        responseType: 'json',
        success: async function (res) {
            Swal.close();
            if (res.oHeader.estado) {
                await llenarVariable(res.UsuarioList, 'new');
                await listTable(usuarioList);
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
    $('#tblUsuario').DataTable({
        destroy: true,
        data: res,
        columns: [{ data: "id_usuario" }, { data: "username" },
        { data: "fecha_registro", render: function (data) { return convertFecha(data) } }, { data: "nombre_rol" },
        { data: "nombre_perfil" }, { data: "nombre_estado" },
        buttonsDatatTable("edit")],
        rowId: "id_usuario",
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

const validarFormCreate = () => {
    let username = document.getElementsByName("username")[0];
    let clave = document.getElementsByName("clave")[0];
    let clave2 = document.getElementsByName("clave2")[0];

    if (username.value.trim().length === 0) {
        return false;
    }
    if (clave.value.trim().length < 5) {
        return false;
    }
    if (clave.value != clave2.value) {
        return false;
    }

    return true;

}

$("#form-create-perfil").on("submit", function (e) {
    let id_usuario = document.getElementsByName("id_usuario")[0];

    if (id_usuario.value == "" || id_usuario.value == undefined) {
        id_usuario.value = 0;
    }

    e.preventDefault();
    if (
        swCamposValid.swRoles
    ) {
        let formData = {};
        let validate = true;
        $("#form-create-perfil input").each(function (index) {
            if (this.value.trim().length != 0) {
                formData[this.name] = this.value;
            } else {
                validate = false;
            }

        });
        $("#form-create-perfil select").each(function (index) {
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
                url: urlSaveUsuario,
                data: formData,
                responseType: 'json',
                success: async function (res) {
                    Swal.close();
                    let { UsuarioList, oHeader } = res;
                    if (oHeader.estado) {
                        if (id_usuario.value == "0") {
                            await llenarVariable(UsuarioList, 'new');
                        } else {
                            await llenarVariable(UsuarioList, 'edit');
                        }
                        await listTable(usuarioList);
                        Swal.fire('ok', oHeader.mensaje, 'success');
                        ClearValues();
                        document.getElementById("form-create-perfil").reset();
                    }
                    else {
                        Swal.fire('Error', 'Error al cambiar los datos', 'error');
                    }

                },
                error: function (err) {
                    Swal.close();
                }
            });

        }
        else {
            Swal.fire('ERROR EN EL FORMULARIO', 'error en los datos del formulario', 'error');
        }

        $("#form-create-perfil").hide(500);
        $("#view-table").show(1000);
    }
    else {
        Swal.fire('ERROR EN EL FORMULARIO', 'error en los datos del formulario', 'error');
    }
});



const getUsuariolId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetUsuario + "?id=" + id,
        responseType: 'json',
        success: async function (res) {
            let {UsuarioList, oHeader } = res;
            if (oHeader.estado) {
                await llenarCampos(UsuarioList);
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
        $("#form-create-perfil input").each(function (ind) {
            for (var propName in list[0]) {
                if (this.name === propName) {
                    this.value = list[0][propName];
                }
            }

        });
        $("#form-create-perfil select").each(function (ind) {
            for (var propName in list[0]) {
                if (this.name === propName) {
                    this.value = list[0][propName];

                }


            }

        });
    }
}




init();



