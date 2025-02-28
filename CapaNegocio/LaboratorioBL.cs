using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class LaboratorioBL
    {
        public List<LaboratorioCLS> listarLaboratorios()
        {
            LaboratorioDAL obj = new LaboratorioDAL();
            return obj.listarLaboratorios();
        }

        public List<LaboratorioCLS> filtrarLaboratorios(LaboratorioCLS objLaboratorio)
        {
            LaboratorioDAL obj = new LaboratorioDAL();
            return obj.filtrarLaboratorios(objLaboratorio);
        }
        public int GuardarSucursal(SucursalCLS objSucursal)
        {
            SucursalDAL obj = new SucursalDAL();
            return obj.GuardarSucursal(objSucursal);
        }

        public SucursalCLS recuperarSucursal(int idSucursal)
        {
            SucursalDAL obj = new SucursalDAL();
            return obj.recuperarSucursal(idSucursal);
        }

        public int GuardarCambiosSucursal(SucursalCLS objSucursal)
        {
            SucursalDAL obj = new SucursalDAL();
            return obj.GuardarCambiosSucursal(objSucursal);
        }



    }
}
