using AccesoADatos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccesoADatos.Repositories
{
    /// <summary>
    /// Consultas de solo lectura para el módulo de Reservas Administrativas.
    /// </summary>
    public interface IReservaAdminRepository
    {
        Task<IEnumerable<Cita>> GetHistoricoAsync();
        Task<IEnumerable<Cita>> GetPorServicioAsync(int IdServicio);
    }
}
