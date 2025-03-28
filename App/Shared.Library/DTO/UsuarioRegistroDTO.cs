using System.ComponentModel.DataAnnotations;

namespace Shared.Library.DTO
{
    public class UsuarioRegistroDTO
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? UrlFoto { get; set; }
    }
}
