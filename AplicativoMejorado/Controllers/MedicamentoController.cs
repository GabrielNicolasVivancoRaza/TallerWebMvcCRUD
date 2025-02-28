using CapaDatos;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PrimeraAPPNetCore.Controllers
{
    public class MedicamentoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<MedicamentoCLS> ListarMedicamentos()
        {
            MedicamentoDAL obj = new MedicamentoDAL();
            return obj.ListarMedicamentos();
        }


    }
}

