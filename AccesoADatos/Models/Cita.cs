using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoADatos.Models
{
    [Table("CITAS")]
    public class Cita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Mapeos exactos a nombres del script:
        [Required, StringLength(150)]
        [Column("NombreDeLaPersona")]
        public string NombreDeLaPersona { get; set; } = string.Empty;

        [Required, StringLength(30)]
        public string Identificacion { get; set; } = string.Empty;

        [Required, StringLength(10)]
        public string Telefono { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required, StringLength(200)]
        public string Direccion { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoTotal { get; set; }

        [Required]
        [Column("FechaDeLaCita")]
        public DateTime FechaDeLaCita { get; set; }

        [Required]
        public DateTime FechaDeRegistro { get; set; }

        // FK -> SERVICIOS
        [Required]
        [Column("IdServicio")]
        public int IdServicio { get; set; }

    }
}
