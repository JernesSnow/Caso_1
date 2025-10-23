using AccesoADatos.Models;
using AccesoADatos.Repositories;
using LogicaDeNegocio.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Servicios
{
    public class ServicioService : IServicioService
    {
        private readonly IServicioRepository _repo;
        public ServicioService(IServicioRepository repo) => _repo = repo;

        public Task<IEnumerable<Servicio>> ListarAsync() => _repo.GetAllAsync();

        public Task<Servicio?> ObtenerAsync(int id) => _repo.GetByIdAsync(id);

        public async Task<Result> CrearAsync(Servicio nuevo)
        {
            if (string.IsNullOrWhiteSpace(nuevo.Nombre)) return Result.Fail("El nombre es requerido.");
            if (string.IsNullOrWhiteSpace(nuevo.Descripcion)) return Result.Fail("La descripción es requerida.");
            if (nuevo.Monto < 0) return Result.Fail("El monto debe ser mayor o igual a 0.");
            if (nuevo.IVA < 0) return Result.Fail("El IVA no puede ser negativo.");
            if (nuevo.Especialidad is < 1 or > 3) return Result.Fail("Especialidad inválida.");

            nuevo.FechaDeRegistro = DateTime.Now;

            await _repo.AddAsync(nuevo);
            await _repo.SaveAsync();
            return Result.Success();
        }

        public async Task<Result> EditarAsync(Servicio modificado)
        {
            var actual = await _repo.GetByIdAsync(modificado.Id);
            if (actual is null) return Result.Fail("El servicio no existe.");

            actual.Nombre = modificado.Nombre;
            actual.Descripcion = modificado.Descripcion;
            actual.Monto = modificado.Monto;
            actual.IVA = modificado.IVA;
            actual.Especialista = modificado.Especialista;
            actual.Clinica = modificado.Clinica;
            actual.Estado = modificado.Estado;
            actual.FechaDeModificacion = DateTime.Now;

            await _repo.UpdateAsync(actual);
            await _repo.SaveAsync();
            return Result.Success();
        }
    }
}