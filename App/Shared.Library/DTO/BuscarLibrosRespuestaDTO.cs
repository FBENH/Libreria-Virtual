using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Library.DTO
{
    public class BuscarLibrosRespuestaDTO
    {
        public string Titulo { get; set; }
        public string NombreAutor { get; set; }
        public string Editorial { get; set; }
        public string ISBN { get; set; }       
    }
}
