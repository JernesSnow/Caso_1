using AccesoADatos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace AccesoADatos.Repositories
{
    public interface IServicioRepository
    {
        Task<IEnumerable<Servicio>> GetAllAsync();
        Task<Servicio?> GetByIdAsync(int id);
        Task AddAsync(Servicio entity);
        Task UpdateAsync(Servicio entity);
        Task<bool> ExistsAsync(int id);
        Task SaveAsync();
    }
}