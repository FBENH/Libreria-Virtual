using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
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
