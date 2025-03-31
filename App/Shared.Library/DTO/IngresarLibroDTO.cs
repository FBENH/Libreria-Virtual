using Shared.Library.Mensajes.Mensajes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Library.DTO
{
    public class IngresarLibroDTO
    {    
        [Required(ErrorMessage = $"El campo Titulo es requerido.")]
        [MaxLength(200, ErrorMessage = $"Máximo 200 carácteres para Titulo.")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = $"Campo Editorial es requerido.")]
        [MaxLength(100, ErrorMessage = $"Máximo 100 carácteres para Editorial.")]
        public string Editorial { get; set; }
        [Required(ErrorMessage = $"El campo Paginas es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = $"El número de Paginas debe estar entre 1 y 2147483647.")]
        public int Paginas { get; set; }
        [Required(ErrorMessage = $"El campo FechaPublicacion es requerido.")]
        public DateTime FechaPublicacion { get; set; }
        [Required(ErrorMessage = $"El campo Isbn es requerido.")]
        [MaxLength(20, ErrorMessage = $"El Isbn debe tener Máximo 20 carácteres.")]
        public string ISBN { get; set; }
        [Required(ErrorMessage = "El campo Url es requerido")]
        public string Url { get; set; }
    }
}
