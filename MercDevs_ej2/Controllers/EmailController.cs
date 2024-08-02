using MercDevs_ej2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MercDevs_ej2.Controllers
{
    public class EmailController : Controller
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string to, string subject, string body)
        {
            await _emailService.SendEmailAsync(to, subject, body);
            return Ok("Email sent successfully");
        }
    }
}
