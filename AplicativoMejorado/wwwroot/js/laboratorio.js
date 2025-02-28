window.onload = function () {
    listarLaboratorios();
};

function filtrarLaboratorios() {
    let nombre = get("txtLaboratorio");
    if (nombre == "") {
        listarLaboratorios();
    } else {
        objLaboratorios.url = "Laboratorio/filtrarLaboratorios/?nombre=" + nombre;
        pintar(objLaboratorios);
    }
}

let objLaboratorios;

async function listarLaboratorios() {
    objLaboratorios = {
        url: "Laboratorio/listarLaboratorios",
        cabeceras: ["ID Laboratorio", "Nombre", "Dirección"],
        propiedades: ["idLaboratorio", "nombre", "direccion"],
        editar: true,
        eliminar: true,
        propiedadId: "idLaboratorio"
    }
    pintar(objLaboratorios);
}


function BuscarLaboratorio() {
    let forma = document.getElementById("frmBusqueda");
    let frm = new FormData(forma);
    fetchPost("Laboratorio/filtrarLaboratorios", "json", frm, function (res) {
        document.getElementById("divTable").innerHTML = generarTabla(res);
    });
}

function LimpiarLaboratorio() {
    LimpiarDatos("frmGuardarLaboratorio");
    listarLaboratorios();
}

function GuardarLaboratorio() {
    let forma = document.getElementById("frmGuardarLaboratorio");
    let frm = new FormData(forma);
    fetchPost("Laboratorio/GuardarLaboratorio", "text", frm, function (res) {
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Laboratorio agregado'
        });
        toastr.success("Laboratorio agregado.");
        listarLaboratorios();
        LimpiarDatos("frmGuardarLaboratorio");
        $('#modalLaboratorio').modal('hide');
    });
}

function Editar(id) {
    fetchGet("Laboratorio/recuperarLaboratorio/?idLaboratorio=" + id, "json", function (data) {
        setN("idLaboratorio", data.idLaboratorio);
        setN("nombre", data.nombre);
        setN("direccion", data.direccion);   
        $('#modalLaboratorio').modal('show');
    });
}

function GuardarCambiosLaboratorio() {
    let forma = document.getElementById("frmGuardarLaboratorio");
    let frm = new FormData(forma);

    Swal.fire({
        title: "¿Guardar cambios?",
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: "Guardar",
        denyButtonText: `No guardar`
    }).then((result) => {
        if (result.isConfirmed) {
            fetchPost("Laboratorio/GuardarCambiosLaboratorio", "text", frm, function (res) {
                Swal.fire({
                    icon: 'success',
                    title: 'Éxito',
                    text: 'Laboratorio modificado'
                });
                toastr.success("Laboratorio modificado");
                listarLaboratorios();
                LimpiarDatos("frmGuardarLaboratorio");
                $('#modalLaboratorio').modal('hide');
            });
        } else if (result.isDenied) {
            Swal.fire("Cambios no guardados", "", "info");
        }
    });
}

function Eliminar(id) {
    Swal.fire({
        title: "¿Estás seguro?",
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: "Sí, eliminar",
        denyButtonText: "No eliminar"
    }).then((result) => {
        if (result.isConfirmed) {
            fetchGet("Laboratorio/eliminarLaboratorio/?idLaboratorio=" + id, "json", function (res) {
                if (res.success) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Eliminado',
                        text: 'Laboratorio eliminado'
                    });
                    toastr.success("Laboratorio eliminado");
                    listarLaboratorios();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'No se pudo eliminar. Puede que el registro no exista.'
                    });
                    toastr.error("No se pudo eliminar el laboratorio");
                }
            });
        } else if (result.isDenied) {
            Swal.fire("No se ha eliminado el laboratorio", "", "info");
        }
    });
}
