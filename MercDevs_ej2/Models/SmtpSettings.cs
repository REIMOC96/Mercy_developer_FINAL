using Microsoft.AspNetCore.Mvc;

namespace MercDevs_ej2.Models
{
    public class SmtpSettings
    {
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? Username { get; set; } =null!;
        public string? Password { get; set; } =null!;
    }
}
