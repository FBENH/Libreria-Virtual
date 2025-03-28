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
        [Required]
        [MaxLength(200)]
        public string Titulo { get; set; }
        [Required]
        [MaxLength(100)]
        public string Editorial { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Paginas { get; set; }
        [Required]
        public DateTime FechaPublicacion { get; set; }
        [Required]
        [MaxLength(20)]
        public string ISBN { get; set; }
        [Required]
        public string Url { get; set; }
    }
}
