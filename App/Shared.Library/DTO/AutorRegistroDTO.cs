using System.ComponentModel.DataAnnotations;

namespace Shared.Library.DTO
{
    public class AutorRegistroDTO
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nacionalidad { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }
    }
}
