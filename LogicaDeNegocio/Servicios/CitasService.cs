using AccesoADatos.Models;
using AccesoADatos.Repositories;
using LogicaDeNegocio.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Servicios
{
    public class CitasService : ICitasService
    {
        private readonly ICitasRepository _repo;
        private readonly IServicioRepository _repoServicios;

        public CitasService(ICitasRepository repo, IServicioRepository repoServicios)
        {
            _repo = repo;
            _repoServicios = repoServicios;
        }

        public Task<IEnumerable<Cita>> ListarAsync(int? servicioId) =>
            servicioId.HasValue && servicioId.Value > 0
                ? _repo.GetByServicioAsync(servicioId.Value)
                : _repo.GetAllAsync();

        public Task<Cita?> ObtenerAsync(int id) => _repo.GetByIdAsync(id);

        public async Task<Result> CrearAsync(Cita nueva)
        {
            // Validaciones simples
            if (string.IsNullOrWhiteSpace(nueva.NombreDeLaPersona))
                return Result.Fail("El nombre de la persona es obligatorio.");

            // Evitar fechas fuera de rango para SQL (datetime)
            var minSqlDate = new DateTime(1900, 1, 1);
            if (nueva.FechaNacimiento < minSqlDate)
                return Result.Fail("La fecha de nacimiento es inválida (mínimo 1900-01-01).");
            if (nueva.FechaDeLaCita < minSqlDate)
                return Result.Fail("La fecha de la cita es inválida (mínimo 1900-01-01).");

            // Calcular MontoTotal con base en el servicio seleccionado
            var servicio = await _repoServicios.GetByIdAsync(nueva.IdServicio);
            if (servicio is null) return Result.Fail("El servicio no existe.");

            nueva.MontoTotal = servicio.Monto + servicio.IVA;
            nueva.FechaDeRegistro = DateTime.Now;

            await _repo.AddAsync(nueva);
            await _repo.SaveAsync();

            return Result.Success("Cita registrada.");
        }

        public async Task<Result> EditarAsync(Cita editada)
        {
            var actual = await _repo.GetByIdAsync(editada.Id);
            if (actual is null) return Result.Fail("La cita no existe.");

            var minSqlDate = new DateTime(1900, 1, 1);
            if (editada.FechaNacimiento < minSqlDate)
                return Result.Fail("La fecha de nacimiento es inválida (mínimo 1900-01-01).");
            if (editada.FechaDeLaCita < minSqlDate)
                return Result.Fail("La fecha de la cita es inválida (mínimo 1900-01-01).");

            // Actualizar campos editables
            actual.NombreDeLaPersona = editada.NombreDeLaPersona;
            actual.Identificacion = editada.Identificacion;
            actual.Telefono = editada.Telefono;
            actual.Correo = editada.Correo;
            actual.Direccion = editada.Direccion;
            actual.FechaNacimiento = editada.FechaNacimiento;
            actual.FechaDeLaCita = editada.FechaDeLaCita;
            actual.IdServicio = editada.IdServicio;

            // Recalcular total por si cambió el servicio
            var servicio = await _repoServicios.GetByIdAsync(actual.IdServicio);
            if (servicio is null) return Result.Fail("El servicio indicado no existe.");
            actual.MontoTotal = servicio.Monto + servicio.IVA;

            await _repo.UpdateAsync(actual);
            await _repo.SaveAsync();

            return Result.Success("Cita actualizada.");
        }

        public async Task<Result> EliminarAsync(int id)
        {
            var actual = await _repo.GetByIdAsync(id);
            if (actual is null) return Result.Fail("La cita no existe.");

            await _repo.DeleteAsync(actual);
            await _repo.SaveAsync();

            return Result.Success("Cita eliminada.");
        }
    }
}
