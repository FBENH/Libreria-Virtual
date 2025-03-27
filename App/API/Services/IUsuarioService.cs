using API.Models.DTO;

namespace API.Services
{
    public interface IUsuarioService
    {
        Task RegistrarUsuario(UsuarioRegistroDTO usuario);
    }
}