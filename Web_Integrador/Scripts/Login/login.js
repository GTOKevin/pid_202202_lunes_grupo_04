


const signUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container = document.getElementById('container');

signUpButton.addEventListener('click', () => {
    container.classList.add("right-panel-active");
});

signInButton.addEventListener('click', () => {
    container.classList.remove("right-panel-active");
});


const showLoading = () => {
    Swal.fire({
        title: 'Cargando !!',
        html:'Espere por favor...',
        showConfirmButton:false,
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
    if (clave.value.trim().length < 5) {
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

    if (validarFormCreate()) {


        showLoading();



        let formData = {};

        $('.creat').each(function (e) {
            formData[this.name] = this.value;
        });


        $.ajax({
            method: "POST",
            url: "/Auth/CrearUsuario",
            responseType: 'json',
            data: formData,
            success: function (res) {
                Swal.close();
                if (res.estado) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Exito..',
                        text: 'Usuario registrado!'
                    });
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
            
        })
    }

})

$("#form-login").on('submit', function (e) {
    e.preventDefault();


    let formData = {};

    if (validarFormLogin()) {


        showLoading();

        $('.log').each(function (e) {
            formData[this.name] = this.value;
        })


        $.ajax({
            method: "POST",
            url: "/Auth/LoginUsuario",
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
    
})

$(".psw").click(function () {
    let password = (this.parentElement).children[0];
    const type = password.getAttribute("type") === "password" ? "text" : "password";
    password.setAttribute("type", type);
    this.classList.toggle("fa-eye-slash");
});



