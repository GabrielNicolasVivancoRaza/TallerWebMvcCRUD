using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class MedicamentoBL
    {
        MedicamentoDAL objDAL = new MedicamentoDAL();

        public List<MedicamentoCLS> ObtenerMedicamentos()
        {
            return objDAL.ListarMedicamentos();
        }
    }
}
