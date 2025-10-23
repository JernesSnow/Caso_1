using AccesoADatos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Interface
{
    public interface IReservaAdminService
    {
        /// <summary>
        /// Devuelve el histórico de citas. Si servicioId tiene valor, filtra por ese servicio.
        /// </summary>
        Task<IEnumerable<Cita>> ListarAsync(int? servicioId);
    }
}
