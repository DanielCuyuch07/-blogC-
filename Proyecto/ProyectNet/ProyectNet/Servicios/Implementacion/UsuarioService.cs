using Microsoft.EntityFrameworkCore;
using ProyectNet.Models;
using ProyectNet.Servicios.Contrato;

namespace ProyectNet.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly DbpruebaContext _dbContext;

        public UsuarioService(DbpruebaContext dbContext)
        {
            _dbContext = dbContext;
        }

        /*Solution*/
        public async Task<Usuario> GetUsuarios(string correo, string clave)
        {
            return await _dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Clave == clave);
        }

        public async Task<Usuario> SaveUsuario(Usuario modelo)
        {
            _dbContext.Usuarios.Add(modelo);
            await _dbContext.SaveChangesAsync();
            return modelo;
        }

        /********** Codigo descartado **********/

        //public async Task<Usuario> GetUsuario(string correo, string clave)
        //{
        //    return await _dbContext.Usuarios
        //        .FirstOrDefaultAsync(u => u.Correo == correo && u.Clave == clave);
        //}

        //public Task<Usuario> GetUsuarios(string correo, string clave)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
