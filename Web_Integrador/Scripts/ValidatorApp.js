
const Inputs = document.querySelectorAll('#BodyInptus .swValidI');
const CombosPerfil = document.querySelectorAll('#BodyInptus .swValidC');


const ValidDatos = (e) => {
    InputsValid[e.target.name](e.target)
}

const InputsValid = {
    username: (target) => swValidarCampos(expresionesGlobales.username, target, 'SwUsername'),
    clave: (target) => { swValidarCampos(expresionesGlobales.contra, target, 'SwContrasenia1'), validarContraseñas('SwContrasenia2') },
    clave2: () => validarContraseñas('SwContrasenia2'),


    nombre: (target) => swValidarCampos(expresionesGlobales.onlyLetrasForm, target, 'swNombre'),
    unidad: (target) => { ValidCombo(target, 'swUnidad') },

    nombres: (target) => swValidarCampos(expresionesGlobales.onlyLetrasForm, target, 'swNombre'),
    'primer_apellido': (target) => swValidarCampos(expresionesGlobales.onlyLetrasForm, target, 'swApellidoP'),
    'segundo_apellido': (target) => swValidarCampos(expresionesGlobales.onlyLetrasForm, target, 'swApellidoM'),
    'fecha_nacimiento': (target) => swValidarCampos(expresionesGlobales.onlyFecha, target, 'swFecha'),
    'tipo_documento': (target) => { ValidCombo(target,'swTipoDocumento'), ValidTipoDocumento(target, 'swDocumento') },
    'nro_documento': (target) => { swValidarCampos(expresionesGlobales.onlyDni, target, 'swDocumento') },
    'id_rol': (target) => { ValidCombo(target, 'swRoles')},
    genero: (target) => { ValidCombo(target, 'swGenero') },
    nacionalidad: (target) => { ValidCombo(target, 'swNacionalidad') },
    direccion: (target) => swValidarCampos(expresionesGlobales.onlyDirrecion, target, 'swDireccion')
}

const swCamposValid = {
    SwUsername: true,
    SwContrasenia1: true,

    swNombre: true,
    swApellidoP: true,
    swApellidoM: true,
    swFecha: true,
    swTipoDocumento: true,
    swDocumento: true,
    swGenero: true,
    swNacionalidad: true,
    swDireccion: true,

    swRoles: true,
    swUnidad:true
}

const expresionesGlobales = {
    username: /^[a-zA-Z0-9\_\-]{3,25}$/,
    contra: /^[\w@ñ.]{4,50}$/,
    onlyLetrasForm: /^([A-Za-zÁÉÍÓÚáéíóúÑñ]\s?){3,50}$/,
    onlyFecha: /^([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))$/,
    onlyDni: /^([0-9]){8}$/,
    onlyOtherDocumentos: /^([0-9]){11}$/,
    onlyDirrecion: /^([A-Za-z0-9ÁÉÍÓÚáéíóúÑñ](\.?){1}\s?){1,100}$/
}

const swValidarCampos = (expresion, input, campo) => {
    if (expresion.test(input.value)) {
        document.querySelector(`#${campo} input`).classList.remove('border-danger');
        document.querySelector(`#${campo} p`).classList.add('d-none');
        swCamposValid[campo] = true;

    } else {
        document.querySelector(`#${campo} input`).classList.add('border-danger');
        document.querySelector(`#${campo} p`).classList.remove('d-none');
        swCamposValid[campo] = false;
    }
}

const validarContraseñas = (campo) => {
    const inputPass = document.getElementById('contra1');
    const inputPass2 = document.getElementById('contra2');

    if (inputPass.value !== inputPass2.value) {
        document.querySelector(`#${campo} input`).classList.add('border-danger');
        document.querySelector(`#${campo} p`).classList.remove('d-none');
        swCamposValid[campo] = false;
    }
    else {
        document.querySelector(`#${campo} input`).classList.remove('border-danger');
        document.querySelector(`#${campo} p`).classList.add('d-none');
        swCamposValid[campo] = true;
    }
}

const ValidCombo = (input, campo) => {
    if (input.value != "") {
        document.querySelector(`#${campo} select`).classList.remove('border-danger')
        document.querySelector(`#${campo} p`).classList.add('d-none');
        swCamposValid[campo] = true;
    }
    else {
        document.querySelector(`#${campo} select`).classList.add('border-danger')
        document.querySelector(`#${campo} p`).classList.remove('d-none');
        swCamposValid[campo] = false;
    }
}

const ValidTipoDocumento = (e,campo) => {
    const typeDocumento = document.getElementById('iddocumento');
    const opt = e.options[e.selectedIndex];
    const tipoDocu = opt.text.toLowerCase();
    console.log(tipoDocu);
    if (tipoDocu == "dni") {
        swValidarCampos(expresionesGlobales.onlyDni, typeDocumento, campo);
        typeDocumento.addEventListener('keyup', () => { swValidarCampos(expresionesGlobales.onlyDni, typeDocumento, campo); }, false);
    }
    else {
        swValidarCampos(expresionesGlobales.onlyOtherDocumentos, typeDocumento, campo);
        typeDocumento.addEventListener('keyup', () => { swValidarCampos(expresionesGlobales.onlyOtherDocumentos, typeDocumento, campo); }, false);
    }

}

Inputs.forEach((input) => {
    input.addEventListener('keyup', ValidDatos)
    input.addEventListener('blur', ValidDatos)
    input.addEventListener('change', ValidDatos)
});

CombosPerfil.forEach((select) => {
    select.addEventListener('change', ValidDatos)
});