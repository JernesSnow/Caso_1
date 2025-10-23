using Microsoft.AspNetCore.Mvc;
using AccesoADatos.Models;

namespace UI.Controllers
{
    public class ServiciosController : Controller
    {

        private static List<Servicio> servicios = new List<Servicio>();

        public IActionResult Index()
        {
            return View(servicios);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                servicio.Id = servicios.Count + 1;
                servicios.Add(servicio);
                TempData["ok"] = "Servicio registrado correctamente.";
                return RedirectToAction("Index");
            }
            return View(servicio);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var servicio = servicios.FirstOrDefault(s => s.Id == id);
            if (servicio == null) return NotFound();
            return View(servicio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Servicio servicio)
        {
            var original = servicios.FirstOrDefault(s => s.Id == servicio.Id);
            if (original == null) return NotFound();

            if (ModelState.IsValid)
            {
                original.Nombre = servicio.Nombre;
                original.Descripcion = servicio.Descripcion;
                original.Monto = servicio.Monto;
                original.IVA = servicio.IVA;
                original.Especialidad = servicio.Especialidad;
                original.Especialista = servicio.Especialista;
                original.Clinica = servicio.Clinica;
                original.Estado = servicio.Estado;

                TempData["ok"] = "Servicio actualizado correctamente.";
                return RedirectToAction("Index");
            }
            return View(servicio);
        }
    }
}
