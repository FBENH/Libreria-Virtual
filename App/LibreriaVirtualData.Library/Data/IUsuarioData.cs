using LibreriaVirtualData.Library.Models;

namespace LibreriaVirtualData.Library.Data
{
    public interface IUsuarioData
    {
        Task RegistrarUsuario(Usuario usuario);
    }
}