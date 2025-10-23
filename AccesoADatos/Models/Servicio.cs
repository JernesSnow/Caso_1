using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccesoADatos.Models
{
    public enum EspecialidadMedica
    {
        MedicinaGeneral = 1,
        Imagenologia = 2,
        Microbiologia = 3
    }

    [Table("Servicios")]
    public class Servicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required, StringLength(200)]
        public string Descripcion { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999)]
        public decimal Monto { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999)]
        public decimal IVA { get; set; }

        [Required]
        public int Especialidad { get; set; }

        [Required, StringLength(200)]
        public string Especialista { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Clinica { get; set; } = string.Empty;

        [Required]
        public DateTime FechaDeRegistro { get; set; }

        public DateTime? FechaDeModificacion { get; set; }

        [Required]
        public bool Estado { get; set; }

        [NotMapped]
        public string EspecialidadTexto => ((EspecialidadMedica)Especialidad).ToString();
    }
}