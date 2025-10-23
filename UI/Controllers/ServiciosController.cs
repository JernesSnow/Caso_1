using AccesoADatos.Models;
using LogicaDeNegocio.Interface;
using Microsoft.AspNetCore.Mvc;
using UI.Models;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly IServicioService _service;
        public ServiciosController(IServicioService service) => _service = service;

        public async Task<IActionResult> Index()
        {
            var lista = await _service.ListarAsync();
            return View(lista);
        }

        public IActionResult Create() => View(new ServicioCreateVm());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServicioCreateVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var entity = new Servicio
            {
                Nombre = vm.Nombre,
                Descripcion = vm.Descripcion,
                Monto = vm.Monto,
                IVA = vm.IVA,
                Especialidad = vm.Especialidad,
                Especialista = vm.Especialista,
                Clinica = vm.Clinica,
                Estado = vm.Estado
            };

            var result = await _service.CrearAsync(entity);
            if (!result.Ok)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "");
                return View(vm);
            }

            TempData["msg"] = "Servicio registrado correctamente";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var s = await _service.ObtenerAsync(id);
            if (s is null) return NotFound();

            var vm = new ServicioEditVm
            {
                Id = s.Id,
                Nombre = s.Nombre,
                Descripcion = s.Descripcion,
                Monto = s.Monto,
                IVA = s.IVA,
                Especialidad = s.Especialidad,
                Especialista = s.Especialista,
                Clinica = s.Clinica,
                Estado = s.Estado
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServicioEditVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var entity = new Servicio
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Descripcion = vm.Descripcion,
                Monto = vm.Monto,
                IVA = vm.IVA,
                Especialidad = vm.Especialidad, // se mantiene
                Especialista = vm.Especialista,
                Clinica = vm.Clinica,
                Estado = vm.Estado
            };

            var result = await _service.EditarAsync(entity);
            if (!result.Ok)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "");
                return View(vm);
            }

            TempData["msg"] = "Servicio editado correctamente";
            return RedirectToAction(nameof(Index));
        }

        // Preparado para módulo de Citas
        public IActionResult Citas(int id) => RedirectToAction("Index", "Citas", new { servicioId = id });
    }
}