using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class MedicamentoCLS
    {
        public int IdMedicamento { get; set; }
        public string Nombre { get; set; }
        public int? IdLaboratorio { get; set; } // Puede ser null
        public string NombreLaboratorio { get; set; }
        public int? IdTipoMedicamento { get; set; } // Puede ser null
        public string NombreTipoMedicamento { get; set; }
        public bool Habilitado { get; set; }
    }

}
