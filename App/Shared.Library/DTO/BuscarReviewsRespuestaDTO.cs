using Shared.Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Library.DTO
{
    public class BuscarReviewsRespuestaDTO
    {
        public string NombreLibro { get; set; }
        public string NombreUsuario { get; set; }
        public string? Opinion { get; set; }
        public Calificacion Calificacion { get; set; }
        public DateTime Fecha { get; set; }
    }
}
