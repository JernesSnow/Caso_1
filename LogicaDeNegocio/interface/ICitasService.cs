using AccesoADatos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogicaDeNegocio.Interface
{
    public interface ICitasService
    {
        Task<IEnumerable<Cita>> ListarAsync(int? servicioId);
        Task<Cita?> ObtenerAsync(int id);

        Task<Result> CrearAsync(Cita nueva);
        Task<Result> EditarAsync(Cita editada);
        Task<Result> EliminarAsync(int id);
    }
}
