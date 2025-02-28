window.onload = function () {
    listarMedicamentos();
};

let objMedicamentos;

async function listarMedicamentos() {
    objMedicamentos = {
        url: "Medicamento/ListarMedicamentos",
        cabeceras: ["ID Medicamento", "Nombre", "Laboratorio", "Tipo"],
        propiedades: ["idMedicamento", "nombre", "nombreLaboratorio", "nombreTipoMedicamento"],
        editar: true,
        eliminar: true
    };
    pintar(objMedicamentos);
}


function BuscarMedicamento() {
    let forma = document.getElementById("frmBusquedaMedicamento");
    let frm = new FormData(forma);

    fetchPost("Medicamento/filtrarMedicamentos", "json", frm, function (res) {
        document.getElementById("divTableMedicamento").innerHTML = generarTabla(res);
    });
}

function LimpiarMedicamento() {
    LimpiarDatos("frmGuardarMedicamento");
    listarMedicamentos();
}

function GuardarMedicamento() {
    let forma = document.getElementById("frmGuardarMedicamento");
    let frm = new FormData(forma);

    fetchPost("Medicamento/GuardarMedicamento", "text", frm, function (res) {
        listarMedicamentos();
        LimpiarDatos("frmGuardarMedicamento");
    });
}
