using System.ComponentModel.DataAnnotations;

namespace Shared.Library.DTO
{
    public class ActualizarUrlUsuarioDTO
    {
        [Required(ErrorMessage = "El campo Url es requerido.")]
        public string Url { get; set; }        
    }
}
