using System.ComponentModel.DataAnnotations;

namespace Shared.Library.DTO
{
    public class ActualizarUrlUsuarioDTO
    {
        [Required]
        public string Url { get; set; }        
    }
}
