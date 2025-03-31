using Shared.Library.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Library.DTO
{
    public class AgregarReviewDTO
    {
        [Required(ErrorMessage ="El campo Opinion es requerido.")]
        public string Opinion { get; set; }
        [Required(ErrorMessage = "El campo Calificacion es requerido.")]
        public Calificacion Calificacion { get; set; }
    }
}
