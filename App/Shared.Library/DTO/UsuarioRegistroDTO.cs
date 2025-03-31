using System.ComponentModel.DataAnnotations;

namespace Shared.Library.DTO
{
    public class UsuarioRegistroDTO
    {
        [Required(ErrorMessage = "El Id es requerido.")]        
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        [MaxLength(100, ErrorMessage = "Máximo 100 carácteres para Nombre.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Email es requerido.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un Email válido.")]
        public string Email { get; set; }

        public string? UrlFoto { get; set; }
    }
    
}
