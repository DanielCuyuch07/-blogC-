using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProyectNet.Models;
using ProyectNet.Recursos;
using ProyectNet.Servicios.Contrato;
using System.Security.Claims;
using NuGet.Packaging.Signing;

namespace ProyectNet.Controllers
{
    public class InicioController : Controller
    {

        private readonly IUsuarioService _usuarioServicio;

        public InicioController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        /**** Metodos Get ****/

        public IActionResult Registrarse()
        {
            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }


        /********* Metodo Post ******/
        /* 1.Es un metodo asyncrono publico llamado registrarse 
           2.Toma un parametro de tipo Usuario llamado modelo  y devuelve un archivo IActionResult
        */
        [HttpPost]
        public async Task<IActionResult> Registrarse(Usuario modelo)
        {
            // Encripta la clave del usuario utilizando un método llamado Utilidades.EncriptarClave
            modelo.Clave = Utilidades.EncriptarClave(modelo.Clave);

            // Llama a un servicio (_usuarioServicio) para guardar un nuevo usuario con la información proporcionada en el modelo
            Usuario usuario_creado = await _usuarioServicio.SaveUsuario(modelo);

            // Si se ha creado correctamente el usuario (su ID es mayor que 0),
            // redirige a la acción "IniciarSesion" del controlador "Inicio".
            if (usuario_creado.IdUsuario > 0)
                return RedirectToAction("IniciarSesion", "Inicio");

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();
        }

        /********* IniciarSesion ******/
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {
            // Verificacion de la clave si es null o vacio 
            if (string.IsNullOrEmpty(clave))
            {
                throw new ArgumentNullException(nameof(clave), "La clave no puede ser nula o vacía.");
            }

            /* Utiliza un servicio llamado _usuarioServicio para obtener un usuario según el correo y la clave encriptada proporcionados. 
             * La clave se encripta utilizando un método llamado Utilidades.EncriptarClave antes de buscar al usuario*/
            Usuario usuario_encontrado = await _usuarioServicio.GetUsuarios(correo, Utilidades.EncriptarClave(clave));

            if (usuario_encontrado == null || string.IsNullOrEmpty(usuario_encontrado.NombreUsuario))
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, usuario_encontrado.NombreUsuario)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");
        }
    }
}
