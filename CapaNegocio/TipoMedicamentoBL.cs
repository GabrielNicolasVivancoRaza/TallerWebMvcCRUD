using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class TipoMedicamentoBL
    {
        public List<TipoMedicamentoCLS> listarTipoMedicamento()
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.listarTipoMedicamento();
        }

        public List<TipoMedicamentoCLS> filtrarTipoMedicamento(TipoMedicamentoCLS objTipoMedicamento)
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.filtrarTipoMedicamento(objTipoMedicamento);
        }

        public int GuardarTipoMedicamento(TipoMedicamentoCLS objTipoMedicamento)
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.GuardarTipoMedicamento(objTipoMedicamento);
        }

        public TipoMedicamentoCLS recuperarTipoMedicamento(int idTipoMedicamento)
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.recuperarTipoMedicamento(idTipoMedicamento);
        }

        public int GuardarCambiosTipoMedicamento(TipoMedicamentoCLS objTipoMedicamento)
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.GuardarCambiosTipoMedicamento(objTipoMedicamento);
        }
    }
}