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
        var linkhash = Environment.GetEnvironmentVariable("LINK_INICIO") + datos.hash;
        msg.SetTemplateId("d-73fe62492fd94da392e9a7fce793adc2");
        msg.SetTemplateData(new{
            //variables del html de la plantilla recordar que deben venir en la variable datos es decir si en el html de la plantilla se llama nombre, en el modelo debe llamarse nombre
            nombre=datos.nombreDestino,
            Link_inicio=linkhash
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
    //METODO PARA ENVIAR CORREO DE INVITACIÓN A EQUIPO---------------------------------------------------------------------------------------------------------------
    //variables del html: nombre, nombrelider, nombredelequipo, Link_aceptarInvitacion, idEquipo, hash
    [Route("correo-invitacionEquipo")]
    [HttpPost(Name = "correo")]
    public async Task<IActionResult> EnviarCorreoInvitacionEquipo(ModeloCorreo datos)
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
        var linkhash = Environment.GetEnvironmentVariable("LINK_ACEPTAR_INVITACION") + datos.idEquipo + "/" + datos.hash;

        msg.SetTemplateId("d-4f3e4656336c4000b6b4d2bcb6cd4ac6");
        msg.SetTemplateData(new{
            //variables del html de la plantilla recordar que deben venir en la variable datos es decir si en el html de la plantilla se llama nombre, en el modelo debe llamarse nombre
            nombre=datos.nombreDestino,
            nombrelider=datos.nombreLiderEquipo,
            nombredelequipo=datos.nombreEquipo,
            Link_aceptarInvitacion=linkhash
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
    //METODO PARA ENVIAR CORREO DE tikete QR---------------------------------------------------------------------------------------------------------------
    //variables del html: nombre, id_evento, id_datospersonales, hash_validacion, 
    [Route("correo-tiketIngresoEvento_qr")]
    [HttpPost(Name = "correo")]
    public async Task<IActionResult> EnviarCorreoTiketIngresoEvento_QR(ModeloCorreo datos)
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
        //AJUSTAR LINK DE QR A ESTE FORMATO http://127.0.0.1:3001/generateQRCodePDF_GET?id_evento=0&id_datos_personales=0&hash_validacion=ASDASD
        //preparar el nombre del usuario para mandarlo por url, es decir cuando encuetre un espacio lo cambia por %20
        Console.WriteLine("id_evento: "+datos.id_evento);   
        Console.WriteLine("id_datos_personales: "+datos.id_datos_personales);
        Console.WriteLine("hash_validacion: "+datos.hash_validacion);
        Console.WriteLine("nombreDestino: "+datos.nombreDestino);
        Console.WriteLine("nom_evento: "+datos.nom_evento);
        Console.WriteLine("fecha_evento: "+datos.fecha_evento);
        Console.WriteLine("hora_inicio: "+datos.hora_inicio);
        Console.WriteLine("hora_fin: "+datos.hora_fin);




        var nombredestino_con_espacios = datos.nombreDestino!.Replace(" ", "%20");
        Console.WriteLine("nombre destino con espacios: "+nombredestino_con_espacios);
        var nombre_evento_con_espacios = datos.nom_evento!.Replace(" ", "%20");
        Console.WriteLine("nombre evento con espacios: "+nombre_evento_con_espacios);


        var link_codigoQR_PDF = Environment.GetEnvironmentVariable("LINK_MS_LOGICA") + "generateQRCodePDF_GET?id_evento=" +  datos.id_evento + "&id_datos_personales=" + datos.id_datos_personales + "&hash_validacion=" + datos.hash_validacion +"&nombreDestino="+nombredestino_con_espacios+ "&nom_evento=" + nombre_evento_con_espacios + "&fecha_evento=" + datos.fecha_evento + "&hora_inicio=" + datos.hora_inicio + "&hora_fin=" + datos.hora_fin;

        msg.SetTemplateId("d-2e37b2dfbcfd4c2d9d2ae8312b80fbbf");
        msg.SetTemplateData(new{
            //variables del html de la plantilla recordar que deben venir en la variable datos es decir si en el html de la plantilla se llama nombre, en el modelo debe llamarse nombre
            nombre=datos.nombreDestino,
            link_codigoQR_PDF=link_codigoQR_PDF,            
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

    //METODO PARA ENVIAR CORREO DE tikete QR---------------------------------------------------------------------------------------------------------------
    //variables del html: nombre, id_evento, id_datospersonales, hash_validacion, 
    [Route("correo-tiketIngresoEvento_barras")]
    [HttpPost(Name = "correo")]
    public async Task<IActionResult> EnviarCorreoTiketIngresoEvento_Barras(ModeloCorreo datos)
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
        //AJUSTAR LINK DE QR A ESTE FORMATO http://127.0.0.1:3001/generateBarcodedownloadPDF_GET?id_evento=0&id_datos_personales=0&hash_validacion=ASDASD
        
        //preparar el nombre del usuario para mandarlo por url, es decir cuando encuetre un espacio lo cambia por %20
        var nombredestino_con_espacios = datos.nombreDestino!.Replace(" ", "%20");
        var nombre_evento_con_espacios = datos.nom_evento!.Replace(" ", "%20");


        var link_codigoQR_PDF = Environment.GetEnvironmentVariable("LINK_MS_LOGICA") + "generateBarcodedownloadPDF_GET?id_evento=" +  datos.id_evento + "&id_datos_personales=" + datos.id_datos_personales + "&hash_validacion=" + datos.hash_validacion +"&nombreDestino="+nombredestino_con_espacios+ "&nom_evento=" + nombre_evento_con_espacios + "&fecha_evento=" + datos.fecha_evento + "&hora_inicio=" + datos.hora_inicio + "&hora_fin=" + datos.hora_fin;

        msg.SetTemplateId("d-2e37b2dfbcfd4c2d9d2ae8312b80fbbf");
        msg.SetTemplateData(new{
            //variables del html de la plantilla recordar que deben venir en la variable datos es decir si en el html de la plantilla se llama nombre, en el modelo debe llamarse nombre
            nombre=datos.nombreDestino,
            link_codigoQR_PDF=link_codigoQR_PDF,            
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
