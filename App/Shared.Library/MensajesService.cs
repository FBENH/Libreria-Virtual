using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Library
{
    public class MensajesService
    {
        private readonly MensajesConfiguracion _mensajes;

        public MensajesService(IOptions<MensajesConfiguracion> options)
        {
            _mensajes = options.Value;
        }

        public string GetMensaje(Mensajes clave, params object[] parametros)
        {
            var mensaje = clave switch
            {
                Mensajes.AutorRegistroExito => _mensajes.AutorRegistroExito,
                Mensajes.DetalleAutorExito => _mensajes.DetalleAutorExito,
                Mensajes.UsuarioRegistroExito => _mensajes.UsuarioRegistroExito,
                Mensajes.UsuarioFoto => _mensajes.UsuarioFoto,
                Mensajes.UsuarioEliminar => _mensajes.UsuarioEliminar,
                Mensajes.UsuarioSuscribir => _mensajes.UsuarioSuscribir,
                Mensajes.UsuarioEliminarSuscripcion => _mensajes.UsuarioEliminarSuscripcion,
                Mensajes.UsuarioListado => _mensajes.UsuarioListado,
                Mensajes.AutorYaExiste => _mensajes.AutorYaExiste,
                Mensajes.AutorNoExiste => _mensajes.AutorNoExiste,
                Mensajes.UsuarioYaExiste => _mensajes.UsuarioYaExiste,
                Mensajes.UsuarioNoExiste => _mensajes.UsuarioNoExiste,
                Mensajes.UsuarioYaSuscripto => _mensajes.UsuarioYaSuscripto,
                Mensajes.SuscripcionNoExiste => _mensajes.SuscripcionNoExiste,
                _ => ""
            };


            return string.Format(mensaje, parametros);
        }
    }
}
