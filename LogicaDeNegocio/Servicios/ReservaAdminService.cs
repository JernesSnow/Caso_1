using AccesoADatos.Models;
using AccesoADatos.Repositories;
using LogicaDeNegocio.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Servicios
{
    public class ReservaAdminService : IReservaAdminService
    {
        private readonly IReservaAdminRepository _repo;
        public ReservaAdminService(IReservaAdminRepository repo) => _repo = repo;

        public async Task<IEnumerable<Cita>> ListarAsync(int? servicioId)
        {
            if (servicioId.HasValue && servicioId.Value > 0)
                return await _repo.GetPorServicioAsync(servicioId.Value);

            return await _repo.GetHistoricoAsync();
        }
    }
}
