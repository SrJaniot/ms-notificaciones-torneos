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

    //METODO PARA ENVIAR CORREO DE BIENVENIDA----------------------------------------------------------------------------------------------------------------
    //variables del html: nombre, Link_inicio
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
    //METODO PARA ENVIAR CORREO DE CÓDIGO 2FA----------------------------------------------------------------------------------------------------------------
    //variables del html: nombre, codigo2fa
    [Route("correo-codigo2fa")]
    [HttpPost(Name = "correo")]
    public async Task<IActionResult> EnviarCorreoCodigo2fa(ModeloCorreo datos)
    {
        // Console.WriteLine("entre a correo 2fa");
        var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(Environment.GetEnvironmentVariable("EMAIL_FROM"), Environment.GetEnvironmentVariable("NAME_FROM"));
        var subject = datos.asuntoCorreo;
        var to = new EmailAddress(datos.correoDestino,datos.nombreDestino);
        var plainTextContent = "plain text content";
        var htmlContent = datos.contenidoCorreo;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //ajustes para plantilla de correo
        msg.SetTemplateId("d-4d07baf464fc473792a09f7d49fa2c54");
        msg.SetTemplateData(new{
            //variables del html de la plantilla recordar que deben venir en la variable datos es decir si en el html de la plantilla se llama nombre, en el modelo debe llamarse nombre
            nombre=datos.nombreDestino,
            codigo2fa=datos.codigo2fa,
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
    //METODO PARA ENVIAR CORREO DE RECUPERAR CONTRASEÑA---------------------------------------------------------------------------------------------------------------
    //variables del html: nombre, Link_recuperarContrasena
    [Route("correo-recuperarContrasena")]
    [HttpPost(Name = "correo")]
    public async Task<IActionResult> EnviarCorreoRecuperarPassword(ModeloCorreo datos)
    {
        // Console.WriteLine("entre a correo 2fa");
        var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(Environment.GetEnvironmentVariable("EMAIL_FROM"), Environment.GetEnvironmentVariable("NAME_FROM"));
        var subject = datos.asuntoCorreo;
        var to = new EmailAddress(datos.correoDestino,datos.nombreDestino);
        var plainTextContent = "plain text content";
        var htmlContent = datos.contenidoCorreo;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //ajustes para plantilla de correo
        msg.SetTemplateId("d-2d33fc903e6345f59b2d42c4edcf6fc8");
        msg.SetTemplateData(new{
            //variables del html de la plantilla recordar que deben venir en la variable datos es decir si en el html de la plantilla se llama nombre, en el modelo debe llamarse nombre
            nombre=datos.nombreDestino,
            Link_recuperarContrasena=Environment.GetEnvironmentVariable("LINK_RECUPERAR_CONTRASENA")
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
