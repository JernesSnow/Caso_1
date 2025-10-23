using AccesoADatos.Data;
using AccesoADatos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccesoADatos.Repositories
{
    public class ReservaAdminRepository : IReservaAdminRepository
    {
        private readonly AppDbContext _db;
        public ReservaAdminRepository(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Cita>> GetHistoricoAsync() =>
            await _db.Citas.AsNoTracking()
                           .OrderByDescending(c => c.FechaDeRegistro)
                           .ToListAsync();

        public async Task<IEnumerable<Cita>> GetPorServicioAsync(int servicioId) =>
            await _db.Citas.AsNoTracking()
                           .Where(c => c.IdServicio == servicioId)
                           .OrderByDescending(c => c.FechaDeRegistro)
                           .ToListAsync();
    }
}
