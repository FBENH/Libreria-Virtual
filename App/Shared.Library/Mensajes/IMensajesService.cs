namespace Shared.Library.Mensajes.Mensajes
{
    public interface IMensajesService
    {
        string GetMensaje(Mensajes clave, params object[] parametros);
    }
}