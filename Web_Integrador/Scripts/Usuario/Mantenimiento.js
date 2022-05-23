var colsName = ['ID', 'USERNAME', 'FECHA REGISTRO', 'ROL', 'ID_PERFIL','ESTADO'];
var usuarioList = [];
var idEstado = "";

const Formulario = document.getElementById('form-create');
const ClearClassInput = document.querySelectorAll('#BodyInptus .swValidI');
const ClearClassCombo = document.querySelectorAll('#BodyInptus .swValidC');
const ClearErrorMess = document.querySelectorAll('#BodyInptus .label-error')

const ClearValues = () => {
    ClearClassInput.forEach((inputs) => {
        inputs.classList.remove('border-danger');
    });
    ClearClassCombo.forEach((select) => {
        select.classList.remove('border-danger');
    });
    ClearErrorMess.forEach((error) => {
        error.classList.add('d-none');
    });
}


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
        case 'cancel-estado':
            $("#form-usuario-estado").hide(500);
            $("#view-table").show(1000);
            break;
        case 'edit':
            $("#view-table").hide(500);
            $("#form-create-perfil").show(1000);
            $("#form-create-editpass").show(1000);
            let id = ((t.parentElement).parentElement).parentElement.id;
            getUsuariolId(id);
            getPerfilId(id);
            break;
        case 'est_1':
            $("#view-table").hide(500);
            $("#form-usuario-estado").show(1000);
            let id2 = ((t.parentElement).parentElement).parentElement.id;
            getUsuariolId(id2);
            idEstado = "1";
            break;
        case 'est_2':
            $("#view-table").hide(500);
            $("#form-usuario-estado").show(1000);
            let id3 = ((t.parentElement).parentElement).parentElement.id;
            getUsuariolId(id3);
            idEstado = "2";
            break;
        case 'est_3':
            $("#view-table").hide(500);
            $("#form-usuario-estado").show(1000);
            let id4 = ((t.parentElement).parentElement).parentElement.id;
            getUsuariolId(id4);
            idEstado = "3";
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
        destroy:true,
        data: res,
        columns: [{ data: "id_usuario" }, { data: "username" },
            { data: "fecha_registro", render: function (data) { return convertFecha(data) } }, { data: "nombre_rol" },
            { data: "nombre_perfil" }, { data: "nombre_estado"}, 
            buttonsDatatTable("mantUsP")],
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

$("#form-create").on('submit', function (e) {
    e.preventDefault();

    if (swCamposValid.SwUsername && swCamposValid.SwContrasenia1) {
        if (validarFormCreate()) {
            showLoading();
            let formData = {};
            $('.creat').each(function (e) {
                formData[this.name] = this.value;
            });

            $.ajax({
                method: "POST",
                url: urlCreateUser,
                responseType: 'json',
                data: formData,
                success: async function (res) {
                    Swal.close();
                    let { UsuarioList, oHeader } = res;
                    if (oHeader.estado) {
                        await llenarVariable(UsuarioList, 'new');
                        await listTable(usuarioList);
                        Swal.fire('ok', oHeader.mensaje, 'success');
                        ClearValues();
                        document.getElementById("form-create").reset();
                        $("#form-create").hide(500);
                        $("#form-create-perfil").show(1000);
                        document.getElementsByName("id_perfil")[0].value = res.UsuarioList[0].id_perfil;
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: res.oHeader.mensaje
                        });
                    }
                },
                error: function (err) {
                    Swal.close();
                    console.log(err);
                }

            })
        }
    }

    else {
        Swal.fire({
            icon: 'error',
            title: 'ERROR EN EL FORMULARIO',
            text: 'Ingrese datos Correctos'
        });
    }

});

const getPerfilId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetPerfil + "?id=" + id,
        responseType: 'json',
        success: async function (res) {
            let { Lista_Perfiles, oHeader } = res;
            if (oHeader.estado) {
                await llenarCampos(Lista_Perfiles);
            } else {
                Swal.fire('Ooops!', res.oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            Swal.close();
        }

    });
}

const getUsuariolId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetUsuario + "?id=" + id,
        responseType: 'json',
        success: async function (res) {
            let { UsuarioList, oHeader } = res;
            if (oHeader.estado) {
                await llenarCamposUs(UsuarioList);
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
                    if (this.name === 'fecha_nacimiento') {
                        this.value = convertFechav2(list[0]['fecha_nacimiento']);
                    }
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

const llenarCamposUs = (list) => {

    if (list.length > 0) {
        $("#form-usuario-estado input").each(function (ind) {
            for (var propName in list[0]) {
                if (this.name === propName) {
                    this.value = list[0][propName];
                    
                }
            }

        });
        $("#form-create-editpass input").each(function (ind) {
            for (var propName in list[0]) {
                if (this.name === propName) {
                    this.value = list[0][propName];

                }
            }

        });
    }
}



$("#form-create-perfil").on("submit", function (e) {
    let id_perfil = document.getElementsByName("id_perfil")[0];

    if (id_perfil.value == "" || id_perfil.value == undefined) {
        id_perfil.value = 0;
    }

    e.preventDefault();
    if (
        swCamposValid.swNombre && swCamposValid.swApellidoP && swCamposValid.swApellidoM &&
        swCamposValid.swDocumento && swCamposValid.swTipoDocumento && swCamposValid.swFecha &&
        swCamposValid.swGenero && swCamposValid.swNacionalidad && swCamposValid.swDireccion
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
                url: urlSavePerfil,
                data: formData,
                responseType: 'json',
                success: async function (res) {
                    Swal.close();
                    let { ListaUsuarioP, oHeader } = res;
                    if (oHeader.estado) {
                        if (id_perfil.value == "0") {
                            await llenarVariable(ListaUsuarioP, 'new');
                        } else {
                            await llenarVariable(ListaUsuarioP, 'edit');
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


$("#form-usuario-estado").on("submit", function (e) {

    document.getElementsByName("id_estado")[0].value = idEstado;

    let id_usuario = document.getElementsByName("id_usuario")[0];

    if (id_usuario.value == "" || id_usuario.value == undefined) {
        id_usuario.value = 0;
    }

    e.preventDefault();
    let formData = {};
    let validate = true;
    $("#form-usuario-estado input").each(function (index) {
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
            url: urlSaveEstado,
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

                }

            },
            error: function (err) {
                Swal.close();
            }
        });

    }


    $("#form-usuario-estado").hide(500);
    $("#view-table").show(1000);

});






init();



