using AccesoADatos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Interface
{
    public interface IServicioService
    {
        Task<IEnumerable<Servicio>> ListarAsync();
        Task<Servicio?> ObtenerAsync(int id);
        Task<Result> CrearAsync(Servicio nuevo);
        Task<Result> EditarAsync(Servicio modificado);
    }
}