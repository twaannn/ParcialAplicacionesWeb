using System.ComponentModel.DataAnnotations;

namespace AplicacionesWeb.Models
{
    public class Estudiante : Persona
    {
        [Required, StringLength(80)]
        public string Matricula { get; set; } = string.Empty; // ejemplo: "11A", "1 semestre", etc.

    }
}
