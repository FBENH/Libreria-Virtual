using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Library.Mensajes.Mensajes
{
    public class MensajesConfiguracion
    {
        public string AutorRegistroExito { get; set; }
        public string DetalleAutorExito { get; set; }
        public string UsuarioRegistroExito { get; set; }
        public string UsuarioFoto { get; set; }
        public string UsuarioEliminar { get; set; }
        public string UsuarioSuscribir { get; set; }
        public string UsuarioEliminarSuscripcion { get; set; }
        public string UsuarioListado { get; set; }
        public string AutorYaExiste { get; set; }
        public string AutorNoExiste { get; set; }
        public string UsuarioYaExiste { get; set; }
        public string UsuarioNoExiste { get; set; }
        public string UsuarioYaSuscripto { get; set; }
        public string SuscripcionNoExiste { get; set; }
        public string LibrosListado { get; set; }
        public string LibroIngresar { get; set; }
        public string EmailBody { get; set; }
        public string LibroNoExiste { get; set; }
        public string ReviewsListado { get; set; }
        public string ReviewAgregar { get; set; }
        public string ErrorConcurrencia { get; set; }
        public string InternalError { get; set; }
        public string ReviewYaExiste { get; set; }
    }
}
