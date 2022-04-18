
var info = "";

const getPerfil = () => {
    $.ajax({
        method: "GET",
        url: urlGetPerfil ,
        responseType: 'json',
        success: async function (res) {
            let { Lista_Perfiles, oHeader } = res;
            if (oHeader.estado) {
              

                for (let i in Lista_Perfiles) {
                    info += "<li style='list-style:none;'> <strong>Mi nombre: </strong> " + Lista_Perfiles[i].nombres + " " + Lista_Perfiles[i].primer_apellido + " " + Lista_Perfiles[i].segundo_apellido + "</li>";
                    info += "<li style='list-style:none;'> <strong>Genero: </strong> " + convertGenero(Lista_Perfiles[i].genero) + "</li>";
                    info += "<li style='list-style:none;'> <strong>Fecha Nacimiento: </strong> " + convertFecha(Lista_Perfiles[i].fecha_nacimiento) + "</li>";
                    info += "<li style='list-style:none;'> <strong>Nro de Documento: </strong> " + Lista_Perfiles[i].nro_documento + "</li>";
                    info += "<li style='list-style:none;'> <strong>Nacionalidad: </strong> " + convertNacionalidad(Lista_Perfiles[i].nacionalidad) + "</li>";
                    info += "<li style='list-style:none;'> <strong>Direccion: </strong> " + Lista_Perfiles[i].direccion + "</li>";
                }
                var divinfo = document.getElementById('mis_datos');
                divinfo.innerHTML = info;

            } else {
                Swal.fire('Ooops!', res.oHeader.mensaje, 'error');
            }
        },
        error: function (err) {
            Swal.close();
        }

    });
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

$("#form-create").on('submit', function (e) {
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
                    document.getElementById("form-create").reset();
                  
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


init();



