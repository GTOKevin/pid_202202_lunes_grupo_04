


const signUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container = document.getElementById('container');

const formulario = document.getElementById('form-create');
const inputs = document.querySelectorAll('#form-create input');
const inputsL = document.querySelectorAll('#form-login input');

const expresiones = {
    username: /^[a-zA-Z0-9\_\-]{4,25}$/,
    contra: /^[\w@ñ.]{6,50}$/
}

const campos = {
    username: true,
    clave: true,
    usernameL: true,
    claveL: true
}

const validarFormulario = (e) => {
    switch (e.target.name) {
        case "username":
            validarCampos(expresiones.username, e.target, 'username');
            break;
        case "clave":
            validarCampos(expresiones.contra, e.target, 'clave');
            validarContraseñas();
            break;
        case "clave2":
            validarContraseñas();
            break;
    }
}

const validarFormularioL = (e) => {
    switch (e.target.name) {
        case "username":
            validarCampos(expresiones.username, e.target, 'usernameL');
            break;
        case "clave":
            validarCampos(expresiones.contra, e.target, 'claveL');
            break;
    }
}

const validarContraseñas = () => {
    const inputPass = document.getElementById('pass1');
    const inputPass2 = document.getElementById('pass2');

    if (inputPass.value !== inputPass2.value) {
        document.getElementById(`grupo_clave2`).classList.remove('formulario__grupo-correcto');
        document.getElementById(`grupo_clave2`).classList.add('formulario__grupo-incorrecto');
        document.querySelector(`#grupo_clave2 i`).classList.add('fa-times-circle');
        document.querySelector(`#grupo_clave2 i`).classList.remove('fa-check-circle');
        document.querySelector(`#grupo_clave2 .formulario__input-error`).classList.add('formulario__input-error-activo')
        campos['clave'] = false;
    }
    else {
        document.getElementById(`grupo_clave2`).classList.remove('formulario__grupo-incorrecto');
        document.getElementById(`grupo_clave2`).classList.add('formulario__grupo-correcto');
        document.querySelector(`#grupo_clave2 i`).classList.add('fa-check-circle');
        document.querySelector(`#grupo_clave2 i`).classList.remove('fa-times-circle');
        document.querySelector(`#grupo_clave2 .formulario__input-error`).classList.remove('formulario__input-error-activo')
        campos['clave'] = true;
    }
}

const validarCampos = (expresion, input, campo) => {
    if (expresion.test(input.value)) {
        document.getElementById(`grupo_${campo}`).classList.remove('formulario__grupo-incorrecto');
        document.getElementById(`grupo_${campo}`).classList.add('formulario__grupo-correcto');
        document.querySelector(`#grupo_${campo} i`).classList.add('fa-check-circle');
        document.querySelector(`#grupo_${campo} i`).classList.remove('fa-times-circle');
        document.querySelector(`#grupo_${campo} .formulario__input-error`).classList.remove('formulario__input-error-activo')
        campos[campo] = true;

    } else {
        document.getElementById(`grupo_${campo}`).classList.remove('formulario__grupo-correcto');
        document.getElementById(`grupo_${campo}`).classList.add('formulario__grupo-incorrecto');
        document.querySelector(`#grupo_${campo} i`).classList.add('fa-times-circle');
        document.querySelector(`#grupo_${campo} i`).classList.remove('fa-check-circle');
        document.querySelector(`#grupo_${campo} .formulario__input-error`).classList.add('formulario__input-error-activo')
        campos[campo] = false;
        console.log()
        
    }
}

inputs.forEach((input) => {
    input.addEventListener('keyup', validarFormulario);
    input.addEventListener('blur', validarFormulario);
});
inputsL.forEach((input) => {
    input.addEventListener('keyup', validarFormularioL);
    input.addEventListener('blur', validarFormularioL);
});



signUpButton.addEventListener('click', () => {
    container.classList.add("right-panel-active");
});

signInButton.addEventListener('click', () => {
    container.classList.remove("right-panel-active");
});


const showLoading = () => {
    Swal.fire({
        title: 'Cargando !!',
        html: 'Espere por favor...',
        showConfirmButton: false,
        allowOutsideClick: false,
        willOpen: () => {
            Swal.showLoading();
        }
    });
}

const validarFormCreate = () => {
    let username = document.getElementsByName("username")[0];
    let clave = document.getElementsByName("clave")[0];
    let clave2 = document.getElementsByName("clave2")[0];

    if (username.value.trim().length === 0) {
        return false;
    }
    if (clave.value.trim().length <= 5) {
        return false;
    }
    if (clave.value != clave2.value) {
        return false;
    }

    return true;

}

const validarFormLogin = () => {
    let respuesta = true;
    $(".log").each(function (e) {
        if (this.value.trim().length === 0) {
            respuesta = false;
        }
    });
    return respuesta;
}

$("#form-create").on('submit', function (e) {
    e.preventDefault();

    if (campos.username && campos.clave) {


        showLoading();

        let formData = {};

        $('.creat').each(function (e) {
            formData[this.name] = this.value;
        });


        if (validarFormCreate()) {
            $.ajax({
                method: "POST",
                url: urlCreateUser,
                responseType: 'json',
                data: formData,
                success: function (res) {
                    Swal.close();
                    if (res.estado) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Exito..',
                            text: 'Usuario registrado!'

                        }
                        ).then(function () {
                            container.classList.remove("right-panel-active");
                        });
                        var form = document.getElementById("form-create");
                        document.querySelectorAll('.formulario__grupo-correcto').forEach((icono) => {
                            icono.classList.remove('formulario__grupo-correcto');
                        });
                        form.reset();

                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: res.mensaje
                        });
                    }
                },
                error: function (err) {
                    Swal.close();
                    console.log(err);
                }

            });
        }
        else {
            Swal.fire('ERROR EN EL FORMULARIO', 'error en los datos del formulario', 'error');
        }
    }
    else {
        Swal.fire('ERROR EN EL FORMULARIO', 'error en los datos del formulario', 'error');
    }

})

$("#form-login").on('submit', function (e) {
    e.preventDefault();


    let formData = {};

    if (campos.usernameL && campos.claveL) {


        showLoading();

        $('.log').each(function (e) {
            formData[this.name] = this.value;
        })

        
        if (validarFormLogin()) {
            $.ajax({
                method: "POST",
                url: urlLogin,
                responseType: 'json',
                data: formData,
                success: function (res) {
                    Swal.close();
                    if (res.oHeader.estado) {
                        location.href = "/Home/Index";
                    } else {
                        Swal.fire('Ooops!', res.oHeader.mensaje, 'error');
                    }
                },
                error: function (err) {
                    Swal.close();
                    console.log(err);
                }

            });
        }
        else {
            Swal.fire('Ooops!', 'Error al Ingresar al Sistema', 'error');
        }
    }

})

$(".psw").click(function () {
    let password = (this.parentElement).children[0];
    const type = password.getAttribute("type") === "password" ? "text" : "password";
    password.setAttribute("type", type);
    this.classList.toggle("fa-eye-slash");
});



