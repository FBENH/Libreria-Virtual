using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class ActualizarUrlUsuarioDTO
    {
        [Required]
        public string Url { get; set; }
    }
}
