using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class ServicioCreateVm
    {
        [Required, StringLength(100)] public string Nombre { get; set; } = string.Empty;
        [Required, StringLength(200)] public string Descripcion { get; set; } = string.Empty;
        [Range(0, 9999999)] public decimal Monto { get; set; }
        [Range(0, 9999999)] public decimal IVA { get; set; }
        [Required] public int Especialidad { get; set; }
        [Required, StringLength(200)] public string Especialista { get; set; } = string.Empty;
        [Required, StringLength(100)] public string Clinica { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;
    }

    public class ServicioEditVm : ServicioCreateVm
    {
        [Required]
        public int Id { get; set; }
    }
}
