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
        [Required]
        public string Opinion { get; set; }
        [Required]
        public Calificacion Calificacion { get; set; }
    }
}
