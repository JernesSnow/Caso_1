using AccesoADatos.Data;
using AccesoADatos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccesoADatos.Repositories
{
    public class CitasRepository : ICitasRepository
    {
        private readonly AppDbContext _db;
        public CitasRepository(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Cita>> GetAllAsync() =>
            await _db.Citas.AsNoTracking()
                           .OrderByDescending(c => c.FechaDeRegistro)
                           .ToListAsync();

        public async Task<IEnumerable<Cita>> GetByServicioAsync(int id) =>
            await _db.Citas.AsNoTracking()
                           .Where(c => c.IdServicio == id)
                           .OrderByDescending(c => c.FechaDeRegistro)
                           .ToListAsync();

        public Task<Cita?> GetByIdAsync(int id) => _db.Citas.FindAsync(id).AsTask();

        public async Task AddAsync(Cita entity) => await _db.Citas.AddAsync(entity);

        public Task UpdateAsync(Cita entity)
        {
            _db.Citas.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Cita entity)
        {
            _db.Citas.Remove(entity);
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(int id) => _db.Citas.AnyAsync(x => x.Id == id);

        public Task SaveAsync() => _db.SaveChangesAsync();
    }
}
