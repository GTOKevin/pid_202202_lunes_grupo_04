
var colsName = ['ID', 'NOMBRE', 'DESCRIPCION','FECHA'];
var sucursalList=[];

const llenarVariable = (lista, option) => {
    let newArray;
    switch (option) {
        case 'new':
            for (i = 0; i < lista.length; i++) {
                sucursalList.push(lista[i]);
            }
            break;
        case 'edit':
            console.log("edit");
            newArray = sucursalList.map(sucursal => sucursal.id_sucursal == lista[0].id_sucursal ? lista[0] : sucursal)
            sucursalList = newArray;
            break;

    }
}

const init = () => {
    showLoading();
    setColumns("example", colsName, true);

    getListaSucursal();
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
            getSucursalId(id);
            
            break;

    }
};

const getListaSucursal = () => {
    $.ajax({
        method: "GET",
        url: urlGetSucursal,
        responseType: 'json',
        success: async function (res) {
            console.log(res);
            Swal.close();
            if (res.oHeader.estado) {
                await llenarVariable(res.SucursalList, 'new');
                await listTable(sucursalList);
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
        destroy:true,
        data: res,
        columns: [{ data: "id_sucursal" }, { data: "nombre" }, { data: "descripcion" } ,
        { data: "fecha_creacion", render: function (data) { return convertFecha(data) } },
        buttonsDatatTable("edit")],
        rowId: "id_sucursal",
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
    e.preventDefault();

    let id_sucursal = document.getElementsByName("id_sucursal")[0];

    if (id_sucursal.value == "" || id_sucursal.value == undefined) {
        id_sucursal.value = 0;
    }

 

    const { formData, validate } = validar();

    if (validate) {
        showLoading();
        $.ajax({
            method: "POST",
            url: urlSaveSucursal,
            data: formData,
            responseType: 'json',
            success: async function (res) {
                Swal.close();
       
                let { SucursalList, oHeader } = res;
                if (oHeader.estado) {
                    if (id_sucursal.value == "0") {
                        await llenarVariable(SucursalList, 'new');
                    } else {
                        await llenarVariable(SucursalList, 'edit');
                  
                    }
                    
                    await listTable(sucursalList);
                    Swal.fire('ok', oHeader.mensaje, 'success');
                    $("#view-form").hide(500);
                    $("#view-table").show(1000);
                }

            },
            error: function (err) {
                Swal.close();
            }
        });

    }

 

});

const getSucursalId = (id) => {
    $.ajax({
        method: "GET",
        url: urlGetSucursal +"?id_sucursal="+id,
        responseType: 'json',
        success: async function (res) {
            let { SucursalList, oHeader } = res;
            if (oHeader.estado) {
                await llenarCampos(SucursalList);
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


const validar = () => {
    let formData = { formData: {}, validate:true }
    $("#view-form input").each(function (index) {
        if (this.value.trim().length != 0) {
            formData.formData[this.name] = this.value;
        } else {
            formData.validate = false;
            this.classList.add("border-danger");
            (this.parentElement).lastElementChild.classList.remove("d-none");
        }
    });

    return formData;
}

$(".val").click(function (e) {
    this.classList.remove("border-danger");
    (this.parentElement).lastElementChild.classList.add("d-none");
});


