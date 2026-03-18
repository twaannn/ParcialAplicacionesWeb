using System.ComponentModel.DataAnnotations;

namespace AplicacionesWeb.Models
{
    public class Persona
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Nombre { get; set; } = string.Empty;

        [Range(0, 130)]
        public int Edad { get; set; }
    }

}
