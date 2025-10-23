using AccesoADatos.Models;
using AccesoADatos.Repositories;
using LogicaDeNegocio.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using UI.Models;

namespace UI.Controllers
{
    public class CitasController : Controller
    {
        private readonly ICitasService _service;
        private readonly IServicioRepository _repoServicios;

        public CitasController(ICitasService service, IServicioRepository repoServicios)
        {
            _service = service;
            _repoServicios = repoServicios;
        }

        // /Citas?servicioId=3 (filtro opcional)
        public async Task<IActionResult> Index(int? servicioId)
        {
            ViewBag.ServicioId = servicioId;
            var lista = await _service.ListarAsync(servicioId);
            return View(lista);
        }

        public async Task<IActionResult> Create()
        {
            await CargarServicios();
            // Defaults válidos para SQL datetime
            return View(new CitaCreateVm
            {
                FechaNacimiento = new System.DateTime(1990, 1, 1),
                FechaDeLaCita = System.DateTime.Now.AddDays(1).Date.AddHours(9)
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CitaCreateVm vm)
        {
            if (!ModelState.IsValid) { await CargarServicios(); return View(vm); }

            var entity = new Cita
            {
                NombreDeLaPersona = vm.NombreDeLaPersona,
                Identificacion = vm.Identificacion,
                Telefono = vm.Telefono,
                Correo = vm.Correo,
                FechaNacimiento = vm.FechaNacimiento,
                Direccion = vm.Direccion,
                FechaDeLaCita = vm.FechaDeLaCita,
                IdServicio = vm.IdServicio
            };

            var r = await _service.CrearAsync(entity);
            if (!r.Ok)
            {
                ModelState.AddModelError("", r.Message ?? "No se pudo registrar la cita.");
                await CargarServicios();
                return View(vm);
            }

            TempData["msg"] = "Cita registrada correctamente";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var c = await _service.ObtenerAsync(id);
            if (c is null) return NotFound();

            await CargarServicios();
            var vm = new CitaEditVm
            {
                Id = c.Id,
                NombreDeLaPersona = c.NombreDeLaPersona,
                Identificacion = c.Identificacion,
                Telefono = c.Telefono,
                Correo = c.Correo,
                FechaNacimiento = c.FechaNacimiento,
                Direccion = c.Direccion,
                FechaDeLaCita = c.FechaDeLaCita,
                IdServicio = c.IdServicio
            };
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CitaEditVm vm)
        {
            if (!ModelState.IsValid) { await CargarServicios(); return View(vm); }

            var entity = new Cita
            {
                Id = vm.Id,
                NombreDeLaPersona = vm.NombreDeLaPersona,
                Identificacion = vm.Identificacion,
                Telefono = vm.Telefono,
                Correo = vm.Correo,
                FechaNacimiento = vm.FechaNacimiento,
                Direccion = vm.Direccion,
                FechaDeLaCita = vm.FechaDeLaCita,
                IdServicio = vm.IdServicio
            };

            var r = await _service.EditarAsync(entity);
            if (!r.Ok)
            {
                ModelState.AddModelError("", r.Message ?? "No se pudo actualizar la cita.");
                await CargarServicios();
                return View(vm);
            }

            TempData["msg"] = "Cita actualizada";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var c = await _service.ObtenerAsync(id);
            if (c is null) return NotFound();
            return View(c);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var r = await _service.EliminarAsync(id);
            TempData["msg"] = r.Ok ? "Cita eliminada" : (r.Message ?? "No se pudo eliminar.");
            return RedirectToAction(nameof(Index));
        }

        private async Task CargarServicios()
        {
            var servicios = await _repoServicios.GetAllAsync();
            ViewBag.Servicios = servicios
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Nombre })
                .ToList();
        }
    }
}
