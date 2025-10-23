using System;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class CitaCreateVm
    {
        [Required, StringLength(150)] public string NombreDeLaPersona { get; set; } = string.Empty;
        [Required, StringLength(30)] public string Identificacion { get; set; } = string.Empty;
        [Required, StringLength(10)] public string Telefono { get; set; } = string.Empty;
        [Required, EmailAddress, StringLength(50)] public string Correo { get; set; } = string.Empty;

        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [Required, StringLength(200)] public string Direccion { get; set; } = string.Empty;

        [Required, DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH\\:mm}", ApplyFormatInEditMode = true)]
        public DateTime FechaDeLaCita { get; set; }

        [Required] public int IdServicio { get; set; }
    }

    public class CitaEditVm : CitaCreateVm
    {
        [Required] public int Id { get; set; }
    }
}
