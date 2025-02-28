using CapaDatos;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;

namespace PrimeraAPPNetCore.Controllers
{
    public class SucursalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<SucursalCLS> listarSucursales()
        {
            SucursalDAL obj = new SucursalDAL();
            return obj.listarSucursales();
        }

        public List<SucursalCLS> filtrarSucursales(SucursalCLS objSucursal)
        {
            SucursalDAL obj = new SucursalDAL();
            return obj.filtrarSucursales(objSucursal);
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

        [HttpGet]
        public JsonResult eliminarSucursal(int idSucursal)
        {
            SucursalDAL obj = new SucursalDAL();
            int resultado = obj.EliminarSucursal(idSucursal);
            return Json(new { success = resultado > 0 });
        }


    }
}

