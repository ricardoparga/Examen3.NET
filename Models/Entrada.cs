using System.ComponentModel.DataAnnotations;

namespace Examen3.NET.Models
{
    public class Entrada
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre de la comida")]
        public string Nombre { get; set; }

        public float? Precio { get; set; }

        [Display(Name = "Imagen")]
        public string? UrlImagen { get; set; }
    }
}