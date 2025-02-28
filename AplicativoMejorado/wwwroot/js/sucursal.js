window.onload = function () {
    listarSucursales();
};

function filtrarSucursales() {
    let nombre = get("txtSucursales");
    if (nombre == "") {
        listarSucursales();
    } else {
        objSucursales.url = "Sucursal/filtrarSucursales/?nombre=" + nombre;
        pintar(objSucursales);
    }
}

let objSucursales;

async function listarSucursales() {
    objSucursales = {
        url: "Sucursal/listarSucursales",
        cabeceras: ["ID Sucursal", "Nombre", "Dirección"],
        propiedades: ["idSucursal", "nombre", "direccion"],
        editar: true,
        eliminar: true,
        propiedadId: "idSucursal"
    }
    pintar(objSucursales);
}

function BuscarSucursal() {
    let forma = document.getElementById("frmBusqueda");
    let frm = new FormData(forma);
    fetchPost("Sucursal/filtrarSucursales", "json", frm, function (res) {
        document.getElementById("divContenedorTabla").innerHTML = generarTabla(res);
    });
}

function LimpiarSucursal() {
    LimpiarDatos("frmGuardarSucursal");
    listarSucursales(); // ← Asegurando que refresque correctamente
}

function GuardarSucursal() {
    let forma = document.getElementById("frmGuardarSucursal");
    let frm = new FormData(forma);
    fetchPost("Sucursal/GuardarSucursal", "text", frm, function (res) {
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Sucursal agregada'
        });
        toastr.success("Sucursal correctamente.");
        listarSucursales();
        LimpiarDatos("frmGuardarSucursal");
        $('#modalSucursal').modal('hide');
    });
}

function Editar(id) {
    fetchGet("Sucursal/recuperarSucursal/?idSucursal=" + id, "json", function (data) {
        setN("idSucursal", data.idSucursal); 
        setN("nombre", data.nombre);
        setN("direccion", data.direccion);
        $('#modalSucursal').modal('show');
    });
}

function GuardarCambiosSucursal() {
    let forma = document.getElementById("frmGuardarSucursal");
    let frm = new FormData(forma);

    Swal.fire({
        title: "¿Estás seguro?",
        text: "¿Deseas guardar los cambios realizados?",
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: "Guardar",
        denyButtonText: "No guardar"
    }).then((result) => {
        if (result.isConfirmed) {
            fetchPost("Sucursal/GuardarCambiosSucursal", "text", frm, function (res) {
                Swal.fire({
                    icon: 'success',
                    title: 'Guardado',
                    text: 'Sucursal modificada correctamente'
                });
                toastr.success("Sucursal modificada correctamente.");
                listarSucursales();
                LimpiarDatos("frmGuardarSucursal");
                $('#modalSucursal').modal('hide');
            });
        } else if (result.isDenied) {
            Swal.fire("Los cambios no fueron guardados.", "", "info");
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
            fetchGet("Sucursal/EliminarSucursal/?idSucursal=" + id, "json", function (res) {
                if (res.success) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Eliminado',
                        text: 'Sucursal eliminada'
                    });
                    toastr.success("Sucursal eliminada");
                    listarSucursales();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'No se pudo eliminar. Puede que el registro no exista.'
                    });
                    toastr.error("No se eliminó sucursal.");
                }
            });
        } else if (result.isDenied) {
            Swal.fire("No se ha eliminado la sucursal", "", "info");
        }
    });

}
