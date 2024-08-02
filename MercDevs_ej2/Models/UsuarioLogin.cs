using System;
using System.Collections.Generic;
using BCrypt.Net;

namespace MercDevs_ej2.Models
{
    public class UsuarioLogin
    {
        public string Correo { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }
    }
}
