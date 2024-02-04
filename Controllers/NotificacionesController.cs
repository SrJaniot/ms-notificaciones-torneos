using Microsoft.AspNetCore.Mvc;
using ms_notificaciones_torneos.Models;

// using SendGrid's C# Library
// https://github.com/sendgrid/sendgrid-csharp

//IMPORTACIONES DE SENDGRID PARA EL ENVIO DE CORREOS
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace ms_notificaciones_torneos.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificacionesController : ControllerBase
{
    [Route("correo")]
    [HttpPost(Name = "correo")]
    public async Task<IActionResult> EnviarCorreo(ModeloCorreo datos)
    {
        var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("jv85@soy.sena.edu.co","Esteban Janiot");
        var subject = datos.asuntoCorreo;
        var to = new EmailAddress(datos.correoDestino,datos.nombreDestino);
        var plainTextContent = "plain text content";
        var htmlContent = datos.contenidoCorreo;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
        if(response.StatusCode == System.Net.HttpStatusCode.Accepted)
        {
            return Ok("Correo enviado a "+datos.correoDestino + " exitosamente");
        }
        else
        {
            return BadRequest("Error al enviar el correo a "+ datos.correoDestino );
        }

    }

}
