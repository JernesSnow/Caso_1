using LogicaDeNegocio.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class ReservasAdministrativasController : Controller
    {
        private readonly IReservaAdminService _service;
        public ReservasAdministrativasController(IReservaAdminService service) => _service = service;

        // /ReservasAdministrativas ? servicioId=5
        public async Task<IActionResult> Index(int? servicioId)
        {
            ViewBag.ServicioId = servicioId;
            var lista = await _service.ListarAsync(servicioId);
            return View(lista);
        }
    }
}
