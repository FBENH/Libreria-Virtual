using API.Models.DTO;

namespace API.Services
{
    public interface IUsuarioService
    {
        Task RegistrarUsuario(UsuarioRegistroDTO usuario);
        Task ActualizarFoto(Guid idUsuario, ActualizarUrlUsuarioDTO url);
        Task EliminarUsuario(Guid idUsuario);
    }
}