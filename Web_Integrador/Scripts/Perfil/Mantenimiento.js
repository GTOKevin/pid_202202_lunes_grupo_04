
const getPerfil = () => {
    $.ajax({
        method: "GET",
        url: urlGetPerfil,
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


const init = () => {
    showLoading();
    setTimeout(function () {
        getPerfil();
    },500)
    Swal.close();
};






const validarFormCreate = () => {
    let clave = document.getElementsByName("clave")[0];
    let clave2 = document.getElementsByName("clave2")[0];

    if (clave.value.trim().length < 5) {
        return false;
    }
    if (clave.value != clave2.value) {
        return false;
    }

    return true;

}

$("#form-create-editpass").on('submit', function (e) {
    e.preventDefault();

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
                    Swal.fire('ok', oHeader.mensaje, 'success');
                    document.getElementById("form-create-editpass").reset();
                  
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

});



$("#form-create-perfil").on("submit", function (e) {
    let id_perfil = document.getElementsByName("id_perfil")[0];

    if (id_perfil.value == "" || id_perfil.value == undefined) {
        id_perfil.value = 0;
    }

    e.preventDefault();
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
            url: urlEditPerfil,
            data: formData,
            responseType: 'json',
            success: async function (res) {
                Swal.close();
                let { ListaUsuarioP, oHeader } = res;
                if (oHeader.estado) {                 
                    Swal.fire('ok', oHeader.mensaje, 'success');
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
});


init();



