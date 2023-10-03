using System.Security.Cryptography;
using System.Text;

namespace ProyectNet.Recursos
{
    public class Utilidades
    {
        public static string EncriptarClave(string clave)
        {
            // Verificar si la clave es nula o vacía
            if (string.IsNullOrEmpty(clave))
            {
                throw new ArgumentNullException(nameof(clave), "La clave no puede ser nula o vacía.");
            }

            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(clave));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

    }
}
