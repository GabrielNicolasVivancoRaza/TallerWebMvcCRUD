window.onload = function () {
    listarTiposMedicamento();
};

function filtrarTiposMedicamento() {
    let nombre = get("txtTipoMedicamento");
    if (nombre == "") {
        listarTiposMedicamento();
    } else {
        objTiposMedicamento.url = "TipoMedicamento/filtrarTiposMedicamento/?nombre=" + nombre;
        pintar(objTiposMedicamento);
    }
}

let objTiposMedicamento;

async function listarTiposMedicamento() {
    objTiposMedicamento = {
        url: "TipoMedicamento/listarTiposMedicamento",
        cabeceras: ["ID Tipo Medicamento", "Nombre", "Descripción"],
        propiedades: ["idTipoMedicamento", "nombre", "descripcion"],
        editar: true,
        eliminar: true,
        propiedadId: "idTipoMedicamento"
    }
    pintar(objTiposMedicamento);
}

function LimpiarTipoMedicamento() {
    LimpiarDatos("frmGuardarTipoMedicamento");
}

function GuardarTipoMedicamento() {
    let forma = document.getElementById("frmGuardarTipoMedicamento");
    let frm = new FormData(forma);
    fetchPost("TipoMedicamento/GuardarTipoMedicamento", "text", frm, function (res) {
        Swal.fire({
            icon: 'success',
            title: 'Éxito',
            text: 'Tipo de medicamento agregado correctamente'
        });
        toastr.success("Tipo de medicamento agregado correctamente.");
        listarTiposMedicamento();
        LimpiarDatos("frmGuardarTipoMedicamento");
        $('#modalTipoMedicamento').modal('hide');
    });
}

function Editar(id) {
    fetchGet("TipoMedicamento/recuperarTipoMedicamento/?idTipoMedicamento=" + id, "json", function (data) {
        setN("idTipoMedicamento", data.idTipoMedicamento);
        setN("nombre", data.nombre);
        setN("descripcion", data.descripcion);
        $('#modalTipoMedicamento').modal('show');
    });
}

function GuardarCambiosTipoMedicamento() {
    let forma = document.getElementById("frmGuardarTipoMedicamento");
    let frm = new FormData(forma);

    Swal.fire({
        title: "¿Estás seguro de modificar el tipo de medicamento?",
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: "Guardar",
        denyButtonText: `No guardar`,
    }).then((result) => {
        if (result.isConfirmed) {
            fetchPost("TipoMedicamento/GuardarCambiosTipoMedicamento", "text", frm, function (res) {
                Swal.fire({
                    icon: 'success',
                    title: 'Éxito',
                    text: 'Tipo de medicamento modificado correctamente'
                });
                toastr.success("Tipo de medicamento modificado correctamente.");
                listarTiposMedicamento();
                LimpiarDatos("frmGuardarTipoMedicamento");
                $('#modalTipoMedicamento').modal('hide');
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
            fetchGet("TipoMedicamento/eliminarTipoMedicamento/?idTipoMedicamento=" + id, "json", function (res) {
                if (res.success) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Eliminado',
                        text: 'Tipo de medicamento eliminado correctamente'
                    });
                    toastr.success("Tipo de medicamento eliminado correctamente.");
                    listarTiposMedicamento();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'No se pudo eliminar. Puede que el registro no exista.'
                    });
                    toastr.error("No se pudo eliminar el tipo de medicamento.");
                }
            });
        } else if (result.isDenied) {
            Swal.fire("No se ha eliminado el tipo de medicamento", "", "info");
        }
    });
}
