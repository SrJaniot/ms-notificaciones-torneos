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
    [Route("correo-bienvenida")]
    [HttpPost(Name = "correo")]
    public async Task<IActionResult> EnviarCorreoBienvenida(ModeloCorreo datos)
    {
        var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(Environment.GetEnvironmentVariable("EMAIL_FROM"), Environment.GetEnvironmentVariable("NAME_FROM"));
        var subject = datos.asuntoCorreo;
        var to = new EmailAddress(datos.correoDestino,datos.nombreDestino);
        var plainTextContent = "plain text content";
        var htmlContent = datos.contenidoCorreo;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //ajustes para plantilla de correo
        msg.SetTemplateId("d-73fe62492fd94da392e9a7fce793adc2");
        msg.SetTemplateData(new{
            //variables del html de la plantilla recordar que deben venir en la variable datos es decir si en el html de la plantilla se llama nombre, en el modelo debe llamarse nombre
            nombre=datos.nombreDestino,
            Link_inicio=Environment.GetEnvironmentVariable("LINK_INICIO")
        });

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
