using AccesoADatos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccesoADatos.Repositories
{
    public interface ICitasRepository
    {
        Task<IEnumerable<Cita>> GetAllAsync();                
        Task<IEnumerable<Cita>> GetByServicioAsync(int id);  
        Task<Cita?> GetByIdAsync(int id);

        Task AddAsync(Cita entity);
        Task UpdateAsync(Cita entity);
        Task DeleteAsync(Cita entity);
        Task<bool> ExistsAsync(int id);
        Task SaveAsync();
    }
}
