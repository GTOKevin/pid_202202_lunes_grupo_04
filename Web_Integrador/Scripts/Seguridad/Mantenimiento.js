
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
                    info += "<li style='list-style:none;'> <strong>Genero: </strong> " + Lista_Perfiles[i].genero + "</li>";
                    info += "<li style='list-style:none;'> <strong>Fecha Nacimiento: </strong> " + convertFecha(Lista_Perfiles[i].fecha_nacimiento) + "</li>";
                    info += "<li style='list-style:none;'> <strong>Nro de Documento: </strong> " + Lista_Perfiles[i].nro_documento + "</li>";
                    info += "<li style='list-style:none;'> <strong>Nacionalidad: </strong> " + Lista_Perfiles[i].nacionalidad + "</li>";
                    info += "<li style='list-style:none;'> <strong>Direccion: </strong> " + Lista_Perfiles[i].direccion + "</li>";
                }
                var divinfo = document.getElementById('mis_datos');
                divinfo.innerHTML = info;

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



if (myJsVariable != null) {
    window.alert(myJsVariable);
}


init();



