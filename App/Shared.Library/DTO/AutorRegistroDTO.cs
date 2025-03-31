using System.ComponentModel.DataAnnotations;

namespace Shared.Library.DTO
{
    public class AutorRegistroDTO
    {
        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        [MaxLength(100, ErrorMessage = "Máximo 100 carácteres para Nombre.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Nacionalidad es requerido.")]
        [MaxLength(100, ErrorMessage = "Máximo 100 carácteres para Nacionalidad.")]
        public string Nacionalidad { get; set; }

        [Required(ErrorMessage = "El campo FechaNacimiento es requerido")]
        public DateTime FechaNacimiento { get; set; }
    }
}
