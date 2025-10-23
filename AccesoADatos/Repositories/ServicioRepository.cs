using AccesoADatos.Data;
using AccesoADatos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccesoADatos.Repositories
{
    public class ServicioRepository : IServicioRepository
    {
        private readonly AppDbContext _db;
        public ServicioRepository(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Servicio>> GetAllAsync()
            => await _db.Servicios.AsNoTracking().OrderBy(s => s.Nombre).ToListAsync();

        public async Task<Servicio?> GetByIdAsync(int id)
            => await _db.Servicios.FindAsync(id);

        public async Task AddAsync(Servicio entity)
            => await _db.Servicios.AddAsync(entity);

        public Task UpdateAsync(Servicio entity)
        {
            _db.Servicios.Update(entity);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(int id)
            => await _db.Servicios.AnyAsync(e => e.Id == id);

        public async Task SaveAsync() => await _db.SaveChangesAsync();
    }
}