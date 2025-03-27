using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaVirtualData.Library.Exceptions
{
    public class UsuarioNotFoundException : Exception
    {
        public UsuarioNotFoundException(string idUsuario)
        : base($"No se encontró el usuario con ID {idUsuario}.")
        {
        }

    }
}
