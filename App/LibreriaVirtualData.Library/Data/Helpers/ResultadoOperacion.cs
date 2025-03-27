using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaVirtualData.Library.Data.Helpers
{
    public class ResultadoOperacion
    {
        public bool Exito { get; set; } = false;
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Errores { get; set; } = new List<string>();

        public string TextoErrores()
        {
            return string.Join(", ", Errores);
        }
    }
}
