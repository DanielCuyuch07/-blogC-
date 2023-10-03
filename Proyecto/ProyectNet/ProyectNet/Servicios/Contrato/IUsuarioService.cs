using Microsoft.EntityFrameworkCore;
using ProyectNet.Models;

namespace ProyectNet.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<Usuario> GetUsuarios(string correo, string clave);
        Task<Usuario> SaveUsuario(Usuario modelo);


    }
}
